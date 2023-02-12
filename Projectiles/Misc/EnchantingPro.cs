using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Projectiles.Misc
{
    internal class EnchantingPro : ModProjectile, IMiscProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 34;
            Projectile.height = 20;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.alpha = 0;
            Projectile.timeLeft = 600;
            Projectile.penetrate = 2;
            Main.projFrames[Projectile.type] = 10;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 3;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        private Vector2 basicPos = Vector2.Zero;
        private Vector2 basicVec = Vector2.Zero;
        private float timer = 0;
        public override void AI()
        {
            timer += 0.1f;
            Player player = Main.player[Projectile.owner];
            if (Projectile.timeLeft == 600)
            {
                basicPos = player.Center;
                basicVec = Main.MouseWorld - player.Center;
                basicVec.Normalize();
            }
            basicPos += basicVec * 8;
            Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.BlueTorch, 0f, 0f, 0, default(Color), 1f);
            dust.noGravity = true;
            dust.velocity *= 0.5f;
            Projectile.Center = basicPos + new Vector2((float)Math.Cos(timer), (float)Math.Sin(timer)) * Projectile.ai[0] * 30;

            if (Projectile.timeLeft % 3 == 0)
            {
                Projectile.frame++;
            }
            if (Projectile.frame >= 10)
                Projectile.frame = 0;

            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X);
        }
        public override void Kill(int timeLeft)
        {
            ParticleOrchestraSettings settings;
            
            for (int i = 1; i < 10; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.BlueTorch, 0f, 0f, 0, default(Color), 0.75f);
                dust.noGravity = true;
                dust.velocity *= 2f;
                Vector2 posin = Projectile.Center;
                settings = new ParticleOrchestraSettings
                {
                    PositionInWorld = posin,//位置
                    MovementVector = 5 * Main.rand.NextVector2Unit()
                };
                ParticleOrchestrator.RequestParticleSpawn(clientOnly: true, ParticleOrchestraType.StardustPunch, settings, 255);
            }
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255 - Projectile.alpha, 255 - Projectile.alpha, 255 - Projectile.alpha, 255 - Projectile.alpha);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            float size1 = 1f;

            var texture = ModContent.Request<Texture2D>(Texture, ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;

            Vector2 drawOrigin = new Vector2(Projectile.width * 0.5f, Projectile.height * 0.5f);
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color = lightColor * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length) ;
                Main.spriteBatch.Draw(texture, drawPos, new Rectangle(0, 20 * Projectile.frame, 34, 20), color, Projectile.rotation, drawOrigin, size1, SpriteEffects.None, 0f);
            }
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            return true;

        }
    }
}
