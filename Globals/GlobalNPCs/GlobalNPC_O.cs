using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Globals.GlobalNPCs
{
    internal class GlobalNPC_O :GlobalNPC
    {
        public override void GetChat(NPC npc, ref string chat)
        {
            Player player = Main.player[npc.FindClosestPlayer()];
            if (npc.type == NPCID.ArmsDealer)
            {
                if (player.HasItem(ModContent.ItemType<Items.Misc.SuicideRevolver>()) && Main.rand.NextBool(3))
                {
                    chat = "自尽左轮...其实只要不对着身子，把手拿到一边就可以射击后方的敌人了吧，不过还是很容易失误，我究竟是怎么设计出这鬼玩意的......";
                }
            }
            if(npc.type == NPCID.TravellingMerchant)
            {
                if (player.HasItem(ModContent.ItemType<Items.Series.Foods.GlacierMineralWater>()) && Main.rand.NextBool(3))
                {
                    chat = "冰川矿废水？你为什么会买这种东西？";
                }
            }
            if(npc.type == NPCID.Merchant)
            {
                if (Main.rand.NextBool(4))
                {
                    chat = "嘿嘿嘿，冰川矿泉水，我九死一生从雪山深处的冰川中取出来的，纯天然无任何添加，来一瓶吗？";
                }
            }
        }
        public override bool PreChatButtonClicked(NPC npc, bool firstButton)
        {
            return base.PreChatButtonClicked(npc, firstButton);
        }
        public override void SetupShop(int type, Chest shop, ref int nextSlot)
        {
            base.SetupShop(type, shop, ref nextSlot);
            
            if (type == NPCID.ArmsDealer)
            {
                if (WorldGen.shadowOrbCount >= 1 || WorldGen.heartCount >=1)
                {
                    shop.item[nextSlot++].SetDefaults(ModContent.ItemType<Items.Misc.SuicideRevolver>());
                }
            }
            if(type == NPCID.TravellingMerchant)
            {
                shop.item[nextSlot++].SetDefaults(ModContent.ItemType<Items.Series.Foods.GlacierMineralWater > ());
                //GlacierMineralSpringWater
            }
        }
    }
}
