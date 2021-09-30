using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace PropHunt
{
    public partial class Timer : Panel
    {
        private Label TimerText;

        public Timer()
        {
            StyleSheet.Load("/ui/Timer.scss");
            Panel teamBackground = Add.Panel();

            teamBackground.Add.Label("Timer", "Title");
            TimerText = teamBackground.Add.Label("", "Text");
        }

        public override void Tick()
        {
            base.Tick();
            TimerText.Text = "0:00";
        }
    }
}
