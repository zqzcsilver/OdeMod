﻿using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Items.Misc
{
    internal class WanGift : ModItem, IMiscItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 3;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 32;
            Item.height = 38;
            Item.maxStack = 99;
            Item.rare = ItemRarityID.Expert;
            Item.shopCustomPrice = 100000;
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void RightClick(Player player)
        {
            var entitySource = player.GetSource_OpenItem(Type);
            player.QuickSpawnItem(entitySource, ModContent.ItemType<Wan>(), Main.rand.Next(5, 15));
        }
    }
}