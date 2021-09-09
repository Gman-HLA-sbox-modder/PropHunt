using Sandbox;

namespace PropHunt
{
    public class PropCamera : ThirdPersonCamera
    {
        private Angles orbitAngles;
        private float orbitDistance = 150;

        public override void Update()
        {
            var pawn = Local.Pawn as AnimEntity;
            var client = Local.Client;

            if(pawn == null)
                return;

            Pos = pawn.Position;
            Vector3 targetPos;

            var center = pawn.Position + Vector3.Up * 64;

            if(thirdperson_orbit)
            {
                Pos += Vector3.Up * (pawn.CollisionBounds.Center.z * pawn.Scale);
                Rot = Rotation.From(orbitAngles);

                targetPos = Pos + Rot.Backward * orbitDistance;
            }
            else
            {
                Pos += Vector3.Up * (pawn.CollisionBounds.Maxs.z * pawn.Scale) * 0.75f;
                Rot = Rotation.FromAxis(Vector3.Up, 4) * Input.Rotation;

                targetPos = Pos + Rot.Backward * orbitDistance;
            }

            if(thirdperson_collision)
            {
                var tr = Trace.Ray(Pos, targetPos)
                    .Ignore(pawn)
                    .Radius(8)
                    .Run();

                Pos = tr.EndPos;
            }
            else
            {
                Pos = targetPos;
            }

            FieldOfView = 70;

            Viewer = null;
        }
    }
}
