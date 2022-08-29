using Microsoft.Xna.Framework;

using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace OdeMod.Projectiles.Series.Items.Sharpsand
{
    internal class Bomb : ModProjectile, ISharpsandProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 22;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.timeLeft = 300;
            Projectile.alpha = 0;
            Projectile.penetrate = 1;
            Projectile.scale = 1f;
            Main.projFrames[Projectile.type] = 2;
        }
        public override void AI()
        {
            if (Projectile.timeLeft == 300)
                Projectile.velocity *= 0.6f;
            int num = Dust.NewDust(Projectile.Center - 5 * new Vector2((float)Math.Cos((double)Projectile.rotation), (float)Math.Sin((double)Projectile.rotation)), 2, 2, DustID.FlameBurst, 0f, 0f, 0, Color.White, 1f);
            Main.dust[num].noGravity = true;
            Main.dust[num].velocity.Y -= 3f;
            int num2 = Dust.NewDust(Projectile.Center - 5 * new Vector2((float)Math.Cos((double)Projectile.rotation), (float)Math.Sin((double)Projectile.rotation)), 2, 2, DustID.Smoke, 0f, 0f, 0, Color.White, 1f);
            Main.dust[num2].noGravity = true;
            Main.dust[num2].velocity.Y -= 3f;
            Projectile.velocity.Y += 0.3f;
            if (Projectile.timeLeft % 10 == 0)
            {
                if (Projectile.frame == 0)
                    Projectile.frame++;
                else Projectile.frame = 0;

            }

            Projectile.rotation += 0.14f;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, damage);
        }
        public override void Kill(int timeLeft)
        {
            Player player = Main.player[Projectile.owner];
            Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<Projectiles.Series.Items.Sharpsand.SharpsandBOOM>(), Projectile.damage, 0, player.whoAmI);
            for (int i = 0; i < 40; i++)
            {
                int num = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.GoldFlame, 0f, 0f, 0, Color.White, 1.5f);
                Main.dust[num].velocity *= 4f;
                Main.dust[num].noGravity = true;
            }
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255 - Projectile.alpha, 255 - Projectile.alpha, 255 - Projectile.alpha, 255 - Projectile.alpha);
        }
    }
}