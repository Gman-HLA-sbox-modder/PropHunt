using Sandbox;
using System;
using System.Linq;

namespace PropHunt
{
	partial class PropHuntPlayer : Player
	{
		public override void Respawn()
		{
			SetModel("models/citizen/citizen.vmdl");

			//
			// Use WalkController for movement (you can make your own PlayerController for 100% control)
			//
			Controller = new WalkController();

			//
			// Use StandardPlayerAnimator  (you can make your own PlayerAnimator for 100% control)
			//
			Animator = new StandardPlayerAnimator();

			//
			// Use ThirdPersonCamera (you can make your own Camera for 100% control)
			//
			Camera = new ThirdPersonCamera();

			EnableAllCollisions = true;
			EnableDrawing = true;
			EnableHideInFirstPerson = true;
			EnableShadowInFirstPerson = true;

			base.Respawn();
		}

		/// <summary>
		/// Called every tick, clientside and serverside.
		/// </summary>
		public override void Simulate(Client cl)
		{
			base.Simulate(cl);

			//
			// If you have active children (like a weapon etc) you should call this to 
			// simulate those too.
			//
			SimulateActiveChild(cl, ActiveChild);

			//
			// If we're running serverside and Attack1 was just pressed, spawn a ragdoll
			//
			if(IsServer && Input.Pressed(InputButton.Attack1))
			{
				var ragdoll = new Prop();
				ragdoll.SetModel("models/citizen_props/chair04blackleather.vmdl");  
				ragdoll.Position = EyePos + EyeRot.Forward * 40;
				ragdoll.Rotation = Rotation.LookAt(Vector3.Random.Normal);
				ragdoll.SetupPhysicsFromModel(PhysicsMotionType.Dynamic, false);
				ragdoll.PhysicsGroup.Velocity = EyeRot.Forward * 1000;
			}

            if(Input.Pressed(InputButton.Use))
            {
                TraceResult tr = Trace.Ray(EyePos, EyePos + EyeRot.Forward * 100).UseHitboxes().Ignore(this).Run();
                if(tr.Hit && tr.Body.IsValid() && tr.Entity is Prop && tr.Body.BodyType == PhysicsBodyType.Dynamic)
                {
                    ((Prop)tr.Entity).OnUse(this);
                }
            }
        }

		public override void OnKilled()
		{
			base.OnKilled();

			EnableDrawing = false;
		}

        public void OnUseProp(Sandbox.Prop prop)
        {
            this.SetModel(prop.GetModel());
            this.Scale = prop.Scale;
            this.RenderColor = prop.RenderColor;
            //Bounding Box does not match prop
            this.CollisionBounds = prop.CollisionBounds;
        }
	}
}
