using Sandbox;
using System;
using System.Collections.Generic;

namespace PropHunt
{
    public class SpectatorCamera : ThirdPersonCamera
    {
        private float orbitDistance = 150;
        private int targetId;
        private PropHuntPlayer targetPlayer;

        public override void Update()
        {
            if(Local.Pawn is not PropHuntPlayer player)
                return;

            if(targetPlayer == null || !targetPlayer.IsValid() || Input.Pressed(InputButton.Attack1) || Input.Pressed(InputButton.Attack2))
            {
                List<PropHuntPlayer> players = PropHuntGame.GetPlayersByTeam(player.TeamIndex, true);
                if(players.Count > 0)
                {
                    if(Input.Pressed(InputButton.Attack2))
                        targetId--;
                    else
                        targetId++;

                    if(targetId >= players.Count)
                        targetId = 0;
                    else if(targetId < 0)
                        targetId = players.Count - 1;

                    targetPlayer = players[targetId];
                }
            }

            AnimEntity pawn = targetPlayer;

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
