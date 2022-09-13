using OdeMod.Items.Series;

using System.Collections.Generic;

using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace OdeMod.Globals.GlobalItems
{
    internal class SeriesTooltip : GlobalItem, IGlobalItem
    {
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            base.ModifyTooltips(item, tooltips);
            if (item.ModItem == null || !(item.ModItem is ISeriesItem))
                return;
            int index = tooltips.FindIndex(x => x.Name == "Tooltip0");
            var seriesName = Language.GetTextValue($"Mods.OdeMod.SeriesName.{((ISeriesItem)item.ModItem).SeriesName()}");
            if (index != -1)
                tooltips.Insert(index, new TooltipLine(Mod, "SeriesName", $"[{seriesName}]"));
            else
                tooltips.Add(new TooltipLine(Mod, "SeriesName", $"[{seriesName}]"));
        }
    }
}
