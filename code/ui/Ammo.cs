using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace PropHunt
{
    public partial class Ammo : Panel
    {
        private Panel AmmoBackground;
        private Label CounterText;
        private Label ReserveText;

        public Ammo()
        {
            StyleSheet.Load("/ui/Ammo.scss");
            AmmoBackground = Add.Panel();
            CounterText = AmmoBackground.Add.Label("0", "Counter");
            ReserveText = AmmoBackground.Add.Label("0", "Reserve");
        }

        public override void Tick()
        {
            base.Tick();
            AmmoBackground.AddClass("Hidden");

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

            AmmoBackground.RemoveClass("Hidden");

            CounterText.Text = weapon.AmmoClip.ToString();
            ReserveText.Text = weapon.AmmoReserve.ToString();
        }
    }
}
