namespace ArchTree
{
    public abstract class NPC : Interactable
    {
        public bool IsMale { get; set; }
        public float HP { get; set; }

        public virtual void OpenDialogueWindow()
        {
            // opens dialogue window
        }
    }

    public class Merchant : NPC
    {
        public int Money { get; set; }

        public void Buy(object item)
        {
            // Buys some item
        }

        public void Sell(object item)
        {
            // Sells some item
        }

        public override void OpenDialogueWindow()
        {
            // Opens merchant's dialogue window with ability to sell and buy
        }
    }

    public class Friendly : NPC
    {
        public override void OpenDialogueWindow()
        {
            // Opens Friendly dialogue window
        }
    }

    public class Companion : Friendly
    {
        public override void OpenDialogueWindow()
        {
            // Opens companion's dialogue window with ability to give orders
        }
    }

    public class Partner : Friendly
    {
        public int DaysMarried { get; }

        public override void OpenDialogueWindow()
        {
            // Opens partner's dialogue window with marriage options
        }
    }
}