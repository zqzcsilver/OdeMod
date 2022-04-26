using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OdeMod.Utils;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Items.Series.Frosted
{
    internal class FrostedFlyingcutter : ModItem, FrostedInterface
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Frosted Flying Knife");
            DisplayName.AddTranslation(LanguageType.Chinese, "凝霜飞刀");
            Tooltip.SetDefault("");
            Tooltip.AddTranslation(LanguageType.Chinese, "");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 32;
            Item.height = 32;
            Item.DamageType = DamageClass.Magic;
            Item.knockBack = 4.5f;
            Item.damage = 52;
            Item.crit = 6;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.useTurn = false;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.LightRed;
            Item.value = Item.sellPrice(0, 6, 50, 0);
            Item.autoReuse = true;
            Item.shoot = ProjectileID.WoodenArrowFriendly;//ModContent.ProjectileType<ProFrostedShortcutter>();
            Item.shootSpeed = 20;
        }
        private int ThrownTime;
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 tVec = Vector2.Normalize(Main.MouseWorld - player.Center) * Item.shootSpeed;
            if (ThrownTime < 3)
            {
                for (int i = -2; i <= 2; i++)
                {
                    Vector2 tVecl = tVec + new Vector2(-tVec.Y * 0.2f, tVec.X * 0.2f) * i;
                    tVecl.RotatedBy(i * 0.05);
                    Projectile.NewProjectile(source, player.Center, tVecl, ProjectileID.WoodenArrowFriendly, damage, knockback, player.whoAmI);
                }
                ThrownTime += 1;
            }
            else if (ThrownTime >= 3)
            {
                for (int i = -1; i <= 1; i++)
                {
                    Vector2 tVecl = tVec + new Vector2(-tVec.Y * 0.15f, tVec.X * 0.15f) * i;
                    tVecl.RotatedBy(i * 0.05);
                    Projectile.NewProjectile(source, player.Center, tVecl, ProjectileID.WoodenArrowFriendly, damage * 2, knockback, player.whoAmI);
                }
                ThrownTime = 0;
            }

            return false;
        }
        public override void AddRecipes()
        {
            base.AddRecipes();
            CreateRecipe()
               .AddIngredient(ItemID.FrostCore, 4)
               .AddIngredient(ItemID.ChlorophyteBar, 16)
               .AddIngredient(ItemID.SoulofFright, 14)
               .AddIngredient(ItemID.SoulofMight, 10)
               .AddIngredient(ItemID.SoulofSight, 10)
               .AddTile(TileID.MythrilAnvil)
               .Register();
        }
    }
}
