using OdeMod.CardMode.Scenes.ConfigScene.ConfigSystem.Attributes;

namespace OdeMod.CardMode.Scenes.ConfigScene.ConfigSystem.Configs
{
    public struct FF
    {
        [FieldConfig("RR", "RRRRR")]
        [ConfigFloatRange(0f, 100f)]
        public float RR;
        [FieldConfig("YY", "YYYYYY")]
        [ConfigFloatRange(0f, 100f)]
        public float YY;
    }
    internal class MusicConfig : ConfigBase
    {
        public override string Name => "音乐设置";

        public override string SaveName => "MusicConfig";

        [FieldConfig("音量", "调整这个设置会改变你的音量大小")]
        [ConfigFloatRange(0f, 100f)]
        public float Volume = 50f;
        [FieldConfig("方法", "发放")]
        public FF FF;
    }
}