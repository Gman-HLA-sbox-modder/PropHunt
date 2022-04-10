using Sandbox;

namespace PropHunt
{
    [Library("weapon_smg", Title = "SMG", Spawnable = true)]
    partial class SMG : Weapon
    {
        public override string ViewModelPath => "weapons/rust_smg/v_rust_smg.vmdl";

        public override float PrimaryRate => 15.0f;
        public override float SecondaryRate => 1.0f;
        public override float ReloadTime => 5.0f;
        public override int ClipSize => 45;
        public override int MaxReserve => 225;

        public override void Spawn()
        {
            base.Spawn();

            SetModel("weapons/rust_smg/rust_smg.vmdl");
        }

        public override void AttackPrimary()
        {
            if(!UseAmmo(1))
            {
                if(AmmoReserve > 0)
                {
                    Reload();
                    return;
                }

                PlaySound("rust_smg.dryfire");
                return;
            }

            TimeSincePrimaryAttack = 0;
            TimeSinceSecondaryAttack = 0;

            (Owner as AnimEntity)?.SetAnimParameter("b_attack", true);

            //
            // Tell the clients to play the shoot effects
            //
            ShootEffects();
            PlaySound("rust_smg.shoot");

            //
            // Shoot the bullets
            //
            ShootBullet(0.1f, 1.5f, 5.0f, 3.0f);
        }

        public override void AttackSecondary()
        {
            //try to make a new grenade at some point
        }

        [ClientRpc]
        protected override void ShootEffects()
        {
            Host.AssertClient();

            Particles.Create("particles/pistol_muzzleflash.vpcf", EffectEntity, "muzzle");
            Particles.Create("particles/pistol_ejectbrass.vpcf", EffectEntity, "ejection_point");

            if(Owner == Local.Pawn)
            {
                new Sandbox.ScreenShake.Perlin(0.5f, 4.0f, 1.0f, 0.5f);
            }

            ViewModelEntity?.SetAnimParameter("fire", true);
            CrosshairPanel?.CreateEvent("fire");
        }

        public override void SimulateAnimator(PawnAnimator anim)
        {
            anim.SetAnimParameter("holdtype", 2); // TODO this is shit
            anim.SetAnimParameter("aimat_weight", 1.0f);
        }
    }
}
