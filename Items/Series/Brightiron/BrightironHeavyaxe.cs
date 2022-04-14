using OdeMod.Utils;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Items.Series.Brightiron
{
    internal class BrightironHeavyaxe : ModItem, BrightironInterface
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("BrightironHeavyaxe");
            DisplayName.AddTranslation(LanguageType.Chinese, "熙铁重斧");
            Tooltip.SetDefault("");
            Tooltip.AddTranslation(LanguageType.Chinese, "");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 42;
            Item.height = 36;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 8f;
            Item.damage = 41;
            Item.crit = 6;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(0, 0, 31, 75);
            Item.autoReuse = true;
            Item.axe = 17;
        }
        /*public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<BrightironBreastplate>() && legs.type == ModContent.ItemType<BrightironLeggings>();
        }*/
        public override void UpdateArmorSet(Player player) => Item.damage = (int)(Item.damage * 1.3);
        public override void AddRecipes()
        {
            base.AddRecipes();
            CreateRecipe().
                AddIngredient(ModContent.ItemType<BrightironBar>(), 6).
                AddIngredient(ModContent.ItemType<SpiritPieces>(), 9).
                AddTile(TileID.Anvils).
                Register();
        }
    }
}
