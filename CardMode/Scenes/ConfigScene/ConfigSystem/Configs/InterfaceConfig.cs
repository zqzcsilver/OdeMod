using System.Collections.Generic;

using FontStashSharp;

using OdeMod.CardMode.Scenes.ConfigScene.ConfigSystem.Attributes;
using OdeMod.Utils;

namespace OdeMod.CardMode.Scenes.ConfigScene.ConfigSystem.Configs
{
    internal class InterfaceConfig : ConfigBase
    {
        private const string LOCALIZATION_BASE_PATH = "$Configs.InterfaceConfig";
        public override string Name => $"{LOCALIZATION_BASE_PATH}.Title";
        public FontSystem Font => OdeMod.FontManager[$"Fonts/{LanguageHelper.GetTextSuffix(FontName)}.ttf"];
        public string TextureStyleName => LanguageHelper.GetTextSuffix(TextureStyle);
        public override string SaveName => "InterfaceConfig";

        [FieldConfig($"{LOCALIZATION_BASE_PATH}.FontName.Name", $"{LOCALIZATION_BASE_PATH}.FontName.Description")]
        [ConfigStringRange($"{LOCALIZATION_BASE_PATH}.FontName.Options.SourceHanSansHWSC_VF")]
        public string FontName = $"{LOCALIZATION_BASE_PATH}.FontName.Options.SourceHanSansHWSC_VF";

        [FieldConfig($"{LOCALIZATION_BASE_PATH}.Laguage.Name", $"{LOCALIZATION_BASE_PATH}.Laguage.Description")]
        [ConfigStringRange(
            $"{LOCALIZATION_BASE_PATH}.Laguage.Options.Chinese(Simplified)",
            $"{LOCALIZATION_BASE_PATH}.Laguage.Options.Chinese(Traditional)",
            $"{LOCALIZATION_BASE_PATH}.Laguage.Options.English")]
        public string Laguage = $"{LOCALIZATION_BASE_PATH}.Laguage.Options.Chinese(Simplified)";

        [FieldConfig($"{LOCALIZATION_BASE_PATH}.AnimationSpeed.Name", $"{LOCALIZATION_BASE_PATH}.AnimationSpeed.Description")]
        [ConfigFloatRange(0.1f, 2f)]
        public float AnimationSpeed = 1f;

        [FieldConfig($"{LOCALIZATION_BASE_PATH}.TextureStyle.Name", $"{LOCALIZATION_BASE_PATH}.TextureStyle.Description")]
        [ConfigStringRange($"{LOCALIZATION_BASE_PATH}.TextureStyle.Options.Original")]
        public string TextureStyle = $"{LOCALIZATION_BASE_PATH}.TextureStyle.Options.Original";
    }
}