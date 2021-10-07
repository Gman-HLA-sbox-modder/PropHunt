using Sandbox;
using Sandbox.UI;

namespace PropHunt
{
    class Blind : Panel
    {
        public Blind()
        {
            StyleSheet.Load("/ui/Blind.scss");
        }

        public override void Tick()
        {
            base.Tick();

            PropHuntPlayer player = Local.Pawn as PropHuntPlayer;
            if(player == null)
                return;

            SetClass("Active", PropHuntGame.GetTeam(player.TeamIndex) == PropHuntGame.SeekerTeam && PropHuntGame.Round == PropHuntGame.HidingRound);
        }
    }
}