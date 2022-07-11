using Microsoft.Xna.Framework;
using OdeMod.Utils;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Items.Series.Brightiron
{
    internal class BrightironShortbow : ModItem, IBrightiron
    {
        public override void SetStaticDefaults()
        {
            /*
            base.SetStaticDefaults();
            DisplayName.SetDefault("BrightironShortbow");
            DisplayName.AddTranslation(LanguageType.Chinese, "熙铁短弓");
            Tooltip.SetDefault("");
            Tooltip.AddTranslation(LanguageType.Chinese, "");
            */
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 24;
            Item.height = 44;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 2.3f;
            Item.damage = 25;
            Item.crit = 5;
            Item.useTime = 24;
            Item.useAnimation = 24;
            Item.useTurn = false;
            Item.noMelee = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(0, 0, 42, 0);
            Item.autoReuse = true;
            Item.useAmmo = AmmoID.Arrow;
            Item.shoot = ProjectileID.WoodenArrowFriendly;
            Item.shootSpeed = 41;
        }
        public override Vector2? HoldoutOffset() => new Vector2(-6, 0);
        /*public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<BrightironBreastplate>() && legs.type == ModContent.ItemType<BrightironLeggings>();
        }*/
        public override void UpdateArmorSet(Player player) => Item.damage = (int)(Item.damage * 1.3);
        public override void AddRecipes()
        {
            base.AddRecipes();
            CreateRecipe().
                AddIngredient(ModContent.ItemType<BrightironBar>(), 9).
                AddIngredient(ModContent.ItemType<SpiritPieces>(), 13).
                AddTile(TileID.Anvils).
                Register();
        }
    }
}
