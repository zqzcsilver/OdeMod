using OdeMod.Utils;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Items.Series.Californium
{
    internal class NeutronSource : ModItem, ICalifornium
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Neutron Source");
            DisplayName.AddTranslation(LanguageType.Chinese, "中子源");
            Tooltip.SetDefault("You can watch the location of treasure and ore\n" +
                "Your defense increase 10%");
            Tooltip.AddTranslation(LanguageType.Chinese, "你可以看到宝藏和矿石的位置\n" +
                "你的防御增加10%");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 34;
            Item.height = 30;
            Item.rare = ItemRarityID.Cyan;
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statDefense = (int)(player.statDefense * 1.1f);
            player.AddBuff(BuffID.Spelunker, 1);
        }
        public override void AddRecipes()
        {
            base.AddRecipes();
            CreateRecipe()
               .AddIngredient(ModContent.ItemType<CaliforniumBar>(), 4)
               .AddTile(TileID.LunarCraftingStation)
               .Register();
        }
    }
}
