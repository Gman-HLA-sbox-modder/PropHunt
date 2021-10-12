using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using System;

namespace PropHunt
{
    public partial class EndScreen : Panel
    {
        private Label Winner;

        public EndScreen()
        {
            StyleSheet.Load("/ui/EndScreen.scss");
            Panel background = Add.Panel("Background");
            Winner = background.Add.Label("", "Title");

        }

        public override void Tick()
        {
            base.Tick();
            RemoveClass("Show");
            if(PropHuntGame.Round != PropHuntGame.FinishedRound)
                return;

            AddClass("Show");
            Winner.Text = PropHuntGame.GetTeam(PropHuntGame.Winner)?.HudName + " wins!";
        }
    }
}
