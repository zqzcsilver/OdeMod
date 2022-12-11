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
            //顶点离弹幕坐标的距离，也是顶点三角形宽度的一半
            // 把所有的点都生成出来，按照顺序
            int width = 15;
            for (int i = 1; i < Projectile.oldPos.Length; ++i)
            {
                width -= 1;

                if (Projectile.oldPos[i] == Vector2.Zero) break;//貌似删掉影响不大，弹幕的位置在（0，0）是一种几乎不可能遇到的情况
                /*Main.spriteBatch.Draw(TextureAssets.MagicPixel.Value, Projectile.oldPos[i] - Main.screenPosition,
                new Rectangle(0, 0, 1, 1), Color.White, 0f, new Vector2(0.5f, 0.5f), 5f, SpriteEffects.None, 0f);*/
                //干掉注释，上面两行可以显示出弹幕30帧以内的oldpos
                //宽度
                var normalDir = Projectile.oldPos[i - 1] - Projectile.oldPos[i];//两帧之间的切线向量
                normalDir = Vector2.Normalize(new Vector2(-normalDir.Y, normalDir.X));//算切线的垂线（即法向量）

                var factor = i / (float)Projectile.oldPos.Length;
                //这里是计算颜色用的插值，但最终效果实际上是用图片上色，所以这里的颜色处理没有必要
                var color = Color.Lerp(Color.White, Color.Red, factor);
                var w = MathHelper.Lerp(1f, 0.05f, factor);
                //w是纹理坐标的插值，使纹理的位置能够正确对应
                //朝切线的两个方向分别找顶点
                bars.Add(new CustomVertexInfo(Projectile.oldPos[i] + 0.5f * new Vector2(Projectile.width, Projectile.height) + normalDir * width, color, new Vector3(factor, 1, w)));
                bars.Add(new CustomVertexInfo(Projectile.oldPos[i] + 0.5f * new Vector2(Projectile.width, Projectile.height) + normalDir * -width, color, new Vector3(factor, 0, w)));
            }

            List<CustomVertexInfo> triangleList = new List<CustomVertexInfo>();
            //count用于返回bars里面的元素数量（即顶点数量）
            if (bars.Count > 2)
            {
                /*triangleList.Add(bars[0]);
                var vertex = new CustomVertexInfo((bars[0].Position + bars[1].Position) * 0.5f + Vector2.Normalize(Projectile.velocity) * 30, Color.White,
                    new Vector3(0, 0.5f, 1));
                triangleList.Add(bars[1]);
                triangleList.Add(vertex);//用于绘制最前面的三角形，是个等腰三角形*/

                for (int i = 0; i < bars.Count - 2; i += 2)
                {
                    triangleList.Add(bars[i]);
                    triangleList.Add(bars[i + 2]);
                    triangleList.Add(bars[i + 1]);

                    triangleList.Add(bars[i + 1]);
                    triangleList.Add(bars[i + 2]);
                    triangleList.Add(bars[i + 3]);
                }
                // 按照顺序连接三角形，连接顺序请看裙子视频

                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
                RasterizerState originalState = Main.graphics.GraphicsDevice.RasterizerState;

                var projection = Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, 0, 1);
                var model = Matrix.CreateTranslation(new Vector3(-Main.screenPosition.X, -Main.screenPosition.Y, 0)) * Main.Transform;

                //启用即时加载加载Shader
                var shader = ModContent.Request<Effect>("OdeMod/Effects/VertexShaders/Trail", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
                var MainColor = ModContent.Request<Texture2D>("OdeMod/Images/Effects/heatmap", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
                var MaskColor2 = ModContent.Request<Texture2D>("OdeMod/Images/Effects/Flame0", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
                var MainShape2 = ModContent.Request<Texture2D>("OdeMod/Images/Effects/Extra_197", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
                // 把变换和所需信息丢给shader
                shader.Parameters["uTransform"].SetValue(model * projection);//坐标变换，详见小裙子视频
                shader.Parameters["uTime"].SetValue(-(float)Main.time * 0.05f);//使纹理随时间变化

                Main.graphics.GraphicsDevice.Textures[0] = MainColor;
                Main.graphics.GraphicsDevice.Textures[1] = MainShape2;
                Main.graphics.GraphicsDevice.Textures[2] = MaskColor2;
                Main.graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
                Main.graphics.GraphicsDevice.SamplerStates[1] = SamplerState.PointWrap;
                Main.graphics.GraphicsDevice.SamplerStates[2] = SamplerState.PointWrap;

                shader.CurrentTechnique.Passes[0].Apply();

                Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangleList.ToArray(), 0, triangleList.Count / 3);
                //连三角形，其中那个0是偏移量
                Main.graphics.GraphicsDevice.RasterizerState = originalState;
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            }
        }
    }
}