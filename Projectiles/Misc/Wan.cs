using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Projectiles.Misc
{
    internal class Wan : ModProjectile, IMiscProjectile
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.width = 32;
            Projectile.height = 38;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.aiStyle = 149;
            Projectile.alpha = 0;
            Projectile.penetrate = -1;
            Projectile.scale = 0.8f;
        }
        public override void AI()
        {
            Projectile.ai[1]++;
            if (Projectile.ai[1] >= 120)
            {
                Projectile.tileCollide = false;
                Projectile.rotation += 0.1f;
                Projectile.velocity.Y = -10f;
                Projectile.velocity.X = 0f;
            }
            if (Projectile.ai[1] >= 180)
            {
                Projectile.Kill();
            }
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            base.ModifyHitNPC(target, ref damage, ref knockback, ref crit, ref hitDirection);
            for (int i = 0; i < 30; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.GreenTorch, 0f, 0f, 100, default(Color), 2f);
                dust.noGravity = true;
                dust.velocity *= 2.5f;
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            for (int i = 0; i < 30; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.GreenTorch, 0f, 0f, 100, default(Color), 2f);
                dust.noGravity = true;
                dust.velocity *= 2.5f;

            }
            return base.OnTileCollide(oldVelocity);
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 30; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.GreenTorch, 0f, 0f, 100, default(Color), 2f);
                dust.noGravity = true;
                dust.velocity *= 1.5f;
            }
        }
    }
}
