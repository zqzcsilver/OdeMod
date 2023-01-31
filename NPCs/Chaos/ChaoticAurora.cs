using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.NPCs.Chaos
{
    //记得原本设定貌似是发激光 但是后来因为设定经过三年空白了 这里写作极光
    //混沌日内怪物被击杀时皆有20%几率爆出来一个小虫子
    internal class ChaoticAurora : ModNPC, IChaos
    {
        private enum NPC_State
        {
            FindP,
            ReadyShoot,
            OnShoot,
            FinalShoot
        }
        private NPC_State State
        {
            get { return (NPC_State)(int)NPC.ai[0]; }
            set { NPC.ai[0] = (int)value; }
        }
        private void SwitchTo(NPC_State state)
        {
            State = state;
        }
        //之后简化
        //public bool ReadyShoot = false;//当发现玩家时 NPC会尝试进入玩家周围20-30格距离的地方 徘徊1-2s,然后展开射击
        //public bool OnShoot = false;//射击时无法移动 射出大量激光
        //public int LimitShoot = 0;//计算射击数量
        //public bool FinalShoot = false;//当射击的激光数量达到阙值(任意)时,展开最终射击,射出一道类似于棱镜的激光,持续3-5s,随着玩家位置缓慢移动(追不上),发射完后自爆
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 13;
        }
        public override void SetDefaults()
        {
            NPC.lifeMax = 2000;
            NPC.damage = 60;
            NPC.defense = 30;
            NPC.knockBackResist = 0.5f;
            NPC.width = 54;
            NPC.height = 50;
            NPC.aiStyle = -1;
            NPC.boss = false;
            NPC.noGravity = true;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath6;
            NPC.value = Item.buyPrice(0, 0, 10, 0);
            NPC.noTileCollide = false;
            NPCID.Sets.TrailingMode[NPC.type] = 1;
        }
        int framecontrol;
        int framecontrol2;
        int framecontrol3;
        public override void FindFrame(int frameHeight)
        {

            if (State == NPC_State.FindP)
            {
                framecontrol++;
                if (framecontrol % 8 == 0)
                {
                    if (NPC.frame.Y < frameHeight * 5)
                        NPC.frame.Y += frameHeight;
                    else
                        NPC.frame.Y = 0;
                }
            }
            if (State == NPC_State.ReadyShoot)
            {
                framecontrol2++;
                if (framecontrol2 % 10 == 0)
                {
                    if (NPC.frame.Y < frameHeight * 9 && NPC.frame.Y >= frameHeight * 6)
                        NPC.frame.Y += frameHeight;
                    else
                        NPC.frame.Y = frameHeight * 6;
                }
            }
            if (State == NPC_State.OnShoot)
            {
                framecontrol3++;
                if (framecontrol3 % 10 == 0)
                {
                    if (NPC.frame.Y < frameHeight * 12 && NPC.frame.Y > frameHeight * 9)
                        NPC.frame.Y += frameHeight;
                    else
                        NPC.frame.Y = frameHeight * 10;
                }
            }
        }
        public override void AI()
        {
            Player player = Main.player[NPC.target];
            var targetPos = player.Center;
            Vector2 shooooot = Vector2.Normalize(targetPos - NPC.Center) * 10f; ;
            switch (State)
            {
                case NPC_State.FindP:
                    NPC.ai[2]++;
                    if (NPC.ai[2] >= 180)
                    {
                        NPC.ai[2] = 0;
                        Main.NewText("在蓄能啦！", Color.Red);//这里放到怪物上面
                        SwitchTo(NPC_State.ReadyShoot);
                    }
                    break;
                case NPC_State.ReadyShoot:
                    NPC.ai[2]++;
                    if (NPC.ai[2] >= 180)
                    {
                        NPC.ai[2] = 0;

                        Main.NewText("要来咯！", Color.Red);
                        SwitchTo(NPC_State.OnShoot);
                    }
                    break;
                case NPC_State.OnShoot:
                    NPC.ai[2]++;
                    if (NPC.ai[2] % 30 == 0)
                    {
                        Main.NewText("Ready!", Color.Red);
                    }
                    break;
                default:
                    break;
            }
            base.AI();
            //之后简化完善
        }
        public override void OnKill()
        {
            if (Main.rand.NextBool(5))
            {
                NPC.NewNPC(NPC.GetSource_Death(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<ChaoticMite>());
            }
            for (int i = 0; i < 40; i++)
            {
                int num = Dust.NewDust(NPC.Center - new Vector2(16f, 16f), 32, 32, DustID.Smoke, 0f, 0f, 0, new Color(39, 39, 100), 3f);
                Main.dust[num].noGravity = true;
                Main.dust[num].velocity *= 1.5f;
            }
        }
    }
}
