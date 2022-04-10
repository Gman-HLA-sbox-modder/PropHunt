using Sandbox;
using System.Collections.Generic;

namespace PropHunt
{
    public class WaitingRound : BaseRound
    {
        public override string RoundName => "Waiting for players";

        public override void OnFinish()
        {
            Map.Reset(Game.DefaultCleanupFilter);
        }
    }
}