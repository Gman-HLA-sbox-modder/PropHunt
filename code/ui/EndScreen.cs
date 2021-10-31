using Sandbox.UI;
using Sandbox.UI.Construct;

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
            Classes = "";
            if(PropHuntGame.Round != PropHuntGame.FinishedRound)
                return;

            BaseTeam team = PropHuntGame.GetTeam(PropHuntGame.Winner);

            AddClass("Show");
            AddClass(team?.HudName);
            Winner.Text = team?.HudName + " win!";
        }
    }
}
