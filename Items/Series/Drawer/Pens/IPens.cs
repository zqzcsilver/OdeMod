using Terraria.ModLoader;

namespace OdeMod.Items.Series.Drawer
{
    /// <summary>
    /// ������̳д˽ӿڡ�����<see cref="IDrawer"/>
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
