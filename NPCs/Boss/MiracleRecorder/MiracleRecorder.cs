using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OdeMod.Players;
using OdeMod.Utils;

using System;
using System.Collections.Generic;

using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.ModLoader;

using static Terraria.Utils;

namespace OdeMod.NPCs.Boss.MiracleRecorder
{
    public class MiracleRecorder : ModNPC, IBoss
    {
        /*Boss设定详述：
         
        开场：boss逐渐出现并看向玩家。一段时间后震屏并释放弹幕，此时boss无敌解除。

         一阶段：
        冲刺：boss按照三叶玫瑰线轨迹冲刺并释放弹幕。

        放球：boss围绕玩家释放阻止玩家走位并追踪玩家的球，一段时间后消失
        
        激光：短暂蓄力后瞄准玩家释放瞬时激光，有引导线做提示。
        
        聚集：boss静止并在周围形成反弹弹幕的力场，一段时间后力场爆炸（造成伤害）并释放尖刺。
        前两次聚集会使得第二阶段的僚机数量增加2，接下来的每一次聚集都会提升boss的攻击力。

        一二阶段切换时boss无敌，并伴随落星和震屏，boss的主色调由粉色变为金色
        切换结束时，boss周围生成数量由聚集次数决定的僚机（无法被摧毁）
        僚机将伴随boss本体做一阶段的变招。

        二阶段：
        冲刺：冲刺频率加快，同时冲刺尾迹留下与一阶段放球时类似的随机连线，此时僚机围绕boss本体圆周运动并阻挡可能伤害boss的弹幕

        放球：改为释放僚机，每次释放一个僚机，僚机将做冲刺运动，并朝玩家释放弹幕。

        激光：僚机按照圆弧排列，与boss本体同时释放激光。
         
        聚集：改为回复生命值（但不超过一半生命值），僚机围绕boss做变速圆周运动同时发射激光。

        增加技能：次元斩：大型瞬时冲刺。
         */
        private enum NPCState
        {
            /// <summary>
            /// 出场
            /// </summary>
            Entrance,

            /// <summary>
            /// 游荡
            /// </summary>
            Wandering,

            /// <summary>
            /// 冲刺
            /// </summary>
            Dash,

            /// <summary>
            /// 发射激光
            /// </summary>
            EmitLaser,

            /// <summary>
            /// 聚集
            /// </summary>
            Focus,

            /// <summary>
            /// 死亡
            /// </summary>
            Dead,

            /// <summary>
            /// 换阶段
            /// </summary>
            ChangeRank
        }

        private Dictionary<NPCState, Action<Player>> _npcLogic;

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 4;
        }

        public override void SetDefaults()
        {
            NPC.lifeMax = 30000;
            NPC.damage = 60;
            NPC.defense = 20;
            NPC.knockBackResist = 0f;
            NPC.width = 134;
            NPC.height = 134;
            NPC.aiStyle = -1;
            NPC.boss = true;
            NPC.alpha = 255;
            NPC.noGravity = true;
            NPC.HitSound = SoundID.NPCHit1;
            Main.npcFrameCount[NPC.type] = 8;
            NPC.DeathSound = SoundID.NPCDeath6;
            NPC.value = Item.buyPrice(0, 15, 0, 0);
            NPC.noTileCollide = true;
            NPCID.Sets.TrailingMode[NPC.type] = 0;
            NPCID.Sets.TrailCacheLength[NPC.type] = 8;

            _npcLogic = new Dictionary<NPCState, Action<Player>>
            {
                { NPCState.Entrance, entrance },
                { NPCState.Wandering, wandering },
                { NPCState.Dash, dash },
                { NPCState.EmitLaser, emitLaser },
                { NPCState.Focus, focus },
                { NPCState.Dead, dead },
                { NPCState.ChangeRank, changerank }
            };
        }
        private int rank = 1;
        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter++;

            if (rank == 1)
            {
                if (NPC.frameCounter % 8 == 0)
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
            if (rank == 2)
            {
                if (NPC.frameCounter % 8 == 0)
                {
                    if (NPC.frame.Y <= frameHeight * 6)
                    {
                        NPC.frame.Y += frameHeight;
                    }
                    else
                    {
                        NPC.frame.Y = 4 * frameHeight;
                    }
                }
            }
            //Main.NewText(NPC.frame.Y / frameHeight + 1);
        }

        private NPCState state = NPCState.Entrance;
        private int servantCount = 2;
        private int randomFactor = 0;

        private float[] rads = new float[3] { 0.5236f, 2.618f, 4.7116f };//冲刺用的角度数组
        private float[] rot = new float[10];
        private int act = 0;//控制不同行为的draw
        private int line = 0;//是否绘制瞄准线
        private float timer = 0;//计时器
        private Vector2 plrCenter = Vector2.Zero;//定时记录玩家位置
        private float distance = 0;//玩家距离
        private int ok = -1;//冲刺用1
        private float ok2 = 0;//冲刺用2
        private Vector2 noticeVec = Vector2.Zero;
        private int count = 0;//冲刺次数
        private int count2 = 0;//召唤球球数量
        private float rando = Main.rand.Next(-10, 20) * 0.05f;//随机偏移量
        private Vector2 dir = Vector2.Zero;
        private bool extra = false;

