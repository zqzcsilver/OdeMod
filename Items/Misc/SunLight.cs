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
            DisplayName.SetDefault("曜");
            Tooltip.SetDefault("奇奇怪怪\n射出魔能弹\n穿透怪物会提升魔能弹的威力\n增加乱七八糟的奇怪能力\n似乎并不属于这个世界？");
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
