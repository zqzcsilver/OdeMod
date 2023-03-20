﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.ItemDropRules;
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
            foreach (Player player in Main.player)//我原本想的是只有玩家背包里才会在购买页面出现 多人联机有的那个人能够买 但是这里好像写成了任意玩家有就出现
            {
                if (player.talkNPC != -1 &&Main.npc[player.talkNPC].type ==  NPCID.GoblinTinkerer && type == NPCID.GoblinTinkerer && player.HasItem(ItemID.PulseBow))
                {
                    shop.item[nextSlot++].SetDefaults(ModContent.ItemType<Items.Misc.Weapons.FlickeringPhantom>());
                }
            }
            if (type == NPCID.ArmsDealer)
            {
                if (WorldGen.shadowOrbCount >= 1 || WorldGen.heartCount >= 1)
                {
                    shop.item[nextSlot++].SetDefaults(ModContent.ItemType<Items.Misc.SuicideRevolver>());
                }
            }
            if (type == NPCID.TravellingMerchant)
            {
                shop.item[nextSlot++].SetDefaults(ModContent.ItemType<Items.Series.Foods.GlacierMineralWater>());
                //GlacierMineralSpringWater
            }
            if(type == NPCID.SkeletonMerchant)
            {
                shop.item[nextSlot++].SetDefaults(ModContent.ItemType<Items.Series.Foods.JiaoDui>());
            }
            if(type == NPCID.Merchant)
            {
                shop.item[nextSlot++].SetDefaults(ModContent.ItemType<Items.Series.Foods.CandiedFruit>());
            }
        }
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if(npc.type == NPCID.Vampire)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Misc.Redmoon>(), 200, 1, 1));//赤红幼月
            }
            if(npc.type == NPCID.Harpy||npc.type == NPCID.WyvernHead)//设定上是天空怪物，但是火星探测器也算，但...有点怪 所以没加
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Misc.Weapons.ChillyMoonStaff>(), 20, 1, 1));//清月法杖
            }
            base.ModifyNPCLoot(npc, npcLoot);
        }
    }
}
