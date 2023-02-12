using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace OdeMod.Projectiles.Series.Items.RiftValley
{
    internal class RosewoodCrystal : ModProjectile, IRiftBalleyProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 22;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.aiStyle = -1;
            Projectile.alpha = 0;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 120;
        }
        private float basicVelocityY = -6f;
        public override void AI()
        {
            if (Projectile.timeLeft == 120)
            {
                Projectile.velocity *= 0.5f;
            }
            Projectile.velocity.X *= 0.94f;
            basicVelocityY *= 0.96f;
            Projectile.velocity.Y += (basicVelocityY - Projectile.velocity.Y) * 0.05f;

            if (Projectile.ai[0] == 0)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.LifeDrain, 0f, 0f, 100, default(Color), 1f);
                dust.noGravity = true;
            }
            else
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.GemEmerald, 0f, 0f, 100, default(Color), 1f);
                dust.noGravity = true;
            }
        }
        private Vector2 finalVec1 = new Vector2((float)Math.Cos(0.3f), (float)Math.Sin(0.3f)) * 6;
        private Vector2 finalVec2 = new Vector2((float)Math.Cos(-0.3f), (float)Math.Sin(-0.3f)) * 6;
        private Vector2 finalVec3 = new Vector2((float)Math.Cos(2.84f), (float)Math.Sin(2.84f)) * 6;
        private Vector2 finalVec4 = new Vector2((float)Math.Cos(3.44f), (float)Math.Sin(3.44f)) * 6;
        public override void Kill(int timeLeft)
        {
            
            for (int i = 0; i < 30; i++)
            {
                if (Projectile.ai[0] == 0)
                {
                    Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.LifeDrain, 0f, 0f, 100, default(Color), 1.5f);
                    dust.noGravity = true;
                }
                else
                {
                    Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.GemEmerald, 0f, 0f, 100, default(Color), 1.5f);
                    dust.noGravity = true;
                }
            }

            if (Projectile.ai[0] == 0)
            {
     
                Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center, finalVec1, ModContent.ProjectileType<RosewoodCrystal_2>(), Projectile.damage, 0f, Projectile.whoAmI);
                Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center, finalVec2, ModContent.ProjectileType<RosewoodCrystal_2>(), Projectile.damage, 0f, Projectile.whoAmI);
                Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center, finalVec3, ModContent.ProjectileType<RosewoodCrystal_2>(), Projectile.damage, 0f, Projectile.whoAmI);
                Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center, finalVec4, ModContent.ProjectileType<RosewoodCrystal_2>(), Projectile.damage, 0f, Projectile.whoAmI);
            }
            else
            {
                Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center, finalVec1, ModContent.ProjectileType<RosewoodCrystal_3>(), Projectile.damage, 0f, Projectile.whoAmI);
                Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center, finalVec2, ModContent.ProjectileType<RosewoodCrystal_3>(), Projectile.damage, 0f, Projectile.whoAmI);
                Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center, finalVec3, ModContent.ProjectileType<RosewoodCrystal_3>(), Projectile.damage, 0f, Projectile.whoAmI);
                Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center, finalVec4, ModContent.ProjectileType<RosewoodCrystal_3>(), Projectile.damage, 0f, Projectile.whoAmI);
            }
        }
        public override void PostDraw(Color lightColor)
        {
            float size1 = 1f;

            var texture = ModContent.Request<Texture2D>(Texture, ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;

            Vector2 drawOrigin = new Vector2(Projectile.width * 0.5f, Projectile.height * 0.5f);
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);

            Vector2 drawPos = Projectile.position - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
            Color color2 = lightColor;
            if (Projectile.ai[0] == 0) color2 = Color.Red; else color2 = Color.Green;
            Main.spriteBatch.Draw(texture, drawPos, null, color2, Projectile.rotation, drawOrigin, size1, SpriteEffects.None, 0f);
            Main.spriteBatch.Draw(texture, drawPos, null, color2, Projectile.rotation, drawOrigin, size1 + (float)Math.Sin(Projectile.timeLeft * 0.05f) * 0.3f + 0.7f, SpriteEffects.None, 0f);
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255 - Projectile.alpha, 255 - Projectile.alpha, 255 - Projectile.alpha, 255 - Projectile.alpha);
        }
    }
}
