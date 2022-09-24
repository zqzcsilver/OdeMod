using OdeMod.Utils;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.NPCs.Chaos
{
    //吸 你 脑 髓 不如加个设定 被击中后眩晕 击中次数越多眩晕越多 最后直接暴毙（x
    internal class ChaoticLicker :ModNPC,IChaos
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 8;
        }
        public override void SetDefaults()
        {
            NPC.lifeMax = 100;
            NPC.damage = 80;
            NPC.defense = 20;
            NPC.knockBackResist = 0.1f;
            NPC.width = 48;
            NPC.height = 40;
            NPC.aiStyle = -1;
            NPC.boss = false;
            NPC.noGravity = true;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath6;
            NPC.value = Item.buyPrice(0, 0, 10, 0);
            NPC.noTileCollide = false;
            NPCID.Sets.TrailingMode[NPC.type] = 1;
        }
    }
}
