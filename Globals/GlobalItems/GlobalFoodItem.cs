using Terraria.DataStructures;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using OdeMod.Players;
using System.Collections.Generic;

namespace OdeMod.Globals.GlobalItems
{
    internal class GlobalFoodItem : GlobalItem
    {
        public static int InappetenceItem_Type;
        public static int a = 0;
        public override bool CanUseItem(Item item, Player player)
        {
            
            if (InappetenceItem_Type != 0)
            {
                if (item.type == InappetenceItem_Type)
                {
                    return false;
                }
            }
            return base.CanUseItem(item, player);
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (InappetenceItem_Type != 0)
            {
                if (item.type == InappetenceItem_Type)
                {
                    var line = new TooltipLine(Mod, "Inappetence", "无法使用");
                    tooltips.Add(line);
                }
                base.ModifyTooltips(item, tooltips);
            }
        }
        public static void SetInappetence(Item item)
        {
            InappetenceItem_Type = item.type;
        }
    }
}
