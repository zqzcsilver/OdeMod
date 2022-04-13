using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OdeMod.Utils;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Items.Series.Californium
{
    internal class Plaguebatter : ModItem, CaliforniumInterface
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Plaguebatter");
            DisplayName.AddTranslation(LanguageType.Chinese, "瘟疫打击者");
            Tooltip.SetDefault("");
            Tooltip.AddTranslation(LanguageType.Chinese, "三连散射\n十一转化");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 68;
            Item.height = 26;
            Item.DamageType = DamageClass.Ranged;
            Item.useAmmo = AmmoID.Bullet;
            Item.knockBack = 6;
            Item.damage = 48;
            Item.useTime = 9;
            Item.useAnimation = 9;
            Item.useTurn = false;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Yellow;
            Item.value = Item.sellPrice(0, 9, 25, 60);
            Item.autoReuse = true;
            Item.shoot = ProjectileID.BulletHighVelocity;
            Item.shootSpeed = 37;
        }
        public override Vector2? HoldoutOffset() => new Vector2(-10, -8);
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            for (int i = -1; i <= 1; i++)
            {
                Vector2 tVEC = Vector2.Normalize(Main.MouseWorld - player.Center) * Item.shootSpeed;
                tVEC.RotatedBy(Main.rand.NextFloat(-0.3f, 0.3f) * i);
                Projectile.NewProjectile(source, player.Center, tVEC, Main.rand.Next(1, 10) <= 1 ? ProjectileID.ChlorophyteBullet :
                    ProjectileID.BulletHighVelocity, damage, knockback, player.whoAmI);
            }
            return false;
        }
        public override void AddRecipes()
        {
            base.AddRecipes();
            CreateRecipe()
               .AddIngredient(ModContent.ItemType<CaliforniumBar>(), 12)
               .AddIngredient(ItemID.VenusMagnum)
               .AddTile(TileID.LunarCraftingStation)
               .Register();
        }
    }
}
