using Terraria.ModLoader;

namespace OdeMod.Items.Series.Miracle
{
    /// <summary>
    /// 辉煌系列物品的接口，属于<see cref="ISeriesItem"/>
    /// </summary>
    internal interface IMiracle : ISeriesItem
    {
        [NoJIT]
        string ISeriesItem.SeriesName()
        {
            return "Miracle";
        }
    }
}
