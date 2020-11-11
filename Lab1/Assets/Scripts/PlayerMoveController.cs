using System;
using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{
    private Animator animator;
    private static readonly int CrouchingAnimation = Animator.StringToHash("crouching");
    private static readonly int WalkingAnimation = Animator.StringToHash("walking");
    private static readonly int JumpAnimation = Animator.StringToHash("jump");

    private const float DefaultSpeed = 4f;
    private bool faceRight = true;
    private int facingDirection;
    public bool isDead;
    private Rigidbody2D rb;

    private bool isInversed;
    private int inversionFactor = 1;

    private const float JumpForce = 600f;
    private bool canJump;
    private bool canDoubleJump;

    private bool isGrounded;
    private bool hasSpaceAbove;
    public Transform groundCheck;
    public Transform headCheck;
    private const float CheckRadius = 0.1f;
    public LayerMask whatIsGround;

    private bool crouchingState;
    private bool holdS;

    private bool isTouchingFront;
    public Transform frontCheck;
    private bool wallSliding;
    private const float WallSlidingSpeed = 1f;

    private const float XWallJumpForce = 300f;

    void Start()
    {
        Physics2D.gravity = new Vector2(0, -30f);

        rb = gameObject.GetComponent<Rigidbody2D>();
        whatIsGround = LayerMask.GetMask("Ground Platforms");
        animator = gameObject.GetComponent<Animator>();

        animator.SetBool(WalkingAnimation, false);
        animator.SetBool(JumpAnimation, false);
        animator.SetBool(CrouchingAnimation, false);
    }

    void Update()
    {
        if (isDead == false)
        {
            facingDirection = faceRight ? 1 : -1;

            #region Move Features

            float move = Input.GetAxis("Horizontal") * inversionFactor;
            int moveCeiling = (int) Math.Ceiling(move) * inversionFactor;
            int moveFloor = (int) Math.Floor(Math.Abs(move)) * inversionFactor;
            Debug.Log($"{move} {moveFloor}");
            if (animator.GetBool(CrouchingAnimation) == false)
            {
                rb.velocity =
                    new Vector2(move * DefaultSpeed, rb.velocity.y);
            }

            #endregion

            #region Attack Features

            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.LeftControl))
            {
                animator.Play("Attack");
            }

            #endregion

            #region Jump Features

            isGrounded = Physics2D.OverlapCircle(groundCheck.position, CheckRadius, whatIsGround);

            if (Input.GetButtonDown("Jump"))
            {
                if (isGrounded)
                {
                    if (crouchingState == false)
                    {
                        canJump = true;
                    }
                    else
                    {
                        canJump = false;
                    }
                }

                if (canJump)
                {
                    rb.AddForce(new Vector2(0, JumpForce));
                    canJump = false;
                    canDoubleJump = true;
                }
                else if (wallSliding)
                {
                    rb.velocity = new Vector2(0, 0);
                    rb.AddForce(new Vector2(-moveFloor * XWallJumpForce, JumpForce));
                    InverseControls();
                }
                else if (canDoubleJump)
                {
                    canDoubleJump = false;
                    rb.velocity = new Vector2(0, 0);
                    rb.AddForce(new Vector2(0, JumpForce));
                }
            }

            #endregion

            #region Crouch Features

            hasSpaceAbove = !Physics2D.OverlapCircle(headCheck.position, CheckRadius, whatIsGround);

            if (Input.GetKeyDown(KeyCode.S))
            {
                holdS = true;
                crouchingState = true;
                animator.SetBool(CrouchingAnimation, true);
                rb.velocity =
                    new Vector2(moveCeiling * 2f * DefaultSpeed, rb.velocity.y);
            }
            else if (Input.GetKeyUp(KeyCode.S) || holdS == false || hasSpaceAbove == false && crouchingState)
            {
                if (hasSpaceAbove)
                {
                    animator.SetBool(CrouchingAnimation, false);
                    crouchingState = false;
                }
                else
                {
                    rb.velocity =
                        new Vector2(facingDirection * 2f * DefaultSpeed, rb.velocity.y);
                }

                holdS = false;
            }

            #endregion

            #region Movement Animations & Look

            if (move != 0)
            {
                animator.SetBool(WalkingAnimation, true);
            }
            else
            {
                animator.SetBool(WalkingAnimation, false);
            }

            if (move > 0)
            {
                if (faceRight == false)
                {
                    Flip();
                }
            }
            else if (move < 0)
            {
                if (faceRight)
                {
                    Flip();
                }
            }

            #endregion

            #region Wallsliding Features

            isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, CheckRadius, whatIsGround);
            if (isTouchingFront && isGrounded == false && move != 0)
            {
                wallSliding = true;
            }
            else
            {
                wallSliding = false;
            }

            if (wallSliding)
            {
                var velocity = rb.velocity;
                rb.velocity = new Vector2(0f, Mathf.Clamp(velocity.y, -WallSlidingSpeed, float.MaxValue));
            }

            if (moveFloor == 0 && isInversed)
            {
                InverseControls();
            }

            #endregion
        }
    }

    void Flip()
    {
        faceRight = !faceRight;
        var transform1 = transform;
        var localScale = transform1.localScale;
        localScale.x *= -1;
        transform1.localScale = localScale;
    }

    void InverseControls()
    {
        inversionFactor = -inversionFactor;
        isInversed = !isInversed;
    }
}