using Sandbox;
using Sandbox.UI;

namespace PropHunt
{
    public partial class Crosshair : Panel
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

            RemoveClass("Hidden");

            if(PropHuntGame.GetTeam(player.TeamIndex) == PropHuntGame.SeekerTeam && PropHuntGame.Round == PropHuntGame.HidingRound)
                AddClass("Hidden");
            else if(player.TeamIndex == 0)
                AddClass("Hidden");
            else if(player.LifeState != LifeState.Alive)
                AddClass("Hidden");
        }
    }
}
