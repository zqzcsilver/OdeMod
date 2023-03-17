using System;

namespace OdeMod.CardMode.Scenes.ConfigScene.ConfigSystem.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    internal class ConfigUintRangeAttribute : Attribute
    {
        public readonly uint Min;
        public readonly uint Max;
        public ConfigUintRangeAttribute(uint min, uint max)
        {
            if (max < min)
                throw new Exception($"The max value:{max} is less than the min value:{min}");
            Min = min;
            Max = max;
        }
    }
}