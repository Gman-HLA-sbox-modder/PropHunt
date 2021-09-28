using Sandbox;
using System;

namespace PropHunt
{
    public class PropCamera : ThirdPersonCamera
    {
        private float orbitDistance = 150;

        public override void Update()
        {
            AnimEntity pawn = Local.Pawn as AnimEntity;

            if(pawn == null)
                return;

            Pos = pawn.Position;
            Vector3 targetPos;

            Pos += Vector3.Up * Math.Max(pawn.CollisionBounds.Maxs.z * pawn.Scale * 0.75f, 8f);
            Rot = Rotation.FromAxis(Vector3.Up, 4) * Input.Rotation;

            targetPos = Pos + Rot.Backward * orbitDistance;

            Pos += Vector3.Up * 0f;
            TraceResult tr = Trace.Ray(Pos, targetPos).Ignore(pawn).Radius(8).Run();
            Pos = tr.EndPos;

            FieldOfView = 70;
            Viewer = null;
        }
    }
}
