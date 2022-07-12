using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Items.Series.Brightiron
{
    internal class BrightironBroadsword : ModItem, IBrightiron
    {
        public override void SetStaticDefaults()
        {
            /*
            base.SetStaticDefaults();
            DisplayName.SetDefault("Brightiron Broadsword");
            DisplayName.AddTranslation(LanguageType.Chinese, "熙铁阔剑");
            Tooltip.SetDefault("");
            Tooltip.AddTranslation(LanguageType.Chinese, "");
            */
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 42;
            Item.height = 42;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 5.9f;
            Item.damage = 31;
            Item.crit = 6;
            Item.useTime = 22;
            Item.useAnimation = 22;
            Item.useTurn = true;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(0, 0, 30, 0);
            Item.autoReuse = true;
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
                AddIngredient(ModContent.ItemType<BrightironBar>(), 7).
                AddIngredient(ModContent.ItemType<SpiritPieces>(), 10).
                AddTile(TileID.Anvils).
                Register();
        }
    }
}
