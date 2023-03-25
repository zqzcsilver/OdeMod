using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace OdeMod.Projectiles.Series.Items.Miracle
{
    internal class GloryLight : ModProjectile, IMiracleProjectile
    {
        private float m;
        public override void SetDefaults()
        {
            Projectile.width = 80;
            Projectile.height = 20;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 300;
            Projectile.alpha = 255;
            Projectile.penetrate = -1;
            Projectile.scale = 1f;
            ProjectileID.Sets.TrailCacheLength[base.Projectile.type] = 20;
            ProjectileID.Sets.TrailingMode[base.Projectile.type] = 0;
            Main.projFrames[Projectile.type] = 10;
            Projectile.extraUpdates = 1;
        }
        private int wid = 0;
        private int direc = 0;
        private int start = 80;
        public override void AI()
        {
            m += 0.1f;
            Player player = Main.player[Projectile.owner];
            if (Projectile.timeLeft == 300)
            {
                direc = player.direction;
            }
            if (start > 0) start -= 2;
            if (wid < 80) wid += 2;

            int num = Dust.NewDust(Projectile.Center + 32 * (float)Math.Cos(m) * new Vector2(-(float)Math.Sin(Projectile.velocity.ToRotation()), (float)Math.Cos(Projectile.velocity.ToRotation())), 1, 1, DustID.PinkTorch, 0f, 0f, 0, Color.White, 1.5f);
            Main.dust[num].noGravity = true;
            Main.dust[num].velocity *= 2f;

            int num2 = Dust.NewDust(Projectile.Center - 32 * (float)Math.Cos(m) * new Vector2(-(float)Math.Sin(Projectile.velocity.ToRotation()), (float)Math.Cos(Projectile.velocity.ToRotation())), 1, 1, DustID.MagicMirror, 0f, 0f, 0, Color.White, 1.2f);
            Main.dust[num2].noGravity = true;
            Main.dust[num2].velocity *= 2f;

            if (Projectile.timeLeft >= 270)
            {
                Projectile.alpha -= 10;
            }
            if (direc == 1)
            {
                Projectile.rotation = (float)
      System.Math.Atan2((double)Projectile.velocity.Y,
      (double)Projectile.velocity.X) + 1.57f;
            }
            else
            {
                Projectile.rotation = (float)
      System.Math.Atan2((double)Projectile.velocity.Y,
      (double)Projectile.velocity.X) - 1.57f;
            }

            if (Projectile.timeLeft < 230)
            {
                Projectile.velocity *= 0.97f;
            }
            if (Projectile.timeLeft < 230)
            {
                Projectile.alpha += 10;

                if (Projectile.alpha >= 255)
                {
                    Projectile.active = false;
                }
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            var texture = ModContent.Request<Texture2D>(Texture, ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
            Texture2D texture2 = ModContent.Request<Texture2D>("OdeMod/Projectiles/Series/Items/Miracle/GloryLight2").Value;
            Vector2 drawOrigin = new Vector2(Projectile.width * 0.5f, Projectile.height * 0.5f);
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            float scale2 = 2f;
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                scale2 *= 0.85f;
                Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                if (direc == 1)
                    Main.spriteBatch.Draw(texture, drawPos, new Rectangle(0, 20 * Projectile.frame, wid, 20), color, Projectile.rotation, drawOrigin, scale2, SpriteEffects.None, 0f);
                else
                    Main.spriteBatch.Draw(texture2, drawPos, new Rectangle(0, 20 * Projectile.frame, wid, 20), color, Projectile.rotation, drawOrigin, scale2, SpriteEffects.None, 0f);
            }
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            return false;
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            Player player = Main.player[Projectile.owner];
            float point = 0f;
            return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Projectile.Center - 40 * new Vector2((float)Math.Cos(Projectile.rotation), (float)Math.Sin(Projectile.rotation)),
                Projectile.Center + 40 * new Vector2((float)Math.Cos(Projectile.rotation), (float)Math.Sin(Projectile.rotation)), 20, ref point);
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255 - Projectile.alpha, 255 - Projectile.alpha, 255 - Projectile.alpha, 255 - Projectile.alpha);
        }
    }
}