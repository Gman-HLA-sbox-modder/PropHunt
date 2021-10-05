using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using System;

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
            if(PropHuntGame.Round == null)
                return;

            TitleText.Text = PropHuntGame.Round.RoundName;

            float timeLeft = PropHuntGame.TimerEnd - Time.Now;
            int minutes = Math.Max((timeLeft / 60).FloorToInt(), 0);
            int seconds = Math.Max((timeLeft % 60).FloorToInt(), 0);
            TimerText.Text = minutes.ToString() + ":" + (seconds < 10? "0" : "") + seconds.ToString();
        }
    }
}
