using Sandbox;

namespace PropHunt
{
    public class HidingRound : BaseRound
    {
        public override int RoundDuration => 30;
        public override string RoundName => "Hiding";

        public override void OnStart()
        {
            Log.Info(RoundName + " Round has started.");
        }
    }
}