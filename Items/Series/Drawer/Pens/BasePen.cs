using Microsoft.Xna.Framework;

using OdeMod.Players;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static OdeMod.QuickAssetReference.ModAssets_Hjson;

namespace OdeMod.Items.Series.Drawer.Pens
{
    internal abstract class BasePen : ModItem, IPens
    {
        internal int BaseDamage = 0;
        //internal string BaseColor;

        public override void SetDefaults()
        {
            
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            base.ModifyTooltips(tooltips);
            string tooltipText2 = $" {BaseDamage} 基准伤害 ";
            //string tooltipText3 = $"{BaseColor} 画笔颜色";
            tooltips.Add(new TooltipLine(base.Mod, "BaseDamage", tooltipText2));
            //tooltips.Add(new TooltipLine(base.Mod, "Type", "画笔颜色"));
        }
    }
}