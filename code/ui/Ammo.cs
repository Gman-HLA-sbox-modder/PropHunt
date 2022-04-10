using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using System;
using System.Collections.Generic;

namespace PropHunt
{
    public partial class Ammo : Panel
    {
        private Panel AmmoPanel;
        private Label CounterText;
        private Label ReserveText;

        private Panel AltPanel;
        private Label AltText;

        private Weapon lastWeapon;
        private float startGlowBackground;

        private int desiredOffset = 40;
        private int offset = 40;

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

            PropHuntPlayer player = Local.Pawn as PropHuntPlayer;
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

            if(lastWeapon != weapon)
            {
                lastWeapon = weapon;
                startGlowBackground = Time.Now;
            }

            RemoveClass("Hidden");

            CounterText.Text = weapon.AmmoClip.ToString();
            ReserveText.Text = weapon.AmmoReserve.ToString();

            float color = Math.Max(0, (1 - (Time.Now - startGlowBackground)) * 255);
            AmmoPanel.Style.Right = offset;

            if(weapon.MaxAlt > 0)
            {
                AltPanel.RemoveClass("Hidden");
                AltText.Text = weapon.AmmoAlt.ToString();
                desiredOffset = 202;
            }
            else
            {
                AltPanel.AddClass("Hidden");
                desiredOffset = 40;
            }

            if(desiredOffset > offset)
                offset += 3;
            else if(desiredOffset < offset)
                offset -= 3;

            if(Math.Abs(desiredOffset - offset) < 3)
                offset = desiredOffset;
        }
    }
}
