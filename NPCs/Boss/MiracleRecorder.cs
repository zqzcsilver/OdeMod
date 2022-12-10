using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OdeMod.Players;
using Steamworks;
using System;
using System.Collections.Generic;

using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.ModLoader;

using static Terraria.Utils;

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
            NPC.alpha = 255;
            NPC.noGravity = true;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath6;
            NPC.value = Item.buyPrice(0, 0, 10, 0);
            NPC.noTileCollide = true;
            NPCID.Sets.TrailingMode[NPC.type] = 0;
            NPCID.Sets.TrailCacheLength[NPC.type] = 8;
        }

        private int control = 0;//控制怪物的行为：0：游荡，1：冲刺
        private int framecontrol = 0;

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

        private int mainlyCtrl = 0;
        private float[] rads = new float[3] { 0.5236f, 2.618f, 4.7116f };//冲刺用的角度数组
        private int act = 0;//控制不同行为的draw
        private int line = 0;//是否绘制瞄准线
        private bool IsDoing = false;
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

        private float oldrotate = 0;
        private float newrotate = 0;
        private int times = 0;

        private int[] mode = new int[3] { 0, 0, 0 };

        public override void AI()
        {
            if (!IsDoing)
            {
                if (mode[0] == 0)
                {
                    control = -1;
                    mode[0] = 1;
                }
                else
                {
                    mainlyCtrl++;
                    control = mainlyCtrl % 3;
                }
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

            if (control == -1)
            {
                if (timer == 1)
                {
                    IsDoing = true;
                    NPC.velocity *= 0f;
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
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center + randomDis * new Vector2((float)Math.Cos(randomRad / 100f), (float)Math.Sin(randomRad / 100f)), Vector2.Zero, ModContent.ProjectileType<Projectiles.Series.Boss.Space>(), 0, 0, player.whoAmI, NPC.Center.X, NPC.Center.Y);
                        for (int i = 0; i < 3; i++)
                        {
                            Vector2 dustpos = NPC.Center + randomDis * Main.rand.NextVector2Unit();
                            var dust2 = Dust.NewDustDirect(dustpos, 1, 1, DustID.PinkTorch, 0, 0, 0, Color.White, 2.5f);
                            dust2.velocity = (NPC.Center - dustpos) / 20f;
                            dust2.noGravity = true;

                            var dust3 = Dust.NewDustDirect(dustpos, 1, 1, ModContent.DustType<Dusts.Dream>(), 0, 0, 0, Color.White, 1f);
                            dust3.velocity = (NPC.Center - dustpos) / 20f;
                            dust3.noGravity = true;
                        }
                    }
                    if (timer % 20 == 0)
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<Projectiles.Series.Boss.Circle0>(), 0, 0, player.whoAmI);
                    }

                    //int num = Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<Dusts.Dream>(), 0f, 0f, 0, Color.White, timer/60f);
                }
                if (timer == 260)
                {
                    for (int i = 1; i < 40; i++)
                    {
                        var dust2 = Dust.NewDustDirect(NPC.Center, 1, 1, DustID.PinkTorch, 0, 0, 0, Color.White, 2f);
                        dust2.velocity = 12 * Main.rand.NextVector2Unit();
                        dust2.noGravity = true;
                    }
                    float demo = 1 + Vector2.DistanceSquared(Main.player[Main.myPlayer].Center, player.Center) / 420000;
                    player.GetModPlayer<OdePlayer>().ShakeInt = Math.Max(player.GetModPlayer<OdePlayer>().ShakeInt, (int)(45 / demo));
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<Projectiles.Series.Boss.Circle1>(), 0, 0, player.whoAmI);
                    for (int i = 1; i <= 15; i++)
                    {
                        float rad2 = 0.41888f * i;
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, new Vector2((float)Math.Cos(rad2), (float)Math.Sin(rad2)) * 15f, ModContent.ProjectileType<Projectiles.Series.Boss.Holyproj>(), NPC.damage, 0, player.whoAmI);
                    }
                    player.GetModPlayer<OdePlayer>().MiracleRecorderShader = 1;
                }
                if (timer >= 300)
                {
                    act = 1;
                    IsDoing = false;
                    timer = 0;
                }
            }
            if (control == 0)
            {
                if (timer == 1)
                {
                    IsDoing = true;
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
                        float rad2 = 1.0472f * i;
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, new Vector2((float)Math.Cos(rad2), (float)Math.Sin(rad2)) * 12f, ModContent.ProjectileType<Projectiles.Series.Boss.Holyproj>(), NPC.damage, 0, player.whoAmI);
                    }
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

                    if (count <= 4)
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
                    noticeVec = NPC.Center;
                    IsDoing = true;

                    rando = 0.6f;

                    plrCenter = player.Center;
                    dir = player.Center - NPC.Center;
                    dir.Normalize();
                    if (count2 == 0)
                    {
                    }
                    else
                    {
                        dir = new Vector2((float)Math.Cos(dir.ToRotation() + rando), (float)Math.Sin(dir.ToRotation() + rando));
                    }

                    newrotate = dir.ToRotation() - 1.57f;
                    oldrotate = NPC.rotation;
                    noticeVec = NPC.Center;
                }
                if (timer > 1 && timer < 20)
                {
                    if (oldrotate <= 0 && newrotate >= 0)
                    {
                        oldrotate += 6.28318f;
                    }
                    NPC.rotation = (oldrotate * (20 - timer) * 0.05f) + newrotate * timer * 0.05f;
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
                    NPC.velocity += new Vector2(NPC.velocity.Y, -NPC.velocity.X) * 0.01f;
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
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<Projectiles.Series.Boss.Spark>(), 0, 0, player.whoAmI, count2, times);
                    timer = 0;
                    if (count2 >= 4)
                    {
                        NPC.alpha = 0;
                        IsDoing = false;
                        ok2 = 0;
                        count2 = 0;
                        rando = 0;
                        times++;
                    }
                }
            }
            if (control == 2)
            {
                if (timer == 1)
                {
                    IsDoing = true;
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
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), new Vector2((float)Math.Cos(NPC.rotation + 1.57f), (float)Math.Sin(NPC.rotation + 1.57f)) * 45 + NPC.Center, Vector2.Zero, ModContent.ProjectileType<Projectiles.Series.Boss.Laser01>(), 0, 0, player.whoAmI, NPC.rotation);
                    distance = Vector2.Distance(NPC.Center, player.Center);
                }
                if (timer > 38 && timer < 58)
                {
                    NPC.velocity *= 0.5f;
                }
                if (timer == 58)
                {
                    if (count2 == 3)
                    {
                        count2 = 0;
                        NPC.alpha = 0;
                        NPC.velocity *= 0f;
                        IsDoing = false;
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
            if (control == 3)
            {
                if (timer == 1)
                {
                    IsDoing = true;
                    NPC.velocity = new Vector2((float)Math.Cos(NPC.rotation + 1.57f), (float)Math.Sin(NPC.rotation + 1.57f)) * 32f;
                }
                if (timer > 1 && timer < 40)
                {
                    NPC.velocity *= 0.95f;
                }
            }
        }

        public override void OnKill()
        {
            Main.LocalPlayer.GetModPlayer<OdePlayer>().MiracleRecorderShader = 0;
        }

        private RenderTarget2D render;

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D texture = TextureAssets.Npc[NPC.type].Value;
            Texture2D texture2 = ModContent.Request<Texture2D>("OdeMod/NPCs/Boss/MiracleRecorderDrawer").Value;
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
            if (render == null)
            {
                render = new RenderTarget2D(Main.graphics.GraphicsDevice, Main.screenWidth, Main.screenHeight);
            }
            gd.SetRenderTarget(render);
            gd.Clear(Color.Transparent);
            sb.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
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
            sb.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            sb.Draw(Main.screenTargetSwap, Vector2.Zero, Color.White);
            sb.End();
            //在screenTarget上绘制保存过的原图
            sb.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            var shader = ModContent.Request<Effect>("OdeMod/Effects/Content/Starry", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
            gd.Textures[0] = render;
            gd.Textures[1] = ModContent.Request<Texture2D>("OdeMod/Images/Effects/Night", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
            Main.graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
            Main.graphics.GraphicsDevice.SamplerStates[1] = SamplerState.PointWrap;
            shader.CurrentTechnique.Passes[0].Apply();
            sb.Draw(render, Vector2.Zero, Color.White);
            sb.End();
            sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

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
                    var model = Matrix.CreateTranslation(new Vector3(-Main.screenPosition.X, -Main.screenPosition.Y, 0)) * Main.Transform;

                    //启用即时加载加载Shader
                    var shader2 = ModContent.Request<Effect>("OdeMod/Effects/Content/Trail", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
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
                    Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
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
                    var shader3 = ModContent.Request<Effect>("OdeMod/Effects/Content/Trail", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
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
                    Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
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
            if (line == 1)
            {
                Player player = Main.player[NPC.target];
                Vector2 tor = player.Center - NPC.Center;
                Color color1 = new Color(255, 255, 255, 1);
                Color color2 = new Color(0, 0, 0, 1);
                Color color4;
                if (timer < 30f)
                    color4 = Color.Lerp(color2, color1, timer / 30f);
                else
                    color4 = Color.Lerp(color2, color1, 1);
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
                DrawLine(Main.spriteBatch, NPC.Center, new Vector2((float)Math.Cos(NPC.rotation + 1.57f), (float)Math.Sin(NPC.rotation + 1.57f)) * 5000 + NPC.Center, color4, color4, 1.5f);
                Main.spriteBatch.End();
                Main.spriteBatch.Begin();
                Main.NewText(1);
            }
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            Main.spriteBatch.Draw(texture, drawPos, new Rectangle(0, NPC.frame.Y, 134, 209), drawColor * ((255f - (float)NPC.alpha) / 255f), NPC.rotation, drawOrigin, 1f, SpriteEffects.None, 0f);
            Main.spriteBatch.End();
            Main.spriteBatch.Begin();
            //绘制本体
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