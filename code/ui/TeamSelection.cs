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
            Panel seeker = Add.Panel("Selection");
            Panel prop = Add.Panel("Selection");

            seeker.Add.Label(new SeekerTeam().HudName, "TeamText");
            prop.Add.Label(new PropTeam().HudName, "TeamText");

            //Clicking stops working on Hotload
            seeker.AddEventListener("onclick", () =>
            {
                AddClass("Hidden");
                PropHuntGame.JoinTeam("seeker");
            });

            prop.AddEventListener("onclick", () =>
            {
                AddClass("Hidden");
                PropHuntGame.JoinTeam("props");
            });
        }
    }
}
