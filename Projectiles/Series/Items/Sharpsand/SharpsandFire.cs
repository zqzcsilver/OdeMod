using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OdeMod.Players;

using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace OdeMod.Projectiles.Series.Items.Sharpsand
{
    internal class SharpsandFire : ModProjectile, ISharpsandProjectile
    {
        private float m;
        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 32;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.timeLeft = 1200;
            Projectile.alpha = 0;
            Projectile.penetrate = 1;
            Projectile.scale = 1f;
            Main.projFrames[Projectile.type] = 3;
            Projectile.extraUpdates = 2;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 8;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override void AI()
        {
            m += 0.1f;
            int num = Dust.NewDust(Projectile.Center + new Vector2(-3, -1) + 13 * (float)Math.Cos(m) * new Vector2(-(float)Math.Sin(Projectile.velocity.ToRotation()), (float)Math.Cos(Projectile.velocity.ToRotation())), 1, 1, DustID.FlameBurst, 0f, 0f, 0, Color.White, 1.2f);
            Main.dust[num].noGravity = true;
            Main.dust[num].velocity *= 0.1f;

            int num2 = Dust.NewDust(Projectile.Center + new Vector2(-3, -1) - 13 * (float)Math.Cos(m) * new Vector2(-(float)Math.Sin(Projectile.velocity.ToRotation()), (float)Math.Cos(Projectile.velocity.ToRotation())), 1, 1, DustID.FlameBurst, 0f, 0f, 0, Color.White, 1.2f);
            Main.dust[num2].noGravity = true;
            Main.dust[num2].velocity *= 0.1f;

            Projectile.rotation = (float)
                   System.Math.Atan2((double)Projectile.velocity.Y,
                   (double)Projectile.velocity.X) - 1.57f;
            if (Projectile.timeLeft % 4 == 0)
            {
                if (Projectile.frame == 0 || Projectile.frame == 1)
                    Projectile.frame++;
                else Projectile.frame = 0;

            }
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

        public override bool PreDraw(ref Color lightColor)
        {
            float size1 = 2f;
            Texture2D texture = ModContent.Request<Texture2D>("OdeMod/Projectiles/Series/Items/Sharpsand/SharpsandFire").Value;
            Vector2 drawOrigin = new Vector2(Projectile.width * 0.5f, Projectile.height * 0.5f);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                size1 *= 0.92f;
                Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color = Color.Orange * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length) * 0.8f;
                Main.spriteBatch.Draw(texture, drawPos, new Rectangle(0, 32 * Projectile.frame, 14, 32), color, Projectile.rotation, drawOrigin, size1, SpriteEffects.None, 0f);
            }
            return true;
        }
    }
}