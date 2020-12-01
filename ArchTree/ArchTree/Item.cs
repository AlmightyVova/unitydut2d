namespace ArchTree
{
    public abstract class Item : Interactable
    {
        public abstract int Weight { get; set; }
        public abstract int Price { get; set; }
    }

    public abstract class Consumable : Item
    {
        public abstract void Consume();
    }

    public abstract class NonConsumable : Item
    {
        public abstract int Quality { get; set; }
    }

    public class HealthPotion : Consumable
    {
        public override void Consume()
        {
            throw new System.NotImplementedException();
        }

        public override int Weight { get; set; } = 5;
        public override int Price { get; set; } = 1;
    }

    public class Sword : NonConsumable
    {
        public override int Quality { get; set; } = 100;
        public override int Weight { get; set; }
        public override int Price { get; set; }
    }
}