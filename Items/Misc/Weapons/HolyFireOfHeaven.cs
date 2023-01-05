using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
namespace OdeMod.Items.Misc.Weapons
{
    internal class HolyFireOfHeaven : ModItem, IMiscItem
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 36;
            Item.height = 40;
            Item.DamageType = DamageClass.Magic;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.damage = 15;
            Item.crit = 4;
            Item.shoot = ModContent.ProjectileType<Projectiles.Misc.BigTouchPro>();
            Item.shootSpeed = 10f;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.value = Item.sellPrice(0, 0, 50, 0);
        }
        public override void AddRecipes()
        {
            base.AddRecipes();
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<Items.Misc.Materials.DarkMoonSoul>(), 10)
                .AddIngredient(ModContent.ItemType<Items.Misc.Materials.GreenSoul>(), 10)
                .AddIngredient(ModContent.ItemType<Items.Misc.Weapons.PhantomTorch>())
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }
    }
}
