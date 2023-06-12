using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Items.Series.Miracle
{
    internal class CrystalSpur : ModItem, IMiracle
    {
        public override void SetDefaults()
        {
            Item.width = 56;
            Item.height = 54;
            Item.DamageType = DamageClass.Magic;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.damage = 45;
            Item.crit = 4;
            Item.knockBack = 1.5f;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.mana = 10;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Series.>();
            Item.shootSpeed = 15f;
            Item.rare = ItemRarityID.LightPurple;
            Item.value = Item.sellPrice(0, 2, 0, 0);
            Item.noMelee = true;
            Item.staff[Item.type] = true;
            base.SetDefaults();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            return false;
        }
    }
}
