using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Items.Series.Brightiron
{
    internal class SpiritPieces : ModItem, IBrightiron
    {
        public override void SetStaticDefaults()
        {
            /*
            base.SetStaticDefaults();
            DisplayName.SetDefault("Spirit Pieces");
            DisplayName.AddTranslation(LanguageType.Chinese, "异灵体");
            Tooltip.SetDefault("Post-Eye of Cthulhu, Crimson / Corrupt Biome");
            Tooltip.AddTranslation(LanguageType.Chinese, "克鲁苏之眼后，猩红地/腐地");
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(9, 8));
            */
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 7;
            Item.height = 10;
            Item.maxStack = 99;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(0, 0, 1, 20);
        }
    }
}
