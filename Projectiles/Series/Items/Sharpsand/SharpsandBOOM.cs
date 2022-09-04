using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace OdeMod.Projectiles.Series.Items.Sharpsand
{
    internal class SharpsandBOOM : ModProjectile, ISharpsandProjectile
    {
        public override void SetDefaults()
        {
            Main.projFrames[Projectile.type] = 10;
            Projectile.width = 120;
            Projectile.height = 120;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 29;
            Projectile.alpha = 0;
            Projectile.penetrate = -1;
            Projectile.scale = 1f;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 1;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        public override void AI()
        {
            if (Projectile.timeLeft == 16)
            {
                for (int i = 0; i <= 30; i++)
                {
                    int num = Dust.NewDust(Projectile.Center - new Vector2(20f, 20f), 40, 40, DustID.Torch, 0f, 0f, 0, Color.White, 1.2f);
                    Main.dust[num].velocity *= 4f;
                    Main.dust[num].noGravity = true;
                }
            }
            if (Projectile.timeLeft % 3 == 0)
                Projectile.frame++;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, damage);
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255 - Projectile.alpha, 255 - Projectile.alpha, 255 - Projectile.alpha, 255 - Projectile.alpha);
        }
    }
}