using Sandbox;

namespace PropHunt
{
    public class PropAnimator : PawnAnimator
    {
        public override void Simulate()
        {
            if((Pawn as PropHuntPlayer).LockRotatation)
                return;

            Rotation idealRotation = Pawn.EyeRot;
            idealRotation.x = 0f;
            idealRotation.y = 0f;
            Rotation = Rotation.Slerp(Rotation, idealRotation, Time.Delta * 25f);
        }
    }
}
