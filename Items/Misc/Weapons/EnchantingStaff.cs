using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace OdeMod.Items.Misc.Weapons
{
    internal class EnchantingStaff : ModItem,IMiscItem
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 36;
            Item.height = 46;
            Item.DamageType = DamageClass.Magic;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.damage = 10;
            Item.crit = 1;
            Item.shoot = ModContent.ProjectileType<Projectiles.Misc.EnchantingPro>();
            Item.shootSpeed = 10f;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.staff[Item.type] = true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            for (int i = -1; i <= 1; i += 2)
            {
                Vector2 vector2 = new Vector2(-velocity.Y, velocity.X) * i;
                vector2.RotatedBy(velocity.ToRotation());
                Projectile.NewProjectile(source, player.Center + vector2 * 2, velocity, ModContent.ProjectileType<Projectiles.Misc.EnchantingPro>(), damage, knockback, player.whoAmI, 1);
            }
            return false;
        }
    }
}
