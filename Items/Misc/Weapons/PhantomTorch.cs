using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Items.Misc.Weapons
{
    internal class PhantomTorch : ModItem,IMiscItem
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 54;
            Item.height = 72;
            Item.DamageType = DamageClass.Melee;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.damage = 111;
            Item.crit = 5;
            Item.shoot = ModContent.ProjectileType<Projectiles.Misc.PhantomTorch>();
            Item.shootSpeed = 40f;
            Item.useAnimation = 30;
            Item.useTime = 15;
            Item.value = Item.sellPrice(0, 5, 0, 0);
        }
        int num = 1;
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 tVec = Vector2.Normalize(Main.MouseWorld - player.Center) * Item.shootSpeed;
            for (int i = -num; i <= num; i++)
            {
                Vector2 tVecl = tVec + new Vector2(-tVec.Y * 0.12f, tVec.X * 0.12f) * i;
                tVecl.RotatedBy(i * 0.03);
                Projectile.NewProjectile(source, player.Center, tVecl, ModContent.ProjectileType<Projectiles.Misc.PhantomTorch>(), damage, knockback, player.whoAmI);
            }
            num = num < 2 ? 2 : 1;
            return false;
        }
        public override void AddRecipes()
        {
            base.AddRecipes();
            CreateRecipe()
                .AddIngredient(ItemID.SoulofLight, 10)
                .AddIngredient(ItemID.SoulofNight, 10)
                .AddIngredient(ModContent.ItemType<BigTorch>(), 1)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}
