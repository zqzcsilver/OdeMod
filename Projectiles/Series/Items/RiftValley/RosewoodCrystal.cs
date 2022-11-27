using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Projectiles.Series.Items.RiftValley
{
    internal class RosewoodCrystal : ModProjectile, IRiftBalleyProjectile
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.width = 14;
            Projectile.height = 22;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.aiStyle = -1;
            Projectile.alpha = 0;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 120;
        }
        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;
            Lighting.AddLight(Projectile.position, 0.5f, 0.0f, 0.0f);
            for (int i = 0; i < 10; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.CrystalPulse, 0f, 0f, 100, default(Color), 0.75f);
                dust.noGravity = true;
                dust.velocity *= 1f;
            }
            base.AI();
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 30; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.CrystalPulse, 0f, 0f, 100, default(Color), 1.5f);
                dust.noGravity = true;
                dust.velocity *= 1f;
            }
            Vector2 shhhot = new Vector2(-Projectile.velocity.X, -Projectile.velocity.Y);
            for (float rad = -0.628f; rad <= 0.628f; rad += 0.314f)
            {
                Vector2 finalVec = (shhhot.ToRotation() + rad).ToRotationVector2() * 10f;
                if (rad >= -0.314f && rad <= 0.314f && rad != 0f)
                {
                    Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, finalVec * 0.5f, ModContent.ProjectileType<RosewoodCrystal_3>(), Projectile.damage * 2, 0f, Projectile.whoAmI);
                }
                else
                {
                    Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, finalVec, ModContent.ProjectileType<RosewoodCrystal_2>(), Projectile.damage, 0f, Projectile.whoAmI);

                }
            }
            base.Kill(timeLeft);
        }
    }
}
