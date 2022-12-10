using OdeMod.Players;

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

        public override void GetChat(NPC npc, ref string chat)
        {
            if (npc.type == NPCID.Nurse)
            {
                Player player = Main.player[npc.FindClosestPlayer()];
                if (player.statLife >= player.statLifeMax || Main.rand.NextBool(5))
                {
                    chat = "我唯一能给的建议就是打个胶先";
                }
                if (player.GetModPlayer<EggPlayer>().GG >= 1)
                {
                    chat = player.name + "，你的脑浆都流出来了";
                }
            }
        }

        public override bool PreChatButtonClicked(NPC npc, bool firstButton)
        {
            if(npc.type == NPCID.Nurse)
            {
                Player player = Main.player[npc.FindClosestPlayer()];
                return !(player.statLife >= player.statLifeMax);
            }
            return base.PreChatButtonClicked(npc, firstButton);
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