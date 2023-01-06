using OdeMod.NPCs.Enemies;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Items.Misc.Materials
{
    internal class WildSilkworm : ModNPC, IEnemy
    {
        public override string Texture => $"Terraria/Images/NPC_{NPCID.Worm}";//银烛或者Wan麻烦之后把贴图补一下 Wan原本留下的只有1帧
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = Main.npcFrameCount[NPCID.Worm];
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            NPC.CloneDefaults(357);
            NPC.lifeMax = 10;
            NPC.damage = 0;
            NPC.defense = 10;
            NPC.knockBackResist = 0.5f;
            NPC.friendly = false;
            AnimationType = NPCID.Worm;
            base.SetDefaults();
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            //var WormDropRules = Main.ItemDropsDB.GetRulesForNPCID(NPCID.Worm, false);
            //foreach (var WormDrop in WormDropRules)
            //{
            //    npcLoot.Add(WormDrop);
            //}
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType <Items.Misc.Materials.Silk>(), 10, 2, 7));
            base.ModifyNPCLoot(npcLoot);
        }
    }
}
