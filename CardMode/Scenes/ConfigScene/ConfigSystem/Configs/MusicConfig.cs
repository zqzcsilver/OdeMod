using OdeMod.CardMode.Scenes.ConfigScene.ConfigSystem.Attributes;

namespace OdeMod.CardMode.Scenes.ConfigScene.ConfigSystem.Configs
{
    internal class MusicConfig : ConfigBase
    {
        public override string Name => "音乐设置";

        public override string SaveName => "MusicConfig";

        [FieldConfig("开启音乐", "调整这个设置以开启或关闭游戏音乐")]
        [ConfigBool]
        public bool EnableMusic = true;

        [FieldConfig("开启音效", "调整这个设置以开启或关闭游戏音效")]
        [ConfigBool]
        public bool EnableSound = true;

        [FieldConfig("主音量", "调整这个设置会改变你的音量大小")]
        [ConfigFloatRange(0f, 1f)]
        public float MainVolume = 1f;

        [FieldConfig("音乐音量", "调整这个设置会改变你的音乐音量大小")]
        [ConfigFloatRange(0f, 1f)]
        public float MusicVolume = 1f;

        [FieldConfig("音效音量", "调整这个设置会改变你的音效音量大小")]
        [ConfigFloatRange(0f, 1f)]
        public float SoundVolume = 1f;
    }
}