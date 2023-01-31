using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OdeMod.Players;
using OdeMod.Utils;

using System.Collections.Generic;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Projectiles.Series.Boss.MiracleRecorder
{
    internal class Sparkle : ModProjectile, IMiracleRecorderProj
    {
        public override void SetDefaults()
        {
            Projectile.width = 72;
            Projectile.height = 72;
            Projectile.aiStyle = -1;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.alpha = 0;
            Projectile.timeLeft = 120;
            Projectile.penetrate = 1;
            Projectile.scale = 1f;
            ProjectileID.Sets.TrailCacheLength[base.Projectile.type] = 10;
            ProjectileID.Sets.TrailingMode[base.Projectile.type] = 0;
        }

        private float a = 0;
        private bool ok = false;

        public override void AI()
        {
            Projectile.rotation = (float)
                  System.Math.Atan2((double)Projectile.velocity.Y,
                  (double)Projectile.velocity.X);
            Player player = Main.player[Projectile.owner];
            Projectile.velocity *= 1.04f;

        }

        private float scaleDraw = 1f;

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;
            Vector2 drawOrigin = new Vector2(36f, 36f);


            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition, null, Color.White * ((255f - (float)Projectile.alpha) / 255f), Projectile.rotation, drawOrigin, 1f, SpriteEffects.None, 0f);
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            return false;
        }

      

        public override void PostDraw(Color lightColor)
        {
            List<CustomVertexInfo> bars = new();
            //�����뵯Ļ����ľ��룬Ҳ�Ƕ��������ο�ȵ�һ��
            // �����еĵ㶼���ɳ���������˳��
            int width = 8;
            var normalDir = Projectile.position - Projectile.oldPos[1];
            normalDir = Vector2.Normalize(new Vector2(-normalDir.Y, normalDir.X));
            var factor = 0.1f / (float)Projectile.oldPos.Length;
            var color = Color.Lerp(Color.White, Color.Red, factor);
            var w = MathHelper.Lerp(1f, 0f, factor);
            bars.Add(new CustomVertexInfo(Projectile.position + 0.5f * new Vector2(Projectile.width, Projectile.height) + normalDir * width, color, new Vector3(factor, 1, w * (255 - Projectile.alpha) / 255f)));
            bars.Add(new CustomVertexInfo(Projectile.position + 0.5f * new Vector2(Projectile.width, Projectile.height) + normalDir * -width, color, new Vector3(factor, 0, w * (255 - Projectile.alpha) / 255f)));
            for (int i = 1; i < Projectile.oldPos.Length; ++i)
            {
                width -= 1;

                if (Projectile.oldPos[i] == Vector2.Zero) break;//ò��ɾ��Ӱ�첻�󣬵�Ļ��λ���ڣ�0��0����һ�ּ������������������
                /*Main.spriteBatch.Draw(TextureAssets.MagicPixel.Value, Projectile.oldPos[i] - Main.screenPosition,
                new Rectangle(0, 0, 1, 1), Color.White, 0f, new Vector2(0.5f, 0.5f), 5f, SpriteEffects.None, 0f);*/
                //�ɵ�ע�ͣ��������п�����ʾ����Ļ30֡���ڵ�oldpos
                //���
                normalDir = Projectile.oldPos[i - 1] - Projectile.oldPos[i];//��֮֡�����������
                normalDir = Vector2.Normalize(new Vector2(-normalDir.Y, normalDir.X));//�����ߵĴ��ߣ�����������

                factor = i / (float)Projectile.oldPos.Length;
                //�����Ǽ�����ɫ�õĲ�ֵ��������Ч��ʵ��������ͼƬ��ɫ�������������ɫ����û�б�Ҫ
                color = Color.Lerp(Color.White, Color.Red, factor);
                w = MathHelper.Lerp(1f, 0.05f, factor);
                //w����������Ĳ�ֵ��ʹ�����λ���ܹ���ȷ��Ӧ
                //�����ߵ���������ֱ��Ҷ���
                bars.Add(new CustomVertexInfo(Projectile.oldPos[i] + 0.5f * new Vector2(Projectile.width, Projectile.height) + normalDir * width, color, new Vector3(factor, 1, w * (255 - Projectile.alpha) / 255f)));
                bars.Add(new CustomVertexInfo(Projectile.oldPos[i] + 0.5f * new Vector2(Projectile.width, Projectile.height) + normalDir * -width, color, new Vector3(factor, 0, w * (255 - Projectile.alpha) / 255f)));
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
                var MainColor = ModContent.Request<Texture2D>("OdeMod/Images/Effects/heatmap3", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
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