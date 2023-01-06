using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace OdeMod.Items.Series.Miracle
{
    /// <summary>
    /// 这个的英文名之后改改
    /// </summary>
    internal class ShiningSpirit : ModItem, IMiracle
    {
        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 58;
            Item.DamageType = DamageClass.Ranged;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.damage = 35;
            Item.crit = 4;
            Item.useAmmo = AmmoID.Arrow;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.rare = ItemRarityID.LightPurple;
            Item.noMelee = true;
            Item.value = Item.sellPrice(0, 2, 0, 0);
            base.SetDefaults();
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-12f, -4f);
        }
    }
}
