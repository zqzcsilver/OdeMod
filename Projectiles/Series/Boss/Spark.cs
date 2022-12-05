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
            //顶点离弹幕坐标的距离，也是顶点三角形宽度的一半
            // 把所有的点都生成出来，按照顺序
            var factor = 1;
            //这里是计算颜色用的插值，但最终效果实际上是用图片上色，所以这里的颜色处理没有必要
            var color = Color.Lerp(Color.White, Color.Red, factor);
            //w是纹理坐标的插值，使纹理的位置能够正确对应
            //朝切线的两个方向分别找顶点
            bars.Add(new CustomVertexInfo(Projectile.Center - new Vector2(Projectile.width, Projectile.height), color, new Vector3(1, 1, 1)));
            bars.Add(new CustomVertexInfo(Projectile.Center + new Vector2(Projectile.width, -Projectile.height), color, new Vector3(1, 0, 1)));
            bars.Add(new CustomVertexInfo(Projectile.Center - new Vector2(Projectile.width, -Projectile.height), color, new Vector3(0, 1, 1)));
            bars.Add(new CustomVertexInfo(Projectile.Center + new Vector2(Projectile.width, Projectile.height), color, new Vector3(0, 0, 1)));

            List<CustomVertexInfo> triangleList = new List<CustomVertexInfo>();
            //count用于返回bars里面的元素数量（即顶点数量）

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
            triangleList.Add(vertex);//用于绘制最前面的三角形，是个等腰三角形*/

            // 按照顺序连接三角形，连接顺序请看裙子视频

            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.Transform);
            RasterizerState originalState = Main.graphics.GraphicsDevice.RasterizerState;
            // 干掉注释掉就可以只显示三角形栅格
            //RasterizerState rasterizerState = new RasterizerState();
            //rasterizerState.CullMode = CullMode.None;
            //rasterizerState.FillMode = FillMode.WireFrame;
            //Main.graphics.GraphicsDevice.RasterizerState = rasterizerState;

            var projection = Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, 0, 1);
            var model = Matrix.CreateTranslation(new Vector3(-Main.screenPosition.X, -Main.screenPosition.Y, 0)) * Main.Transform;

            //启用即时加载加载Shader
            var shader = ModContent.Request<Effect>("OdeMod/Effects/Content/Trail", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
            var MainColor = ModContent.Request<Texture2D>("OdeMod/Images/Effects/heatmap", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
            var MaskColor = ModContent.Request<Texture2D>("OdeMod/Images/Effects/Sp", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
            var MainShape = ModContent.Request<Texture2D>("OdeMod/Images/Effects/Flame0", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
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