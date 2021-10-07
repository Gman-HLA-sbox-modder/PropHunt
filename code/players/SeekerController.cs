using Sandbox;

namespace PropHunt
{
    public class SeekerController : WalkController
    {
        public override void Simulate()
        {
            if(PropHuntGame.Round != PropHuntGame.HidingRound)
            {
                base.Simulate();
            }
        }
    }
}
