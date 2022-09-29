﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Items.Series.SoulCemetery
{
	internal class SoulCongealingSoil : ModItem, ISoulCemetery
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("凝魂土");
			Tooltip.SetDefault("哀嚎");
		}
		public override void SetDefaults()
		{
			Item.width = 16;
			Item.height = 18;
			Item.maxStack = 999;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 10;
			Item.useTime = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.consumable = true;
			Item.createTile = ModContent.TileType<Tiles.RiftValley.SoulCemetery.SoulCongealingSoil>();
		}
	}
}
