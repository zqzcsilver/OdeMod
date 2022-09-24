using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Items.Misc
{
    internal class SimpleSteelCannon : ModItem, IMiscItem
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 32;
            Item.height = 38;
            Item.DamageType = DamageClass.Ranged;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.damage = 17;
            Item.crit = 5;
            Item.shoot = ProjectileID.Bullet;
            Item.useAmmo = AmmoID.Bullet;
            Item.shootSpeed = 15f;
            Item.useAnimation = 20;
            Item.useTime = 20;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(6f, 2f);
        }
        public override void AddRecipes()
        {
            base.AddRecipes();
            CreateRecipe()
               .AddIngredient(ItemID.IronBar, 15)
               .AddIngredient(ItemID.IllegalGunParts, 1)
               .AddTile(TileID.Anvils)
               .Register();

            CreateRecipe()
                .AddIngredient(ItemID.LeadBar, 15)
                .AddIngredient(ItemID.IllegalGunParts, 1)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
