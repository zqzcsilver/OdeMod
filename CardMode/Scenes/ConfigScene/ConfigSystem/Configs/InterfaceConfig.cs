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

        public override string SaveName => "InterfaceConfig";

        [FieldConfig("字体设置", "单击<和>可以切换不同字体")]
        [ConfigStringRange("思源黑体")]
        public string FontName = "思源黑体";

        [FieldConfig("XXX", "XXX")]
        [ConfigIntRange(0, 100)]
        public int XXX = 0;
    }
}