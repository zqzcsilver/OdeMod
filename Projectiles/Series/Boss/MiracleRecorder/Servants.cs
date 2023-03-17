using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OdeMod.Players;
using OdeMod.Utils;

using System;
using System.Collections.Generic;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.Utils;

namespace OdeMod.Projectiles.Series.Boss.MiracleRecorder
{

    internal class Servants : ModProjectile, IMiracleRecorderProj
    {
        private float easyLerp(float from, float to, float range)
        {
            return (to * range + from * (1f - range));
        }
        public override void SetDefaults()
        {
            Projectile.width = 42;
            Projectile.height = 42;
            Projectile.aiStyle = -1;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.alpha = 255;
            Projectile.timeLeft = 10;
            Projectile.penetrate = -1;
            Projectile.scale = 1f;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
            Projectile.hide = true;
        }

        private float timer = 0;
        private float oldrotate = 0;
        private float newrotate = 0;
        private int line = 0;
        private bool shadow = false;
        private bool repeat = false;
        private float rad2 = 0;
        private float rad3 = 0;
        private float stop = 0;
        private float lerp = 2;
        private float scale0 = 0;
        private float distance0 = 0;
        private float rotation0 = 0;
        private Vector2 cent = Vector2.Zero;
        private Vector2 note = Vector2.Zero;

        Vector2 roundCenter = Vector2.Zero;
        Vector2 destination = Vector2.Zero;

