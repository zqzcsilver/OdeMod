using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.NPCs.Chaos
{
    //The Nameless Mist 彩蛋 无名之雾 当击杀此NPC达到999只 并且在同一区域有13个对应旗子 出现彩蛋无名之雾（之后让Wan和银烛画个好点的特效XD
    internal class Mist : ModNPC, IChaos
    {

        //我忘记了这个的设定了
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 5;
        }
        public override void SetDefaults()
        {
            NPC.lifeMax = 200;
            NPC.damage = 40;
            NPC.defense = 10;
            NPC.knockBackResist = 0.2f;
            NPC.width = 62;
            NPC.height = 64;
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
    }
}
