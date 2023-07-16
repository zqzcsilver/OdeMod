using ReLogic.Content;
using Microsoft.Xna.Framework.Graphics;

namespace OdeMod.QuickAssetReference;
public static class ModAssets_Effect
{
    public static class PixelShaders
    {
        public static Asset<Effect> BrightnessGradientAsset => ModAssets_Utils.Mod.Assets.Request<Effect>(BrightnessGradientPath);
        public static Asset<Effect> BrightnessGradientImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Effect>(BrightnessGradientPath, AssetRequestMode.ImmediateLoad);
        public const string BrightnessGradientPath = "Effects/PixelShaders/BrightnessGradient";
        public static Asset<Effect> GaussianBlurAsset => ModAssets_Utils.Mod.Assets.Request<Effect>(GaussianBlurPath);
        public static Asset<Effect> GaussianBlurImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Effect>(GaussianBlurPath, AssetRequestMode.ImmediateLoad);
        public const string GaussianBlurPath = "Effects/PixelShaders/GaussianBlur";
        public static Asset<Effect> HighlightExtractionAsset => ModAssets_Utils.Mod.Assets.Request<Effect>(HighlightExtractionPath);
        public static Asset<Effect> HighlightExtractionImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Effect>(HighlightExtractionPath, AssetRequestMode.ImmediateLoad);
        public const string HighlightExtractionPath = "Effects/PixelShaders/HighlightExtraction";
        public static Asset<Effect> MappingAsset => ModAssets_Utils.Mod.Assets.Request<Effect>(MappingPath);
        public static Asset<Effect> MappingImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Effect>(MappingPath, AssetRequestMode.ImmediateLoad);
        public const string MappingPath = "Effects/PixelShaders/Mapping";
        public static Asset<Effect> StarryAsset => ModAssets_Utils.Mod.Assets.Request<Effect>(StarryPath);
        public static Asset<Effect> StarryImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Effect>(StarryPath, AssetRequestMode.ImmediateLoad);
        public const string StarryPath = "Effects/PixelShaders/Starry";
        public static class ScreenShaders
        {
            public static Asset<Effect> SSD1Asset => ModAssets_Utils.Mod.Assets.Request<Effect>(SSD1Path);
            public static Asset<Effect> SSD1ImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Effect>(SSD1Path, AssetRequestMode.ImmediateLoad);
            public const string SSD1Path = "Effects/PixelShaders/ScreenShaders/SSD1";
        }

    }

    public static class VertexShaders
    {
        public static Asset<Effect> DrawInTriangleAsset => ModAssets_Utils.Mod.Assets.Request<Effect>(DrawInTrianglePath);
        public static Asset<Effect> DrawInTriangleImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Effect>(DrawInTrianglePath, AssetRequestMode.ImmediateLoad);
        public const string DrawInTrianglePath = "Effects/VertexShaders/DrawInTriangle";
        public static Asset<Effect> TrailAsset => ModAssets_Utils.Mod.Assets.Request<Effect>(TrailPath);
        public static Asset<Effect> TrailImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Effect>(TrailPath, AssetRequestMode.ImmediateLoad);
        public const string TrailPath = "Effects/VertexShaders/Trail";
        public static Asset<Effect> TriangleDrawAsset => ModAssets_Utils.Mod.Assets.Request<Effect>(TriangleDrawPath);
        public static Asset<Effect> TriangleDrawImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Effect>(TriangleDrawPath, AssetRequestMode.ImmediateLoad);
        public const string TriangleDrawPath = "Effects/VertexShaders/TriangleDraw";
    }

}

