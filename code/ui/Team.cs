using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace PropHunt
{
    public partial class Team : Panel
    {
        private Label TeamText;

        public Team()
        {
            StyleSheet.Load("/ui/Team.scss");
            Panel teamBackground = Add.Panel();
            TeamText = teamBackground.Add.Label("", "Text");
        }

        public override void Tick()
        {
            base.Tick();
            PropHuntPlayer player = Local.Pawn as PropHuntPlayer;
            if(player == null)
                return;

            BaseTeam team = PropHuntGame.GetTeam(player.TeamIndex);
            if(team == null)
                TeamText.AddClass("Hidden");
            else
                TeamText.RemoveClass("Hidden");

            TeamText.Text = team?.HudName.ToUpper();
        }
    }
}
