using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Items.Series.RiftValley
{
    internal class RottenMeatcs : ModItem, IRiftValley
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
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
            Item.createTile = ModContent.TileType<Tiles.RiftValley.RottenMeatcs>();
        }
    }
}
