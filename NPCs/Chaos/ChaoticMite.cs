using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.NPCs.Chaos
{
    internal class ChaoticMite : ModNPC, IChaos
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
            NPC.aiStyle = 3;
            NPC.boss = false;
            NPC.noGravity = true;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath6;
            NPC.value = Item.buyPrice(0, 0, 10, 0);
            NPC.noTileCollide = false;
            NPCID.Sets.TrailingMode[NPC.type] = 1;
        }
        int framecontrol = 0;
        public override void FindFrame(int frameHeight)
        {
            framecontrol++;
            if (framecontrol % 5 == 0)
            {
                if (NPC.frame.Y < frameHeight * 4)
                    NPC.frame.Y += frameHeight;
                else
                    NPC.frame.Y = 0;
            }

        }
        public override void AI()
        {
            Player player = Main.player[NPC.target];
            base.AI();
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            //if (Chaos)
            //    return 1.0f;
            return 0;
        }
        public override void OnKill()
        {
            Main.NewText(NPC.ai[0], Microsoft.Xna.Framework.Color.Red);
            if (/*Main.rand.NextBool(1) &&*/ NPC.ai[0] != 114514f)
            {
                for (int a = 0; a < 1; a++)
                {
                    NPC.NewNPC(NPC.GetSource_Death(), (int)NPC.position.X + Main.rand.Next(30), (int)NPC.position.Y + Main.rand.Next(30), ModContent.NPCType<ChaoticMite>(), 0, 114514f, 111f);
                }
            }
        }
    }
}
