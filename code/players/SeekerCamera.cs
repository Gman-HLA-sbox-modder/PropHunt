using Sandbox;

namespace PropHunt
{
    public class SeekerCamera : FirstPersonCamera
    {
        public override void BuildInput(InputBuilder input)
        {
            if(PropHuntGame.Round != PropHuntGame.HidingRound)
            {
                base.BuildInput(input);
            }
        }
    }
}
