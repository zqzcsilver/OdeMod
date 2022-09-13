using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terraria.ModLoader;

namespace OdeMod.Items.Series.Recharge
{
    /// <summary>
    /// 充能饰品请继承此接口。属于<see cref="ISeriesItem"/>
    /// </summary>
    internal interface IRechargeAccessory : ISeriesItem
    {
        /// <summary>
        /// 充能速度倍速（乘算）
        /// </summary>
        public float RechargeSpeedMult { get; }
        /// <summary>
        /// 充能速度
        /// </summary>
        public float RechargeSpeedAdd { get; }
        [NoJIT]
        string ISeriesItem.SeriesName()
        {
            return "RechargeableWeapon";
        }
    }
}
