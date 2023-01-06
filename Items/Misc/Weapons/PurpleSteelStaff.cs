using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Items.Misc.Weapons
{
    internal class PurpleSteelStaff : ModItem,IMiscItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 38;
            Item.DamageType = DamageClass.Ranged;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.damage = 10;
            Item.crit = 1;
            Item.shoot = ModContent.ProjectileType<Projectiles.Misc.PurpleSteel>();
            Item.shootSpeed = 5f;
            Item.useAnimation = 20;
            Item.useTime = 20;
            base.SetDefaults();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            for (float r = 0f; r < MathHelper.TwoPi; r += MathHelper.TwoPi / 5f)
            {
                Vector2 vel = new Vector2((float)Math.Cos(r), (float)Math.Sin(r)) * 10f;
                Projectile.NewProjectile(source, player.Center, vel, ModContent.ProjectileType<Projectiles.Misc.PurpleSteel>(), damage, knockback, player.whoAmI);
            }
            return false;
        }
    }
}
