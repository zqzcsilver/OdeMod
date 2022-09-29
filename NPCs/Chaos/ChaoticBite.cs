using OdeMod.Utils;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.NPCs.Chaos
{
    internal class ChaoticBite :ModNPC,IChaos
    {
        //攻击张嘴时 防御减半
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 13;
        }
        public override void SetDefaults()
        {
            NPC.lifeMax = 200;
            NPC.damage = 40;
            NPC.defense = 20;
            NPC.knockBackResist = 0.1f;
            NPC.width = 64;
            NPC.height = 68;
            NPC.aiStyle = 3;
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
