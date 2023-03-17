using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OdeMod.Utils;

using System;
using System.Collections.Generic;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Projectiles.Series.Boss.MiracleRecorder
{
    internal class Laser02 : ModProjectile, IMiracleRecorderProj
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
            Projectile.penetrate = -1;
            Projectile.scale = 1f;
            ProjectileID.Sets.DrawScreenCheckFluff[Type] = 6000;
        }

        private float a = 0;
        private float factor = 0;

        public override void AI()
        {
            Projectile.velocity *= 0;
            //Projectile.Center=
            //Player player = Main.player[Projectile.owner];
            if (Projectile.timeLeft > 25)
            {
                factor += 0.2f;
                a += 0.2f;
            }
            if (Projectile.timeLeft < 10)
            {
                factor -= 0.1f;
                a -= 0.1f;
            }
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            float point = 0f;
            return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Projectile.Center, Projectile.Center + 5000 * new Vector2((float)Math.Cos(Projectile.ai[0] + 1.57f), (float)Math.Sin(Projectile.ai[0] + 1.57f)), 27, ref point);
        }
        public override void PostDraw(Color lightColor)
        {
            List<CustomVertexInfo> bars = new();
            //�����뵯Ļ����ľ��룬Ҳ�Ƕ��������ο�ȵ�һ��
            // �����еĵ㶼���ɳ���������˳��
            //�����Ǽ�����ɫ�õĲ�ֵ��������Ч��ʵ��������ͼƬ��ɫ�������������ɫ����û�б�Ҫ
            var color = Color.Lerp(Color.White, Color.Red, factor);
            //w����������Ĳ�ֵ��ʹ�����λ���ܹ���ȷ��Ӧ
            //�����ߵ���������ֱ��Ҷ���
            float width = 0f;
            /*for (int i = 0; i < 5; i++)
            {
                width = (float)i / 4f;
                width = (float)Math.Sqrt(width);
                width *= 30f;
                width *= factor;
                //bars.Add(new CustomVertexInfo(Projectile.Center + new Vector2((float)Math.Cos(Projectile.ai[0]), (float)Math.Sin(Projectile.ai[0])) * width, color, new Vector3(factor, 1, a)));
                //bars.Add(new CustomVertexInfo(Projectile.Center - new Vector2((float)Math.Cos(Projectile.ai[0]), (float)Math.Sin(Projectile.ai[0])) * width, color, new Vector3(factor, 0, a)));
                bars.Add(new CustomVertexInfo(new Vector2((float)Math.Cos(Projectile.ai[0] + 1.57f), (float)Math.Sin(Projectile.ai[0] + 1.57f)) * i*5 + Projectile.Center + new Vector2((float)Math.Cos(Projectile.ai[0]), (float)Math.Sin(Projectile.ai[0])) * width, color, new Vector3(factor, 1, a)));
                bars.Add(new CustomVertexInfo(new Vector2((float)Math.Cos(Projectile.ai[0] + 1.57f), (float)Math.Sin(Projectile.ai[0] + 1.57f)) * i*5 + Projectile.Center - new Vector2((float)Math.Cos(Projectile.ai[0]), (float)Math.Sin(Projectile.ai[0])) * width, color, new Vector3(factor, 0, a)));
            }*/
            for (int i = 0; i < 25; i++)
            {
                width = (float)i / 6f;
                width = -(float)Math.Pow(2.71828, 0.4f - width) + 1.5f;
                width *= 18f;
                width *= factor;
                var m = (float)i / 24;
                bars.Add(new CustomVertexInfo(new Vector2((float)Math.Cos(Projectile.ai[0] + 1.57f), (float)Math.Sin(Projectile.ai[0] + 1.57f)) * i * 4 + Projectile.Center + new Vector2((float)Math.Cos(Projectile.ai[0]), (float)Math.Sin(Projectile.ai[0])) * width, color, new Vector3(m, 1, a * m)));
                bars.Add(new CustomVertexInfo(new Vector2((float)Math.Cos(Projectile.ai[0] + 1.57f), (float)Math.Sin(Projectile.ai[0] + 1.57f)) * i * 4 + Projectile.Center - new Vector2((float)Math.Cos(Projectile.ai[0]), (float)Math.Sin(Projectile.ai[0])) * width, color, new Vector3(m, 0, a * m)));
            }
            width = 27f;
            width *= factor;
            for (int i = 2; i < 50; i++)
            {
                var m = 0;
                if (i % 2 == 0) m = 0; else m = 1;
                bars.Add(new CustomVertexInfo(new Vector2((float)Math.Cos(Projectile.ai[0] + 1.57f), (float)Math.Sin(Projectile.ai[0] + 1.57f)) * i * 100 + Projectile.Center + new Vector2((float)Math.Cos(Projectile.ai[0]), (float)Math.Sin(Projectile.ai[0])) * width, color, new Vector3(m, 1, a)));
                bars.Add(new CustomVertexInfo(new Vector2((float)Math.Cos(Projectile.ai[0] + 1.57f), (float)Math.Sin(Projectile.ai[0] + 1.57f)) * i * 100 + Projectile.Center - new Vector2((float)Math.Cos(Projectile.ai[0]), (float)Math.Sin(Projectile.ai[0])) * width, color, new Vector3(m, 0, a)));
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
            }
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.Transform);
            RasterizerState originalState = Main.graphics.GraphicsDevice.RasterizerState;

            var projection = Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, 0, 1);
            var model = Matrix.CreateTranslation(new Vector3(-Main.screenPosition.X, -Main.screenPosition.Y, 0)) * Main.Transform;

            //���ü�ʱ���ؼ���Shader
            var shader = ModContent.Request<Effect>("OdeMod/Effects/VertexShaders/Trail", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
            var MainColor = ModContent.Request<Texture2D>("OdeMod/Images/Effects/heatmap3", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
            var MaskColor = ModContent.Request<Texture2D>("OdeMod/Images/Effects/LaserFx", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
            var MainShape = ModContent.Request<Texture2D>("OdeMod/Images/Effects/Extra_197", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
            // �ѱ任��������Ϣ����shader
            shader.Parameters["uTransform"].SetValue(model * projection);//����任�����Сȹ����Ƶ
            shader.Parameters["uTime"].SetValue(-(float)Main.time * 0.03f);//ʹ������ʱ��仯

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

        /*public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            Player player = Main.player[Projectile.owner];

            foreach (Projectile spawn in Main.projectile)
            {
                if (spawn.type == ModContent.ProjectileType<Projectiles.Series.Boss.Spark>() && spawn.ai[0] - Projectile.ai[0] == -1 && spawn.ai[1] == Projectile.ai[1])
                {
                    float point = 0f;
                    return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Projectile.Center + 8 * new Vector2((float)Math.Cos(Projectile.rotation + 1.57f), (float)Math.Sin(Projectile.rotation + 1.57f)),
                        Projectile.Center - 96 * new Vector2((float)Math.Cos(Projectile.rotation + 1.57f), (float)Math.Sin(Projectile.rotation + 1.57f)), 40, ref point);
                }
            }
        }*/

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255 - Projectile.alpha, 255 - Projectile.alpha, 255 - Projectile.alpha, 255 - Projectile.alpha);
        }
    }
}