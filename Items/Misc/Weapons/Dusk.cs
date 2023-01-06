using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Items.Misc.Weapons
{
    internal class Dusk : ModItem, IMiscItem
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
            //Item.shoot = ModContent.ProjectileType<Projectiles.Misc.???>();
            //Item.shootSpeed = 40f;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.value = Item.sellPrice(0, 5, 0, 0);
        }
    }
}
