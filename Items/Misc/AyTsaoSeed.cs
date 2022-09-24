using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;

namespace OdeMod.Items.Misc 
{
    internal class AyTsaoSeed :ModItem, IMiscItem
    {
		//仅仅是个测试 正式版删除
		public override void SetDefaults()
		{
			Item.autoReuse = true;
			Item.useTurn = true;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.maxStack = 999;
			Item.consumable = true;
			Item.placeStyle = 0;
			Item.width = 12;
			Item.height = 14;
			Item.value = 80;
			Item.createTile = ModContent.TileType<Tiles.AyTsao>();
		}
	}
}
