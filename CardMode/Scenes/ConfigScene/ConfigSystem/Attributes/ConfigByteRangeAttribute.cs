using System;

namespace OdeMod.CardMode.Scenes.ConfigScene.ConfigSystem.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    internal class ConfigByteRangeAttribute : Attribute
    {
        public readonly byte Min;
        public readonly byte Max;
        public ConfigByteRangeAttribute(byte min, byte max)
        {
            if (max < min)
                throw new Exception($"The max value:{max} is less than the min value:{min}");
            Min = min;
            Max = max;
        }
    }
}