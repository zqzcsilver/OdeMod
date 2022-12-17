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
    }
}
