using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

using OdeMod.CardMode.KeyBindSystem;
using OdeMod.CardMode.Scenes.ConfigScene.ConfigSystem.Attributes;
using OdeMod.CardMode.Scenes.ConfigScene.ConfigSystem.Configs;
using OdeMod.CardMode.Scenes.ConfigScene.UIElements;
using OdeMod.UI.OdeUISystem.UIElements;

namespace OdeMod.CardMode.Scenes.ConfigScene.ConfigSystem
{
    //做梦梦到的
    internal abstract class ConfigBase
    {
        public ConfigBase()
        { }

        /// <summary>
        /// 当前设置的储存路径
        /// </summary>
        public string SavePath { get => Path.Combine(ConfigManager.SavePath, $"{SaveName}.config"); }

        /// <summary>
        /// 设置名字
        /// </summary>
        public abstract string Name { get; }

        public abstract string SaveName { get; }
        public virtual List<string> ConfigOrder { get; }

        /// <summary>
        /// 设置的页面
        /// </summary>
        /// <returns></returns>
        public virtual BaseElement GetPage()
        {
            var fields = Array.FindAll(GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance),
                        field => field.GetCustomAttribute<FieldConfigAttribute>() != null);
            var configOrder = ConfigOrder;

            if (fields.Length == 0)
                return null;

            if (!(configOrder == null || configOrder.Count == 0))
            {
                Array.Sort(fields, (f1, f2) =>
                {
                    int i1 = configOrder.IndexOf(f1.Name);
                    int i2 = configOrder.IndexOf(f2.Name);
                    if (i1 == -1 && i2 == i1)
                        return 0;
                    if (i1 == -1)
                        return 1;
                    if (i2 == -1)
                        return -1;
                    return i1 < i2 ? -1 : 1;
                });
            }

            BaseElement op = new BaseElement();
            op.Info.Width.SetValue(0f, 1f);

            UIText title = new UIText(Name, CardSystem.ConfigManager.GetConfig<InterfaceConfig>().Font.GetFont(60f));
            title.Info.Left.SetValue(-title.Info.Width.Pixel / 2f, 0.5f);
            title.Info.Top.SetValue(0f, 0f);
            op.Register(title);
            op.Info.Height = title.Info.Height + new BaseElement.PositionStyle(60f, 0f);

            List<BaseElement> elements = new List<BaseElement>();
            Array.ForEach(fields, field =>
            {
                var c = GetConfigStyle(field, this);
                if (c != null)
                    elements.Add(c);
            });
            if (elements.Count > 0)
            {
                op.Info.Height += new BaseElement.PositionStyle(5f, 0f);
                for (int i = 0; i < elements.Count; i++)
                {
                    BaseElement element = elements[i];
                    element.Info.Top = op.Info.Height;
                    op.Register(element);
                    op.Info.Height += element.Info.Height + new BaseElement.PositionStyle(10f, 0f);
                }
            }
            //op.Info.Height -= elements[elements.Count - 1].Info.Height + new BaseElement.PositionStyle(5f, 0f);

            return op;
        }