        private float oldrotate = 0;
        private float newrotate = 0;
        private int times = 0;

        /// <summary>
        /// 出场
        /// </summary>
        /// <param name="player"></param>
        private void entrance(Player player)
        {
            if (timer == 1)
            {
                NPC.dontTakeDamage = true;
                NPC.velocity *= 0f;
                NPC.dontTakeDamage = true;
            }
            if (timer > 120 && timer < 250)
            {
                NPC.alpha -= 4;
                if (NPC.alpha < 0) NPC.alpha = 0;
            }
            if (timer > 1 && timer < 250)
            {
                act = 0;
                if (timer % 8 == 0)
                {
                    float randomRad = Main.rand.Next(0, 629);
                    int randomDis = Main.rand.Next(260, 400);
                    Projectile.NewProjectile(NPC.GetSource_FromAI(),
                        NPC.Center + randomDis * new Vector2((float)Math.Cos(randomRad / 100f), (float)Math.Sin(randomRad / 100f)),
                        Vector2.Zero, ModContent.ProjectileType<Projectiles.Series.Boss.MiracleRecorder.Space>(), 0, 0,
                        player.whoAmI, NPC.Center.X, NPC.Center.Y);

                    for (int i = 0; i < 3; i++)
                    {
                        Vector2 dustpos = NPC.Center + randomDis * Main.rand.NextVector2Unit();
                        var dust2 = Dust.NewDustDirect(dustpos, 1, 1,
                            DustID.PinkTorch, 0, 0, 0, Color.White, 2.5f);
                        dust2.velocity = (NPC.Center - dustpos) / 20f;
                        dust2.noGravity = true;

                        dustpos = NPC.Center + randomDis * Main.rand.NextVector2Unit();

                        var dust3 = Dust.NewDustDirect(dustpos, 1, 1,
                            ModContent.DustType<Dusts.Dream>(), 0, 0, 0, Color.White, 1f);
                        dust3.velocity = (NPC.Center - dustpos) / 20f;
                        dust3.noGravity = true;
                    }
                }
                if (timer % 20 == 0)
                {
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, Vector2.Zero,
                        ModContent.ProjectileType<Projectiles.Series.Boss.MiracleRecorder.Circle0>(), 0, 0, player.whoAmI);
                }
            }
            if (timer > 100)
            {
                Vector2 witness = new Vector2(player.Center.X - NPC.Center.X, player.Center.Y - NPC.Center.Y);
                witness.Normalize();
                float lerp = (witness.ToRotation() - NPC.rotation - 1.57f);
                while (lerp > 3.14159f)
                    lerp -= 6.28318f;

                while (lerp < -3.14159f)
                    lerp += 6.28318f;

                if (Math.Abs(lerp) < 0.01f) lerp = 0;
                NPC.rotation += lerp * 0.02f;
            }
            if (timer == 260)
            {
                NPC.dontTakeDamage = false;
                for (int i = 1; i < 40; i++)
                {
                    var dust2 = Dust.NewDustDirect(NPC.Center, 1, 1, DustID.PinkTorch
                        , 0, 0, 0, Color.White, 2f);
                    dust2.velocity = 12 * Main.rand.NextVector2Unit();
                    dust2.noGravity = true;
                }
                float demo = 1 + Vector2.DistanceSquared(Main.player[Main.myPlayer].Center, player.Center) / 420000;
                player.GetModPlayer<OdePlayer>().ShakeInt = Math.Max(player.GetModPlayer<OdePlayer>().ShakeInt, (int)(45 / demo));
                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, Vector2.Zero,
                    ModContent.ProjectileType<Projectiles.Series.Boss.MiracleRecorder.Circle1>(), 0, 0, player.whoAmI);

