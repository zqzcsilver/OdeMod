using Terraria.ModLoader;

namespace OdeMod.Items.Series.Drawer
{
    /// <summary>
    /// 画师系列物品请继承此接口。属于<see cref="ISeriesItem"/>
    /// </summary>
    internal interface IDrawer : ISeriesItem
    {
        [NoJIT]
        string ISeriesItem.SeriesName()
        {
            return "Drawer";
        }
    }
}
