using System;
using EnemyScripts;
using UnityEngine;

namespace PlayerScripts
{
    public class PlayerMoveController : MonoBehaviour
    {
    
        private Animator _animator;
        private static readonly int CrouchingAnimation = Animator.StringToHash("crouching");
        private static readonly int WalkingAnimation = Animator.StringToHash("walking");

        private const float DefaultSpeed = 4f;
        private bool _faceRight = true;
        private int _facingDirection;
        public bool isDead;
        private Rigidbody2D _rb;

        private bool _isInversed;
        private int _inversionFactor = 1;

        private const float JumpForce = 600f;
        private bool _canJump;
        private bool _canDoubleJump;

        private bool _isGrounded;
        private bool _hasSpaceAbove;
        public Transform groundCheck;
        public Transform headCheck;
        private const float CheckRadius = 0.1f;
        public LayerMask whatIsGround;
    
        public Transform attackPoint;
        public float attackRange = 0.5f;
        public LayerMask enemyLayers;

        private bool _crouchingState;
        private bool _holdS;

        private bool _isTouchingFront;
        public Transform frontCheck;
        private bool _wallSliding;
        private const float WallSlidingSpeed = 1f;

        private const float XWallJumpForce = 300f;

        void Start()
        {
            Physics2D.gravity = new Vector2(0, -30f);

            _rb = gameObject.GetComponent<Rigidbody2D>();
            whatIsGround = LayerMask.GetMask("Ground Platforms");
            _animator = gameObject.GetComponent<Animator>();

            _animator.SetBool(WalkingAnimation, false);
            // animator.SetBool("jump", false);
            _animator.SetBool(CrouchingAnimation, false);
        }

        void Update()
        {
            if (isDead == false)
            {
                _facingDirection = _faceRight ? 1 : -1;

                #region Move Features

                float move = Input.GetAxis("Horizontal") * _inversionFactor;
                int moveCeiling = (int) Math.Ceiling(move) * _inversionFactor;
                int moveFloor = (int) Math.Floor(Math.Abs(move)) * _inversionFactor;
                if (_animator.GetBool(CrouchingAnimation) == false)
                {
                    _rb.velocity =
                        new Vector2(move * DefaultSpeed, _rb.velocity.y);
                }

                #endregion

                #region Attack Features

                if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.LeftControl))
                {
                    _animator.Play("Attack");
                    Invoke("Attack",0.2f);
                }

                #endregion

                #region Jump Features

                _isGrounded = Physics2D.OverlapCircle(groundCheck.position, CheckRadius, whatIsGround);

                if (Input.GetButtonDown("Jump"))
                {
                    if (_isGrounded)
                    {
                        if (_crouchingState == false)
                        {
                            _canJump = true;
                        }
                        else
                        {
                            _canJump = false;
                        }
                    }

                    if (_canJump)
                    {
                        _rb.AddForce(new Vector2(0, JumpForce));
                        _canJump = false;
                        _canDoubleJump = true;
                    }
                    else if (_wallSliding)
                    {
                        _rb.velocity = new Vector2(0, 0);
                        _rb.AddForce(new Vector2(-moveFloor * XWallJumpForce, JumpForce));
                        InverseControls();
                    }
                    else if (_canDoubleJump)
                    {
                        _canDoubleJump = false;
                        _rb.velocity = new Vector2(0, 0);
                        _rb.AddForce(new Vector2(0, JumpForce));
                    }
                }

                #endregion

                #region Crouch Features

                _hasSpaceAbove = !Physics2D.OverlapCircle(headCheck.position, CheckRadius, whatIsGround);

                if (Input.GetKeyDown(KeyCode.S))
                {
                    _holdS = true;
                    _crouchingState = true;
                    _animator.SetBool(CrouchingAnimation, true);
                    _rb.velocity =
                        new Vector2(moveCeiling * 2f * DefaultSpeed, _rb.velocity.y);
                }
                else if (Input.GetKeyUp(KeyCode.S) || _holdS == false || _hasSpaceAbove == false && _crouchingState)
                {
                    if (_hasSpaceAbove)
                    {
                        _animator.SetBool(CrouchingAnimation, false);
                        _crouchingState = false;
                    }
                    else
                    {
                        _rb.velocity =
                            new Vector2(_facingDirection * 2f * DefaultSpeed, _rb.velocity.y);
                    }

                    _holdS = false;
                }

                #endregion

                #region Movement Animations & Look

                if (move != 0)
                {
                    _animator.SetBool(WalkingAnimation, true);
                }
                else
                {
                    _animator.SetBool(WalkingAnimation, false);
                }

                if (move > 0)
                {
                    if (_faceRight == false)
                    {
                        Flip();
                    }
                }
                else if (move < 0)
                {
                    if (_faceRight)
                    {
                        Flip();
                    }
                }

                #endregion

                #region Wallsliding Features

                _isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, CheckRadius, whatIsGround);
                if (_isTouchingFront && _isGrounded == false && move != 0)
                {
                    _wallSliding = true;
                }
                else
                {
                    _wallSliding = false;
                }

                if (_wallSliding)
                {
                    var velocity = _rb.velocity;
                    _rb.velocity = new Vector2(0f, Mathf.Clamp(velocity.y, -WallSlidingSpeed, float.MaxValue));
                }

                if (moveFloor == 0 && _isInversed)
                {
                    InverseControls();
                }

                #endregion
            }
        }

        void Flip()
        {
            _faceRight = !_faceRight;
            var transform1 = transform;
            var localScale = transform1.localScale;
            localScale.x *= -1;
            transform1.localScale = localScale;
        }

        void InverseControls()
        {
            _inversionFactor = -_inversionFactor;
            _isInversed = !_isInversed;
        }
        void Attack()
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

            foreach (var enemy in hitEnemies)
            {
                enemy.GetComponent<EnemyHealthController>().HealthSystem.Damage();
            }
        }
        void OnDrawGizmosSelected()
        {
            if (attackPoint == null)
                return;
            Gizmos.DrawWireSphere(attackPoint.position,attackRange);
        }
    }
}