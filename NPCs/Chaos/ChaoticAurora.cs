using OdeMod.Utils;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.NPCs.Chaos
{
    //记得原本设定貌似是发激光 但是后来因为设定经过三年空白了 这里写作极光
    internal class ChaoticAurora : ModNPC, IChaos
    {
        //private enum NPC_State
        //{
        //    FindP,
        //    ReadyShoot,
        //    OnShoot,
        //    FinalShoot
        //}
        //之后简化
        public bool ReadyShoot = false;//当发现玩家时 NPC会尝试进入玩家周围20-30格距离的地方 徘徊1-2s,然后展开射击
        public bool OnShoot = false;//射击时无法移动 射出大量激光
        public int LimitShoot = 0;//计算射击数量
        public bool FinalShoot = false;//当射击的激光数量达到阙值(任意)时,展开最终射击,射出一道类似于棱镜的激光,持续3-5s,随着玩家位置缓慢移动(追不上),发射完后自爆
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
        public override void AI()
        {
            base.AI();
            //之后简化完善
            if (ReadyShoot)
            {

            }
            if (OnShoot)
            {
                for(int i = 0; i < 60; i++)
                {
                    
                    LimitShoot++;
                }
            }
            if (FinalShoot)
            {

            }
        }
    }
}
