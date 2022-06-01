using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OdeMod.Utils;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Items.Series.Mosscobble
{
    internal class MosscobblePickaxe : ModItem, IMosscobble
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Mosscobble Pickaxe");
            DisplayName.AddTranslation(LanguageType.Chinese, "苔石稿");
            Tooltip.SetDefault("");
            Tooltip.AddTranslation(LanguageType.Chinese, "");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 28;
            Item.height = 30;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 3.5f;
            Item.damage = 4;
            Item.crit = 5;
            Item.useTime = 23;
            Item.useAnimation = 23;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(0, 0, 1, 0);
            Item.tileBoost = 1;
            Item.autoReuse = true;
        }
        public override bool AltFunctionUse(Player player) => true;
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.hammer = 35;
                Item.useTime = 33;
                Item.useAnimation = 33;
            }
            else
            {
                Item.pick = 35;
                Item.hammer = 0;
                Item.useTime = 23;
                Item.useAnimation = 23;
            }
            return true;
        }
        public override void AddRecipes()
        {
            base.AddRecipes();
            CreateRecipe()
               .AddIngredient(ModContent.ItemType<Mosscobble>(), 12)
               .AddIngredient(ItemID.Gel, 7)
               .AddIngredient(ItemID.Vine, 10)
               .AddTile(TileID.WorkBenches)
               .Register();
        }
    }
}
