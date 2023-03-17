using System;

namespace OdeMod.CardMode.Scenes.ConfigScene.ConfigSystem.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    internal class ConfigCharRangeAttribute : Attribute
    {
        public readonly char Min;
        public readonly char Max;
        public ConfigCharRangeAttribute(char min, char max)
        {
            if (max < min)
                throw new Exception($"The max value:{max} is less than the min value:{min}");
            Min = min;
            Max = max;
        }
    }
}