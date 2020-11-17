using UnityEngine;

namespace PlayerScripts
{
    public class PlayerHealthController : MonoBehaviour
    {
        // public static PlayerHealthController Instance;
        private readonly HealthSystem HealthSystem = new HealthSystem();
        private Animator _animator;
        private PlayerMoveController _pmc;
        public HealthBar healthBar;
        public PotionBar potionBar;

        void Start()
        {
            /*if (Instance == null)
            {
                Instance = this;
            }*/

            _animator = gameObject.GetComponent<Animator>();
            _pmc = gameObject.GetComponent<PlayerMoveController>();

            healthBar.SetMaxHealth(1);
            potionBar.SetMaxShield(1);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("SpikyEnemy"))
            {
                HealthSystem.Damage();
                healthBar.slider.value = HealthSystem.Health;
                potionBar.slider.value = HealthSystem.Shield;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Potion"))
            {
                Destroy(other.gameObject);
                HealthSystem.HealShield();
                potionBar.slider.value = HealthSystem.Shield;
            }
            else if (other.gameObject.CompareTag("Obstacle"))
            {
                HealthSystem.Damage();
                healthBar.slider.value = HealthSystem.Health;
                potionBar.slider.value = HealthSystem.Shield;
            } else if (other.gameObject.CompareTag("Deadly Obstacle"))
            {
                HealthSystem.Kill();
            }
        }

        void Update()
        {
            if (HealthSystem.Health == 0)
            {
                _animator.Play("Dying");
                _pmc.isDead = true;
            }
        }
    }
}