using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OdeMod.Utils;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Items.Series.Mosscobble
{
    internal class MosscobbleAxe : ModItem, MosscobbleInterface
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Mosscobble Axe");
            DisplayName.AddTranslation(LanguageType.Chinese, "苔石斧");
            Tooltip.SetDefault("");
            Tooltip.AddTranslation(LanguageType.Chinese, "");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 30;
            Item.height = 30;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 3.5f;
            Item.damage = 4;
            Item.crit = 5;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(0, 0, 1, 75);
            Item.shoot = ProjectileID.None;
            Item.shootSpeed = 20;
            Item.autoReuse = true;
        }
        public override bool AltFunctionUse(Player player) => true;
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 0)
            {
                Item.shoot = 0;
                Item.damage = 3;
                Item.useTime = 15;
                Item.knockBack = 4.5f;
                Item.useAnimation = 15;
                Item.noMelee = false;
                Item.noUseGraphic = false;
            }
            else
            {
                Item.noMelee = true;
                Item.noUseGraphic = true;
                Item.shoot = ProjectileID.PaladinsHammerFriendly;//ModContent.ProjectileType<ProMosscobbleAxe>();
                Item.damage = 13;
                Item.useTime = 30;
                Item.knockBack = 8;
                Item.useAnimation = 30;
            }
            return true;
        }
        public override void AddRecipes()
        {
            base.AddRecipes();
            CreateRecipe()
               .AddIngredient(ModContent.ItemType<Mosscobble>(), 9)
               .AddIngredient(ItemID.Gel, 5)
               .AddIngredient(ItemID.Vine, 6)
               .AddTile(TileID.WorkBenches)
               .Register();
        }
    }
}
