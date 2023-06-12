using System.Linq;

using OdeMod.CardMode;

using Terraria.Localization;

namespace OdeMod.Utils
{
    internal class LanguageHelper
    {
        public static string GetPrefixTextValue(string key)
        {
            return key.StartsWith("$") ? GetTextValue(key.Substring(1, key.Length - 1)) : key;
        }

        public static string GetTextSuffix(string text) => text.Split('.').Last();

        public static string GetTextValue(string key)
        {
            if (CardSystem.Instance.CardModeVisible)
                return CardSystem.LocalizationSystem.GetTextValue(key);
            else
                return Language.GetTextValue(key);
        }
    }
}