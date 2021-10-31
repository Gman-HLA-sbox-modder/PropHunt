using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace PropHunt
{
    class TeamSelection : Panel
    {
        Panel SeekerBlocked;
        Panel PropBlocked;

        Label SeekerPlayers;
        Label PropPlayers;

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

            seeker.Add.Label(PropHuntGame.SeekerTeam.HudName, "TeamText");
            prop.Add.Label(PropHuntGame.PropTeam.HudName, "TeamText");

            SeekerBlocked = seeker.Add.Panel("ImageWrapper").Add.Panel("Image").Add.Panel("Blocked");
            PropBlocked = prop.Add.Panel("ImageWrapper").Add.Panel("Image").Add.Panel("Blocked");

            SeekerPlayers = seeker.Add.Label("0 Players", "PlayersText");
            PropPlayers = prop.Add.Label("0 Players", "PlayersText");

            seeker.AddClass(PropHuntGame.SeekerTeam.HudName);
            prop.AddClass(PropHuntGame.PropTeam.HudName);

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

            int seekerCount = PropHuntGame.GetPlayersByTeam(PropHuntGame.SeekerTeam.Index).Count;
            int propCount = PropHuntGame.GetPlayersByTeam(PropHuntGame.PropTeam.Index).Count;
            SeekerPlayers.Text = seekerCount.ToString() + " Player" + (seekerCount == 1 ? "" : "s");
            PropPlayers.Text = propCount.ToString() + " Player" + (propCount == 1 ? "" : "s");

            SeekerBlocked.SetClass("Blocked", seekerCount > propCount);
            PropBlocked.SetClass("Blocked", seekerCount < propCount);
        }

        public override void OnHotloaded()
        {
            base.OnHotloaded();

            CreateButtons();
        }
    }
}
