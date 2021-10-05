using Sandbox;

namespace PropHunt
{
    public class FinishedRound : BaseRound
    {
        public override int RoundDuration => 15;
        public override string RoundName => "Finished";

        public override void OnStart()
        {
            Log.Info(RoundName + " Round has started.");
        }

        public override void OnTimerEnd()
        {
            PropHuntGame.ChangeRound(new WaitingRound());
        }
    }
}