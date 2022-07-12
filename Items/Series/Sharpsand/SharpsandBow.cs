using Microsoft.Xna.Framework;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Items.Series.Sharpsand
{
    internal class SharpsandBow : ModItem, ISharpsand
    {
        public override void SetStaticDefaults()
        {
            /*
            base.SetStaticDefaults();
            DisplayName.SetDefault("Sharpsand Bow");
            DisplayName.AddTranslation(LanguageType.Chinese, "纯砂弓");
            Tooltip.SetDefault("");
            Tooltip.AddTranslation(LanguageType.Chinese, "");
            */
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 28;
            Item.height = 46;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 4.5f;
            Item.damage = 94;
            Item.crit = 10;
            Item.useTime = 18;
            Item.useAnimation = 18;
            Item.useTurn = false;
            Item.noMelee = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Pink;
            Item.value = Item.sellPrice(0, 8, 0, 0);
            Item.autoReuse = true;
            Item.useAmmo = AmmoID.Arrow;
            Item.shoot = ProjectileID.WoodenArrowFriendly;
            Item.shootSpeed = 20;
        }
        public override Vector2? HoldoutOffset() => new Vector2(-6, 0);
        private int num;
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (num > 1)
            {
                Projectile.NewProjectile(source, player.Center, velocity, ProjectileID.IceBoomerang, damage, knockback);
                num = 0;
            }
            num++;
            return false;
        }
        public override void AddRecipes()
        {
            base.AddRecipes();
            CreateRecipe()
               .AddIngredient(ItemID.AncientBattleArmorMaterial, 3)
               .AddIngredient(ItemID.ChlorophyteBar, 18)
               .AddIngredient(ItemID.SoulofFright, 15)
               .AddIngredient(ItemID.SoulofMight, 8)
               .AddIngredient(ItemID.SoulofSight, 8)
               .AddTile(TileID.MythrilAnvil)
               .Register();
        }
    }
}
