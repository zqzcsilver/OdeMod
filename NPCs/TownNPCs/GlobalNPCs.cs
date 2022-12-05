using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.NPCs.TownNPCs
{
    internal class GlobalNPCs : GlobalNPC, ITownNPC
    {
        public override bool? CanChat(NPC npc)
        {
            if (npc.type == NPCID.Zombie)
                return true;
            return base.CanChat(npc);
        }
        public override void SetupShop(int type, Chest shop, ref int nextSlot)
        {
            base.SetupShop(type, shop, ref nextSlot);
            if (type == NPCID.PartyGirl)
            {
                shop.item[nextSlot++].SetDefaults(ModContent.ItemType<Items.Misc.WanGift>());
            }
        }
    }
}
