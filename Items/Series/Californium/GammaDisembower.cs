using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OdeMod.Utils;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Items.Series.Californium
{
    internal class GammaDisembower : ModItem, ICalifornium
    {
        public override void SetStaticDefaults()
        {
            /*
            base.SetStaticDefaults();
            DisplayName.SetDefault("Gamma Disembower");
            DisplayName.AddTranslation(LanguageType.Chinese, "伽马开膛者");
            Tooltip.SetDefault("");
            Tooltip.AddTranslation(LanguageType.Chinese, "");
            */
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 92;
            Item.height = 28;
            Item.DamageType = DamageClass.Ranged;
            Item.useAmmo = AmmoID.Bullet;
            Item.knockBack = 9;
            Item.damage = 412;
            Item.crit = 12;
            Item.useTime = 32;
            Item.useAnimation = 32;
            Item.useTurn = false;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Red;
            Item.value = Item.sellPrice(0, 10, 0, 0);
            Item.autoReuse = true;
            Item.shoot = ProjectileID.BulletHighVelocity;
            Item.shootSpeed = 50;
        }
        public override Vector2? HoldoutOffset() => new Vector2(-10, -10);
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source,player.Center, velocity, ProjectileID.BulletHighVelocity, damage, knockback, player.whoAmI);
            return false;
        }
        public override void AddRecipes()
        {
            base.AddRecipes();
            CreateRecipe()
               .AddIngredient(ModContent.ItemType<CaliforniumBar>(), 14)
               .AddIngredient(ItemID.SniperRifle)
               .AddTile(TileID.LunarCraftingStation)
               .Register();
        }
    }
}
