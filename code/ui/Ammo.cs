using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace PropHunt
{
    public partial class Ammo : Panel
    {
        private Label CounterText;
        private Label ReserveText;
        private Label AltText;
        private Panel Alt;

        public Ammo()
        {
            StyleSheet.Load("/ui/Ammo.scss");
            Panel panel = Add.Panel();
            CounterText = panel.Add.Label("0", "Counter");
            ReserveText = panel.Add.Label("0", "Reserve");
            Alt = Add.Panel("Alt");
            AltText =  Alt.Add.Label("0", "Counter");
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
            Alt.SetClass("Hidden", weapon.MaxAlt == 0);

            CounterText.Text = weapon.AmmoClip.ToString();
            ReserveText.Text = weapon.AmmoReserve.ToString();
            AltText.Text = weapon.AmmoAlt.ToString();
        }
    }
}
