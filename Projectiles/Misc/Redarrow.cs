using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace OdeMod.Projectiles.Misc
{
    internal class Redarrow : ModProjectile, IMiscProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 46;
            Projectile.height = 192;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 300;
            Projectile.alpha = 0;
            Projectile.penetrate = -1;
            Projectile.scale = 1f;
            ProjectileID.Sets.TrailCacheLength[base.Projectile.type] = 8;
            ProjectileID.Sets.TrailingMode[base.Projectile.type] = 0;
            Projectile.extraUpdates = 3;
        }
        public override void AI()
        {
            Projectile.rotation = (float)
                  System.Math.Atan2((double)Projectile.velocity.Y,
                  (double)Projectile.velocity.X) + 1.57f;
            int num = Dust.NewDust(Projectile.Center - new Vector2(1f, 1f), 2, 2, 235, 0f, 0f, 0, Color.White, 2f);
            Main.dust[num].velocity *= 0.1f;
            Main.dust[num].noGravity = true;

               

        }
        Texture2D texture, texture2;
        public override bool PreDraw(ref Color lightColor)
        {
            texture = ModContent.Request<Texture2D>("OdeMod/Projectiles/Misc/Redarrow").Value;
            Vector2 drawOrigin = new Vector2(Projectile.width * 0.5f, Projectile.height * 0.5f);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color = Color.Red * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                Main.spriteBatch.Draw(texture, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
            }
            return true;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            for (int i = 0; i < 60; i++)
            {
                var dust2 = Dust.NewDustDirect(target.Center, 1, 1, 235, 0, 0, 0, Color.White, 2f);
                dust2.velocity = 4 * Main.rand.NextVector2Unit();
                dust2.noGravity = true;

            }
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255 - Projectile.alpha, 255 - Projectile.alpha, 255 - Projectile.alpha, 255 - Projectile.alpha);
        }
    }
}