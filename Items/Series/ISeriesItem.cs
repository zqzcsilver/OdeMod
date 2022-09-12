namespace OdeMod.Items.Series
{
    /// <summary>
    /// 表示形成一个系列的物品的接口，系列物品的接口请继承此接口。属于<see cref="IOdeItem"/>
    /// </summary>
    internal interface ISeriesItem : IOdeItem
    {
        public string SeriesName()
        {
            return "NoneSeries";
        }
    }
}
