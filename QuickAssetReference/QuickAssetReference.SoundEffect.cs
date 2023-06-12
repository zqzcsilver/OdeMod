using ReLogic.Content;
using Microsoft.Xna.Framework.Audio;

namespace OdeMod.QuickAssetReference;
public static class ModAssets_SoundEffect
{
    public static class SoundEffects
    {
        public static Asset<SoundEffect> TestAsset => ModAssets_Utils.Mod.Assets.Request<SoundEffect>(TestPath);
        public static Asset<SoundEffect> TestImmediateAsset => ModAssets_Utils.Mod.Assets.Request<SoundEffect>(TestPath, AssetRequestMode.ImmediateLoad);
        public static string TestPath = "Sounds/SoundEffects/Test";
    }

}

