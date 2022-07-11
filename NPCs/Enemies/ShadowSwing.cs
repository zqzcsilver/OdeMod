using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;

using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.NPCs.Enemies
{
    public class ShadowSwing : ModNPC, IEnemy
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("暗翼");
            Main.npcFrameCount[NPC.type] = 15;
        }
        public override void SetDefaults()
        {
            NPC.lifeMax = 180;
            NPC.damage = 40;
            NPC.defense = 10;
            NPC.knockBackResist = 0.5f;
            NPC.width = 102;
            NPC.height = 86;
            NPC.aiStyle = 14;
            NPC.boss = false;
            NPC.noGravity = true;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath6;
            NPC.value = Item.buyPrice(0, 0, 10, 0);
            NPC.noTileCollide = false;
            NPCID.Sets.TrailingMode[NPC.type] = 0;
            NPCID.Sets.TrailCacheLength[NPC.type] = 6;
        }
        public override bool? CanFallThroughPlatforms()
        {
            return true;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (!Main.dayTime) return 0.12f;
            else return 0;
        }
        int control = 0;//控制怪物的行为：0：游荡，1：冲刺
        int framecontrol = 0;
        int framecontrol2 = 0;
        public override void OnKill()
        {
            for (int i = 0; i < 40; i++)
            {
                int num = Dust.NewDust(NPC.Center - new Vector2(16f, 16f), 32, 32, DustID.Smoke, 0f, 0f, 0, new Color(39, 39, 100), 3f);
                Main.dust[num].noGravity = true;
                Main.dust[num].velocity *= 1.5f;
            }
        }
        public override void FindFrame(int frameHeight)
        {
            framecontrol++;
            if (control == 0)
            {
                framecontrol2 = 0;
                if (framecontrol % 5 == 0)
                {
                    if (NPC.frame.Y < frameHeight * 8)
                        NPC.frame.Y += frameHeight;
                    else
                        NPC.frame.Y = 0;
                }
            }
            if (control == 1)
            {
                framecontrol2++;
                if (framecontrol2 < 5)
                    NPC.frame.Y = frameHeight * 10;
                else NPC.frame.Y = frameHeight * 11;
            }
        }
        int timer = 0;//冲刺倒计时
        int rush = 0;//冲刺中变量
        int ok = 0;//用于给npc加速
        public override void AI()
        {
            if (timer % 2 == 0 && control == 0)
            {
                NPC.knockBackResist = 0.5f;
                int num = Dust.NewDust(NPC.Center - new Vector2(16f, 16f), 32, 32, DustID.Water_Corruption, 0f, 0f, 0, default, 1.5f);
                Main.dust[num].noGravity = true;
            }
            if (control == 1)
            {
                NPC.knockBackResist = 0;
                int num = Dust.NewDust(NPC.Center - new Vector2(16f, 16f), 32, 32, DustID.Water_Corruption, 0f, 0f, 0, default, 2f);
                Main.dust[num].noGravity = true;
                Main.dust[num].velocity *= 0.1f;
                int num2 = Dust.NewDust(NPC.Center - new Vector2(16f, 16f), 32, 32, DustID.Shadowflame, 0f, 0f, 0, default, 1.5f);
                Main.dust[num2].noGravity = true;
            }

            Player player = Main.player[NPC.target];
            NPC.TargetClosest(true);
            if (player.position.X > NPC.position.X)
            {
                NPC.spriteDirection = 1;
            }
            else
            {
                NPC.spriteDirection = -1;
            }
            timer++;
            //
            if (control == 0)
            {
                if (ok == 0)
                {
                    NPC.aiStyle = 14;
                    NPC.velocity *= 2f;
                    ok = 1;
                }
                if (NPC.velocity.Y * NPC.velocity.X > 0)
                {
                    NPC.rotation = Math.Abs(NPC.velocity.X) * 0.1f;
                }
                else
                {
                    NPC.rotation = Math.Abs(NPC.velocity.X) * -0.1f;
                }
            }
            //

            if (timer > 240 && timer < 260)
            {
                NPC.velocity *= 0.93f;
            }
            if (timer >= 260)
            {
                //Main.NewText(rush);
                rush++;
                if (rush < 20)
                {
                    NPC.velocity *= 0.92f;
                }
                else
                {

                    if (rush == 20)
                    {
                        NPC.aiStyle = -1;
                        control = 1;
                        NPC.rotation = new Vector2(player.Center.X - NPC.Center.X, player.Center.Y - NPC.Center.Y).ToRotation() - 1.571f;
                        NPC.velocity = new Vector2(player.Center.X - NPC.Center.X, player.Center.Y - NPC.Center.Y) / Vector2.Distance(player.Center, NPC.Center) * 15f;
                    }
                    if (rush <= 50)
                    {
                        control = 1;
                        if (NPC.collideX || NPC.collideY)
                        {
                            rush = 51;
                        }
                    }
                    if (rush > 50)
                    {
                        NPC.velocity *= 0.8f;
                        if (rush == 51)
                            ok = 0;

                        control = 0;

                    }
                    if (rush > 60)
                    {

                        timer = 0;
                        rush = 0;
                    }
                }
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (control == 1)
            {
                Vector2 drawOrigin = new Vector2(NPC.width * 0.5f, NPC.height * 0.5f);
                for (int i = 0; i < 6; i++)
                {
                    Vector2 drawPos = NPC.oldPos[i] - Main.screenPosition + drawOrigin + new Vector2(0f, NPC.gfxOffY);
                    Texture2D texture = TextureAssets.Npc[NPC.type].Value;
                    Color color = new Color(39, 39, 100) * ((NPC.oldPos.Length - i - 1) / (float)NPC.oldPos.Length);
                    if (NPC.spriteDirection == -1)
                        Main.spriteBatch.Draw(texture, drawPos, new Rectangle(0, 86 * 11, 102, 86), color, NPC.rotation, drawOrigin, NPC.scale, SpriteEffects.None, 0f);
                    else
                        Main.spriteBatch.Draw(texture, drawPos, new Rectangle(0, 86 * 11, 102, 86), color, NPC.rotation, drawOrigin, NPC.scale, SpriteEffects.FlipHorizontally, 0f);
                }
            }
            return true;
        }
    }
}