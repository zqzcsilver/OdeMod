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
            Projectile.width = 72;
            Projectile.height = 60;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 300;
            Projectile.alpha = 255;
            Projectile.penetrate = -1;
            Projectile.scale = 1f;
            ProjectileID.Sets.TrailCacheLength[base.Projectile.type] = 6;
            ProjectileID.Sets.TrailingMode[base.Projectile.type] = 0;
            Projectile.extraUpdates = 1;
        }
        public override void AI()
        {
            m += 0.1f;
            int num = Dust.NewDust(Projectile.Center + 32 * (float)Math.Cos(m) * new Vector2(-(float)Math.Sin(Projectile.velocity.ToRotation()), (float)Math.Cos(Projectile.velocity.ToRotation())), 1, 1, DustID.PinkTorch, 0f, 0f, 0, Color.White, 2f);
            Main.dust[num].noGravity = true;
            Main.dust[num].velocity *= 2f;

            int num2 = Dust.NewDust(Projectile.Center - 32 * (float)Math.Cos(m) * new Vector2(-(float)Math.Sin(Projectile.velocity.ToRotation()), (float)Math.Cos(Projectile.velocity.ToRotation())), 1, 1, DustID.PinkTorch, 0f, 0f, 0, Color.White, 2f);
            Main.dust[num2].noGravity = true;
            Main.dust[num2].velocity *= 2f;
            if (Projectile.timeLeft >= 270)
            {
                Projectile.alpha -= 10;
            }
            Projectile.rotation = (float)
                  System.Math.Atan2((double)Projectile.velocity.Y,
                  (double)Projectile.velocity.X) + 1.57f;
            if(Projectile.timeLeft<230)
            {
                Projectile.velocity *= 0.97f;
            }
            if(Projectile.timeLeft<200)
            {
                Projectile.alpha += 10;

                if(Projectile.alpha>=255)
                {
                    Projectile.active = false;
                }
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            var texture = ModContent.Request<Texture2D>(Texture, ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
            Vector2 drawOrigin = new Vector2(Projectile.width * 0.5f, Projectile.height * 0.5f);
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length) * 0.8f;
                Main.spriteBatch.Draw(texture, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale + ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length), SpriteEffects.None, 0f);
            }
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            return false;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255 - Projectile.alpha, 255 - Projectile.alpha, 255 - Projectile.alpha, 255 - Projectile.alpha);
        }
    }
}