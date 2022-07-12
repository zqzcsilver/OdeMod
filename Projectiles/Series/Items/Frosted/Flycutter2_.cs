using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace OdeMod.Projectiles.Series.Items.Frosted
{
    internal class Flycutter2_ : ModProjectile, IFrostedProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 20;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.timeLeft = 300;
            Projectile.alpha = 0;
            Projectile.penetrate = -1;
            Projectile.scale = 1f;
            ProjectileID.Sets.TrailCacheLength[base.Projectile.type] = 8;
            ProjectileID.Sets.TrailingMode[base.Projectile.type] = 0;
            Projectile.extraUpdates = 2;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(44, damage);
        }
        public override void AI()
        {
            int num = Dust.NewDust(Projectile.Center - new Vector2(1f, 1f), 2, 2, DustID.IceTorch, 0f, 0f, 0, Color.White, 1.2f);
            Main.dust[num].velocity *= 0.1f;
            Main.dust[num].noGravity = true;
            if (Projectile.timeLeft > 30)
            {
                Projectile.rotation = (float)
                   System.Math.Atan2((double)Projectile.velocity.Y,
                   (double)Projectile.velocity.X) + 1.57f;
            }
            else
            {
                Projectile.alpha += 9;
                Projectile.rotation += 0.4f;
            }

        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 30; i++)
            {
                int num = Dust.NewDust(Projectile.Center - new Vector2(3f, 3f), 6, 6, DustID.IceTorch, 0f, 0f, 0, Color.White, 1.2f);
                int num2 = Dust.NewDust(Projectile.Center - new Vector2(3f, 3f), 6, 6, DustID.IceTorch, 0f, 0f, 0, Color.White, 1.2f);
                Main.dust[num].velocity = Projectile.velocity * 0.4f;
                Main.dust[num].noGravity = true;
                Main.dust[num2].velocity = Projectile.velocity * -0.4f;
                Main.dust[num2].noGravity = true;
            }
        }
        Texture2D texture, texture2;
        public override bool PreDraw(ref Color lightColor)
        {
            texture = ModContent.Request<Texture2D>("OdeMod/Projectiles/Series/Items/Frosted/Flycutter3").Value;
            Vector2 drawOrigin = new Vector2(Projectile.width * 0.5f, Projectile.height * 0.5f);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                Main.spriteBatch.Draw(texture, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
            }
            return true;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255 - Projectile.alpha, 255 - Projectile.alpha, 255 - Projectile.alpha, 255 - Projectile.alpha);
        }
    }
}