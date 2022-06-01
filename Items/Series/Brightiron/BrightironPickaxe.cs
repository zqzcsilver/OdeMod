using OdeMod.Utils;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Items.Series.Brightiron
{
    internal class BrightironPickaxe : ModItem, IBrightiron
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Brightiron Pickaxe");
            DisplayName.AddTranslation(LanguageType.Chinese, "熙铁镐");
            Tooltip.SetDefault("");
            Tooltip.AddTranslation(LanguageType.Chinese, "");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 34;
            Item.height = 34;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 4f;
            Item.damage = 16;
            Item.crit = 10;
            Item.useTime = 18;
            Item.useAnimation = 18;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(0, 0, 40, 0);
            Item.tileBoost = 1;
            Item.autoReuse = true;
            Item.pick = 85;
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
                AddIngredient(ModContent.ItemType<BrightironBar>(), 14).
                AddIngredient(ModContent.ItemType<SpiritPieces>(), 20).
                AddTile(TileID.Anvils).
                Register();
        }
    }
}
