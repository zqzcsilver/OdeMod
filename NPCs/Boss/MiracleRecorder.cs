using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;

using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.NPCs.Boss
{
    public class MiracleRecorder : ModNPC, IBoss
    {

        public float SafeToRotation(Vector2 vec0)
        {
            if (vec0.ToRotation() <= 0) return -vec0.ToRotation();
            else return (2 * MathHelper.Pi) - vec0.ToRotation();
        }
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 4;
        }
        public override void SetDefaults()
        {
            NPC.lifeMax = 28000;
            NPC.damage = 60;
            NPC.defense = 20;
            NPC.knockBackResist = 0f;
            NPC.width = 134;
            NPC.height = 134;
            NPC.aiStyle = -1;
            NPC.boss = true;
            NPC.alpha = 0;
            NPC.noGravity = true;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath6;
            NPC.value = Item.buyPrice(0, 0, 10, 0);
            NPC.noTileCollide = true;
            NPCID.Sets.TrailingMode[NPC.type] = 0;
            NPCID.Sets.TrailCacheLength[NPC.type] = 8;

        }
        int control = 0;//控制怪物的行为：0：游荡，1：冲刺
        int framecontrol = 0;
        public override void FindFrame(int frameHeight)
        {
            framecontrol++;

            if (framecontrol % 8 == 0)
            {
                if (NPC.frame.Y <= frameHeight * 2)
                {
                    NPC.frame.Y += frameHeight;
                }
                else
                {
                    NPC.frame.Y = 0;
                }
            }

        }
        float[] rads = new float[3] { 0.5236f, 2.618f, 4.7116f };//冲刺用的角度数组
        int act = 0;//控制不同行为的draw
        bool IsDoing = false;
        float timer = 0;//计时器
        Vector2 plrCenter = Vector2.Zero;//定时记录玩家位置
        float distance = 0;//玩家距离
        int ok = -1;//冲刺用1
        float ok2 = 0;//冲刺用2
        Vector2 noticeVec = Vector2.Zero;
        int count = 0;//冲刺次数
        int count2 = 0;//召唤球球数量
        float rando = Main.rand.Next(-10, 20) * 0.05f;//随机偏移量
        Vector2 dir = Vector2.Zero;

        float oldrotate = 0;
        float newrotate = 0;

        public override void AI()
        {
            if (!IsDoing)
            {
                control = Main.rand.Next(1, 2);//左闭右开，能取0,1
            }


            Lighting.AddLight(NPC.Center, 0.9647f, 0.635f, 1);
            ParticleOrchestraSettings settings;
            timer++;
            NPC.TargetClosest(true);
            Player player = Main.player[NPC.target];

            /*Vector2 witness = new Vector2(player.Center.X - NPC.Center.X, player.Center.Y - NPC.Center.Y);
            witness.Normalize();
            float lerp = (witness.ToRotation() - NPC.rotation - 1.57f);
            while (lerp > 3.14159f)
                lerp -= 6.28318f;

            while (lerp < -3.14159f)
                lerp += 6.28318f;

            if (Math.Abs(lerp) < 0.01f) lerp = 0;
            NPC.rotation += lerp * 0.05f;*/


            if (control == 0)
            {
                if (timer == 1)
                {
                    IsDoing = true;
                    NPC.alpha = 0;
                    ok++;
                    ok2 = 0;
                    act = 0;
                    plrCenter = player.Center;
                    distance = 400f;
                    NPC.Center = plrCenter + new Vector2((float)Math.Cos(rads[ok]), (float)Math.Sin(rads[ok])) * distance;
                    for (int i = 1; i < 40; i++)
                    {
                        var dust2 = Dust.NewDustDirect(NPC.Center, 1, 1, DustID.PinkTorch, 0, 0, 0, Color.White, 2.5f);
                        dust2.velocity = 4 * Main.rand.NextVector2Unit();
                        dust2.noGravity = true;
                    }
                    for (int i = 1; i < 10; i++)
                    {
                        Vector2 value = Vector2.UnitX.RotatedBy(Main.rand.NextFloat() * ((float)Math.PI * 2f) + (float)Math.PI / 2f) * 13;
                        Vector2 posin = NPC.position + new Vector2(0f, -80f) + new Vector2(67, 147) + value;
                        settings = new ParticleOrchestraSettings
                        {
                            PositionInWorld = posin,//位置
                            MovementVector = 4 * Main.rand.NextVector2Unit()

                        };
                        ParticleOrchestrator.RequestParticleSpawn(clientOnly: true, ParticleOrchestraType.PrincessWeapon, settings, 255);
                    }
                    noticeVec = NPC.Center;
                }
                if (timer > 1 && timer < 61)
                {
                    ok2 += 0.02741f * (float)Math.Sin((timer - 1) / 60 * Math.PI);
                    NPC.Center = plrCenter + new Vector2((float)(Math.Sin(3 * (rads[ok] + ok2)) * Math.Cos(rads[ok] + ok2)), (float)(Math.Sin(3 * (rads[ok] + ok2)) * Math.Sin(rads[ok] + ok2))) * distance;
                    NPC.rotation = (NPC.Center - noticeVec).ToRotation() - 1.57f;
                    noticeVec = NPC.Center;
                }
                if (timer > 15 && timer < 61)
                {
                    act = 1;
                }
                if (timer >= 61 && timer < 81)
                {
                    act = 2;
                    NPC.alpha += 15;
                }
                if (timer == 81)
                {
                    for (int i = 1; i < 40; i++)
                    {
                        var dust2 = Dust.NewDustDirect(NPC.Center, 1, 1, DustID.PinkTorch, 0, 0, 0, Color.White, 2.5f);
                        dust2.velocity = 4 * Main.rand.NextVector2Unit();
                        dust2.noGravity = true;
                    }
                    for (int i = 1; i < 10; i++)
                    {
                        Vector2 value = Vector2.UnitX.RotatedBy(Main.rand.NextFloat() * ((float)Math.PI * 2f) + (float)Math.PI / 2f) * 13;
                        Vector2 posin = NPC.position + new Vector2(0f, -80f) + new Vector2(67, 147) + value;
                        settings = new ParticleOrchestraSettings
                        {
                            PositionInWorld = posin,//位置
                            MovementVector = 4 * Main.rand.NextVector2Unit()

                        };
                        ParticleOrchestrator.RequestParticleSpawn(clientOnly: true, ParticleOrchestraType.PrincessWeapon, settings, 255);
                    }
                    count++;
                    timer = 0;

                    if (ok == 2)
                    {
                        ok = -1;
                    }
                    if (count == 6)
                    {
                        IsDoing = false;
                        count = 0;
                        ok = -1;
                        ok2 = 0;
                        NPC.alpha = 0;
                    }
                }
            }
            if (control == 1)
            {
                if (timer == 1)
                {
                    count2++;
                    noticeVec = NPC.Center;
                    IsDoing = true;
                    rando = Main.rand.Next(-15, 16) * 0.03f;
                    plrCenter = player.Center;
                    dir = player.Center - NPC.Center;
                    dir.Normalize();
                    dir = new Vector2((float)Math.Cos(dir.ToRotation() + rando), (float)Math.Sin(dir.ToRotation() + rando));
                    newrotate = dir.ToRotation() - 1.57f;
                    oldrotate = NPC.rotation;
                }
                if (timer > 1 && timer < 15)
                {
                    NPC.rotation = (oldrotate * (15 - timer) * 0.067f) + newrotate * timer * 0.067f;
                }
                if (timer == 15)
                {
                    NPC.velocity = dir * 30f;
                    NPC.rotation = newrotate;
                }
                if (timer >= 15 && timer <= 45)
                {
                    act = 3;
                    NPC.velocity *= 0.96f;
                }
                if (timer > 45)
                {
                    if (count2 >= 4)
                    {
                        NPC.alpha = 0;
                        IsDoing = false;
                        ok2 = 0;
                        count2 = 0;
                    }
                    timer = 0;
                }
            }

        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D texture = TextureAssets.Npc[NPC.type].Value;
            Texture2D texture2 = ModContent.Request<Texture2D>("OdeMod/NPCs/Boss/MiracleRecorderDrawer").Value;
            Vector2 drawOrigin = new Vector2(134 * 0.5f, 209 * 0.703f);
            Vector2 drawPos = NPC.position - Main.screenPosition + drawOrigin + new Vector2(0f, -80f);
            if (act == 1)
            {
                for (int i = 0; i < NPC.oldPos.Length - 4; i++)
                {
                    Vector2 drawPos2 = NPC.oldPos[i] - Main.screenPosition + drawOrigin + new Vector2(0f, NPC.gfxOffY) + new Vector2(0f, -80f);
                    Color color = new Color(246, 162, 255) * ((NPC.oldPos.Length - i - 1) / (float)NPC.oldPos.Length) * 0.8f;
                    Main.spriteBatch.Draw(texture2, drawPos2, new Rectangle(0, NPC.frame.Y, 134, 209), color, NPC.rotation, drawOrigin, 1f, SpriteEffects.None, 0f);
                }

                int width = 72;
                List<CustomVertexInfo> bars = new();
                //顶点离弹幕坐标的距离，也是顶点三角形宽度的一半
                // 把所有的点都生成出来，按照顺序
                for (int i = 1; i < NPC.oldPos.Length; ++i)
                {
                    width -= 4;
                    if (NPC.oldPos[i] == Vector2.Zero) break;//貌似删掉影响不大，弹幕的位置在（0，0）是一种几乎不可能遇到的情况
                    /*Main.spriteBatch.Draw(TextureAssets.MagicPixel.Value, Projectile.oldPos[i] - Main.screenPosition,
                    new Rectangle(0, 0, 1, 1), Color.White, 0f, new Vector2(0.5f, 0.5f), 5f, SpriteEffects.None, 0f);*/
                    //干掉注释，上面两行可以显示出弹幕30帧以内的oldpos
                    //宽度
                    var normalDir = NPC.oldPos[i - 1] - NPC.oldPos[i];//两帧之间的切线向量
                    normalDir = Vector2.Normalize(new Vector2(-normalDir.Y, normalDir.X));//算切线的垂线（即法向量）

                    var factor = i / (float)NPC.oldPos.Length;
                    //这里是计算颜色用的插值，但最终效果实际上是用图片上色，所以这里的颜色处理没有必要
                    var color = Color.Lerp(Color.White, Color.Red, factor);
                    var w = MathHelper.Lerp(1f, 0f, factor);
                    //w是纹理坐标的插值，使纹理的位置能够正确对应
                    //朝切线的两个方向分别找顶点
                    bars.Add(new CustomVertexInfo(NPC.oldPos[i] + new Vector2(0f, -80f) + new Vector2(67, 147) + normalDir * width, color, new Vector3(factor, 1, w)));
                    bars.Add(new CustomVertexInfo(NPC.oldPos[i] + new Vector2(0f, -80f) + new Vector2(67, 147) + normalDir * -width, color, new Vector3(factor, 0, w)));
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
                    Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.Transform);
                    RasterizerState originalState = Main.graphics.GraphicsDevice.RasterizerState;
                    // 干掉注释掉就可以只显示三角形栅格
                    //RasterizerState rasterizerState = new RasterizerState();
                    //rasterizerState.CullMode = CullMode.None;
                    //rasterizerState.FillMode = FillMode.WireFrame;
                    //Main.graphics.GraphicsDevice.RasterizerState = rasterizerState;

                    var projection = Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, 0, 1);
                    var model = Matrix.CreateTranslation(new Vector3(-Main.screenPosition.X, -Main.screenPosition.Y, 0));

                    //启用即时加载加载Shader
                    var shader = ModContent.Request<Effect>("OdeMod/Effects/Content/Trail", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
                    var MainColor = ModContent.Request<Texture2D>("OdeMod/Images/Effects/heatmap", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
                    var MaskColor = ModContent.Request<Texture2D>("OdeMod/Images/Effects/Extra_189", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
                    var MainShape = ModContent.Request<Texture2D>("OdeMod/Images/Effects/Extra_199", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
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
                    Main.spriteBatch.Begin();
                }

            }
            if (act == 2)
            {
                int width = 60 - (int)(timer - 60) * 2;
                List<CustomVertexInfo> bars = new();
                //顶点离弹幕坐标的距离，也是顶点三角形宽度的一半
                // 把所有的点都生成出来，按照顺序
                for (float i = 1; i <= 60; i++)
                {
                    var normalDir = new Vector2((float)Math.Cos(i / 60f * 6.28318f), (float)Math.Sin(i / 60f * 6.28318f));
                    var color = Color.Lerp(Color.White, Color.Red, 1);
                    bars.Add(new CustomVertexInfo(NPC.position + new Vector2(0f, -80f) + new Vector2(67, 147) + normalDir * (width + (83 - timer) * 25), color, new Vector3(1, 1, 1 - (Math.Abs(timer - 60) / 25))));
                    bars.Add(new CustomVertexInfo(NPC.position + new Vector2(0f, -80f) + new Vector2(67, 147) + normalDir * (-width + (83 - timer) * 25), color, new Vector3(1, 0, 1 - (Math.Abs(timer - 60) / 25))));
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
                    var model = Matrix.CreateTranslation(new Vector3(-Main.screenPosition.X, -Main.screenPosition.Y, 0));

                    //启用即时加载加载Shader
                    var shader = ModContent.Request<Effect>("OdeMod/Effects/Content/Trail", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
                    var MainColor = ModContent.Request<Texture2D>("OdeMod/Images/Effects/heatmap3", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
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
                    Main.spriteBatch.Begin();
                }
            }
            if (act == 3)
            {
                for (int i = 0; i < NPC.oldPos.Length - 4; i++)
                {
                    Vector2 drawPos2 = NPC.oldPos[i] - Main.screenPosition + drawOrigin + new Vector2(0f, NPC.gfxOffY) + new Vector2(0f, -80f);
                    Color color = new Color(246, 162, 255) * ((NPC.oldPos.Length - i - 1) / (float)NPC.oldPos.Length) * 0.8f;
                    Main.spriteBatch.Draw(texture2, drawPos2, new Rectangle(0, NPC.frame.Y, 134, 209), color, NPC.rotation, drawOrigin, 1f, SpriteEffects.None, 0f);
                }
            }

            Main.spriteBatch.Draw(texture, drawPos, new Rectangle(0, NPC.frame.Y, 134, 209), drawColor * ((255f - (float)NPC.alpha) / 255f), NPC.rotation, drawOrigin, 1f, SpriteEffects.None, 0f);

            return false;
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