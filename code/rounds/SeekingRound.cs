using Sandbox;

namespace PropHunt
{
    public class SeekingRound : BaseRound
    {
        public override int RoundDuration => 180;
        public override string RoundName => "Seeking";

        public override void OnStart()
        {
            Log.Info(RoundName + " Round has started.");
        }
    }
}