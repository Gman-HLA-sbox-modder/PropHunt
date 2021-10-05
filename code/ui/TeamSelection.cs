using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace PropHunt
{
    class TeamSelection : Panel
    {
        public TeamSelection()
        {
            StyleSheet.Load("/ui/TeamSelection.scss");
            CreateButtons();
        }

        private void CreateButtons()
        {
            DeleteChildren(true);

            Panel seeker = Add.Panel("Selection");
            Panel prop = Add.Panel("Selection");

            seeker.Add.Label(new SeekerTeam().HudName, "TeamText");
            prop.Add.Label(new PropTeam().HudName, "TeamText");

            //Clicking stops working on Hotload
            seeker.AddEventListener("onclick", () =>
            {
                PropHuntGame.JoinTeam("seeker");
            });

            prop.AddEventListener("onclick", () =>
            {
                PropHuntGame.JoinTeam("props");
            });
        }

        public override void Tick()
        {
            base.Tick();

            PropHuntPlayer player = Local.Pawn as PropHuntPlayer;
            if(player == null)
                return;

            Parent.SetClass("ShowTeamSelection", player.ShowTeamSelection);
        }

        public override void OnHotloaded()
        {
            base.OnHotloaded();

            CreateButtons();
        }
    }
}
