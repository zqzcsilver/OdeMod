using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OdeMod.Utils;
using System;
using System.Collections.Generic;

using Terraria;
using Terraria.ModLoader;

namespace OdeMod.Projectiles.Series.Boss.MiracleRecorder
{
    internal class DamageCircle3 : ModProjectile, IMiracleRecorderProj
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
            Projectile.timeLeft = 30;
            Projectile.penetrate = 1;
            Projectile.scale = 1f;
        }

        private float a = 0;
        private float width = 0;
        private float factor = 0.25f;

        public override void AI()
        {
            if (Projectile.timeLeft < 30 && Projectile.timeLeft > 20)
            {
                factor += 0.1f;
                a += 0.1f;
            }
            if (Projectile.timeLeft < 10)
            {
                factor -= 0.1f;
                a -= 0.1f;
            }
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            var result = Math.Sqrt((projHitbox.X - targetHitbox.X) * (projHitbox.X - targetHitbox.X) + (projHitbox.Y - targetHitbox.Y) * (projHitbox.Y - targetHitbox.Y));
            if (result < 160) return true;
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

            for (int i = 0; i < 8; i++)
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

            if (Projectile.timeLeft > 15)
            {
                width = 20f + (30 - Projectile.timeLeft) * 2;
            }
            if (Projectile.timeLeft < 10)
            {
                width = Projectile.timeLeft*5;
            }
            for (float i = 1; i <= 60; i++)
            {
                var normalDir = new Vector2((float)Math.Cos(i / 60f * 6.28318f), (float)Math.Sin(i / 60f * 6.28318f));
                bars.Add(new CustomVertexInfo(Projectile.position + normalDir * (width + 160), color, new Vector3(1, 1, a)));
                bars.Add(new CustomVertexInfo(Projectile.position + normalDir * (-width + 160), color, new Vector3(1, 0, a)));
            }

            List<CustomVertexInfo> triangleList = new List<CustomVertexInfo>();
            //count���ڷ���bars�����Ԫ��������������������
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

                //count���ڷ���bars�����Ԫ��������������������

                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.Transform);
                RasterizerState originalState = Main.graphics.GraphicsDevice.RasterizerState;

                var projection = Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, 0, 1);
                var model = Matrix.CreateTranslation(new Vector3(-Main.screenPosition.X, -Main.screenPosition.Y, 0)) * Main.Transform;

                //���ü�ʱ���ؼ���Shader
                var shader = ModContent.Request<Effect>("OdeMod/Effects/VertexShaders/Trail", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
                var MainColor = ModContent.Request<Texture2D>("OdeMod/Images/Effects/heatmap3", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
                var MaskColor = ModContent.Request<Texture2D>("OdeMod/Images/Effects/Flame0", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
                var MainShape = ModContent.Request<Texture2D>("OdeMod/Images/Effects/Extra_202", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
                // �ѱ任��������Ϣ����shader
                shader.Parameters["uTransform"].SetValue(model * projection);//����任�����Сȹ����Ƶ
                shader.Parameters["uTime"].SetValue(-(float)Main.time * 0.05f);//ʹ������ʱ��仯

                Main.graphics.GraphicsDevice.Textures[0] = MainColor;
                Main.graphics.GraphicsDevice.Textures[1] = MainShape;
                Main.graphics.GraphicsDevice.Textures[2] = MaskColor;
                Main.graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
                Main.graphics.GraphicsDevice.SamplerStates[1] = SamplerState.PointWrap;
                Main.graphics.GraphicsDevice.SamplerStates[2] = SamplerState.PointWrap;

                shader.CurrentTechnique.Passes[0].Apply();

                Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangleList.ToArray(), 0, triangleList.Count / 3);
                //�������Σ������Ǹ�0��ƫ����
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