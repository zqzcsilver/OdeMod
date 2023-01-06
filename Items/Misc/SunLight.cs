using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Items.Misc
{
    internal class SunLight : ModItem,IMiscItem
    {
        public override void SetStaticDefaults()
        {
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 98;
            Item.height = 32;
            Item.DamageType = DamageClass.Ranged;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.damage = 328;
            Item.crit = 5;
            Item.shoot = ModContent.ProjectileType<Projectiles.Misc.Yao>();
            Item.shootSpeed = 40f;
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.value = Item.sellPrice(0, 10, 0, 0);
            Item.noMelee = true;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-12f, -4f);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 oldvec = velocity;
            Vector2 tVec = Vector2.Normalize(Main.MouseWorld - player.Center) * Item.shootSpeed;
            for (int i = -2; i <= 2; i++)
            {
                Vector2 tVecl = tVec + new Vector2(-tVec.Y * 0.12f, tVec.X * 0.12f) * i;
                tVecl.RotatedBy(i * 0.03);
                Projectile.NewProjectile(source, player.Center, tVecl, ModContent.ProjectileType<Projectiles.Misc.Yao>(), damage, knockback, player.whoAmI);
            }
            return false;
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            Vector2 muzzleOffset = Vector2.Normalize(velocity) * 25f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
        }
    }
}
