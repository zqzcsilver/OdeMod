using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace OdeMod.Items.Misc.Weapons
{
    internal class ShadowGhost : ModItem, IMiscItem
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 56;
            Item.height = 58;
            Item.DamageType = DamageClass.Melee;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.damage = 47;
            Item.crit = 4;
            Item.shoot = ModContent.ProjectileType<Projectiles.Misc.StickyGhost>();
            Item.shootSpeed = 8f;
            Item.useAnimation = 24;
            Item.useTime = 24;
        }
    }
}