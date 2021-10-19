using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace PropHunt
{
    public partial class Ammo : Panel
    {
        private Panel AmmoPanel;
        private Label CounterText;
        private Label ReserveText;

        private Panel AltPanel;
        private Label AltText;

        public Ammo()
        {
            StyleSheet.Load("/ui/Ammo.scss");

            AmmoPanel = Add.Panel("Ammo");
            AmmoPanel.Add.Label("AMMO", "AmmoText");
            CounterText = AmmoPanel.Add.Label("0", "Counter");
            ReserveText = AmmoPanel.Add.Label("0", "Reserve");

            AltPanel = Add.Panel("Alt");
            AltPanel.Add.Label("ALT", "AmmoText");
            AltText = AltPanel.Add.Label("0", "Counter");
        }

        public override void Tick()
        {
            base.Tick();
            AddClass("Hidden");

            Entity player = Local.Pawn;
            if(player == null)
                return;

            if(player.Inventory == null)
                return;

            if(player.Inventory.Count() == 0)
                return;

            Weapon weapon = player.Inventory.Active as Weapon;
            if(weapon == null)
                return;

            if(weapon.ClipSize == 0)
                return;

            RemoveClass("Hidden");

            CounterText.Text = weapon.AmmoClip.ToString();
            ReserveText.Text = weapon.AmmoReserve.ToString();

            if(weapon.MaxAlt > 0)
            {
                AltPanel.RemoveClass("Hidden");
                AltText.Text = weapon.AmmoAlt.ToString();

                if(AmmoPanel.Style.Right != 202)
                {
                    AmmoPanel.Style.Right = 202;
                    AmmoPanel.Style.Dirty();
                }
            }
            else
            {
                AltPanel.AddClass("Hidden");

                if(AmmoPanel.Style.Right != 40)
                {
                    AmmoPanel.Style.Right = 40;
                    AmmoPanel.Style.Dirty();
                }
            }
        }
    }
}
