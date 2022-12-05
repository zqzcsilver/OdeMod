using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace OdeMod.NPCs.Enemies
{
    internal class Raider : ModNPC, IEnemy
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 5;
        }
        public override void SetDefaults()
        {
            NPC.lifeMax = 1000;
            NPC.damage = 80;
            NPC.defense = 10;
            NPC.knockBackResist = 0.5f;
            NPC.width = 70;
            NPC.height = 74;
            NPC.aiStyle = 26;
            NPC.boss = false;
            NPC.noGravity = true;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath6;
            NPC.value = Item.buyPrice(0, 0, 10, 0);
            NPC.noTileCollide = false;
            NPCID.Sets.TrailingMode[NPC.type] = 0;
            //NPCID.Sets.TrailCacheLength[NPC.type] = 6;
        }
    }
}
