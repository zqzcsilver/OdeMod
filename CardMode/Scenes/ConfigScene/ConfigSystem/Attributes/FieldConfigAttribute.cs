using System;

namespace OdeMod.CardMode.Scenes.ConfigScene.ConfigSystem.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    internal class FieldConfigAttribute : Attribute
    {
        public readonly string Tip;
        public readonly string Description;
        public FieldConfigAttribute(string tip, string description)
        {
            Tip = tip;
            Description = description;
        }
    }
}