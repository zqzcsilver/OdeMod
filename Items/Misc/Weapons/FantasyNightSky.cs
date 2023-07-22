using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace OdeMod.Items.Misc.Weapons
{
    internal class FantasyNightSky : ModItem, IMiscItem
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 36;
            Item.height = 46;
            Item.DamageType = DamageClass.Melee;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.damage = 94;
            Item.crit = 1;
            Item.shoot = ModContent.ProjectileType<Projectiles.Misc.EnchantingPro>();
            Item.shootSpeed = 8f;
            Item.useAnimation = 15;
            Item.useTime = 15;
        }
        public override void AddRecipes()
        {
            base.AddRecipes();
            CreateRecipe()
                .AddIngredient(ItemID.Ruby, 1)
                .AddIngredient(ItemID.Topaz, 1)
                .AddIngredient(ItemID.Amethyst, 1)
                .AddIngredient(ItemID.Amber, 1)
                .AddIngredient(ItemID.Emerald, 1)
                .AddIngredient(ItemID.Sapphire, 1)
                .AddIngredient(ItemID.Diamond, 1)
                .AddIngredient(ItemID.FallenStar, 30)
                .AddIngredient(ItemID.HallowedBar, 10)
                .AddIngredient(ItemID.SoulofLight, 10)
                .AddIngredient(ItemID.RainbowBrick, 10)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}