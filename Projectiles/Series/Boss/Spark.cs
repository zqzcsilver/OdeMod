using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System.Collections.Generic;

using Terraria;
using Terraria.ModLoader;
namespace OdeMod.Projectiles.Series.Boss
{
    internal class Spark : ModProjectile, IBossProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.timeLeft = 300;
            Projectile.alpha = 0;
            Projectile.penetrate = 1;
            Projectile.scale = 1f;
        }
        public override void AI()
        {
            Projectile.velocity *= 0;
        }
        public override void PostDraw(Color lightColor)
        {
            List<CustomVertexInfo> bars = new();
            //�����뵯Ļ����ľ��룬Ҳ�Ƕ��������ο�ȵ�һ��
            // �����еĵ㶼���ɳ���������˳��
            var factor = 1;
            //�����Ǽ�����ɫ�õĲ�ֵ��������Ч��ʵ��������ͼƬ��ɫ�������������ɫ����û�б�Ҫ
            var color = Color.Lerp(Color.White, Color.Red, factor);
            //w����������Ĳ�ֵ��ʹ�����λ���ܹ���ȷ��Ӧ
            //�����ߵ���������ֱ��Ҷ���
            bars.Add(new CustomVertexInfo(Projectile.Center - new Vector2(Projectile.width, Projectile.height), color, new Vector3(1, 1, 1)));
            bars.Add(new CustomVertexInfo(Projectile.Center + new Vector2(Projectile.width, -Projectile.height), color, new Vector3(1, 0, 1)));
            bars.Add(new CustomVertexInfo(Projectile.Center - new Vector2(Projectile.width, -Projectile.height), color, new Vector3(0, 1, 1)));
            bars.Add(new CustomVertexInfo(Projectile.Center + new Vector2(Projectile.width, Projectile.height), color, new Vector3(0, 0, 1)));

            List<CustomVertexInfo> triangleList = new List<CustomVertexInfo>();
            //count���ڷ���bars�����Ԫ��������������������

            triangleList.Add(bars[0]);
            triangleList.Add(bars[1]);
            triangleList.Add(bars[2]);

            triangleList.Add(bars[1]);
            triangleList.Add(bars[2]);
            triangleList.Add(bars[3]);

            /*triangleList.Add(bars[0]);
            var vertex = new CustomVertexInfo((bars[0].Position + bars[1].Position) * 0.5f + Vector2.Normalize(Projectile.velocity) * 30, Color.White,
                new Vector3(0, 0.5f, 1));
            triangleList.Add(bars[1]);
            triangleList.Add(vertex);//���ڻ�����ǰ��������Σ��Ǹ�����������*/

            // ����˳�����������Σ�����˳���뿴ȹ����Ƶ

            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.Transform);
            RasterizerState originalState = Main.graphics.GraphicsDevice.RasterizerState;
            // �ɵ�ע�͵��Ϳ���ֻ��ʾ������դ��
            //RasterizerState rasterizerState = new RasterizerState();
            //rasterizerState.CullMode = CullMode.None;
            //rasterizerState.FillMode = FillMode.WireFrame;
            //Main.graphics.GraphicsDevice.RasterizerState = rasterizerState;

            var projection = Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, 0, 1);
            var model = Matrix.CreateTranslation(new Vector3(-Main.screenPosition.X, -Main.screenPosition.Y, 0)) * Main.Transform;

            //���ü�ʱ���ؼ���Shader
            var shader = ModContent.Request<Effect>("OdeMod/Effects/Content/Trail", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
            var MainColor = ModContent.Request<Texture2D>("OdeMod/Images/Effects/heatmap", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
            var MaskColor = ModContent.Request<Texture2D>("OdeMod/Images/Effects/Sp", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
            var MainShape = ModContent.Request<Texture2D>("OdeMod/Images/Effects/Flame0", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
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