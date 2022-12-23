using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace OdeMod.Items.Series.Miracle
{
    internal class HolySpiritCurse : ModItem, IMiracle
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 28;
            Item.height = 30;
            Item.maxStack = 30;
            Item.rare = ItemRarityID.LightPurple;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.useAnimation = 10;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<Tiles.HolySpiritCurse>();//Tiles我不知道该怎么分类啊
        }
    }
}
