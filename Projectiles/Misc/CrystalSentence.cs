using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace OdeMod.Projectiles.Misc
{
    internal class CrystalSentence : ModProjectile, IMiscProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 40;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 300;
            Projectile.alpha = 255;
            Projectile.penetrate = -1;
            Projectile.scale = 1f;
            ProjectileID.Sets.TrailCacheLength[base.Projectile.type] = 15;
            ProjectileID.Sets.TrailingMode[base.Projectile.type] = 0;
        }
        private int reduceVel = 0;
        private int rotate = 0;
        private int ok = 0;
        private int ok2 = 0;
        private float norm = 0;
        private float lerpRad = 0;
        private float vel = 0;
        private float vel2 = 5;
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Vector2 plr2Proj = Vector2.Zero;
            Vector2 plrOrig = Vector2.Zero;
            Vector2 plrOrig2 = Vector2.Zero;
            if (reduceVel < 40)
            {
                if (Projectile.timeLeft <= 297 && Projectile.alpha > 0)
                {
                    Projectile.alpha -= 15;

                }

                if (ok2 == 0)
                {
                    ok2 = 1;
                    plrOrig = player.Center;
                    plr2Proj = Main.MouseWorld - plrOrig;

                    norm = Projectile.velocity.ToRotation();
                }
                Projectile.rotation = (float)
                  System.Math.Atan2((double)Projectile.velocity.Y,
                  (double)Projectile.velocity.X) + 1.57f;
                Projectile.velocity *= 0.92f;
                reduceVel++;
            }
            else if (rotate <= 30)
            {
                if (ok == 0)
                {
                    ok = 1;
                    plrOrig2 = Projectile.Center;
                }
                Projectile.Center = plrOrig + (vel + (Vector2.Distance(plrOrig, plrOrig2))) * new Vector2((float)Math.Cos(norm), (float)Math.Sin(norm));
                vel += vel2;
                vel2 *= 0.92f;
                rotate++;
                Projectile.rotation = norm + 1.57f;
                if (player.direction != 1)
                {
                    norm -= lerpRad;
                }
                else
                {
                    norm += lerpRad;
                }
                if (rotate < 15)
                {
                    lerpRad += 0.0075f;
                }
                else
                {
                    lerpRad *= 0.9f;
                }

                if (rotate > 20)
                {
                    Projectile.alpha += 5;
                }
            }

            if (rotate > 30)
            {
                Projectile.alpha += 20;
                if (Projectile.alpha > 255)
                    Projectile.active = false;
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            var texture = ModContent.Request<Texture2D>(Texture).Value;
            Vector2 drawOrigin = new Vector2(Projectile.width * 0.5f, Projectile.height * 0.5f);
            if (rotate > 0)
            {
                for (int k = 0; k < Projectile.oldPos.Length - 9; k++)
                {
                    Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                    Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - 9 - k) / (float)Projectile.oldPos.Length);
                    Main.spriteBatch.Draw(texture, drawPos, null, color, Projectile.rotation - (k * 0.2f), drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
                }
            }
            else
            {
                for (int k = 0; k < Projectile.oldPos.Length - 9; k++)
                {
                    Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                    Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - 9 - k) / (float)Projectile.oldPos.Length) * 0.8f;
                    Main.spriteBatch.Draw(texture, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
                }
            }
            return true;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {

        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255 - Projectile.alpha, 255 - Projectile.alpha, 255 - Projectile.alpha, 255 - Projectile.alpha);
        }
        int width = 24;
        public override void PostDraw(Color lightColor)
        {
            List<CustomVertexInfo> bars = new();
            //�����뵯Ļ����ľ��룬Ҳ�Ƕ��������ο�ȵ�һ��
            // �����еĵ㶼���ɳ���������˳��
            if (rotate > 0)
            {
                width = 40;
            }
            for (int i = 1; i < Projectile.oldPos.Length; ++i)
            {
                if (rotate > 0)
                    width -= 2;

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
                Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone);
                RasterizerState originalState = Main.graphics.GraphicsDevice.RasterizerState;
                // �ɵ�ע�͵��Ϳ���ֻ��ʾ������դ��
                //RasterizerState rasterizerState = new RasterizerState();
                //rasterizerState.CullMode = CullMode.None;
                //rasterizerState.FillMode = FillMode.WireFrame;
                //Main.graphics.GraphicsDevice.RasterizerState = rasterizerState;

                var projection = Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, 0, 1);
                var model = Matrix.CreateTranslation(new Vector3(-Main.screenPosition.X, -Main.screenPosition.Y, 0));

                //���ü�ʱ���ؼ���Shader
                var shader = ModContent.Request<Effect>("OdeMod/Effects/Content/Trail", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
                var MainColor = ModContent.Request<Texture2D>("OdeMod/Images/Effects/heatmap", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
                var MaskColor = ModContent.Request<Texture2D>("OdeMod/Images/Effects/Extra_190", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
                var MainShape = ModContent.Request<Texture2D>("OdeMod/Images/Effects/Extra_197", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
                var MaskColor2 = ModContent.Request<Texture2D>("OdeMod/Images/Effects/Extra_189", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
                var MainShape2 = ModContent.Request<Texture2D>("OdeMod/Images/Effects/Extra_198", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
                var MainColor2 = ModContent.Request<Texture2D>("OdeMod/Images/Effects/heatmap2", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
                // �ѱ任��������Ϣ����shader
                shader.Parameters["uTransform"].SetValue(model * projection);//����任�����Сȹ����Ƶ
                shader.Parameters["uTime"].SetValue(-(float)Main.time * 0.05f);//ʹ������ʱ��仯
                if (rotate < 5)
                {
                    Main.graphics.GraphicsDevice.Textures[0] = MainColor;
                    Main.graphics.GraphicsDevice.Textures[1] = MainShape;
                    Main.graphics.GraphicsDevice.Textures[2] = MaskColor;
                    Main.graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
                    Main.graphics.GraphicsDevice.SamplerStates[1] = SamplerState.PointWrap;
                    Main.graphics.GraphicsDevice.SamplerStates[2] = SamplerState.PointWrap;
                }
                else
                {
                    Main.graphics.GraphicsDevice.Textures[0] = MainColor;
                    Main.graphics.GraphicsDevice.Textures[1] = MainShape2;
                    Main.graphics.GraphicsDevice.Textures[2] = MaskColor2;
                    Main.graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
                    Main.graphics.GraphicsDevice.SamplerStates[1] = SamplerState.PointWrap;
                    Main.graphics.GraphicsDevice.SamplerStates[2] = SamplerState.PointWrap;
                }
                //Main.graphics.GraphicsDevice.Textures[0] = Main.magicPixel;
                //Main.graphics.GraphicsDevice.Textures[1] = Main.magicPixel;
                //Main.graphics.GraphicsDevice.Textures[2] = Main.magicPixel;

                shader.CurrentTechnique.Passes[0].Apply();


                Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangleList.ToArray(), 0, triangleList.Count / 3);
                //�������Σ������Ǹ�0��ƫ����
                Main.graphics.GraphicsDevice.RasterizerState = originalState;
                Main.spriteBatch.End();
                Main.spriteBatch.Begin();
            }
        }

        private struct CustomVertexInfo : IVertexType
        {
            private static VertexDeclaration _vertexDeclaration = new VertexDeclaration(new VertexElement[3]
            {
                new VertexElement(0, VertexElementFormat.Vector2, VertexElementUsage.Position, 0),
                new VertexElement(8, VertexElementFormat.Color, VertexElementUsage.Color, 0),
                new VertexElement(12, VertexElementFormat.Vector3, VertexElementUsage.TextureCoordinate, 0)
            });
            public Vector2 Position;
            public Color Color;
            public Vector3 TexCoord;

            public CustomVertexInfo(Vector2 position, Color color, Vector3 texCoord)
            {
                this.Position = position;
                this.Color = color;
                this.TexCoord = texCoord;
            }

            public VertexDeclaration VertexDeclaration
            {
                get
                {
                    return _vertexDeclaration;
                }
            }
        }
    }
}