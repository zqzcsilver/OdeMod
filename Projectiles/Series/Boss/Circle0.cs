using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
namespace OdeMod.Projectiles.Series.Boss
{
    internal class Circle0 : ModProjectile, IBossProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 1;
            Projectile.height = 1;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.alpha = 255;
            Projectile.timeLeft = 30;
            Projectile.penetrate = 1;
            Projectile.scale = 1f;

        }
        float a = 0;
        float width = 0;
        public override void AI()
        {
            Projectile.velocity *= 0;
        }

        public override void PostDraw(Color lightColor)
        {

            List<CustomVertexInfo> bars = new();
            var factor = 1;
            var color = Color.Lerp(Color.White, Color.Red, factor);
            width = ((float)Projectile.timeLeft + 15) * 1.5f;
            for (float i = 1; i <= 60; i++)
            {
                var normalDir = new Vector2((float)Math.Cos(i / 60f * 6.28318f), (float)Math.Sin(i / 60f * 6.28318f));
                bars.Add(new CustomVertexInfo(Projectile.position + normalDir * (width + Projectile.timeLeft * 30), color, new Vector3(1, 1, 0.6f - ((float)Projectile.timeLeft / 30f))));
                bars.Add(new CustomVertexInfo(Projectile.position + normalDir * (-width + Projectile.timeLeft * 30), color, new Vector3(1, 0, 0.6f - ((float)Projectile.timeLeft / 30f))));
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
                var shader = ModContent.Request<Effect>("OdeMod/Effects/Content/Trail", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
                var MainColor = ModContent.Request<Texture2D>("OdeMod/Images/Effects/heatmap", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
                var MaskColor = ModContent.Request<Texture2D>("OdeMod/Images/Effects/Flame0", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
                var MainShape = ModContent.Request<Texture2D>("OdeMod/Images/Effects/Extra_200", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
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