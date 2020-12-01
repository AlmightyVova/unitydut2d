namespace ArchTree
{
    public abstract class Enemy : Interactable
    {
        public abstract float HP { get; set; }
        public abstract float Speed { get; set; }
        public abstract float Damage { get; set; }

        public abstract void Attack();

        public abstract void Jump();
    }

    public class CommonEnemy : Enemy
    {
        public override float HP { get; set; } = 100;
        public override float Speed { get; set; }
        public override float Damage { get; set; }
        public override void Attack()
        {
            // Casts attack animation, does damage
        }

        public override void Jump()
        {
            // Casts jump animation, changes enemy's Y coord
        }
    }

    public abstract class Boss : Enemy
    {
        public override float HP { get; set; } = 500;
        public override float Speed { get; set; }
        public override float Damage { get; set; }

        public float MP { get; set; }
        public float MagicalDamage { get; set; }

        public void SpecialAttack()
        {
            // Casts special attack animation, makes double the physical damage
        }
    }

    public class SuperBoss : Boss
    {
        public override float HP { get; set; }
        public override void Attack()
        {
            // Casts attack animation, does damage
        }

        public override void Jump()
        {
            // Casts jump animation, changes enemy's Y coord
        }

        public void SpecialAbility()
        {
            // Casts special ability animation, makes pure damage
        }
    }
}