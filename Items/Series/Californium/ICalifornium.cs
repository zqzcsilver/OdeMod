﻿namespace OdeMod.Items.Series.Californium
{
    /// <summary>
    /// 锎系列物品请继承此接口。属于<see cref="ISeriesItem"/>
    /// </summary>
    internal interface ICalifornium : ISeriesItem
    {
        public new string SeriesName()
        {
            return "Californium";
        }
    }
}
