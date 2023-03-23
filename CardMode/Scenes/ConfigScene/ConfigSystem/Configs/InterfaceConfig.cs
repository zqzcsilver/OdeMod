using System.Collections.Generic;

using FontStashSharp;

using OdeMod.CardMode.Scenes.ConfigScene.ConfigSystem.Attributes;

namespace OdeMod.CardMode.Scenes.ConfigScene.ConfigSystem.Configs
{
    internal class InterfaceConfig : ConfigBase
    {
        public override string Name => "页面设置";
        public FontSystem Font => OdeMod.FontManager[$"Fonts/{fontTable[FontName]}.ttf"];

        private static Dictionary<string, string> fontTable = new Dictionary<string, string>()
        {
            {"思源黑体","SourceHanSansHWSC-VF" }
        };

        public string TextureStyleName => textureStyleTable[TextureStyle];

        private static Dictionary<string, string> textureStyleTable = new Dictionary<string, string>()
        {
            {"原版","Original" }
        };

        public override string SaveName => "InterfaceConfig";

        [FieldConfig("字体设置", "单击<和>可以切换不同字体")]
        [ConfigStringRange("思源黑体")]
        public string FontName = "思源黑体";

        [FieldConfig("语言设置", "单击<和>可以切换不同语言")]
        [ConfigStringRange("简体中文", "繁體中文", "English")]
        public string Laguage = "简体中文";

        [FieldConfig("动画速度", "通过调整拖动条可以加快或减缓动画速度")]
        [ConfigFloatRange(0.1f, 2f)]
        public float AnimationSpeed = 1f;

        [FieldConfig("材质风格", "单击<和>可以切换不同风格")]
        [ConfigStringRange("原版")]
        public string TextureStyle = "原版";
    }
}