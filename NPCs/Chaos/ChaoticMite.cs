using OdeMod.Utils;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.NPCs.Chaos
{
    internal class ChaoticMite : ModNPC,IChaos
    {
        //每次生成一大堆
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 5;
        }
        public override void SetDefaults()
        {
            NPC.lifeMax = 100;
            NPC.damage = 80;
            NPC.defense = 20;
            NPC.knockBackResist = 0.1f;
            NPC.width = 24;
            NPC.height = 16;
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
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            //if (Chaos)
            //    return 1.0f;
            return 0;
        }
    }
}
