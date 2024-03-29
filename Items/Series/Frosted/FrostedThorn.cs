﻿using Microsoft.Xna.Framework;

using OdeMod.Projectiles.Series.Items.Frosted;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;


namespace OdeMod.Items.Series.Frosted
{
    internal class FrostedThorn : ModItem, IFrosted
    {
        public override void SetStaticDefaults()
        {
            /*
            base.SetStaticDefaults();
            DisplayName.SetDefault("Forsted Sickle");
            DisplayName.AddTranslation(LanguageType.Chinese, "凝霜荆棘");
            Tooltip.SetDefault("");
            Tooltip.AddTranslation(LanguageType.Chinese, "");
            */
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 42;
            Item.height = 46;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 14;
            Item.knockBack = 2.6f;
            Item.damage = 75;
            Item.crit = 7;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useTurn = false;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.rare = ItemRarityID.LightRed;
            Item.scale = 1.2f;
            Item.value = Item.sellPrice(0, 8, 0, 0);
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<ProFrostedThorn>();
            Item.shootSpeed = 32;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            var proj = (ProFrostedThorn)Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback).ModProjectile;
            proj.startCenter = proj.Projectile.Center;
            proj.Longer = 120;
            proj.PutNextPro();
            return false;
        }
        public override void AddRecipes()
        {
            base.AddRecipes();
            CreateRecipe()
               .AddIngredient(ItemID.FrostCore, 5)
               .AddIngredient(ItemID.ChlorophyteBar, 15)
               .AddIngredient(ItemID.SoulofFright, 11)
               .AddIngredient(ItemID.SoulofMight, 11)
               .AddIngredient(ItemID.SoulofSight, 15)
               .AddTile(TileID.MythrilAnvil)
               .Register();
        }
    }
}
