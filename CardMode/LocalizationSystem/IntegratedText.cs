using System.Collections.Generic;

using OdeMod.CardMode.LocalizationSystem.Languages;

namespace OdeMod.CardMode.LocalizationSystem
{
    internal class IntegratedText
    {
        private Dictionary<LanguageBase, string> texts = new Dictionary<LanguageBase, string>();
        private readonly string _key = string.Empty;

        public IntegratedText(string key)
        {
            _key = key;
        }

        public string this[LanguageBase language]
        {
            get
            {
                return texts.ContainsKey(language) ? texts[language] : _key;
            }
            set
            {
                if (!texts.ContainsKey(language))
                    texts.Add(language, value);
                else
                    texts[language] = value;
            }
        }
    }
}