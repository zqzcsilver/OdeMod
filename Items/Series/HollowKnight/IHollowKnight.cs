using Terraria.ModLoader;

namespace OdeMod.Items.Series.HollowKnight
{
    /// <summary>
    /// �ն�ϵ����Ʒ��̳д˽ӿڡ�����<see cref="ISeriesItem"/>
    /// </summary>
    internal interface IHollowKnight : ISeriesItem
    {
        [NoJIT]
        string ISeriesItem.SeriesName()
        {
            return "HollowKnight";
        }
    }
}
