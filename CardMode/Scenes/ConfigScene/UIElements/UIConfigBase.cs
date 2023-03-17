using System;
using System.Reflection;

using OdeMod.UI.OdeUISystem.UIElements;

namespace OdeMod.CardMode.Scenes.ConfigScene.UIElements
{
    internal abstract class UIConfigBase<T> : BaseElement
    {
        public string Tip;
        public string Description;
        protected FieldInfo FieldInfo;
        protected object Obj;

        public UIConfigBase(string tip, string description, object obj, FieldInfo fieldInfo)
        {
            Tip = tip;
            Description = description;
            if (fieldInfo == null || fieldInfo.FieldType != typeof(T))
                throw new ArgumentException($"{fieldInfo.FieldType.FullName} is not {typeof(T).FullName}");
            FieldInfo = fieldInfo;
            Obj = obj;
        }
    }
}