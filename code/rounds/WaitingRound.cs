using Sandbox;

namespace PropHunt
{
    public class WaitingRound : BaseRound
    {
        public override string RoundName => "Waiting";

        public override void OnStart()
        {
            Log.Info(RoundName + " Round has started.");
        }
    }
}