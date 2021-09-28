using Sandbox;

namespace PropHunt
{
    [Library("weapon_shotgun", Title = "Shotgun", Spawnable = true)]
    partial class Shotgun : Weapon
    {
        public override string ViewModelPath => "weapons/rust_pumpshotgun/v_rust_pumpshotgun.vmdl";
        public override float PrimaryRate => 1;
        public override float SecondaryRate => 1;
        public override float ReloadTime => 0.5f;
        public override int ClipSize => 6;
        public override int MaxReserve => 30;

        public override void Spawn()
        {
            base.Spawn();

            SetModel("weapons/rust_pumpshotgun/rust_pumpshotgun.vmdl");
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

            (Owner as AnimEntity)?.SetAnimBool("b_attack", true);

            //
            // Tell the clients to play the shoot effects
            //
            ShootEffects();
            PlaySound("rust_pumpshotgun.shoot");

            //
            // Shoot the bullets
            //
            ShootBullets(10, 0.1f, 10.0f, 9.0f, 3.0f);
        }

        public override void AttackSecondary()
        {
            if(!UseAmmo(2))
            {
                if(AmmoReserve > 0 && AmmoClip == 0)
                    Reload();
                return;
            }

            TimeSincePrimaryAttack = -0.5f;
            TimeSinceSecondaryAttack = -0.5f;

            (Owner as AnimEntity)?.SetAnimBool("b_attack", true);

            //
            // Tell the clients to play the shoot effects
            //
            DoubleShootEffects();
            PlaySound("rust_pumpshotgun.shootdouble");

            //
            // Shoot the bullets
            //
            ShootBullets(20, 0.4f, 20.0f, 8.0f, 3.0f);
        }

        [ClientRpc]
        protected override void ShootEffects()
        {
            Host.AssertClient();

            Particles.Create("particles/pistol_muzzleflash.vpcf", EffectEntity, "muzzle");
            Particles.Create("particles/pistol_ejectbrass.vpcf", EffectEntity, "ejection_point");

            ViewModelEntity?.SetAnimBool("fire", true);

            if(IsLocalPawn)
            {
                new Sandbox.ScreenShake.Perlin(1.0f, 1.5f, 2.0f);
            }

            CrosshairPanel?.CreateEvent("fire");
        }

        [ClientRpc]
        protected virtual void DoubleShootEffects()
        {
            Host.AssertClient();

            Particles.Create("particles/pistol_muzzleflash.vpcf", EffectEntity, "muzzle");

            ViewModelEntity?.SetAnimBool("fire_double", true);
            CrosshairPanel?.CreateEvent("fire");

            if(IsLocalPawn)
            {
                new Sandbox.ScreenShake.Perlin(3.0f, 3.0f, 3.0f);
            }
        }

        public override void OnReloadFinish()
        {
            IsReloading = false;

            TimeSincePrimaryAttack = 0;
            TimeSinceSecondaryAttack = 0;

            if(AmmoClip >= ClipSize)
                return;

            if(AmmoReserve == 0)
                return;

            AmmoClip++;
            AmmoReserve--;

            if(AmmoClip < ClipSize && AmmoReserve > 0)
                Reload();
            else
                FinishReload();
        }

        [ClientRpc]
        protected virtual void FinishReload()
        {
            ViewModelEntity?.SetAnimBool("reload_finished", true);
        }

        public override void SimulateAnimator(PawnAnimator anim)
        {
            anim.SetParam("holdtype", 3); // TODO this is shit
            anim.SetParam("aimat_weight", 1.0f);
        }
    }
}