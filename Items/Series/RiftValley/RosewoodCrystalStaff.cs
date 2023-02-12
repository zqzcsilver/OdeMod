using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Items.Series.RiftValley
{
    internal class RosewoodCrystalStaff : ModItem, IRiftValley
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 32;
            Item.height = 38;
            Item.DamageType = DamageClass.Magic;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.staff[Item.type] = true;
            Item.damage = 12;
            Item.crit = 5;
            Item.knockBack = 0f;
            Item.shoot = ModContent.ProjectileType<Projectiles.Series.Items.RiftValley.RosewoodCrystal>();
            Item.shootSpeed = 20f;
            Item.useAnimation = 28;
            Item.useTime = 28;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.mana = 14;
            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(0, 2, 50, 0);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, player.Center, velocity, ModContent.ProjectileType<Projectiles.Series.Items.RiftValley.RosewoodCrystal>(), damage, knockback, player.whoAmI, Main.rand.Next(0, 2));
            return false;
        }
    }
}