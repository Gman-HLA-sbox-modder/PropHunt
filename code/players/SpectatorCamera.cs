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

            bool showSpawn = false;
            if(targetPlayer == null || !targetPlayer.IsValid() || Input.Pressed(InputButton.Attack1) || Input.Pressed(InputButton.Attack2))
            {
                List<PropHuntPlayer> players = PropHuntGame.GetPlayersByTeam(PropHuntGame.SeekerTeam.Index, true);
                if(player.TeamIndex != PropHuntGame.SeekerTeam.Index)
                    players.AddRange(PropHuntGame.GetPlayersByTeam(PropHuntGame.PropTeam.Index, true));

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
                else
                {
                    Position = new Vector3();
                    Rotation = new Rotation();
                    showSpawn = true;
                }
            }

            if(!showSpawn)
            {
                AnimEntity pawn = targetPlayer;

                if(pawn == null)
                    return;

                Local.Pawn.Health = targetPlayer.Health;

                Position = pawn.Position;

                Position += Vector3.Up * Math.Max(pawn.CollisionBounds.Maxs.z * pawn.Scale * 0.75f, 8f);
                Rotation = Rotation.FromAxis(Vector3.Up, 4) * Input.Rotation;

                Vector3 targetPos = Position + Rot.Backward * orbitDistance;

                Position += Vector3.Up * 0f;
                TraceResult tr = Trace.Ray(Pos, targetPos).Ignore(pawn).Radius(8).Run();
                Position = tr.EndPos;
            }

            FieldOfView = 70;
            Viewer = null;
        }
    }
}
