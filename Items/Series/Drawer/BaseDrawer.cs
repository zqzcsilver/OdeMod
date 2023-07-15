using Microsoft.Xna.Framework;

using OdeMod.Players;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static OdeMod.QuickAssetReference.ModAssets_Hjson;

namespace OdeMod.Items.Series.Drawer
{
    internal abstract class BaseDrawer : ModItem, IDrawer
    {
        internal int BaseDamage = 0;
        internal int DrawerWidth = 0;
        internal int DrawerHeight = 0;

        public override void SetDefaults()
        {
            Item.DamageType = DamageClass.Ranged;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.useTurn = false;
            Item.useStyle = ItemUseStyleID.None;
            Item.noUseGraphic = true;
            
            Item.value = Item.sellPrice(0, 6, 50, 0);
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Series.Items.Frosted.Flycutter1>();
            Item.shootSpeed = 1;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            base.ModifyTooltips(tooltips);
            string tooltipText = $"{BaseDamage} 画师基准伤害";
            tooltips.Add(new TooltipLine(base.Mod, "BaseDamage", tooltipText));
            string tooltipText2 = $"画布尺寸: {DrawerWidth}*{DrawerHeight} ";
            tooltips.Add(new TooltipLine(base.Mod, "BaseSize", tooltipText2));
        }
    }
}