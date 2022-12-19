using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Items.Series.SoulCemetery
{
    internal class BoneOak : ModItem, ISoulCemetery
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 20;
            Item.maxStack = 999;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 10;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<Tiles.RiftValley.SoulCemetery.Plant>();
        }
    }
}