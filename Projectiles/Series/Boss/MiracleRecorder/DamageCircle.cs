using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OdeMod.Utils;
using System;
using System.Collections.Generic;

using Terraria;
using Terraria.ModLoader;

namespace OdeMod.Projectiles.Series.Boss.MiracleRecorder
{
    internal class DamageCircle : ModProjectile, IMiracleRecorderProj
    {
        public override void SetDefaults()
        {
            Projectile.width = 1;
            Projectile.height = 1;
            Projectile.aiStyle = -1;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.alpha = 255;
            Projectile.timeLeft = 180;
            Projectile.penetrate = 1;
            Projectile.scale = 1f;
        }

        private float a = 0;
        private float width = 0;
        private float factor = 0.25f;

        public override void AI()
        {
            foreach (Projectile proj in Main.projectile)
            {
                if (proj.friendly && Vector2.Distance(proj.Center, Projectile.Center) < 160)
                {
                    proj.friendly = false;
                    proj.hostile = true;
                    proj.velocity *= -1f;
                }
            }
            Projectile.velocity *= 0;
            if (Projectile.timeLeft > 160)
            {
                a += 0.04f;
            }
            if (Projectile.timeLeft < 30 && Projectile.timeLeft > 20)
            {
                factor += 0.07f;
            }
            if (Projectile.timeLeft < 15)
            {
                factor -= 0.05f;
            }
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            var result = Math.Sqrt((projHitbox.X - targetHitbox.X) * (projHitbox.X - targetHitbox.X) + (projHitbox.Y - targetHitbox.Y) * (projHitbox.Y - targetHitbox.Y));
            if (result < 160 && Projectile.timeLeft < 30) return true;
            else return false;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            var range = 1f;
            Texture2D texture = ModContent.Request<Texture2D>("OdeMod/Projectiles/Series/Boss/MiracleRecorder/Round", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
            Vector2 drawOrigin = new Vector2(160, 160);
            Vector2 drawPos = Projectile.position - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY);
            var color = lightColor * factor;

            for (int i = 0; i < 10; i++)
            {

                Main.spriteBatch.Draw(texture, drawPos, null, color, Projectile.rotation, drawOrigin, range, SpriteEffects.None, 0f);

                range -= 0.05f;
                color *= 0.9f;
            }
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            return false;
        }
        public override void PostDraw(Color lightColor)
        {
            List<CustomVertexInfo> bars = new();
            var factor = 1;
            var color = Color.Lerp(Color.White, Color.Red, factor);
            if (Projectile.timeLeft >= 30)
                width = 5f;
            else
             if (Projectile.timeLeft > 20)
            {
                width = 5f + (30 - Projectile.timeLeft);
            }
            if (Projectile.timeLeft < 15)
            {
                width -= 2f;
            }
            for (float i = 1; i <= 60; i++)
            {
                var normalDir = new Vector2((float)Math.Cos(i / 60f * 6.28318f), (float)Math.Sin(i / 60f * 6.28318f));
                bars.Add(new CustomVertexInfo(Projectile.position + normalDir * (width + 160), color, new Vector3(1, 1, a)));
                bars.Add(new CustomVertexInfo(Projectile.position + normalDir * (-width + 160), color, new Vector3(1, 0, a)));
            }

            List<CustomVertexInfo> triangleList = new List<CustomVertexInfo>();
            //count用于返回bars里面的元素数量（即顶点数量）
            if (bars.Count > 2)
            {
                for (int i = 0; i < bars.Count - 2; i += 2)
                {
                    triangleList.Add(bars[i]);
                    triangleList.Add(bars[i + 2]);
                    triangleList.Add(bars[i + 1]);

                    triangleList.Add(bars[i + 1]);
                    triangleList.Add(bars[i + 2]);
                    triangleList.Add(bars[i + 3]);
                }

                triangleList.Add(bars[bars.Count - 2]);
                triangleList.Add(bars[bars.Count - 1]);
                triangleList.Add(bars[0]);

                triangleList.Add(bars[bars.Count - 1]);
                triangleList.Add(bars[0]);
                triangleList.Add(bars[1]);

                //count用于返回bars里面的元素数量（即顶点数量）

                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.Transform);
                RasterizerState originalState = Main.graphics.GraphicsDevice.RasterizerState;

                var projection = Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, 0, 1);
                var model = Matrix.CreateTranslation(new Vector3(-Main.screenPosition.X, -Main.screenPosition.Y, 0)) * Main.Transform;

                //启用即时加载加载Shader
                var shader = ModContent.Request<Effect>("OdeMod/Effects/VertexShaders/Trail", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
                var MainColor = ModContent.Request<Texture2D>("OdeMod/Images/Effects/heatmap3", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
                var MaskColor = ModContent.Request<Texture2D>("OdeMod/Images/Effects/Flame0", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
                var MainShape = ModContent.Request<Texture2D>("OdeMod/Images/Effects/Extra_197", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
                // 把变换和所需信息丢给shader
                shader.Parameters["uTransform"].SetValue(model * projection);//坐标变换，详见小裙子视频
                shader.Parameters["uTime"].SetValue(-(float)Main.time * 0.05f);//使纹理随时间变化

                Main.graphics.GraphicsDevice.Textures[0] = MainColor;
                Main.graphics.GraphicsDevice.Textures[1] = MainShape;
                Main.graphics.GraphicsDevice.Textures[2] = MaskColor;
                Main.graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
                Main.graphics.GraphicsDevice.SamplerStates[1] = SamplerState.PointWrap;
                Main.graphics.GraphicsDevice.SamplerStates[2] = SamplerState.PointWrap;

                shader.CurrentTechnique.Passes[0].Apply();

                Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangleList.ToArray(), 0, triangleList.Count / 3);
                //连三角形，其中那个0是偏移量
                Main.graphics.GraphicsDevice.RasterizerState = originalState;
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            }
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255 - Projectile.alpha, 255 - Projectile.alpha, 255 - Projectile.alpha, 255 - Projectile.alpha);
        }
    }
}