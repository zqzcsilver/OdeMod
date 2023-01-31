using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace OdeMod.Projectiles.Series.Boss.MiracleRecorder
{
    internal class Star2 : ModProjectile, IMiracleRecorderProj
    {
        private float m;
        public override void SetDefaults()
        {
            Projectile.width = 26;
            Projectile.height = 28;
            Projectile.aiStyle = -1;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.ignoreWater = true;
            Projectile.tileCollide =false;
            Projectile.timeLeft = 1200;
            Projectile.alpha = 0;
            Projectile.penetrate = 1;
            Projectile.scale = 1f;
            Projectile.extraUpdates = 5;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 10;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        Vector2 oldvec = Vector2.Zero;
        Vector2 destination= Vector2.Zero;
        public override void AI()
        {
            m += 0.1f;
            Player player = Main.player[Projectile.owner];

            destination = player.Center - Projectile.Center;
            destination.Normalize();
            Projectile.velocity += destination*0.04f;
            Projectile.velocity.Normalize();
            Projectile.velocity *= 5f;
            Projectile.rotation += 0.02f;
            int num = Dust.NewDust(Projectile.Center + new Vector2(-3, -1) + 8 * (float)Math.Cos(m) * new Vector2(-(float)Math.Sin(Projectile.velocity.ToRotation()), (float)Math.Cos(Projectile.velocity.ToRotation())), 1, 1, DustID.GoldFlame, 0f, 0f, 0, Color.White, 1.2f);
            Main.dust[num].noGravity = true;
            Main.dust[num].velocity *= 0.1f;

            int num2 = Dust.NewDust(Projectile.Center + new Vector2(-3, -1) - 8 * (float)Math.Cos(m) * new Vector2(-(float)Math.Sin(Projectile.velocity.ToRotation()), (float)Math.Cos(Projectile.velocity.ToRotation())), 1, 1, DustID.GoldFlame, 0f, 0f, 0, Color.White, 1.7f);
            Main.dust[num2].noGravity = true;
            Main.dust[num2].velocity *= 0.1f;


        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, damage);
        }
        public override void Kill(int timeLeft)
        {
            Player player = Main.player[Projectile.owner];

            
            for (int i = 0; i < 40; i++)
            {
                int num = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.PinkTorch, 0f, 0f, 0, Color.White, 1.5f);
                Main.dust[num].velocity = 5 * Main.rand.NextVector2Unit();
                Main.dust[num].noGravity = true;
            }
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255 - Projectile.alpha, 255 - Projectile.alpha, 255 - Projectile.alpha, 255 - Projectile.alpha);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            float size1 = 1.2f;
            
            Texture2D texture = ModContent.Request<Texture2D>("OdeMod/Projectiles/Series/Boss/MiracleRecorder/Star").Value;
            
            Vector2 drawOrigin = new Vector2(Projectile.width * 0.5f, Projectile.height * 0.5f);
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color = lightColor * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length) * 0.8f;
                Main.spriteBatch.Draw(texture, drawPos, null, color, Projectile.rotation, drawOrigin, size1, SpriteEffects.None, 0f);
            }
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            return false;

        }
        public override void PostDraw(Color lightColor)
        {
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            float size2 = 1f;
            Texture2D texture2 = ModContent.Request<Texture2D>("OdeMod/Projectiles/Series/Boss/MiracleRecorder/projTex").Value;
            Vector2 drawOrigin = new Vector2(texture2.Width * 0.5f, texture2.Height * 0.5f);
            
                Vector2 drawPos = Projectile.position - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY)+new Vector2(14,16) ;
            Color color = Color.LightPink * 0.6f;
                Main.spriteBatch.Draw(texture2, drawPos, null, color, 0, drawOrigin, size2, SpriteEffects.None, 0f);
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
        }
    }
}