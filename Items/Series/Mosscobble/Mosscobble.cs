using OdeMod.Utils;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Items.Series.Mosscobble
{
    internal class Mosscobble: ModItem,MosscobbleInterface
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Mosscobble");
            DisplayName.AddTranslation(LanguageType.Chinese, "苔石");
            Tooltip.SetDefault("");
            Tooltip.AddTranslation(LanguageType.Chinese, "");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 16;
            Item.height = 16;
            Item.maxStack = 999;
            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(0, 0, 1, 0);
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTurn = true;
            Item.useAnimation = 20;
            Item.useTime = 20;
            Item.consumable = true;
            Item.autoReuse = true;
            //Item.createTile = ModContent.TileType<>();
        }
    }
}
