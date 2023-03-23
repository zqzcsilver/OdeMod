using System;

namespace OdeMod.CardMode.Scenes.ConfigScene.ConfigSystem.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    internal class ConfigKeyGroupAttribute : Attribute
    {
        public ConfigKeyGroupAttribute()
        {
        }
    }
}