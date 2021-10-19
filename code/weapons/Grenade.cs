using Sandbox;
using System;

namespace PropHunt
{
    public class Grenade : Prop
    {
        public override void Spawn()
        {
            base.Spawn();

            SetModel("models/citizen_props/sodacan01.vmdl");
            SetupPhysicsFromModel(PhysicsMotionType.Dynamic);
        }

        public override void Touch(Entity other)
        {
            base.Touch(other);

            Explode();
            Delete();
        }

        private void Explode()
        {
            ModelExplosionBehavior explosion = new ModelExplosionBehavior()
            {
                Sound = "sounds/common/explosions/explo_gas_can_01.vsnd",
                Damage = 120f,
                Radius = 200f,
                Force = 100f
            };

            Vector3 pos = PhysicsBody.MassCenter;
            Sound.FromWorld(explosion.Sound, pos);
            Particles.Create("particles/explosion/barrel_explosion/explosion_flash.vpcf", pos);
            Particles.Create("particles/explosion/barrel_explosion/explosion_gib.vpcf", pos);
            Particles.Create("particles/explosion/barrel_explosion/explosion_gib2.vpcf", pos);
            Particles.Create("particles/explosion/barrel_explosion/explosion_gib_large.vpcf", pos);

            var entities = Physics.GetEntitiesInSphere(pos, explosion.Radius);

            foreach(Entity entity in entities)
            {
                if(entity is not ModelEntity ent || !ent.IsValid())
                    continue;

                if(ent.LifeState != LifeState.Alive)
                    continue;

                if(!ent.PhysicsBody.IsValid())
                    continue;

                if(ent.IsWorld)
                    continue;

                Vector3 target = ent.PhysicsBody.MassCenter;
                float distance = Vector3.DistanceBetween(pos, target);
                if(distance > explosion.Radius)
                    continue;

                TraceResult tr = Trace.Ray(pos, target).Ignore(this).WorldOnly().Run();
                if(tr.Fraction < 1)
                    continue;

                float multiplier = 1 - Math.Clamp(distance / explosion.Radius, 0, 1);
                float damage = explosion.Damage * multiplier;
                float force = explosion.Force * multiplier * (float)Math.Pow(ent.PhysicsBody.Mass, 0.5f);
                Vector3 direction = (target - pos).Normal;

                ent.TakeDamage(DamageInfo.Explosion(pos, direction * force, damage).WithAttacker(Owner));
            }
        }
    }
}
