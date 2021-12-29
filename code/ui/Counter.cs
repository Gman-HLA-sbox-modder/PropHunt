using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace PropHunt
{
    public partial class Counter : Panel
    {
        private Label counterSeekers;
        private Label counterProps;

        public Counter()
        {
            StyleSheet.Load("/ui/Counter.scss");
			Panel left = Add.Panel("Left Column");
			Panel right = Add.Panel("Right Column");

            left.Add.Label(PropHuntGame.SeekerTeam.HudName.ToUpper(), "CounterText");
            left.Add.Label(PropHuntGame.PropTeam.HudName.ToUpper(), "CounterText");

            counterSeekers = right.Add.Label("0", "CounterText");
            counterProps = right.Add.Label("0", "CounterText");
        }

        public override void Tick()
        {
            counterSeekers.Text = PropHuntGame.GetPlayersByTeam(PropHuntGame.SeekerTeam.Index, true).Count.ToString();
            counterProps.Text = PropHuntGame.GetPlayersByTeam(PropHuntGame.PropTeam.Index, true).Count.ToString();
        }
    }
}
