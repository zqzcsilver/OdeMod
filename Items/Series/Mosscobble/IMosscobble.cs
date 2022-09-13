using Terraria.ModLoader;

namespace OdeMod.Items.Series.Mosscobble
{
    /// <summary>
    /// 苔石系列物品请继承此接口。属于<see cref="ISeriesItem"/>
    /// </summary>
    internal interface IMosscobble : ISeriesItem
    {
        [NoJIT]
        string ISeriesItem.SeriesName()
        {
            return "Mosscobble";
        }
    }
}
