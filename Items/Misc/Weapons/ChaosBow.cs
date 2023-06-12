using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Items.Misc.Weapons
{
    internal class ChaosBow : ModItem, IMiscItem
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 36;
            Item.height = 40;
            Item.DamageType = DamageClass.Magic;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.damage = 15;
            Item.crit = 4;
            Item.shoot = ModContent.ProjectileType<Projectiles.Misc.BigTouchPro>();
            Item.shootSpeed = 10f;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.value = Item.sellPrice(0, 0, 50, 0);
            Item.staff[Item.type] = true;
        }
        public override void AddRecipes()
        {
            base.AddRecipes();
            CreateRecipe()
                .AddIngredient(ItemID.Bone,35)
                .AddIngredient(ItemID.SoulofNight,15)
                .AddIngredient(ItemID.Ectoplasm,20)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}
