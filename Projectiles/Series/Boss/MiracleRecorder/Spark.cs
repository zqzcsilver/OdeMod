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
    internal class Spark : ModProjectile, IMiracleRecorderProj
    {
        public override void SetDefaults()
        {
            Projectile.width = 38;
            Projectile.height = 38;
            Projectile.aiStyle = -1;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.alpha = 255;
            Projectile.timeLeft = 400;
            Projectile.penetrate = 1;
            Projectile.scale = 1f;

            Main.projFrames[Projectile.type] = 4;
            ProjectileID.Sets.TrailCacheLength[base.Projectile.type] = 12;
            ProjectileID.Sets.TrailingMode[base.Projectile.type] = 0;
        }

        private float a = 0;
        private bool ok = false;

        public override void AI()
        {
            Projectile.velocity *= 0;
            Player player = Main.player[Projectile.owner];
            if (Projectile.timeLeft < 100) a -= 0.025f;
            if (Projectile.timeLeft > 360) a += 0.025f;
            if (Projectile.timeLeft < 40) Projectile.alpha += 7;
            if (Projectile.timeLeft > 360) Projectile.alpha -= 7;

            if (Projectile.timeLeft % 5 == 0)
            {
                if (Projectile.frame < 3) Projectile.frame++;
                else Projectile.frame = 0;
            }

            Projectile.velocity = player.Center - Projectile.Center;
            Projectile.velocity.Normalize();
            Projectile.velocity *= 0.012f * (400 - Projectile.timeLeft);
        }

        public override void Kill(int timeLeft)
        {
            Player player = Main.player[Projectile.owner];
            player.GetModPlayer<OdePlayer>().Rolling = 0;
        }

        private float scaleDraw = 1f;

        public override bool PreDraw(ref Color lightColor)
        {
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            double range = Projectile.scale * 1;
            double range2 = Projectile.scale * 1;
            if (scaleDraw <= 2f)
            {
                scaleDraw += 0.025f;
            }
            else
            {
                scaleDraw = 1f;
            }
            double range3 = Projectile.scale * scaleDraw;
            Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;
            Vector2 drawOrigin = new Vector2(19, 19);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                range *= 1.04;
                range2 *= 0.96;
                Vector2 drawPos = Projectile.position - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - (int)((k + 5) * 1.5)) / (float)Projectile.oldPos.Length);
                Main.spriteBatch.Draw(texture, drawPos, new Rectangle(0, 38 * Projectile.frame, 38, 38), color, Projectile.rotation, drawOrigin, (float)range, SpriteEffects.None, 0f);
                Main.spriteBatch.Draw(texture, drawPos, new Rectangle(0, 38 * Projectile.frame, 38, 38), color, Projectile.rotation, drawOrigin, (float)range2, SpriteEffects.None, 0f);
            }
            Vector2 drawPos2 = Projectile.position - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
            Color color2 = Projectile.GetAlpha(lightColor) * (1f - 0.4f * scaleDraw);
            Main.spriteBatch.Draw(texture, drawPos2, new Rectangle(0, 38 * Projectile.frame, 38, 38), color2, Projectile.rotation, drawOrigin, (float)range3, SpriteEffects.None, 0f);

            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            return true;
        }

        private bool found = false;

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            foreach (Projectile spawn in Main.projectile)
            {
                if (spawn.type == ModContent.ProjectileType<Projectiles.Series.Boss.MiracleRecorder.Spark>() && spawn.ai[0] - Projectile.ai[0] == -1 && spawn.ai[1] == Projectile.ai[1] && spawn.active)
                {
                    float point = 0f;
                    bool a;
                    if (Projectile.alpha < 100)
                        a = projHitbox.Intersects(targetHitbox);
                    else
                        a = false;
                    bool b = Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Projectile.Center, spawn.Center, 10, ref point);
                    bool c = a | b;
                    return c;

                }
            }
            return projHitbox.Intersects(targetHitbox);
        }
        public override void PostDraw(Color lightColor)
        {
            if (Projectile.ai[0] > 1)
            {
                List<CustomVertexInfo> bars = new();
                //顶点离弹幕坐标的距离，也是顶点三角形宽度的一半
                // 把所有的点都生成出来，按照顺序
                var factor = 1;
                //这里是计算颜色用的插值，但最终效果实际上是用图片上色，所以这里的颜色处理没有必要
                var color = Color.Lerp(Color.White, Color.Red, factor);
                //w是纹理坐标的插值，使纹理的位置能够正确对应
                //朝切线的两个方向分别找顶点

                foreach (Projectile spawn in Main.projectile)
                {
                    if (spawn.type == ModContent.ProjectileType<Projectiles.Series.Boss.MiracleRecorder.Spark>() && spawn.ai[0] - Projectile.ai[0] == -1 && spawn.ai[1] == Projectile.ai[1])
                    {
                        var normalDir = spawn.Center - Projectile.Center;//两帧之间的切线向量
                        normalDir = Vector2.Normalize(new Vector2(-normalDir.Y, normalDir.X));
                        bars.Add(new CustomVertexInfo(Projectile.Center + normalDir * 10, color, new Vector3(factor, 1, a)));
                        bars.Add(new CustomVertexInfo(Projectile.Center + normalDir * -10, color, new Vector3(factor, 0, a)));
                        bars.Add(new CustomVertexInfo(spawn.Center + normalDir * 10, color, new Vector3(factor, 1, a)));
                        bars.Add(new CustomVertexInfo(spawn.Center + normalDir * -10, color, new Vector3(factor, 0, a)));
                    }
                }
                List<CustomVertexInfo> triangleList = new List<CustomVertexInfo>();
                //count用于返回bars里面的元素数量（即顶点数量）
                if (bars.Count > 2)
                {
                    triangleList.Add(bars[0]);
                    triangleList.Add(bars[2]);
                    triangleList.Add(bars[1]);

                    triangleList.Add(bars[1]);
                    triangleList.Add(bars[2]);
                    triangleList.Add(bars[3]);
                }
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.Transform);
                RasterizerState originalState = Main.graphics.GraphicsDevice.RasterizerState;

                var projection = Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, 0, 1);
                var model = Matrix.CreateTranslation(new Vector3(-Main.screenPosition.X, -Main.screenPosition.Y, 0)) * Main.Transform;

                //启用即时加载加载Shader
                var shader = ModContent.Request<Effect>("OdeMod/Effects/VertexShaders/Trail", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
                var MainColor = ModContent.Request<Texture2D>("OdeMod/Images/Effects/heatmap", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
                var MaskColor = ModContent.Request<Texture2D>("OdeMod/Images/Effects/Extra_189", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
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