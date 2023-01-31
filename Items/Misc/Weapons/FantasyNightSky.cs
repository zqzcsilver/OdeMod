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
            Item.DamageType = DamageClass.Magic;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.damage = 10;
            Item.crit = 1;
            Item.shoot = ModContent.ProjectileType<Projectiles.Misc.EnchantingPro>();
            Item.shootSpeed = 8f;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.staff[Item.type] = true;
        }
    }
}