        protected virtual BaseElement GetConfigStyle(FieldInfo fieldInfo, object obj)
        {
            BaseElement baseElement = new BaseElement();
            var t = fieldInfo.FieldType;
            var info = fieldInfo.GetCustomAttribute<FieldConfigAttribute>();
            if (info == null)
                return null;
            if (t == typeof(byte))
            {
                var attribute = fieldInfo.GetCustomAttribute<ConfigByteRangeAttribute>();
                if (attribute != null)
                {
                    baseElement = new UIByteSeekBarConfig(info.Tip, info.Description, obj, fieldInfo, attribute.Max, attribute.Min);
                }
            }
            else if (t == typeof(char))
            {
                var attribute = fieldInfo.GetCustomAttribute<ConfigCharRangeAttribute>();
                if (attribute != null)
                {
                    baseElement = new UICharSeekBarConfig(info.Tip, info.Description, obj, fieldInfo, attribute.Max, attribute.Min);
                }
            }
            else if (t == typeof(decimal))
            {
                var attribute = fieldInfo.GetCustomAttribute<ConfigDecimalRangeAttribute>();
                if (attribute != null)
                {
                    baseElement = new UIDecimalSeekBarConfig(info.Tip, info.Description, obj, fieldInfo, attribute.Max, attribute.Min);
                }
            }
            else if (t == typeof(double))
            {
                var attribute = fieldInfo.GetCustomAttribute<ConfigDoubleRangeAttribute>();
                if (attribute != null)
                {
                    baseElement = new UIDoubleSeekBarConfig(info.Tip, info.Description, obj, fieldInfo, attribute.Max, attribute.Min);
                }
            }
            else if (t == typeof(float))
            {
                var attribute = fieldInfo.GetCustomAttribute<ConfigFloatRangeAttribute>();
                if (attribute != null)
                {
                    baseElement = new UIFloatSeekBarConfig(info.Tip, info.Description, obj, fieldInfo, attribute.Max, attribute.Min);
                }
            }
            else if (t == typeof(int))
            {
                var attribute = fieldInfo.GetCustomAttribute<ConfigIntRangeAttribute>();
                if (attribute != null)
                {
                    baseElement = new UIIntSeekBarConfig(info.Tip, info.Description, obj, fieldInfo, attribute.Max, attribute.Min);
                }
            }
            else if (t == typeof(long))
            {
                var attribute = fieldInfo.GetCustomAttribute<ConfigLongRangeAttribute>();
                if (attribute != null)
                {
                    baseElement = new UILongSeekBarConfig(info.Tip, info.Description, obj, fieldInfo, attribute.Max, attribute.Min);
                }
            }
            else if (t == typeof(sbyte))
            {
                var attribute = fieldInfo.GetCustomAttribute<ConfigSbyteRangeAttribute>();
                if (attribute != null)
                {
                    baseElement = new UISbyteSeekBarConfig(info.Tip, info.Description, obj, fieldInfo, attribute.Max, attribute.Min);
                }
            }
            else if (t == typeof(short))
            {
                var attribute = fieldInfo.GetCustomAttribute<ConfigShortRangeAttribute>();
                if (attribute != null)
                {
                    baseElement = new UIShortSeekBarConfig(info.Tip, info.Description, obj, fieldInfo, attribute.Max, attribute.Min);
                }
            }
            else if (t == typeof(string))
            {
                var attribute = fieldInfo.GetCustomAttribute<ConfigStringRangeAttribute>();
                if (attribute != null)
                {
                    baseElement = new UIStringSelectBarConfig(info.Tip, info.Description, obj, fieldInfo, attribute.Strings);
                }
            }
            else if (t == typeof(uint))
            {
                var attribute = fieldInfo.GetCustomAttribute<ConfigUintRangeAttribute>();
                if (attribute != null)
                {
                    baseElement = new UIUintSeekBarConfig(info.Tip, info.Description, obj, fieldInfo, attribute.Max, attribute.Min);
                }
            }
            else if (t == typeof(ulong))
            {
                var attribute = fieldInfo.GetCustomAttribute<ConfigUlongRangeAttribute>();
                if (attribute != null)
                {
                    baseElement = new UIUlongSeekBarConfig(info.Tip, info.Description, obj, fieldInfo, attribute.Max, attribute.Min);
                }
            }
            else if (t == typeof(ushort))
            {
                var attribute = fieldInfo.GetCustomAttribute<ConfigUshortRangeAttribute>();
                if (attribute != null)
                {
                    baseElement = new UIUshortSeekBarConfig(info.Tip, info.Description, obj, fieldInfo, attribute.Max, attribute.Min);
                }
            }
            else if (t == typeof(bool))
            {
                var attribute = fieldInfo.GetCustomAttribute<ConfigBoolAttribute>();
                if (attribute != null)
                {
                    baseElement = new UIBoolConfig(info.Tip, info.Description, obj, fieldInfo);
                }
            }
            else if (t == typeof(KeyGroup))
            {
                var attribute = fieldInfo.GetCustomAttribute<ConfigKeyGroupAttribute>();
                if (attribute != null)
                {
                    baseElement = new UIKeyGroupConfig(info.Tip, info.Description, obj, fieldInfo);
                }
            }
            else
            {
                var o = fieldInfo.GetValue(obj);
                var fields = Array.FindAll(o.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance),
                        field => field.GetCustomAttribute<FieldConfigAttribute>() != null);
                List<BaseElement> elements = new List<BaseElement>();
                Array.ForEach(fields, field =>
                {
                    elements.Add(GetConfigStyle(field, o));
                });
                if (elements.Count > 0)
                {
                    baseElement.Info.Height.SetValue(5f, 0f);
                    for (int i = 0; i < elements.Count; i++)
                    {
                        BaseElement element = elements[i];
                        element.Info.Top = baseElement.Info.Height;
                        baseElement.Register(element);
                        baseElement.Info.Height += element.Info.Height + new BaseElement.PositionStyle(10f, 0f);
                    }
                    //baseElement.Info.Height -= elements[elements.Count - 1].Info.Height + new BaseElement.PositionStyle(5f, 0f);
                }
            }
            baseElement.Info.Width.SetValue(0f, 1f);
            if (baseElement.Info.Height.Pixel == 0 && baseElement.Info.Height.Percent == 0)
            {
                baseElement.Info.Height.Pixel = 40f;
            }

