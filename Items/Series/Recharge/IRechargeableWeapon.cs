using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace OdeMod.Items.Series.Recharge
{
    /// <summary>
    /// 充能武器请继承此接口。属于<see cref="ISeriesItem"/>
    /// </summary>
    internal interface IRechargeableWeapon : ISeriesItem
    {
        /// <summary>
        /// 充能框
        /// </summary>
        public Texture2D RechargeBar { get; }
        /// <summary>
        /// 充能条的贴图
        /// </summary>
        public Texture2D RechargeBarInner { get; }
        /// <summary>
        /// 充能条的位置偏移值
        /// </summary>
        public Vector2 RechargeBarInnerOffset { get; }
        /// <summary>
        /// 充能条的截取矩形
        /// </summary>
        public Rectangle RechargeBarInnerSource { get; }
        /// <summary>
        /// 充能饰品
        /// </summary>
        public IRechargeAccessory[] RechargeAccessories { get; }
        /// <summary>
        /// 当前能量值
        /// </summary>
        public float Energy { get; set; }
        /// <summary>
        /// 最大能量
        /// </summary>
        public float EnergyMax { get; }
        /// <summary>
        /// 默认充能回复速度
        /// </summary>
        public float RechargeSpeed { get; }
        /// <summary>
        /// 能量消耗量
        /// </summary>
        public float EnergyConsumption { get; }
        /// <summary>
        /// 判断是否有足够能量供使用
        /// </summary>
        /// <returns>如果有，返回true，否则返回false</returns>
        public bool HaveEnergyToUse() => Energy >= EnergyConsumption;
        /// <summary>
        /// 储存充能饰品
        /// </summary>
        /// <param name="tag"></param>
        public void SaveRechargeAccessories(TagCompound tag)
        {
            int c = -1;
            Array.ForEach(RechargeAccessories, acc =>
            {
                c++;
                if (acc == null)
                    return;
                tag.Add($"rechargeAccessories{c}", ItemIO.Save(((ModItem)acc).Item));
            });
        }
        /// <summary>
        /// 加载充能饰品
        /// </summary>
        /// <param name="tag"></param>
        public void LoadRechargeAccessories(TagCompound tag)
        {
            for(int c = 0; c < RechargeAccessories.Length; c++)
            {
                if (tag.ContainsKey($"rechargeAccessories{c}"))
                    RechargeAccessories[c] = (IRechargeAccessory)ItemIO.Load(tag.Get<TagCompound>($"rechargeAccessories{c}")).ModItem;
            }
        }
        public new string SeriesName()
        {
            return "RechargeableWeapon";
        }
    }
}
