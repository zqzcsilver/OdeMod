using Terraria.ModLoader;

namespace OdeMod.Items.Series.Drawer
{
    /// <summary>
    /// ��ʦϵ����Ʒ��̳д˽ӿڡ�����<see cref="ISeriesItem"/>
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
