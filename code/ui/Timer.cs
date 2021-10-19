using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using System;

namespace PropHunt
{
    public partial class Timer : Panel
    {
        private Label RoundText;
        private Label TimerText;
        private Panel TimerPanel;

        public Timer()
        {
            StyleSheet.Load("/ui/Timer.scss");
            RoundText = Add.Panel("Round").Add.Label("");

            TimerPanel = Add.Panel("Timer");

            TimerPanel.Add.Panel();
            TimerText = TimerPanel.Add.Panel().Add.Label("");
        }

        public override void Tick()
        {
            base.Tick();
            TimerPanel.RemoveClass("Show");
            if(PropHuntGame.Round == null)
                return;

            RoundText.Text = PropHuntGame.Round.RoundName.ToUpper();

            if(PropHuntGame.Round == PropHuntGame.HidingRound || PropHuntGame.Round == PropHuntGame.SeekingRound)
            {
                TimerPanel.AddClass("Show");

                float timeLeft = PropHuntGame.TimerEnd - Time.Now;
                int minutes = Math.Max((timeLeft / 60).FloorToInt(), 0);
                int seconds = Math.Max((timeLeft % 60).FloorToInt(), 0);
                TimerText.Text = minutes.ToString() + ":" + (seconds < 10 ? "0" : "") + seconds.ToString();
            }
        }
    }
}
