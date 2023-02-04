using System.ComponentModel;

using Terraria.ModLoader.Config;

namespace OdeMod.Configs
{
    [Label("卡牌模式测试设置")]
    internal class CardSystemTestConfig : ModConfig, IOdeConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [Label("绘制测试")]
        [Tooltip("启用此项以开始绘制测试")]
        [DefaultValue(false)]
        public bool EnableDrawTest;
    }
}