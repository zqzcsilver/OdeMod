using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OdeMod.Utils;

using System.Collections.Generic;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Projectiles.Series.Boss.MiracleRecorder
{
    internal class Holyproj : ModProjectile, IMiracleRecorderProj
    {
        public override void SetDefaults()
        {
            Projectile.width = 46;
            Projectile.height = 46;
            Projectile.aiStyle = -1;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.alpha = 0;
            Projectile.timeLeft = 180;
            Projectile.penetrate = 1;
            Projectile.scale = 1f;
            Projectile.extraUpdates = 1;
            Main.projFrames[Projectile.type] = 4;
            ProjectileID.Sets.TrailCacheLength[base.Projectile.type] = 12;
            ProjectileID.Sets.TrailingMode[base.Projectile.type] = 0;
        }

        private Vector2 vec = Vector2.Zero;

        public override void AI()
        {
            //Projectile.rotation += 0.05f;
            /*if (Projectile.timeLeft == 179)
            {
                Projectile.velocity *= Main.rand.Next(50, 150) * 0.01f;
            }*/
            if (Projectile.timeLeft < 169)
            {
                Projectile.velocity *= 0.98f;
            }
            if (Projectile.timeLeft < 50)
            {
                Projectile.alpha += 5;
            }
            if (Projectile.timeLeft % 5 == 0)
            {
                if (Projectile.frame < 3) Projectile.frame++;
                else Projectile.frame = 0;
            }
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255 - Projectile.alpha, 255 - Projectile.alpha, 255 - Projectile.alpha, 255 - Projectile.alpha);
        }

        public override void PostDraw(Color lightColor)
        {
            List<CustomVertexInfo> bars = new();
            //�����뵯Ļ����ľ��룬Ҳ�Ƕ��������ο�ȵ�һ��
            // �����еĵ㶼���ɳ���������˳��
            int width = 15;
            for (int i = 1; i < Projectile.oldPos.Length; ++i)
            {
                width -= 1;

                if (Projectile.oldPos[i] == Vector2.Zero) break;//ò��ɾ��Ӱ�첻�󣬵�Ļ��λ���ڣ�0��0����һ�ּ������������������
                /*Main.spriteBatch.Draw(TextureAssets.MagicPixel.Value, Projectile.oldPos[i] - Main.screenPosition,
                new Rectangle(0, 0, 1, 1), Color.White, 0f, new Vector2(0.5f, 0.5f), 5f, SpriteEffects.None, 0f);*/
                //�ɵ�ע�ͣ��������п�����ʾ����Ļ30֡���ڵ�oldpos
                //���
                var normalDir = Projectile.oldPos[i - 1] - Projectile.oldPos[i];//��֮֡�����������
                normalDir = Vector2.Normalize(new Vector2(-normalDir.Y, normalDir.X));//�����ߵĴ��ߣ�����������

                var factor = i / (float)Projectile.oldPos.Length;
                //�����Ǽ�����ɫ�õĲ�ֵ��������Ч��ʵ��������ͼƬ��ɫ�������������ɫ����û�б�Ҫ
                var color = Color.Lerp(Color.White, Color.Red, factor);
                var w = MathHelper.Lerp(1f, 0.05f, factor);
                //w����������Ĳ�ֵ��ʹ�����λ���ܹ���ȷ��Ӧ
                //�����ߵ���������ֱ��Ҷ���
                bars.Add(new CustomVertexInfo(Projectile.oldPos[i] + 0.5f * new Vector2(Projectile.width, Projectile.height) + normalDir * width, color, new Vector3(factor, 1, w)));
                bars.Add(new CustomVertexInfo(Projectile.oldPos[i] + 0.5f * new Vector2(Projectile.width, Projectile.height) + normalDir * -width, color, new Vector3(factor, 0, w)));
            }

            List<CustomVertexInfo> triangleList = new List<CustomVertexInfo>();
            //count���ڷ���bars�����Ԫ��������������������
            if (bars.Count > 2)
            {
                /*triangleList.Add(bars[0]);
                var vertex = new CustomVertexInfo((bars[0].Position + bars[1].Position) * 0.5f + Vector2.Normalize(Projectile.velocity) * 30, Color.White,
                    new Vector3(0, 0.5f, 1));
                triangleList.Add(bars[1]);
                triangleList.Add(vertex);//���ڻ�����ǰ��������Σ��Ǹ�����������*/

                for (int i = 0; i < bars.Count - 2; i += 2)
                {
                    triangleList.Add(bars[i]);
                    triangleList.Add(bars[i + 2]);
                    triangleList.Add(bars[i + 1]);

                    triangleList.Add(bars[i + 1]);
                    triangleList.Add(bars[i + 2]);
                    triangleList.Add(bars[i + 3]);
                }
                // ����˳�����������Σ�����˳���뿴ȹ����Ƶ

                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
                RasterizerState originalState = Main.graphics.GraphicsDevice.RasterizerState;

                var projection = Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, 0, 1);
                var model = Matrix.CreateTranslation(new Vector3(-Main.screenPosition.X, -Main.screenPosition.Y, 0)) * Main.Transform;

                //���ü�ʱ���ؼ���Shader
                var shader = ModContent.Request<Effect>("OdeMod/Effects/VertexShaders/Trail", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
                var MainColor = ModContent.Request<Texture2D>("OdeMod/Images/Effects/heatmap", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
                var MaskColor2 = ModContent.Request<Texture2D>("OdeMod/Images/Effects/Flame0", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
                var MainShape2 = ModContent.Request<Texture2D>("OdeMod/Images/Effects/Extra_197", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
                // �ѱ任��������Ϣ����shader
                shader.Parameters["uTransform"].SetValue(model * projection);//����任�����Сȹ����Ƶ
                shader.Parameters["uTime"].SetValue(-(float)Main.time * 0.05f);//ʹ������ʱ��仯

                Main.graphics.GraphicsDevice.Textures[0] = MainColor;
                Main.graphics.GraphicsDevice.Textures[1] = MainShape2;
                Main.graphics.GraphicsDevice.Textures[2] = MaskColor2;
                Main.graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
                Main.graphics.GraphicsDevice.SamplerStates[1] = SamplerState.PointWrap;
                Main.graphics.GraphicsDevice.SamplerStates[2] = SamplerState.PointWrap;

                shader.CurrentTechnique.Passes[0].Apply();

                Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangleList.ToArray(), 0, triangleList.Count / 3);
                //�������Σ������Ǹ�0��ƫ����
                Main.graphics.GraphicsDevice.RasterizerState = originalState;
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            }
        }
    }
}