        private int oldlogic = -1;
        private int newlogic = -1;

        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {
            behindNPCs.Add(index);
        }
        public override void AI()
        {
            Projectile.timeLeft = 2;
            Player player = Main.player[Projectile.owner];
            NPC owner = Main.npc[(int)Projectile.ai[1]];
            var miracleRecorder = owner.ModNPC as NPCs.Boss.MiracleRecorder.MiracleRecorder;
            var bossPos = owner.Center;
            //for (int i = 1; i <= servantCount; i++)
            //{
            //    float rad2 = (6.2832f / servantCount) * i;
            //    Projectile.NewProjectile(NPC.GetSource_FromAI(),
            //        NPC.Center + new Vector2((float)Math.Cos(rad2), (float)Math.Sin(rad2)) * (120f + servantCount * 20f), Vector2.Zero, ModContent.ProjectileType<Projectiles.Series.Boss.MiracleRecorder.Servants>(), NPC.damage, 0, player.whoAmI, i);
            //}

            ///初始化
            ///
            newlogic = miracleRecorder.MiracleLogic;
            if (oldlogic != newlogic)
            {
                timer = 0;
            }
            oldlogic = miracleRecorder.MiracleLogic;

            //转阶段机制
            timer++;
            if (timer % 8 == 0)
            {
                if (Projectile.frame < 3)
                    Projectile.frame++;
                else
                    Projectile.frame = 0;
            }

            if (miracleRecorder.MiracleLogic == 0)
            {
                if (timer == 1)
                {
                    shadow = true;
                    rad2 = (6.2832f / miracleRecorder.ServantCount) * Projectile.ai[0];
                }

                if (Projectile.alpha > 0) Projectile.alpha -= 10;
                if (timer > 1 && timer < 60)
                {
                    int num = Dust.NewDust(Projectile.position, 42, 42, ModContent.DustType<Dusts.Dream>(), 0, 0, 100, default, (60 - timer) * 0.03f);
                    Main.dust[num].noGravity = true;
                }
                if (timer > 1 && timer < 180)
                {
                    rad2 += (float)Math.Sin(timer / 55f) * 0.08f;
                    Projectile.Center = owner.Center + new Vector2((float)Math.Cos(rad2), (float)Math.Sin(rad2)) * (120f + miracleRecorder.ServantCount * 15f);
                    Projectile.rotation = rad2;
                }
                if (timer == 180)
                {
                    oldrotate = Projectile.rotation;
                    newrotate = (Projectile.Center - bossPos).ToRotation() - 1.57f;
                }
                if (timer >= 180 && timer < 210)
                {
                    float minus = newrotate - oldrotate;
                    while (minus > 3.14159f)
                        minus -= 6.28318f;

                    while (minus < -3.14159f)
                        minus += 6.28318f;
                    Projectile.rotation += minus * 0.05f;
                    //line = 1;
                }
                if (timer == 215)
                {
                    //line = 0;
                    cent = owner.Center;
                    //Projectile.NewProjectile(Projectile.GetSource_FromAI(), new Vector2((float)Math.Cos(Projectile.rotation + 1.57f), (float)Math.Sin(Projectile.rotation + 1.57f)) * 16 + Projectile.Center, Vector2.Zero, ModContent.ProjectileType<Laser02>(), 0, 0, player.whoAmI, Projectile.rotation);
                }
            }
            if (miracleRecorder.MiracleLogic == 1)
            {
                if (player.GetModPlayer<OdePlayer>().MiracleX == 1)
                {
                    timer = 1;
                }
                if (timer == 1)
                {
                    if (repeat)
                    {
                        rad2 = (6.2832f / miracleRecorder.ServantCount) * Projectile.ai[0];
                        repeat = false;
                    }
                    shadow = true;
                }
                if (timer > 1 && timer < 30)
                {
                    rad2 += 0.08f;
                    var lerPos = Vector2.Lerp(cent, owner.Center, (float)(0.5f * Math.Tanh((double)(timer - 15) / 7.5f) + 0.5f));
                    Projectile.Center = lerPos + new Vector2((float)Math.Cos(rad2), (float)Math.Sin(rad2)) * (120f + miracleRecorder.ServantCount * 15f);
                    Projectile.rotation = rad2;

                }
                //废案
                /*if (timer == 40 + Projectile.ai[0] * 6)
                {
                    Projectile.NewProjectile(Projectile.GetSource_FromAI(), /*new Vector2((float)Math.Cos(Projectile.rotation + 1.57f), (float)Math.Sin(Projectile.rotation + 1.57f)) * 8 + Projectile.Center, Vector2.Normalize(player.Center - Projectile.Center) * 4f, ModContent.ProjectileType<Sparkle>(), 0, 0, player.whoAmI, Projectile.rotation);
                }*/
                if (timer > 30f && timer < 110)
                {
                    shadow = true;
                    rad2 += 0.08f;
                    Projectile.Center = owner.Center + new Vector2((float)Math.Cos(rad2), (float)Math.Sin(rad2)) * (120f + miracleRecorder.ServantCount * 15f);
                    Projectile.rotation = rad2;
                }
                if (timer == 90)
                {
                    cent = owner.Center;
                }
            }

            if (miracleRecorder.MiracleLogic == 2)
            {
                if (player.GetModPlayer<OdePlayer>().MiracleX == 1)
                {
                    timer = 1;
                }
                if (timer >= 1 && timer < 18)
                {
                    shadow = true;
                    rad2 += 0.08f;
                    Projectile.Center = owner.Center + new Vector2((float)Math.Cos(rad2), (float)Math.Sin(rad2)) * (120f + miracleRecorder.ServantCount * 15f);
                    Projectile.rotation = rad2;
                }
                if (timer == 18)
                {
                    note = Projectile.Center;
                    line = 1;

                }
                if (timer >= 18 && timer < 38)
                {
                    var rad4 = owner.rotation + 1.57f;
                    roundCenter = owner.Center + new Vector2((float)Math.Cos(rad4), (float)Math.Sin(rad4)) * (100 + miracleRecorder.ServantCount * 16f);
                    rad3 = (roundCenter - bossPos).ToRotation();
                    if (Projectile.ai[0] <= miracleRecorder.ServantCount / 2)
                    {
                        rad3 -= 0.25f;
                        rad3 += (float)Math.PI;
                        rad3 -= Projectile.ai[0] * 0.25f;
                    }
                    else
                    {
                        rad3 += 0.25f;
                        rad3 += (float)Math.PI;
                        rad3 += (miracleRecorder.ServantCount - Projectile.ai[0] + 1) * 0.25f;
                    }
                    destination = roundCenter + new Vector2((float)Math.Cos(rad3), (float)Math.Sin(rad3)) * (200 + miracleRecorder.ServantCount * 32f);
                    Projectile.Center = Vector2.Lerp(note, destination, (float)(0.5f * Math.Tanh((double)(timer - 28) / 5f) + 0.5f));
                    Projectile.rotation = (owner.Center + new Vector2((float)Math.Cos((roundCenter - owner.Center).ToRotation()), (float)Math.Sin((roundCenter - owner.Center).ToRotation())) * Vector2.Distance(player.Center, owner.Center) - Projectile.Center).ToRotation() - 1.57f;
                }
                if (timer == 38)
                {
                    line = 0;
                    Projectile.NewProjectile(Projectile.GetSource_FromAI(), new Vector2((float)Math.Cos(Projectile.rotation + 1.57f), (float)Math.Sin(Projectile.rotation + 1.57f)) * 16 + Projectile.Center, Vector2.Zero, ModContent.ProjectileType<Laser02>(), Projectile.damage, 0, player.whoAmI, Projectile.rotation);
                }
                if (timer >= 38 && timer < 58)
                {
                    var rad4 = owner.rotation + 1.57f;
                    roundCenter = owner.Center + new Vector2((float)Math.Cos(rad4), (float)Math.Sin(rad4)) * (100 + miracleRecorder.ServantCount * 16f);
                    rad3 = (roundCenter - bossPos).ToRotation();
                    if (Projectile.ai[0] <= miracleRecorder.ServantCount / 2)
                    {
                        rad3 -= 0.25f;
                        rad3 += 3.1415926f;
                        rad3 -= Projectile.ai[0] * 0.25f;
                    }
                    else
                    {
                        rad3 += 0.25f;
                        rad3 += 3.1415926f;
                        rad3 += (miracleRecorder.ServantCount - Projectile.ai[0] + 1) * 0.25f;
                    }
                    destination = roundCenter + new Vector2((float)Math.Cos(rad3), (float)Math.Sin(rad3)) * (200 + miracleRecorder.ServantCount * 32f);
                    Projectile.Center = destination;
                }
                if (timer == 58)
                {
                    note = Projectile.Center;
                }
                if (timer > 58 && timer < 78)
                {
                    rad2 += 0.08f;
                    Vector2 destination = owner.Center + new Vector2((float)Math.Cos(rad2), (float)Math.Sin(rad2)) * (120f + miracleRecorder.ServantCount * 15f);
                    Projectile.Center = Vector2.Lerp(note, destination, (float)(0.5f * Math.Tanh((double)(timer - 68) / 5f) + 0.5f));
                }
                if (timer >= 78)
                {

                    rad2 += 0.08f;
                    Projectile.Center = owner.Center + new Vector2((float)Math.Cos(rad2), (float)Math.Sin(rad2)) * (120f + miracleRecorder.ServantCount * 15f);
                    Projectile.rotation = rad2;
                }
            }
            if (miracleRecorder.MiracleLogic == 3)
            {
                if (player.GetModPlayer<OdePlayer>().MiracleX == 1)
                {
                    cent = player.Center;
                    note = Projectile.Center;
                    oldrotate = Projectile.rotation;
                    timer = 1;
                }
                if (timer == 1)
                {
                    oldrotate = Projectile.rotation;
                    newrotate = (player.Center - Projectile.Center).ToRotation() - 1.5708f;
                    note = Projectile.Center;
                }
                if (timer > 1 && timer <= 20)
                {
                    rad2 += 0.08f;
                    line = 1;
                    destination = player.Center + new Vector2((float)Math.Cos(rad2), (float)Math.Sin(rad2)) * (200f + miracleRecorder.ServantCount * 15f);
                    Projectile.Center = Vector2.Lerp(note, destination, (float)(0.5f * Math.Tanh((double)(timer - 14) / 5f) + 0.5f));
                    newrotate = rad2 + 1.5708f;
                    if (timer == 20)
                    {
                        Projectile.Center = destination;
                        Projectile.rotation = newrotate;
                    }

                    Projectile.rotation = oldrotate * ((20 - timer) / 20f) + newrotate * (timer / 20f);
                }

                if (timer == 38)
                {
                    line = 0;
                    Projectile.NewProjectile(Projectile.GetSource_FromAI(), new Vector2((float)Math.Cos(Projectile.rotation + 1.5708f), (float)Math.Sin(Projectile.rotation + 1.5708f)) * 16 + Projectile.Center, Vector2.Zero, ModContent.ProjectileType<Laser02>(), Projectile.damage, 0, player.whoAmI, Projectile.rotation);
                }
                if (timer == 58)
                {
                    note = Projectile.Center;
                    oldrotate = Projectile.rotation;
                }
                if (timer > 58 && timer < 78)
                {
                    newrotate = rad2;
                    rad2 += 0.08f;
                    Vector2 destination = owner.Center + new Vector2((float)Math.Cos(rad2), (float)Math.Sin(rad2)) * (120f + miracleRecorder.ServantCount * 15f);
                    Projectile.Center = Vector2.Lerp(note, destination, (float)(0.5f * Math.Tanh((double)(timer - 68) / 5f) + 0.5f));
                    Projectile.rotation = oldrotate * ((77 - timer) / 19f) + newrotate * (timer / 19f);
                }
                if (timer >= 78)
                {
                    rad2 += 0.08f;
                    Projectile.Center = owner.Center + new Vector2((float)Math.Cos(rad2), (float)Math.Sin(rad2)) * (120f + miracleRecorder.ServantCount * 15f);
                    Projectile.rotation = rad2;
                }
                miracleRecorder.CountSort = 0;
            }
            if (miracleRecorder.MiracleLogic == 4)
            {
                if (timer == 1)
                {
                    oldrotate = Projectile.rotation;
                    newrotate = (float)(Main.rand.Next(127, 188)) * 0.01f - 1.57f;
                    note = Projectile.Center;
                    destination = new Vector2((float)Main.screenWidth / (miracleRecorder.ServantCount + 1) * (Projectile.ai[0]) + Main.screenPosition.X + Main.rand.Next(-25, 26), player.Center.Y - (450f + Main.rand.Next(-50, 51)));
                }
                if (timer > 1 && timer <= 30)
                {
                    Projectile.Center = Vector2.Lerp(note, destination, (float)(0.5f * Math.Tanh((double)(timer - 15) / 3f) + 0.5f));
                    Projectile.rotation = oldrotate * ((30 - timer) / 30f) + newrotate * (timer / 30f);
                }
                if (timer == 30) line = 1;
                if (timer == 50)
                {
                    line = 0;
                    Projectile.NewProjectile(Projectile.GetSource_FromAI(), new Vector2((float)Math.Cos(Projectile.rotation + 1.5708f), (float)Math.Sin(Projectile.rotation + 1.5708f)) * 16 + Projectile.Center, Vector2.Zero, ModContent.ProjectileType<Laser02>(), Projectile.damage, 0, player.whoAmI, Projectile.rotation);
                }
                if (timer > 70) timer = 0;
            }
            if (miracleRecorder.MiracleLogic == 5)
            {
                if (timer == 1)
                {
                    note = Projectile.Center;

                }
                if (timer > 1 && timer <= 60)
                {
                    rad2 += 0.08f;
                    destination = owner.Center + new Vector2((float)Math.Cos(rad2), (float)Math.Sin(rad2)) * (120f + miracleRecorder.ServantCount * 15f);
                    Projectile.Center = Vector2.Lerp(note, destination, (float)(0.5f * Math.Tanh((double)(timer - 30) / 10f) + 0.5f));
                }
                if (timer > 60 && timer <= 80)
                {
                    Projectile.rotation = rad2;
                    rad2 += 0.08f;
                    Projectile.Center = bossPos + new Vector2((float)Math.Cos(rad2), (float)Math.Sin(rad2)) * (120f + miracleRecorder.ServantCount * 15f);
                }

                if (timer > 80 && timer <= 180)
                {
                    rad2 += 0.08f;
                    Projectile.rotation = rad2;
                    Projectile.Center = bossPos + new Vector2((float)Math.Cos(rad2), (float)Math.Sin(rad2)) * ((120f + miracleRecorder.ServantCount * 15f) + (float)Math.Sin((timer - 80) / 100f * 3.1416f) * 320f);
                }
                if (timer > 180 && timer <= 190)
                {
                    rad2 += 0.08f - (timer - 180) / 10f * 0.08f;
                    Projectile.rotation = rad2;
                    Projectile.Center = bossPos + new Vector2((float)Math.Cos(rad2), (float)Math.Sin(rad2)) * ((120f + miracleRecorder.ServantCount * 15f));
                }

                if (timer == 200)
                {
                    for (int j = 0; j < 60; j++)
                    {
                        int randomDis = 300;
                        Vector2 dustpos = Projectile.Center + randomDis * Main.rand.NextVector2Unit();
                        var dust2 = Dust.NewDustDirect(dustpos, 1, 1,
                            DustID.PinkTorch, 0, 0, 0, Color.White, 2f);
                        dust2.velocity = (Projectile.Center - dustpos) / 40f;
                        dust2.noGravity = true;
                    }
                    Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<Projectiles.Series.Boss.MiracleRecorder.DamageCircle3>(), Projectile.damage, 0, player.whoAmI);
                }
                cent = owner.Center;
            }
            if (miracleRecorder.MiracleLogic == 114514)
            {
                if (timer == 1)
                {
                    Projectile.velocity *= 0f;
                }
                if (timer > 40 && timer <= 80)
                {
                    scale0 += 0.025f;
                }
                if(timer>120&&timer<=160)
                {
                    scale0 -= 0.025f;
                }
                if (timer > 30) Projectile.alpha += 5;

                    int num = Dust.NewDust(Projectile.position + new Vector2(10, 30), 20, 20, ModContent.DustType<Dusts.Dream>(), 0f, 0f, 0, Color.White, scale0);
                Main.dust[num].noGravity = true;
                Main.dust[num].velocity.Y -= 4f;
                Main.dust[num].velocity.X *= 0.6f;
                if(timer==120)
                {
                    for (int i = 1; i < 40; i++)
                    {
                        var dust2 = Dust.NewDustDirect(Projectile.Center, 1, 1, DustID.PinkTorch, 0, 0, 0, Color.White, 2.5f);
                        dust2.velocity = 8 * Main.rand.NextVector2Unit();
                        dust2.noGravity = true;
                    }
                    Projectile.Kill();
                }
            }
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255 - Projectile.alpha, 255 - Projectile.alpha, 255 - Projectile.alpha, 255 - Projectile.alpha);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            if (line == 1)
            {
                Color color1 = new Color(255, 0, 241, 0f);
                Color color2 = new Color(255, 0, 241, 0.4f);
                Color color4;
                if (timer < 30f)
                    color4 = Color.Lerp(color1, color2, timer / 30f);
                else
                    color4 = Color.Lerp(color1, color2, 1);
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.Transform);
                DrawLine(Main.spriteBatch, Projectile.Center, new Vector2((float)Math.Cos(Projectile.rotation + 1.57f), (float)Math.Sin(Projectile.rotation + 1.57f)) * 5000 + Projectile.Center, color4, color4, 3f);
            }

            if (shadow)
            {
                List<CustomVertexInfo> bars = new();

                int width = 18;
                var normalDir = Projectile.position - Projectile.oldPos[1];
                normalDir = Vector2.Normalize(new Vector2(-normalDir.Y, normalDir.X));
                var factor = 0.1f / (float)Projectile.oldPos.Length;
                var color = Color.Lerp(Color.White, Color.Red, factor);
                var w = MathHelper.Lerp(1f, 0f, factor);
                bars.Add(new CustomVertexInfo(Projectile.position + 0.5f * new Vector2(Projectile.width, Projectile.height) + normalDir * width, color, new Vector3(factor, 1, w * (255 - Projectile.alpha) / 255f)));
                bars.Add(new CustomVertexInfo(Projectile.position + 0.5f * new Vector2(Projectile.width, Projectile.height) + normalDir * -width, color, new Vector3(factor, 0, w * (255 - Projectile.alpha) / 255f)));

                for (int i = 1; i < Projectile.oldPos.Length; ++i)
                {
                    width -= 3;
                    if (Projectile.oldPos[i] == Vector2.Zero) break;
                    normalDir = Projectile.oldPos[i - 1] - Projectile.oldPos[i];
                    normalDir = Vector2.Normalize(new Vector2(-normalDir.Y, normalDir.X));
                    factor = i / (float)Projectile.oldPos.Length;
                    color = Color.Lerp(Color.White, Color.Red, factor);
                    w = MathHelper.Lerp(1f, 0f, factor);
                    bars.Add(new CustomVertexInfo(Projectile.oldPos[i] + 0.5f * new Vector2(Projectile.width, Projectile.height) + normalDir * width, color, new Vector3(factor, 1, w * (255 - Projectile.alpha) / 255f)));
                    bars.Add(new CustomVertexInfo(Projectile.oldPos[i] + 0.5f * new Vector2(Projectile.width, Projectile.height) + normalDir * -width, color, new Vector3(factor, 0, w * (255 - Projectile.alpha) / 255f)));
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
                    var MaskColor2 = ModContent.Request<Texture2D>("OdeMod/Images/Effects/Extra_189", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
                    var MainShape2 = ModContent.Request<Texture2D>("OdeMod/Images/Effects/Extra_200", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
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
                    Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.Transform);
                }
            }

            Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;
            Vector2 drawOrigin = new Vector2(42 * 0.5f, 56 * 0.7f);
            Vector2 drawPos = Projectile.position - Main.screenPosition + drawOrigin + new Vector2(0f, -16f);
            //绘制本体
            Main.spriteBatch.Draw(texture, drawPos,
                new Rectangle(0, Projectile.frame * 56, 42, 56),
                lightColor * ((255f - (float)Projectile.alpha) / 255f), Projectile.rotation,
                drawOrigin, 1f, SpriteEffects.None, 0f);
            return false;
        }
    }
}