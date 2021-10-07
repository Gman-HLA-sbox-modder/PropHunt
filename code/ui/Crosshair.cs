using Sandbox;
using Sandbox.UI;

namespace PropHunt
{
    public partial class Crosshair : RootPanel
    {
        public Crosshair()
        {
            StyleSheet.Load("/ui/Crosshair.scss");
            Add.Panel("Crosshair");
        }

        public override void Tick()
        {
            base.Tick();

            PropHuntPlayer player = Local.Pawn as PropHuntPlayer;
            if(player == null)
                return;

            SetClass("Hidden", PropHuntGame.GetTeam(player.TeamIndex) == PropHuntGame.SeekerTeam && PropHuntGame.Round == PropHuntGame.HidingRound);
        }
    }
}