                for (int i = 1; i <= 15; i++)
                {
                    float rad2 = 0.41888f * i;
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center,
                        new Vector2((float)Math.Cos(rad2), (float)Math.Sin(rad2)) * 15f,
                        ModContent.ProjectileType<Projectiles.Series.Boss.MiracleRecorder.Holyproj>(),
                        NPC.damage, 0, player.whoAmI);
                }
                OdeMod.ScreenShaderDataManager["OdeMod:MiracleRecorder"].Visible = true;
            }
            if (timer >= 300)
            {
                act = 1;
                timer = 0;
                NPC.dontTakeDamage = false;

                if (NPC.life < 0.5 * NPC.lifeMax)
                {
                    state = NPCState.ChangeRank;
                }
                else
                {
                    randomFactor = Main.rand.Next(4, 7);
                    state = NPCState.Wandering;
                }
            }
        }

        /// <summary>
        /// 游荡
        /// </summary>
        /// <param name="player"></param>
        private void wandering(Player player)
        {
            ParticleOrchestraSettings settings;
            if (timer == 1)
            {
                NPC.alpha = 255;
                ok++;
                ok2 = 0;
                act = 0;
                plrCenter = player.Center;
                distance = 400f;
                NPC.Center = plrCenter + new Vector2((float)Math.Cos(rads[ok]), (float)Math.Sin(rads[ok])) * distance;
                for (int i = 1; i < 40; i++)
                {
                    var dust2 = Dust.NewDustDirect(NPC.Center, 1, 1, DustID.PinkTorch, 0, 0, 0, Color.White, 2.5f);
                    dust2.velocity = 10 * Main.rand.NextVector2Unit();
                    dust2.noGravity = true;
                }
                for (int i = 1; i < 15; i++)
                {
                    Vector2 value = Vector2.UnitX.RotatedBy(Main.rand.NextFloat() * ((float)Math.PI * 2f) + (float)Math.PI / 2f) * 13;
                    Vector2 posin = NPC.position + new Vector2(0f, -80f) + new Vector2(67, 147) + value;
                    settings = new ParticleOrchestraSettings
                    {
                        PositionInWorld = posin,//位置
                        MovementVector = 15 * Main.rand.NextVector2Unit()
                    };
                    ParticleOrchestrator.RequestParticleSpawn(clientOnly: true, ParticleOrchestraType.PrincessWeapon, settings, 255);
                }
                for (int i = 1; i <= 6; i++)
                {
                    float rad2 = 1.0472f * i + (player.Center - NPC.Center).ToRotation();
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, new Vector2((float)Math.Cos(rad2), (float)Math.Sin(rad2)) * 12f, ModContent.ProjectileType<Projectiles.Series.Boss.MiracleRecorder.Holyproj>(), NPC.damage, 0, player.whoAmI);
                }
                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, Vector2.Zero,
                    ModContent.ProjectileType<Projectiles.Series.Boss.MiracleRecorder.Circle2>(), 0, 0, player.whoAmI);
                noticeVec = NPC.Center;
            }
            if (timer > 1 && timer <= 10)
            {
                NPC.alpha = 255;
                act = 0;
                ok2 += 0.027f * (float)Math.Sin(1 / 60 * Math.PI);
                NPC.Center = plrCenter + new Vector2((float)(Math.Sin(3 * (rads[ok] + ok2)) * Math.Cos(rads[ok] + ok2)), (float)(Math.Sin(3 * (rads[ok] + ok2)) * Math.Sin(rads[ok] + ok2))) * distance;
                NPC.rotation = (NPC.Center - noticeVec).ToRotation() - 1.57f;
            }
            if (timer > 10 && timer < 60)
            {
                ok2 += 0.029f * (float)Math.Sin((timer - 10) / 50 * Math.PI);
                NPC.Center = plrCenter + new Vector2((float)(Math.Sin(3 * (rads[ok] + ok2)) * Math.Cos(rads[ok] + ok2)), (float)(Math.Sin(3 * (rads[ok] + ok2)) * Math.Sin(rads[ok] + ok2))) * distance;
                NPC.rotation = (NPC.Center - noticeVec).ToRotation() - 1.57f;
                noticeVec = NPC.Center;
                NPC.alpha -= 20;
                if (NPC.alpha <= 0) NPC.alpha = 0;
            }
            if (timer > 25 && timer < 60)
            {
                act = 1;
            }
            if (timer >= 60 && timer < 80)
            {
                act = 0;

                if (count <= randomFactor - 2)
                {
                    int ok3;
                    NPC.velocity = new Vector2((float)Math.Cos(NPC.rotation + 1.57f), (float)Math.Sin(NPC.rotation + 1.57f)) * (80 - timer) * 0.1f;
                    NPC.alpha += 8;
                    if (ok == 0 || ok == 1) ok3 = ok + 1;
                    else ok3 = 0;
                    for (float i = 0; i < 6; i++)
                    {
                        int num = Dust.NewDust(player.Center, 1, 1, ModContent.DustType<Dusts.Dream>(), 0, 0, 120,
                            Color.White, 0f + ((timer - 52) / 12f));

                        float rad = new Vector2((float)Math.Cos(i * 6.28 / 6) * 80f, (float)Math.Sin(i * 6.28 / 6) * 80f).ToRotation();

                        Main.dust[num].position = player.Center + new Vector2((float)Math.Cos(rads[ok3]), (float)Math.Sin(rads[ok3])) * distance +
                            new Vector2(

                                (float)(Math.Cos(i * 6.28 / 6) * 80 + Math.Cos(rad + ((float)(timer - 52) / 30f * 3.14f)) * 80),

                                (float)(Math.Sin(i * 6.28 / 6) * 80 + Math.Sin(rad + ((float)(timer - 52) / 30f * 3.14f)) * 80));

                        Main.dust[num].velocity *= 0.1f;
                        Main.dust[num].noGravity = true;
                    }
                    for (int i = 0; i < 3; i++)
                    {
                        Vector2 dustpos = NPC.Center + 80 * Main.rand.NextVector2Unit();
                        var dust2 = Dust.NewDustDirect(dustpos, 1, 1, DustID.PinkTorch, 0, 0, 0, Color.White, 2.5f);
                        dust2.velocity = (NPC.Center - dustpos) / 10f;
                        dust2.noGravity = true;
                    }
                }
                else
                {
                    timer = 80;
                }
            }
            if (timer == 80)
            {
                NPC.alpha = 255;
                for (int i = 1; i < 40; i++)
                {
                    var dust2 = Dust.NewDustDirect(NPC.Center, 1, 1, DustID.PinkTorch, 0, 0, 0, Color.White, 2.5f);
                    dust2.velocity = 10 * Main.rand.NextVector2Unit();
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
                if (count == randomFactor)
                {
                    if (NPC.life < 0.5 * NPC.lifeMax)
                    {
                        state = NPCState.ChangeRank;
                    }
                    else
                    {

                        state = NPCState.EmitLaser;
                        randomFactor = Main.rand.Next(1, 3);
                        extra = false;
                    }


                    count = 0;
                    ok = -1;
                    ok2 = 0;
                    NPC.alpha = 0;
                }
            }
        }

        /// <summary>
        /// 冲刺
        /// </summary>
        /// <param name="player"></param>
        private void dash(Player player)
        {
            ParticleOrchestraSettings settings;
            if (timer == 1)
            {
                noticeVec = NPC.Center;

                rando = 0.6f;

                plrCenter = player.Center;
                dir = player.Center - NPC.Center;
                dir.Normalize();
                if (count2 == 0)
                {
                    dir = new Vector2((float)Math.Cos(NPC.rotation + 1.57f), (float)Math.Sin(NPC.rotation + 1.57f));
                }
                else
                {
                    dir = new Vector2((float)Math.Cos(dir.ToRotation() - rando), (float)Math.Sin(dir.ToRotation() - rando));
                }

                newrotate = dir.ToRotation() - 1.57f;
                oldrotate = NPC.rotation;
                noticeVec = NPC.Center;
            }
            if (timer > 1 && timer < 20)
            {
                /*if (oldrotate <= 0 && newrotate >= 0)
                {
                    oldrotate += 6.28318f;
                }*/
                float minus = newrotate - oldrotate;
                while (minus > 3.14159f)
                    minus -= 6.28318f;

                while (minus < -3.14159f)
                    minus += 6.28318f;
                NPC.rotation += minus * 0.05f;
                NPC.velocity += dir;
                noticeVec = NPC.Center;
            }
            if (timer == 20)
            {
                act = 1;
                NPC.velocity = dir * 32f;
                NPC.rotation = newrotate;
            }
            if (timer >= 20 && timer <= 40)
            {
                act = 3;
                NPC.velocity *= 0.92f;
                NPC.velocity -= new Vector2(NPC.velocity.Y, -NPC.velocity.X) * 0.015f;
                NPC.rotation = (NPC.Center - noticeVec).ToRotation() - 1.57f;
                noticeVec = NPC.Center;
            }
            if (timer > 40)
            {
                count2++;
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
                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<Projectiles.Series.Boss.MiracleRecorder.Spark>(), 0, 0, player.whoAmI, count2, times);
                timer = 0;
                if (count2 >= randomFactor)
                {
                    NPC.alpha = 0;
                    if (NPC.life < 0.5 * NPC.lifeMax)
                    {
                        state = NPCState.ChangeRank;
                    }
                    else
                    {
                        state = NPCState.EmitLaser;
                        extra = true;
                        randomFactor = Main.rand.Next(1, 3);

                    }

                    ok2 = 0;
                    count2 = 0;
                    rando = 0;
                    times++;
                }
            }
        }

        /// <summary>
        /// 发射激光
        /// </summary>
        /// <param name="player"></param>
        private void emitLaser(Player player)
        {
            if (timer == 1)
            {
                line = 1;
                act = 1;
            }
            if (timer > 1 && timer < 25)
            {
                Vector2 witness = new Vector2(player.Center.X - NPC.Center.X, player.Center.Y - NPC.Center.Y);
                witness.Normalize();
                float lerp = (witness.ToRotation() - NPC.rotation - 1.57f);
                while (lerp > 3.14159f)
                    lerp -= 6.28318f;

                while (lerp < -3.14159f)
                    lerp += 6.28318f;
                NPC.velocity *= 0.85f;
                NPC.velocity += new Vector2((float)Math.Cos(NPC.rotation + 1.57f), (float)Math.Sin(NPC.rotation + 1.57f)) * 0.2f;
                if (Math.Abs(lerp) < 0.01f) lerp = 0;
                NPC.rotation += lerp * (timer / 60f);
            }
            if (timer >= 25 && timer < 38)
            {

            }
            if (timer == 38)
            {
                var r = NPC.rotation + 1.57f;
                for (int i = 1; i <= 60; i++)
                {
                    float r2 = r + (Main.rand.Next(-10, 11) * 0.05f);
                    Vector2 shootVel = r2.ToRotationVector2() * Main.rand.Next(40, 200) * 0.2f;
                    int num = Dust.NewDust(new Vector2((float)Math.Cos(NPC.rotation + 1.57f), (float)Math.Sin(NPC.rotation + 1.57f)) * 45 + NPC.Center, 1, 1, DustID.PinkTorch, 0, 0, 100, default, 1.5f);

                    Main.dust[num].velocity = shootVel;

                    Main.dust[num].noGravity = true;
                    Main.dust[num].scale = Main.rand.Next(15, 25) * 0.1f;
                }
                line = 0;
                Vector2 tor = player.Center - NPC.Center;
                NPC.velocity += new Vector2((float)Math.Cos(NPC.rotation + 1.57f), (float)Math.Sin(NPC.rotation + 1.57f)) * -10f;
                float demo = 1 + Vector2.DistanceSquared(Main.player[Main.myPlayer].Center, player.Center) / 420000;
                player.GetModPlayer<OdePlayer>().ShakeInt = Math.Max(player.GetModPlayer<OdePlayer>().ShakeInt, (int)(30 / demo));
                Projectile.NewProjectile(NPC.GetSource_FromAI(), new Vector2((float)Math.Cos(NPC.rotation + 1.57f), (float)Math.Sin(NPC.rotation + 1.57f)) * 45 + NPC.Center, Vector2.Zero, ModContent.ProjectileType<Projectiles.Series.Boss.MiracleRecorder.Laser01>(), 0, 0, player.whoAmI, NPC.rotation);
                distance = Vector2.Distance(NPC.Center, player.Center);
            }
            if (timer > 38 && timer < 58)
            {
                NPC.velocity *= 0.5f;
            }
            if (timer == 58)
            {
                if (count2 == randomFactor)
                {
                    count2 = 0;
                    NPC.alpha = 0;
                    NPC.velocity *= 0f;

                    if (NPC.life < 0.5 * NPC.lifeMax)
                    {
                        state = NPCState.ChangeRank;
                    }
                    else
                    {
                        if(!extra)
                        {
                            state = NPCState.Dash;
                            randomFactor = Main.rand.Next(4, 6);
                        }
                        else
                        {
                            state = NPCState.Focus;
                        }
                    }

                    count = 0;
                    ok = -1;
                    ok2 = 0;
                    timer = 0;
                }
            }
            if (timer >= 58 && timer < 82)
            {
                Vector2 witness = new Vector2(player.Center.X - NPC.Center.X, player.Center.Y - NPC.Center.Y);
                witness.Normalize();
                float lerp = (witness.ToRotation() - NPC.rotation - 1.57f);
                while (lerp > 3.14159f)
                    lerp -= 6.28318f;

                while (lerp < -3.14159f)
                    lerp += 6.28318f;
                if (Math.Abs(lerp) < 0.01f) lerp = 0;
                NPC.rotation += lerp * (timer / 80f);
                NPC.velocity = new Vector2(-(float)Math.Sin(NPC.rotation + 1.57f), (float)Math.Cos(NPC.rotation + 1.57f)) * (18f - Math.Abs(timer - 77)) * 1.5f;
                NPC.velocity += new Vector2((float)Math.Cos(NPC.rotation + 1.57f), (float)Math.Sin(NPC.rotation + 1.57f));
            }
            if (timer >= 82)
            {
                count2++;
                timer = 0;
            }
        }

        /// <summary>
        /// 死亡
        /// </summary>
        /// <param name="player"></param>
        private void focus(Player player)
        {
            if (timer >= 1 && timer < 20)
            {
                NPC.velocity *= 0f;
                act = 4;
                if (timer == 1)
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<Projectiles.Series.Boss.MiracleRecorder.DamageCircle>(), NPC.damage, 0, player.whoAmI);
            }
            if (timer >= 20 && timer < 200)
            {
                act = 1;
                float randomRad = Main.rand.Next(0, 629);
                int randomDis = Main.rand.Next(260, 400);
                if (timer % 2 == 0)
                {
                    Vector2 dustpos = NPC.Center + randomDis * Main.rand.NextVector2Unit();
                    var dust2 = Dust.NewDustDirect(dustpos, 1, 1,
                        DustID.PinkTorch, 0, 0, 0, Color.White, 2.5f);
                    dust2.velocity = (NPC.Center - dustpos) / 20f;
                    dust2.noGravity = true;

                    dustpos = NPC.Center + randomDis * Main.rand.NextVector2Unit();

                    var dust3 = Dust.NewDustDirect(dustpos, 1, 1,
                        ModContent.DustType<Dusts.Dream>(), 0, 0, 0, Color.White, 1f);
                    dust3.velocity = (NPC.Center - dustpos) / 20f;
                    dust3.noGravity = true;
                }

            }
            if (timer == 180)
            {
                for (int i = 0; i < 40; i++)
                {
                    var dust2 = Dust.NewDustDirect(NPC.Center, 1, 1, DustID.PinkTorch, 0, 0, 0, Color.White, 2.5f);
                    dust2.position = NPC.Center + 160 * Main.rand.NextVector2Unit();
                    dust2.velocity *= 2f;
                    dust2.noGravity = true;

                    var dust3 = Dust.NewDustDirect(NPC.Center, 1, 1,
                        ModContent.DustType<Dusts.Dream>(), 0, 0, 0, Color.White, 1f);
                    dust3.position = NPC.Center + 160 * Main.rand.NextVector2Unit();

                    dust3.noGravity = true;
                }
                for (int i = 1; i <= 6; i++)
                {
                    float rad2 = 1.0472f * i + (player.Center - NPC.Center).ToRotation();
                    Projectile.NewProjectile(NPC.GetSource_FromAI(),
                        NPC.Center + new Vector2((float)Math.Cos(rad2), (float)Math.Sin(rad2)) * 100f, new Vector2((float)Math.Cos(rad2), (float)Math.Sin(rad2)) * 6f, ModContent.ProjectileType<Projectiles.Series.Boss.MiracleRecorder.Pin>(), NPC.damage, 0, player.whoAmI);
                }
            }
            if (timer == 200)
            {

                count2 = 0;
                NPC.alpha = 0;
                count = 0;
                ok = -1;
                ok2 = 0;
                if (servantCount < 6)
                {
                    servantCount += 2;
                }
                else
                {
                    NPC.damage += 10;
                }
                timer = 0;
                if (NPC.life < 0.5 * NPC.lifeMax)
                {
                    state = NPCState.ChangeRank;
                }
                else
                {
                        state = NPCState.Wandering;
                        randomFactor = Main.rand.Next(4, 7);
                }

            }
        }
        float lerp = 0;
        private void changerank(Player player)
        {
            if (timer == 1)
            {
                act = 5;
                NPC.alpha = 0;
                NPC.velocity *= 0f;
                NPC.dontTakeDamage = true;
            }
            if (timer >= 1 && timer < 480)
            {

                if (timer == 1)
                {
                    Vector2 witness = new Vector2(0, -1f);
                    witness.Normalize();
                    lerp = (witness.ToRotation() - NPC.rotation - 1.57f);
                    while (lerp > 3.14159f)
                        lerp -= 6.28318f;

                    while (lerp < -3.14159f)
                        lerp += 6.28318f;

                    if (Math.Abs(lerp) < 0.01f) lerp = 0;
                }

                if (timer <= 100)
                    NPC.rotation += lerp * 0.01f;

                if (timer % 20 == 0)
                {
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, Vector2.Zero,
                        ModContent.ProjectileType<Projectiles.Series.Boss.MiracleRecorder.Circle3>(), 0, 0, player.whoAmI);
                }
                if (timer % 9 == 0)
                {
                    float ceilingX = Main.rand.Next(0, Main.screenWidth);
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), Main.screenPosition + new Vector2(ceilingX, -200), new Vector2(0, 5),
                       ModContent.ProjectileType<Projectiles.Series.Boss.MiracleRecorder.Star>(), NPC.damage, 0, player.whoAmI);
                }
                if (timer % 5 == 0)
                {
                    float demo = 1 + Vector2.DistanceSquared(Main.player[Main.myPlayer].Center, player.Center) / 500000;
                    player.GetModPlayer<OdePlayer>().ShakeInt = Math.Max(player.GetModPlayer<OdePlayer>().ShakeInt, (int)(30 / demo));
                }
                if (timer == 120)
                {
                    for (int i = 1; i <= servantCount; i++)
                    {
                        float rad2 = (6.2832f / servantCount) * i;
                        Projectile.NewProjectile(NPC.GetSource_FromAI(),
                            NPC.Center + new Vector2((float)Math.Cos(rad2), (float)Math.Sin(rad2)) * 160f, Vector2.Zero, ModContent.ProjectileType<Projectiles.Series.Boss.MiracleRecorder.BlackMist>(), NPC.damage, 0, player.whoAmI);
                    }
                }


            }
            if (timer == 480)
            {
                rank = 2;
                NPC.dontTakeDamage = false;
                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, Vector2.Zero,
ModContent.ProjectileType<Projectiles.Series.Boss.MiracleRecorder.GoldCircle1>(), 0, 0, player.whoAmI);
            }
            if (timer == 500)
            {
                count2 = 0;
                NPC.alpha = 0;
               
                count = 0;
                ok = -1;
                ok2 = 0;
                timer = 0;
                state = NPCState.Wandering;
            }
        }
        private void dead(Player player)
        {
            if (timer == 1)
            {
                NPC.velocity = new Vector2((float)Math.Cos(NPC.rotation + 1.57f), (float)Math.Sin(NPC.rotation + 1.57f)) * 32f;
            }
            if (timer > 1 && timer < 40)
            {
                NPC.velocity *= 0.95f;
            }
        }

        public override void AI()
        {
            Lighting.AddLight(NPC.Center, 0.9647f, 0.635f, 1);
            NPC.TargetClosest(true);
            Player player = Main.player[NPC.target];

            _npcLogic[state](player);
            timer++;

            for (int i = 8; i > 0; i--)
            {
                rot[i] = rot[i - 1];
            }
            rot[0] = NPC.rotation;
        }

        public override void OnKill()
        {
            OdeMod.ScreenShaderDataManager["OdeMod:MiracleRecorder"].Visible = false;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D texture = TextureAssets.Npc[NPC.type].Value;
            Texture2D texture2 = ModContent.Request<Texture2D>("OdeMod/NPCs/Boss/MiracleRecorder/MiracleRecorderDrawer").Value;
            Vector2 drawOrigin = new Vector2(134 * 0.5f, 209 * 0.703f);
            Vector2 drawPos = NPC.position - Main.screenPosition + drawOrigin + new Vector2(0f, -80f);

            GraphicsDevice gd = Main.instance.GraphicsDevice;
            SpriteBatch sb = Main.spriteBatch;

            gd.SetRenderTarget(Main.screenTargetSwap);
            sb.End();
            sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            sb.Draw(Main.screenTarget, Vector2.Zero, Color.White);
            sb.End();

            //在screenTargetSwap中保存原图
            var render = OdeMod.RenderTarget2DPool.Pool(Main.ScreenSize);
            gd.SetRenderTarget(render);
            gd.Clear(Color.Transparent);
            sb.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullNone, null, Main.Transform);
            foreach (Dust d in Main.dust)
            {
                if (d.type == ModContent.DustType<Dusts.Dream>() && d.active)
                {
                    Texture2D hallowseal = ModContent.Request<Texture2D>("OdeMod/Images/Effects/ballself", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
                    sb.Draw(hallowseal, d.position - Main.screenPosition, null, Color.White, 0, hallowseal.Size() / 2, d.scale, SpriteEffects.None, 0);
                }
            }
            sb.End();

            //在render中绘制图案
            gd.SetRenderTarget(Main.screenTarget);
            gd.Clear(Color.Transparent);
            sb.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullNone);
            sb.Draw(Main.screenTargetSwap, Vector2.Zero, Color.White);
            sb.End();

            //在screenTarget上绘制保存过的原图
            sb.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            var shader = ModContent.Request<Effect>("OdeMod/Effects/PixelShaders/Starry", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
            gd.Textures[0] = render;
            gd.Textures[1] = ModContent.Request<Texture2D>("OdeMod/Images/Effects/Night", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
            Main.graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
            Main.graphics.GraphicsDevice.SamplerStates[1] = SamplerState.PointWrap;
            shader.CurrentTechnique.Passes[0].Apply();
            sb.Draw(render, Vector2.Zero, Color.White);
            sb.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.Transform);

            if (act == 1)
            {
                for (int i = 0; i < NPC.oldPos.Length - 4; i++)
                {
                    Vector2 drawPos2 = NPC.oldPos[i] - Main.screenPosition + drawOrigin + new Vector2(0f, NPC.gfxOffY) + new Vector2(0f, -80f);
                    Color color = new Color(246, 162, 255) * ((NPC.oldPos.Length - i - 1) / (float)NPC.oldPos.Length) * 0.8f;
                    Main.spriteBatch.Draw(texture2, drawPos2, new Rectangle(0, NPC.frame.Y, 134, 209), color, rot[i], drawOrigin, 1f, SpriteEffects.None, 0f);
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
                    var model = Matrix.CreateTranslation(new Vector3(-Main.screenPosition.X, -Main.screenPosition.Y, 0)) * Main.Transform;

                    //启用即时加载加载Shader
                    var shader2 = ModContent.Request<Effect>("OdeMod/Effects/VertexShaders/Trail", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
                    var MainColor = ModContent.Request<Texture2D>("OdeMod/Images/Effects/heatmap", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
                    var MaskColor = ModContent.Request<Texture2D>("OdeMod/Images/Effects/Extra_189", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
                    var MainShape = ModContent.Request<Texture2D>("OdeMod/Images/Effects/Extra_199", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
                    // 把变换和所需信息丢给shader
                    shader2.Parameters["uTransform"].SetValue(model * projection);//坐标变换，详见小裙子视频
                    shader2.Parameters["uTime"].SetValue(-(float)Main.time * 0.05f);//使纹理随时间变化

                    Main.graphics.GraphicsDevice.Textures[0] = MainColor;
                    Main.graphics.GraphicsDevice.Textures[1] = MainShape;
                    Main.graphics.GraphicsDevice.Textures[2] = MaskColor;
                    Main.graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
                    Main.graphics.GraphicsDevice.SamplerStates[1] = SamplerState.PointWrap;
                    Main.graphics.GraphicsDevice.SamplerStates[2] = SamplerState.PointWrap;
                    shader2.CurrentTechnique.Passes[0].Apply();

                    Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangleList.ToArray(), 0, triangleList.Count / 3);
                    //连三角形，其中那个0是偏移量
                    Main.graphics.GraphicsDevice.RasterizerState = originalState;
                    Main.spriteBatch.End();
                    Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.Transform);
                }
            }
            if (act == 2)
            {
                int width = (int)(timer % 20) * 10;
                List<CustomVertexInfo> bars = new();
                //顶点离弹幕坐标的距离，也是顶点三角形宽度的一半
                // 把所有的点都生成出来，按照顺序
                for (float i = 1; i <= 60; i++)
                {
                    var normalDir = new Vector2((float)Math.Cos(i / 60f * 6.28318f), (float)Math.Sin(i / 60f * 6.28318f));
                    var color = Color.Lerp(Color.White, Color.Red, 1);
                    bars.Add(new CustomVertexInfo(NPC.position + new Vector2(0f, -80f) + new Vector2(67, 147) + normalDir * (width + (timer % 20) * 50), color, new Vector3(1, 1, 1 - (Math.Abs(timer % 20) / 20))));
                    bars.Add(new CustomVertexInfo(NPC.position + new Vector2(0f, -80f) + new Vector2(67, 147) + normalDir * (-width + (timer % 20) * 50), color, new Vector3(1, 0, 1 - (Math.Abs(timer % 20) / 20))));
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
                    var shader3 = ModContent.Request<Effect>("OdeMod/Effects/VertexShaders/Trail", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
                    var MainColor = ModContent.Request<Texture2D>("OdeMod/Images/Effects/heatmap3", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
                    var MaskColor = ModContent.Request<Texture2D>("OdeMod/Images/Effects/Flame0", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
                    var MainShape = ModContent.Request<Texture2D>("OdeMod/Images/Effects/Extra_200", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
                    // 把变换和所需信息丢给shader
                    shader3.Parameters["uTransform"].SetValue(model * projection);//坐标变换，详见小裙子视频
                    shader3.Parameters["uTime"].SetValue(-(float)Main.time * 0.05f);//使纹理随时间变化

                    Main.graphics.GraphicsDevice.Textures[0] = MainColor;
                    Main.graphics.GraphicsDevice.Textures[1] = MainShape;
                    Main.graphics.GraphicsDevice.Textures[2] = MaskColor;
                    Main.graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
                    Main.graphics.GraphicsDevice.SamplerStates[1] = SamplerState.PointWrap;
                    Main.graphics.GraphicsDevice.SamplerStates[2] = SamplerState.PointWrap;
                    shader3.CurrentTechnique.Passes[0].Apply();

                    Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangleList.ToArray(), 0, triangleList.Count / 3);
                    //连三角形，其中那个0是偏移量
                    Main.graphics.GraphicsDevice.RasterizerState = originalState;
                    Main.spriteBatch.End();
                    Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.Transform);
                }
            }
            if (act == 3)
            {
                for (int i = 0; i < NPC.oldPos.Length - 4; i++)
                {
                    Vector2 drawPos2 = NPC.oldPos[i] - Main.screenPosition + drawOrigin + new Vector2(0f, NPC.gfxOffY) + new Vector2(0f, -80f);
                    Color color = new Color(246, 162, 255) * ((NPC.oldPos.Length - i - 1) / (float)NPC.oldPos.Length) * 0.8f;
                    Main.spriteBatch.Draw(texture2, drawPos2, new Rectangle(0, NPC.frame.Y, 134, 209), color, rot[i], drawOrigin, 1f, SpriteEffects.None, 0f);
                }
            }
            if (act == 4)
            {
                float scaleDraw = 1f;
                for (int i = 3; i <= 8; i++)
                {
                    scaleDraw = 1f + i * 0.2f;
                    scaleDraw *= (40 - timer) / 40f;
                    Vector2 drawPos2 = NPC.position - Main.screenPosition + drawOrigin + new Vector2(0f, NPC.gfxOffY) + new Vector2(0f, -80f);
                    Color color2 = new Color(246, 162, 255) * (1f - 0.4f * scaleDraw);
                    Main.spriteBatch.Draw(texture2, drawPos2, new Rectangle(0, NPC.frame.Y, 134, 209), color2, rot[i], drawOrigin, scaleDraw, SpriteEffects.None, 0f);
                }
            }
            if (act == 5)
            {
                float scaleDraw = 1f;
                for (int i = 1; i <= 5; i++)
                {
                    scaleDraw = 0.5f + i * 0.2f;
                    scaleDraw *= 0.5f * (float)Math.Cos((double)(timer / 100f)) + 1.5f;
                    Vector2 drawPos2 = NPC.position - Main.screenPosition + drawOrigin + new Vector2(0f, NPC.gfxOffY) + new Vector2(0f, -80f);
                    Color color2 = new Color(246, 162, 255) * (1f - 0.4f * scaleDraw);
                    Main.spriteBatch.Draw(texture2, drawPos2, new Rectangle(0, NPC.frame.Y, 134, 209), color2, rot[i], drawOrigin, scaleDraw, SpriteEffects.None, 0f);
                }
            }
            if (line == 1)
            {
                Player player = Main.player[NPC.target];
                Vector2 tor = player.Center - NPC.Center;
                Color color1 = new Color(255, 0, 241, 0f);
                Color color2 = new Color(255, 0, 241, 0.4f);
                Color color4;
                if (timer < 30f)
                    color4 = Color.Lerp(color1, color2, timer / 30f);
                else
                    color4 = Color.Lerp(color1, color2, 1);
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.Transform);
                DrawLine(Main.spriteBatch, NPC.Center, new Vector2((float)Math.Cos(NPC.rotation + 1.57f), (float)Math.Sin(NPC.rotation + 1.57f)) * 5000 + NPC.Center, color4, color4, 3f);
            }
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.Transform);
            //绘制本体
            Main.spriteBatch.Draw(texture, drawPos, new Rectangle(0, NPC.frame.Y, 134, 209), drawColor * ((255f - (float)NPC.alpha) / 255f), NPC.rotation, drawOrigin, 1f, SpriteEffects.None, 0f);

            DrawUtils.SetDrawRenderTarget(Main.spriteBatch, (sb) =>
            {
                sb.Draw(ModContent.Request<Texture2D>("OdeMod/Images/Effects/Decrate", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value,
                    new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.White);
            }, OdeMod.RenderTarget2DPool.PoolOther(Main.ScreenSize, "MiracleRecorder:Decrate"), Main.screenTarget, Main.screenTargetSwap);
            return false;
        }
    }
}