using Sandbox;
using System;

namespace PropHunt
{
    [Library("weapon_grenade", Title = "Grenade", Spawnable = false)]
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
            var srcPos = Position;
            if(PhysicsBody.IsValid()) srcPos = PhysicsBody.MassCenter;

            var explosionBehavior = Model.GetExplosionBehavior();

            new ExplosionEntity 
            {
                Position = srcPos,
                Radius = explosionBehavior.Radius,
                Damage = explosionBehavior.Damage,
                ForceScale = explosionBehavior.Force,
                ParticleOverride = explosionBehavior.Effect,
                SoundOverride = explosionBehavior.Sound
            }.Explode(this);

            Vector3 pos = PhysicsBody.MassCenter;
            Particles.Create("particles/explosion/barrel_explosion/explosion_flash.vpcf", pos);
            Particles.Create("particles/explosion/barrel_explosion/explosion_gib.vpcf", pos);
            Particles.Create("particles/explosion/barrel_explosion/explosion_gib2.vpcf", pos);
            Particles.Create("particles/explosion/barrel_explosion/explosion_gib_large.vpcf", pos);

            
        }
    }
}
