using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;

using Newtonsoft.Json.Linq;

using OdeMod.CardMode.LocalizationSystem.Languages;
using OdeMod.CardMode.Scenes.ConfigScene.ConfigSystem.Configs;
using OdeMod.Utils;

namespace OdeMod.CardMode.LocalizationSystem
{
    internal class LocalizationSystem
    {
        private Dictionary<string, IntegratedText> _languages = new Dictionary<string, IntegratedText>();

        private IntegratedText this[string key]
        {
            get
            {
                if (!_languages.ContainsKey(key))
                    _languages.Add(key, new IntegratedText(key));
                return _languages[key];
            }
        }

        public static LanguageBase NowLanguage
        {
            get => LanguageHelper.GetTextSuffix(CardSystem.ConfigManager.GetConfig<InterfaceConfig>().Laguage);
        }

        public void Load()
        {
            LanguageBase.Load();
        }

        public string GetTextValue(string key) => this[key][NowLanguage];

        public void LoadLocalizationFromFile(string path, LanguageBase language)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException(string.Empty, path);
            if (path.EndsWith(".xml"))
            {
                XElement xElement = XElement.Load(File.OpenRead(path));
                addLocalizationFromXElement(Path.GetFileNameWithoutExtension(path), language, xElement);
            }
            else if (path.EndsWith(".json"))
            {
                LoadLocalizationFromJson(Path.GetFileNameWithoutExtension(path), File.ReadAllText(path), language);
            }
        }

        public void LoadLocalizationFromXML(string key, string xmlContent, LanguageBase language)
        {
            LoadLocalizationFromXML(key, Encoding.UTF8.GetBytes(xmlContent), language);
        }

        public void LoadLocalizationFromJson(string key, string jsonContent, LanguageBase language)
        {
            JObject jobj = JObject.Parse(jsonContent);
            addLocalizationFromJToken(key, language, jobj);
        }

        public void LoadLocalizationFromXML(string key, byte[] bytes, LanguageBase language)
        {
            using Stream stream = new MemoryStream(bytes);
            XElement xElement = XElement.Load(stream);
            addLocalizationFromXElement(key, language, xElement);
        }

        public void LoadLocalizationFromJson(string key, byte[] bytes, LanguageBase language)
        {
            LoadLocalizationFromJson(key, Encoding.UTF8.GetString(bytes), language);
        }

        public void LoadLocalizationFromXML(string key, Stream stream, LanguageBase language)
        {
            XElement xElement = XElement.Load(stream);
            addLocalizationFromXElement(key, language, xElement);
        }

        public void LoadLocalizationFromJson(string key, Stream stream, LanguageBase language)
        {
            using StreamReader streamReader = new StreamReader(stream);
            LoadLocalizationFromJson(key, streamReader.ReadToEnd(), language);
        }

        private void addLocalizationFromJToken(string key, LanguageBase language, JToken jToken)
        {
            if (jToken == null)
                return;
            if (jToken is JObject jObject)
            {
                foreach (var item in jObject)
                {
                    addLocalizationFromJToken($"{key}.{item.Key}", language, item.Value);
                }
            }
            else if (jToken is JArray jArray)
            {
                foreach (var item in jArray)
                {
                    addLocalizationFromJToken(key, language, item);
                }
            }
            else
            {
                this[key][language] = jToken.ToString();
            }
        }

        private void addLocalizationFromXElement(string key, LanguageBase language, XElement xElement)
        {
            if (xElement == null)
                return;
            if (xElement.HasElements)
            {
                foreach (var xe in xElement.Elements())
                {
                    addLocalizationFromXElement($"{key}.{xElement.Name.LocalName}", language, xe);
                }
            }
            else if (!string.IsNullOrEmpty(xElement.Value))
                this[$"{key}.{xElement.Name.LocalName}"][language] = xElement.Value;
        }
    }
}