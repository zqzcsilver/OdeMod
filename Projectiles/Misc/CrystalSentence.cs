using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OdeMod.Utils;

using System;
using System.Collections.Generic;

using Terraria;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Projectiles.Misc
{
    internal class CrystalSentence : ModProjectile, IMiscProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 26;
            Projectile.height = 56;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
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
        private float lerpRad = 0.01f;
        private Vector2 plrOrig = Vector2.Zero;
        private Vector2 plrOrig2 = Vector2.Zero;
        private float vel = 0;
        private float direct = 0;
        private float vel2 = 5;
        private int ok3 = 0;
        private bool directionIsLeft = false;

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
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
                    norm = Projectile.velocity.ToRotation();
                    direct = player.direction;
                }
                Projectile.rotation = (float)
                  System.Math.Atan2((double)Projectile.velocity.Y,
                  (double)Projectile.velocity.X) + 1.57f;
                Projectile.velocity *= 0.94f;
                reduceVel++;
            }
            else if (rotate <= 20)
            {
                ParticleOrchestraSettings settings;

                Vector2 value = Vector2.UnitX.RotatedBy(Main.rand.NextFloat() * ((float)Math.PI * 2f) + (float)Math.PI / 2f) * 13;
                Vector2 posin = Projectile.Center + value;
                settings = new ParticleOrchestraSettings
                {
                    PositionInWorld = posin,//位置
                    MovementVector = Projectile.velocity//速度
                };
                ParticleOrchestrator.RequestParticleSpawn(clientOnly: true, ParticleOrchestraType.PrincessWeapon, settings, 255);

                if (ok == 0)
                {
                    ok = 1;
                    plrOrig2 = Projectile.Center;
                }
                Projectile.Center = plrOrig + (vel + Vector2.Distance(plrOrig, plrOrig2)) * new Vector2((float)Math.Cos(norm), (float)Math.Sin(norm));
                vel += vel2;
                vel2 *= 0.92f;
                rotate++;
                Projectile.rotation = norm + 1.57f;
                if (direct != 1)
                {
                    norm -= lerpRad;
                }
                else
                {
                    norm += lerpRad;
                }
                if (rotate < 12)
                {
                    lerpRad *= 1.27f;
                }
                else
                {
                    if (rotate > 16)
                    {
                        lerpRad *= 0.5f;
                    }
                    else
                    {
                        lerpRad *= 0.85f;
                    }
                }
                if (rotate > 14)
                {
                    Projectile.alpha += 10;
                }
            }
            else
            {
                if (ok3 == 0)
                {
                    ok3 = 1;
                    Projectile.alpha = 255;
                    Projectile.velocity *= 0;
                    Projectile.damage = 0;
                    Projectile.timeLeft = 15;
                }
                Projectile.alpha += 10;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Player player = Main.player[Projectile.owner];
            var texture = ModContent.Request<Texture2D>(Texture, ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
            Vector2 drawOrigin = new Vector2(Projectile.width * 0.5f, Projectile.height * 0.5f);
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            if (rotate > 0)
            {
                if (direct == 1)
                {
                    for (int k = 0; k < Projectile.oldPos.Length - 3; k++)
                    {
                        Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                        Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - 3 - k) / (float)Projectile.oldPos.Length);
                        Main.spriteBatch.Draw(texture, drawPos, null, color, Projectile.rotation - (k * 0.15f), drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
                    }
                }
                else
                {
                    for (int k = 0; k < Projectile.oldPos.Length - 3; k++)
                    {
                        Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                        Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - 3 - k) / (float)Projectile.oldPos.Length);
                        Main.spriteBatch.Draw(texture, drawPos, null, color, Projectile.rotation + (k * 0.15f), drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
                    }
                }
            }
            else
            {
                for (int k = 0; k < Projectile.oldPos.Length - 3; k++)
                {
                    Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                    Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - 3 - k) / (float)Projectile.oldPos.Length) * 0.8f;
                    Main.spriteBatch.Draw(texture, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
                }
            }
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            return true;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.NewProjectileDirect(Projectile.GetSource_FromAI(), target.Center, Vector2.Zero, ModContent.ProjectileType<Projectiles.Misc.Light>(), Projectile.damage, 0);
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255 - Projectile.alpha, 255 - Projectile.alpha, 255 - Projectile.alpha, 255 - Projectile.alpha);
        }

        private int width = 24;

        public override void PostDraw(Color lightColor)
        {
            List<CustomVertexInfo> bars = new();
            //顶点离弹幕坐标的距离，也是顶点三角形宽度的一半
            // 把所有的点都生成出来，按照顺序
            if (rotate > 0)
            {
                width = 40;
            }
            for (int i = 1; i < Projectile.oldPos.Length; ++i)
            {
                if (rotate > 0)
                    width -= 3;

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
                var MaskColor = ModContent.Request<Texture2D>("OdeMod/Images/Effects/Extra_190", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
                var MainShape = ModContent.Request<Texture2D>("OdeMod/Images/Effects/Extra_197", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
                var MaskColor2 = ModContent.Request<Texture2D>("OdeMod/Images/Effects/Extra_189", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
                var MainShape2 = ModContent.Request<Texture2D>("OdeMod/Images/Effects/Extra_198", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
                var MainColor2 = ModContent.Request<Texture2D>("OdeMod/Images/Effects/heatmap2", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
                // 把变换和所需信息丢给shader
                shader.Parameters["uTransform"].SetValue(model * projection);//坐标变换，详见小裙子视频
                shader.Parameters["uTime"].SetValue(-(float)Main.time * 0.05f);//使纹理随时间变化
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