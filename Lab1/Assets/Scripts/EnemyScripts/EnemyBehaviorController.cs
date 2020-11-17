using System;
using UnityEngine;

namespace EnemyScripts
{
    public class EnemyBehaviorController : MonoBehaviour
    {
        private const float DefaultSpeed = 2f;
        public float velocity = 1f;
        private Rigidbody2D _rb;
        private float _inversionFactor;
        public bool isFacingRight = true;
        
        private EnemyHealthController _ehc;
        
        private bool _isDead;

        private bool _isTouchingFront;
        public Transform frontCheck;
        private const float CheckRadius = 0.1f;
        private LayerMask _whatIsGround;
        private LayerMask _whatIsEnemy;

        public EnemyBehaviorController(EnemyHealthController ehc)
        {
            _ehc = gameObject.GetComponent<EnemyHealthController>();
        }

        private void Start()
        {
            _inversionFactor = 1f;
            
            if(!isFacingRight)
                Flip();
            
            _ehc = gameObject.GetComponent<EnemyHealthController>();
            _ehc.OnEnemyDamaged += () => _isDead = true;
            _rb = gameObject.GetComponent<Rigidbody2D>();

            _whatIsGround = LayerMask.GetMask("Ground Platforms");
            _whatIsEnemy = LayerMask.GetMask("Enemies");
        }
        
        
        private void Update()
        {
            if (_isDead)
            {
                Destroy(gameObject);
            }

            _rb.velocity = new Vector2(DefaultSpeed*_inversionFactor,_rb.velocity.y);

            var frontCheckPosition = frontCheck.position;
            
            _isTouchingFront = 
                Physics2D.OverlapCircle(frontCheckPosition, CheckRadius, _whatIsGround) ||
                Physics2D.OverlapCircle(frontCheckPosition, CheckRadius, _whatIsEnemy);
            if (_isTouchingFront)
            {
                Flip();
            }
        }
        void Flip()
        {
            isFacingRight = !isFacingRight;
            _inversionFactor = -_inversionFactor;
            var transform1 = transform;
            var localScale = transform1.localScale;
            localScale.x *= -1;
            transform1.localScale = localScale;
        }
    }
}