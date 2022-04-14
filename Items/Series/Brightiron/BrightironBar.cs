using OdeMod.Utils;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Items.Series.Brightiron
{
    internal class BrightironBar : ModItem,BrightironInterface
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Brightiron Bar");
            DisplayName.AddTranslation(LanguageType.Chinese, "熙铁锭");
            Tooltip.SetDefault("");
            Tooltip.AddTranslation(LanguageType.Chinese, "");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 30;
            Item.height = 24;
            Item.maxStack = 99;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(0, 0, 12, 0);
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 22;
            Item.useTime = 22;
            Item.autoReuse = true;
            //Item.createTile = ModContent.TileType<TileBrightironBar>();
        }
        public override void AddRecipes()
        {
            base.AddRecipes();
            CreateRecipe().
                AddIngredient(ModContent.ItemType<BrightironOre>(), 3).
                AddTile(TileID.Furnaces).
                Register();
                

        }
    }
}
