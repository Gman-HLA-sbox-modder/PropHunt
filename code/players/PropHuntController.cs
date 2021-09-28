using Sandbox;
using System;

namespace PropHunt
{
    [Library]
    public class PropHuntController : WalkController
    {
        public override void UpdateBBox()
        {
            AnimEntity pawn = Pawn as AnimEntity;
            Vector3 mins = pawn.CollisionBounds.Mins;
            Vector3 maxs = pawn.CollisionBounds.Maxs;

            float smallest = Math.Max(Math.Min(maxs.x, maxs.y), 2f);

            maxs.x = smallest;
            maxs.y = smallest;

            mins.x = maxs.x * -1;
            mins.y = maxs.y * -1;

            SetBBox(mins, maxs);
        }
    }
}
