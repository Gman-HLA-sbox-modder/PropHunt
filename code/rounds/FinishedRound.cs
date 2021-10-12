using Sandbox;

namespace PropHunt
{
    public class FinishedRound : BaseRound
    {
        public override int RoundDuration => 15;
        public override string RoundName => "Round over";

        public override void OnTimerEnd()
        {
            PropHuntGame.ChangeRound(new WaitingRound());
        }
    }
}