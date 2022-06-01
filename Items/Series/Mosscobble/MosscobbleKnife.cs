using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OdeMod.Utils;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Items.Series.Mosscobble
{
    internal class MosscobbleKnife : ModItem, IMosscobble
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Mosscobble Knife");
            DisplayName.AddTranslation(LanguageType.Chinese, "苔石刀");
            Tooltip.SetDefault("");
            Tooltip.AddTranslation(LanguageType.Chinese, "");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 30;
            Item.height = 34;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 5f;
            Item.damage = 8;
            Item.crit = 6;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(0, 0, 1, 25);
            Item.useTurn = false;
        }
        public override bool AltFunctionUse(Player player) => true;
        public override bool CanUseItem(Player player)
        {
            if(player.altFunctionUse == 2)
            {
                Item.shoot = ProjectileID.Arkhalis;//ModContent.ProjectileType<ProMosscobbleBeam>();
                Item.useTime = 300;
                Item.useAnimation = 300;
                Item.noUseGraphic = true;
                Item.noMelee = true;
                Item.useStyle = ItemUseStyleID.Shoot;
                Item.autoReuse = true;
            }
            else
            {
                Item.noMelee = false;
                Item.noUseGraphic = false;
                Item.shoot = ProjectileID.None;
                Item.useTime = 20;
                Item.useAnimation = 20;
                Item.useStyle = ItemUseStyleID.Swing;
                Item.autoReuse = false;
            }
            return true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                Projectile.NewProjectile(source, player.Center.X + (player.direction * player.width), player.Center.Y, 0, 0,
                   595, damage, knockback, player.whoAmI);
                return false;
            }
            return false;
        }

        public override void AddRecipes()
        {
            base.AddRecipes();
            CreateRecipe()
               .AddIngredient(ModContent.ItemType<Mosscobble>(), 10)
               .AddIngredient(ItemID.Gel, 7)
               .AddIngredient(ItemID.Vine, 8)
               .AddTile(TileID.WorkBenches)
               .Register();
        }
    }
}
