using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Items.Misc
{
    internal class LargePottedPlant : ModItem, IMiscItem
    {
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
            Item.width = 24;
            Item.height = 26;
            Item.value = 80;
            Item.createTile = ModContent.TileType<Tiles.LargePottedPlant>();
        }
    }
}
