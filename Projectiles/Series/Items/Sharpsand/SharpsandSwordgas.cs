using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace OdeMod.Projectiles.Series.Items.Sharpsand
{
    internal class SharpsandSwordgas : ModProjectile, ISharpsandProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 26;
            Projectile.height = 26;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.timeLeft = 300;
            Projectile.alpha = 0;
            Projectile.penetrate = 1;
            Projectile.scale = 1f;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 1;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
            Projectile.extraUpdates = 1;
        }
        public override void AI()
        {
            int num = Dust.NewDust(Projectile.Center - new Vector2(4f, 4f), 8, 8, DustID.FlameBurst, 0f, 0f, 0, Color.White, 1.2f + (float)Math.Sin((300 - Projectile.timeLeft) * 0.06f) * 0.5f);
            Main.dust[num].velocity *= 0.5f;
            Main.dust[num].noGravity = true;


            Projectile.rotation = (float)
               System.Math.Atan2((double)Projectile.velocity.Y,
               (double)Projectile.velocity.X) + 0.785f;
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
        Texture2D texture;
        float sizex = 1.5f;
        public override bool PreDraw(ref Color lightColor)
        {
            sizex = 1.5f + (float)Math.Sin((300 - Projectile.timeLeft) * 0.05f) * 0.32f;
            texture = ModContent.Request<Texture2D>(Texture).Value;
            Vector2 drawOrigin = new Vector2(Projectile.width * 0.5f, Projectile.height * 0.5f);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Color color;
                Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                if (sizex > 1.56f)
                    color = Projectile.GetAlpha(lightColor) * 0.5f * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                else
                    color = Color.Red * 0.5f * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                Main.spriteBatch.Draw(texture, drawPos, null, color, Projectile.rotation, drawOrigin, sizex, SpriteEffects.None, 0f);
            }

            GraphicsDevice gd = Main.instance.GraphicsDevice;
            SpriteBatch sb = Main.spriteBatch;


            return true;

        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255 - Projectile.alpha, 255 - Projectile.alpha, 255 - Projectile.alpha, 255 - Projectile.alpha);
        }
    }
}