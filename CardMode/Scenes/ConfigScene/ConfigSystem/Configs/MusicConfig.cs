using OdeMod.CardMode.Scenes.ConfigScene.ConfigSystem.Attributes;

namespace OdeMod.CardMode.Scenes.ConfigScene.ConfigSystem.Configs
{
    internal class MusicConfig : ConfigBase
    {
        private const string LOCALIZATION_BASE_PATH = "$Configs.MusicConfig";
        public override string Name => $"{LOCALIZATION_BASE_PATH}.Title";

        public override string SaveName => "MusicConfig";

        [FieldConfig($"{LOCALIZATION_BASE_PATH}.EnableMusic.Name", $"{LOCALIZATION_BASE_PATH}.EnableMusic.Description")]
        [ConfigBool]
        public bool EnableMusic = true;

        [FieldConfig($"{LOCALIZATION_BASE_PATH}.EnableSound.Name", $"{LOCALIZATION_BASE_PATH}.EnableSound.Description")]
        [ConfigBool]
        public bool EnableSound = true;

        [FieldConfig($"{LOCALIZATION_BASE_PATH}.MainVolume.Name", $"{LOCALIZATION_BASE_PATH}.MainVolume.Description")]
        [ConfigFloatRange(0f, 1f)]
        public float MainVolume = 1f;

        [FieldConfig($"{LOCALIZATION_BASE_PATH}.MusicVolume.Name", $"{LOCALIZATION_BASE_PATH}.MusicVolume.Description")]
        [ConfigFloatRange(0f, 1f)]
        public float MusicVolume = 1f;

        [FieldConfig($"{LOCALIZATION_BASE_PATH}.SoundVolume.Name", $"{LOCALIZATION_BASE_PATH}.SoundVolume.Description")]
        [ConfigFloatRange(0f, 1f)]
        public float SoundVolume = 1f;
    }
}