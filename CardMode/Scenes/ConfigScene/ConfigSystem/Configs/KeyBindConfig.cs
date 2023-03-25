using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

using Microsoft.Xna.Framework.Input;

using OdeMod.CardMode.KeyBindSystem;
using OdeMod.CardMode.Scenes.ConfigScene.ConfigSystem.Attributes;

namespace OdeMod.CardMode.Scenes.ConfigScene.ConfigSystem.Configs
{
    internal class KeyBindConfig : ConfigBase
    {
        public override string Name => "按键设置";

        public override string SaveName => "KeyBindConfig";

        [FieldConfig("打开菜单", "")]
        [ConfigKeyGroup]
        public KeyGroup OpenMenu = new KeyGroup("CardMode-OpenOpenMenu", new List<Keys> { Keys.Escape });

        [FieldConfig("打开背包", "")]
        [ConfigKeyGroup]
        public KeyGroup OpenMap = new KeyGroup("CardMode-OpenMap", new List<Keys> { Keys.M });

        [FieldConfig("打开卡组", "")]
        [ConfigKeyGroup]
        public KeyGroup OpenCardDeck = new KeyGroup("CardMode-OpenCardDeck", new List<Keys> { Keys.P });

        [FieldConfig("打开牌库", "")]
        [ConfigKeyGroup]
        public KeyGroup OpenCardPile = new KeyGroup("CardMode-OpenCardPile", new List<Keys> { Keys.C });

        [FieldConfig("打开墓地", "")]
        [ConfigKeyGroup]
        public KeyGroup OpenCardGraveyard = new KeyGroup("CardMode-OpenCardGraveyard", new List<Keys> { Keys.Z });

        public override void Init()
        {
            base.Init();
            var fields = Array.FindAll(
                GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance),
                field => field.GetCustomAttribute<FieldConfigAttribute>() != null &&
                                       field.GetCustomAttribute<ConfigKeyGroupAttribute>() != null &&
                                       field.FieldType == typeof(KeyGroup));
            Array.ForEach(fields, field =>
            {
                var kg = (KeyGroup)field.GetValue(this);
                if (CardSystem.KeyGroupManager.GetKeyGroup(kg.Name) == null)
                {
                    CardSystem.KeyGroupManager.RegisterKeyGroup(kg);
                }
            });
        }

        protected override void FieldSave(BinaryWriter binaryWriter, object o, FieldInfo field)
        {
            if (o is KeyGroup)
            {
                binaryWriter.Write(field.Name);
                var kg = (KeyGroup)o;
                kg.Save(binaryWriter);
                return;
            }
            base.FieldSave(binaryWriter, o, field);
        }

        protected override object FieldLoad(BinaryReader binaryReader, out string name)
        {
            name = binaryReader.ReadString();
            var field = GetType().GetField(name, BindingFlags.Public | BindingFlags.Instance);
            if (field.FieldType == typeof(KeyGroup))
            {
                var kg = (KeyGroup)field.GetValue(this);
                kg.Read(binaryReader);
                return kg;
            }
            return OdeMod.BinaryProcessed.SafeLoad(binaryReader);
        }
    }
}