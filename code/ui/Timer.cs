using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace PropHunt
{
    public partial class Timer : Panel
    {
        private Label TitleText;
        private Label TimerText;

        public Timer()
        {
            StyleSheet.Load("/ui/Timer.scss");
            Panel teamBackground = Add.Panel();

            TitleText = teamBackground.Add.Label("", "Title");
            TimerText = teamBackground.Add.Label("", "Text");
        }

        public override void Tick()
        {
            base.Tick();
            TitleText.Text = PropHuntGame.Round?.RoundName;
            TimerText.Text = "0:00";
        }
    }
}
