using System;

namespace OdeMod.CardMode.Scenes.ConfigScene.ConfigSystem.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    internal class ConfigLongRangeAttribute : Attribute
    {
        public readonly long Min;
        public readonly long Max;
        public ConfigLongRangeAttribute(long min, long max)
        {
            if (max < min)
                throw new Exception($"The max value:{max} is less than the min value:{min}");
            Min = min;
            Max = max;
        }
    }
}