            return baseElement;
        }

        protected virtual object ReadParameterAdjustment(FieldInfo fieldInfo, object obj)
        {
            var t = fieldInfo.FieldType;
            var info = fieldInfo.GetCustomAttribute<FieldConfigAttribute>();
            if (info == null)
                return null;
            if (t == typeof(byte))
            {
                var attribute = fieldInfo.GetCustomAttribute<ConfigByteRangeAttribute>();
                if (attribute != null)
                {
                    if (obj is byte objByte)
                    {
                        if (objByte < attribute.Min)
                            return attribute.Min;
                        if (objByte > attribute.Max)
                            return attribute.Max;
                    }
                }
            }
            else if (t == typeof(char))
            {
                var attribute = fieldInfo.GetCustomAttribute<ConfigCharRangeAttribute>();
                if (attribute != null)
                {
                    if (obj is char objChar)
                    {
                        if (objChar < attribute.Min)
                            return attribute.Min;
                        if (objChar > attribute.Max)
                            return attribute.Max;
                    }
                }
            }
            else if (t == typeof(decimal))
            {
                var attribute = fieldInfo.GetCustomAttribute<ConfigDecimalRangeAttribute>();
                if (attribute != null)
                {
                    if (obj is decimal objDecimal)
                    {
                        if (objDecimal < attribute.Min)
                            return attribute.Min;
                        if (objDecimal > attribute.Max)
                            return attribute.Max;
                    }
                }
            }
            else if (t == typeof(double))
            {
                var attribute = fieldInfo.GetCustomAttribute<ConfigDoubleRangeAttribute>();
                if (attribute != null)
                {
                    if (obj is double objDouble)
                    {
                        if (objDouble < attribute.Min)
                            return attribute.Min;
                        if (objDouble > attribute.Max)
                            return attribute.Max;
                    }
                }
            }
            else if (t == typeof(float))
            {
                var attribute = fieldInfo.GetCustomAttribute<ConfigFloatRangeAttribute>();
                if (attribute != null)
                {
                    if (obj is float objFloat)
                    {
                        if (objFloat < attribute.Min)
                            return attribute.Min;
                        if (objFloat > attribute.Max)
                            return attribute.Max;
                    }
                }
            }
            else if (t == typeof(int))
            {
                var attribute = fieldInfo.GetCustomAttribute<ConfigIntRangeAttribute>();
                if (attribute != null)
                {
                    if (obj is char objInt)
                    {
                        if (objInt < attribute.Min)
                            return attribute.Min;
                        if (objInt > attribute.Max)
                            return attribute.Max;
                    }
                }
            }
            else if (t == typeof(long))
            {
                var attribute = fieldInfo.GetCustomAttribute<ConfigLongRangeAttribute>();
                if (attribute != null)
                {
                    if (obj is long objLong)
                    {
                        if (objLong < attribute.Min)
                            return attribute.Min;
                        if (objLong > attribute.Max)
                            return attribute.Max;
                    }
                }
            }
            else if (t == typeof(sbyte))
            {
                var attribute = fieldInfo.GetCustomAttribute<ConfigSbyteRangeAttribute>();
                if (attribute != null)
                {
                    if (obj is sbyte objSByte)
                    {
                        if (objSByte < attribute.Min)
                            return attribute.Min;
                        if (objSByte > attribute.Max)
                            return attribute.Max;
                    }
                }
            }
            else if (t == typeof(short))
            {
                var attribute = fieldInfo.GetCustomAttribute<ConfigShortRangeAttribute>();
                if (attribute != null)
                {
                    if (obj is short objShort)
                    {
                        if (objShort < attribute.Min)
                            return attribute.Min;
                        if (objShort > attribute.Max)
                            return attribute.Max;
                    }
                }
            }
            else if (t == typeof(string))
            {
                var attribute = fieldInfo.GetCustomAttribute<ConfigStringRangeAttribute>();
                if (attribute != null)
                {
                    if (obj is string objString && attribute.Strings.Length > 0)
                    {
                        if (string.IsNullOrEmpty(Array.Find(attribute.Strings, x => x == objString)))
                            return attribute.Strings[0];
                    }
                }
            }
            else if (t == typeof(uint))
            {
                var attribute = fieldInfo.GetCustomAttribute<ConfigUintRangeAttribute>();
                if (attribute != null)
                {
                    if (obj is uint objUInt)
                    {
                        if (objUInt < attribute.Min)
                            return attribute.Min;
                        if (objUInt > attribute.Max)
                            return attribute.Max;
                    }
                }
            }
            else if (t == typeof(ulong))
            {
                var attribute = fieldInfo.GetCustomAttribute<ConfigUlongRangeAttribute>();
                if (attribute != null)
                {
                    if (obj is ulong objULong)
                    {
                        if (objULong < attribute.Min)
                            return attribute.Min;
                        if (objULong > attribute.Max)
                            return attribute.Max;
                    }
                }
            }
            else if (t == typeof(ushort))
            {
                var attribute = fieldInfo.GetCustomAttribute<ConfigUshortRangeAttribute>();
                if (attribute != null)
                {
                    if (obj is ushort objUShort)
                    {
                        if (objUShort < attribute.Min)
                            return attribute.Min;
                        if (objUShort > attribute.Max)
                            return attribute.Max;
                    }
                }
            }
            return obj;
        }

        public virtual void Init()
        {
        }

        /// <summary>
        /// 保存设置
        /// </summary>
        public virtual void SaveConfig()
        {
            using (var stream = new FileStream(SavePath, FileMode.Create))
            {
                using (BinaryWriter binaryWriter = new BinaryWriter(stream))
                {
                    var fields = Array.FindAll(GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance),
                        field => field.GetCustomAttribute<FieldConfigAttribute>() != null);
                    OdeMod.BinaryProcessed.Save(binaryWriter, typeof(int), fields.Length);

                    foreach (var field in fields)
                    {
                        FieldSave(binaryWriter, field.GetValue(this), field);
                    }
                }
            }
        }

        /// <summary>
        /// 读取设置
        /// </summary>
        public virtual void LoadConfig()
        {
            if (!File.Exists(SavePath))
                return;

            try
            {
                using (var stream = new FileStream(SavePath, FileMode.Open))
                {
                    using (BinaryReader binaryReader = new BinaryReader(stream))
                    {
                        int length = (int)OdeMod.BinaryProcessed.Load(binaryReader);
                        Dictionary<string, object> loaders = new Dictionary<string, object>();
                        object o;
                        for (int i = 0; i < length; i++)
                        {
                            o = FieldLoad(binaryReader, out string name);
                            loaders.Add(name, o);
                        }

                        var fields = GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                        foreach (var field in fields)
                        {
                            if (field.GetCustomAttribute<FieldConfigAttribute>() != null && loaders.ContainsKey(field.Name))
                            {
                                field.SetValue(this, ReadParameterAdjustment(field, loaders[field.Name]));
                            }
                        }
                    }
                }
            }
            catch
            {
                File.Delete(SavePath);
            }
        }

        protected virtual void FieldSave(BinaryWriter binaryWriter, object o, FieldInfo field)
        {
            binaryWriter.Write(field.Name);
            OdeMod.BinaryProcessed.SafeSave(binaryWriter, field.FieldType, o);
        }

        protected virtual object FieldLoad(BinaryReader binaryReader, out string name)
        {
            name = binaryReader.ReadString();
            return OdeMod.BinaryProcessed.SafeLoad(binaryReader);
        }
    }
}