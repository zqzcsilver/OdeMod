using Terraria.ModLoader;

namespace OdeMod.Items.Series.Drawer
{
    /// <summary>
    /// 画笔请继承此接口。属于<see cref="IDrawer"/>
    /// </summary>
    internal interface IPens : IDrawer
    {
        [NoJIT]
        string ISeriesItem.SeriesName()
        {
            return "Pen";
        }
    }
}
