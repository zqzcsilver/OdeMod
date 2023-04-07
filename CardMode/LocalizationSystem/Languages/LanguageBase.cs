using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using OdeMod.CardMode.Scenes;

using static System.Formats.Asn1.AsnWriter;

namespace OdeMod.CardMode.LocalizationSystem.Languages
{
    internal abstract class LanguageBase
    {
        private static List<LanguageBase> languages = new List<LanguageBase>();

        /// <summary>
        /// 名字
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// 缩写名
        /// </summary>
        public abstract string AbbreviatedName { get; }

        /// <summary>
        /// 语言ID
        /// </summary>
        public abstract int ID { get; }

        public static void Load()
        {
            languages.Clear();
            foreach (Type type in Assembly.GetExecutingAssembly().GetTypes().OrderBy(
                (Type t) => t.FullName, StringComparer.InvariantCulture))
            {
                if (!type.IsAbstract && type.IsSubclassOf(typeof(LanguageBase)))
                {
                    languages.Add((LanguageBase)Activator.CreateInstance(type));
                }
            }
        }

        public static LanguageBase FromAbbreviatedName(string abbreviatedName)
        {
            return languages.Find(x => x.AbbreviatedName == abbreviatedName);
        }

        public static implicit operator LanguageBase(string name)
        {
            return languages.Find(x => x.Name == name);
        }

        public static implicit operator LanguageBase(int id)
        {
            return languages.Find(x => x.ID == id);
        }
    }
}