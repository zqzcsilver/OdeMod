using System;

namespace OdeMod.CardMode.Scenes.ConfigScene.ConfigSystem.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    internal class ConfigUshortRangeAttribute : Attribute
    {
        public readonly ushort Min;
        public readonly ushort Max;
        public ConfigUshortRangeAttribute(ushort min, ushort max)
        {
            if (max < min)
                throw new Exception($"The max value:{max} is less than the min value:{min}");
            Min = min;
            Max = max;
        }
    }
}