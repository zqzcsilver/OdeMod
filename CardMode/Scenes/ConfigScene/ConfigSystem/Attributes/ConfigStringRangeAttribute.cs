using System;

namespace OdeMod.CardMode.Scenes.ConfigScene.ConfigSystem.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    internal class ConfigStringRangeAttribute : Attribute
    {
        public readonly string[] Strings;
        public ConfigStringRangeAttribute(params string[] strings)
        {
            Strings = strings;
        }
    }
}