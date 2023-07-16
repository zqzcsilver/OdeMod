using ReLogic.Content;
using Microsoft.Xna.Framework.Graphics;

namespace OdeMod.QuickAssetReference;
public static class ModAssets_Texture2D
{
    public static Asset<Texture2D> iconAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(iconPath);
    public static Asset<Texture2D> iconImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(iconPath, AssetRequestMode.ImmediateLoad);
    public const string iconPath = "icon";
    public static class Buffs
    {
        public static Asset<Texture2D> BrillianceAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(BrilliancePath);
        public static Asset<Texture2D> BrillianceImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(BrilliancePath, AssetRequestMode.ImmediateLoad);
        public const string BrilliancePath = "Buffs/Brilliance";
        public static Asset<Texture2D> HolySpiritCurseAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(HolySpiritCursePath);
        public static Asset<Texture2D> HolySpiritCurseImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(HolySpiritCursePath, AssetRequestMode.ImmediateLoad);
        public const string HolySpiritCursePath = "Buffs/HolySpiritCurse";
        public static Asset<Texture2D> LockedAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(LockedPath);
        public static Asset<Texture2D> LockedImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(LockedPath, AssetRequestMode.ImmediateLoad);
        public const string LockedPath = "Buffs/Locked";
        public static Asset<Texture2D> NaturalPowerAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(NaturalPowerPath);
        public static Asset<Texture2D> NaturalPowerImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(NaturalPowerPath, AssetRequestMode.ImmediateLoad);
        public const string NaturalPowerPath = "Buffs/NaturalPower";
        public static Asset<Texture2D> VitalityAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(VitalityPath);
        public static Asset<Texture2D> VitalityImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(VitalityPath, AssetRequestMode.ImmediateLoad);
        public const string VitalityPath = "Buffs/Vitality";
        public static class Foods
        {
            public static Asset<Texture2D> AddictionAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(AddictionPath);
            public static Asset<Texture2D> AddictionImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(AddictionPath, AssetRequestMode.ImmediateLoad);
            public const string AddictionPath = "Buffs/Foods/Addiction";
            public static Asset<Texture2D> HappyAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(HappyPath);
            public static Asset<Texture2D> HappyImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(HappyPath, AssetRequestMode.ImmediateLoad);
            public const string HappyPath = "Buffs/Foods/Happy";
            public static Asset<Texture2D> InappetenceAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(InappetencePath);
            public static Asset<Texture2D> InappetenceImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(InappetencePath, AssetRequestMode.ImmediateLoad);
            public const string InappetencePath = "Buffs/Foods/Inappetence";
        }

    }

    public static class Documentation
    {
        public static class OdeModDocumentation
        {
            public static class Help
            {
                public static Asset<Texture2D> imageAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(imagePath);
                public static Asset<Texture2D> imageImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(imagePath, AssetRequestMode.ImmediateLoad);
                public const string imagePath = "Documentation/OdeModDocumentation/Help/image";
                public static class icons
                {
                    public static Asset<Texture2D> iconAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(iconPath);
                    public static Asset<Texture2D> iconImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(iconPath, AssetRequestMode.ImmediateLoad);
                    public const string iconPath = "Documentation/OdeModDocumentation/Help/icons/icon";
                }

            }

            public static class icons
            {
                public static Asset<Texture2D> iconAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(iconPath);
                public static Asset<Texture2D> iconImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(iconPath, AssetRequestMode.ImmediateLoad);
                public const string iconPath = "Documentation/OdeModDocumentation/icons/icon";
            }

        }

    }

    public static class Dusts
    {
        public static Asset<Texture2D> DreamAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(DreamPath);
        public static Asset<Texture2D> DreamImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(DreamPath, AssetRequestMode.ImmediateLoad);
        public const string DreamPath = "Dusts/Dream";
        public static Asset<Texture2D> FocusAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(FocusPath);
        public static Asset<Texture2D> FocusImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(FocusPath, AssetRequestMode.ImmediateLoad);
        public const string FocusPath = "Dusts/Focus";
        public static Asset<Texture2D> TestDustAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(TestDustPath);
        public static Asset<Texture2D> TestDustImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(TestDustPath, AssetRequestMode.ImmediateLoad);
        public const string TestDustPath = "Dusts/TestDust";
        public static Asset<Texture2D> TorchAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(TorchPath);
        public static Asset<Texture2D> TorchImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(TorchPath, AssetRequestMode.ImmediateLoad);
        public const string TorchPath = "Dusts/Torch";
    }

    public static class Images
    {
        public static class Card
        {
            public static class Original
            {
                public static class Rare
                {
                    public static Asset<Texture2D> CardNameRareAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(CardNameRarePath);
                    public static Asset<Texture2D> CardNameRareImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(CardNameRarePath, AssetRequestMode.ImmediateLoad);
                    public const string CardNameRarePath = "Images/Card/Original/Rare/CardNameRare";
                    public static Asset<Texture2D> CardTipRareAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(CardTipRarePath);
                    public static Asset<Texture2D> CardTipRareImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(CardTipRarePath, AssetRequestMode.ImmediateLoad);
                    public const string CardTipRarePath = "Images/Card/Original/Rare/CardTipRare";
                }

                public static class Room
                {
                    public static Asset<Texture2D> EyeballIconAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(EyeballIconPath);
                    public static Asset<Texture2D> EyeballIconImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(EyeballIconPath, AssetRequestMode.ImmediateLoad);
                    public const string EyeballIconPath = "Images/Card/Original/Room/EyeballIcon";
                }

                public static class Scene
                {
                    public static class CharacterSelectionScene
                    {
                        public static Asset<Texture2D> SceneBackgroundAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(SceneBackgroundPath);
                        public static Asset<Texture2D> SceneBackgroundImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(SceneBackgroundPath, AssetRequestMode.ImmediateLoad);
                        public const string SceneBackgroundPath = "Images/Card/Original/Scene/CharacterSelectionScene/SceneBackground";
                        public static class UI
                        {
                            public static Asset<Texture2D> CharacterSelectionButtomAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(CharacterSelectionButtomPath);
                            public static Asset<Texture2D> CharacterSelectionButtomImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(CharacterSelectionButtomPath, AssetRequestMode.ImmediateLoad);
                            public const string CharacterSelectionButtomPath = "Images/Card/Original/Scene/CharacterSelectionScene/UI/CharacterSelectionButtom";
                        }

                    }

                    public static class ConfigScene
                    {
                        public static Asset<Texture2D> SceneBackgroundAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(SceneBackgroundPath);
                        public static Asset<Texture2D> SceneBackgroundImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(SceneBackgroundPath, AssetRequestMode.ImmediateLoad);
                        public const string SceneBackgroundPath = "Images/Card/Original/Scene/ConfigScene/SceneBackground";
                    }

                    public static class MenuScene
                    {
                        public static Asset<Texture2D> SceneBackgroundAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(SceneBackgroundPath);
                        public static Asset<Texture2D> SceneBackgroundImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(SceneBackgroundPath, AssetRequestMode.ImmediateLoad);
                        public const string SceneBackgroundPath = "Images/Card/Original/Scene/MenuScene/SceneBackground";
                    }

                }

                public static class Summoner
                {
                    public static Asset<Texture2D> CardBodyAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(CardBodyPath);
                    public static Asset<Texture2D> CardBodyImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(CardBodyPath, AssetRequestMode.ImmediateLoad);
                    public const string CardBodyPath = "Images/Card/Original/Summoner/CardBody";
                    public static Asset<Texture2D> CardCostAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(CardCostPath);
                    public static Asset<Texture2D> CardCostImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(CardCostPath, AssetRequestMode.ImmediateLoad);
                    public const string CardCostPath = "Images/Card/Original/Summoner/CardCost";
                    public static Asset<Texture2D> CardIllustrationAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(CardIllustrationPath);
                    public static Asset<Texture2D> CardIllustrationImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(CardIllustrationPath, AssetRequestMode.ImmediateLoad);
                    public const string CardIllustrationPath = "Images/Card/Original/Summoner/CardIllustration";
                    public static Asset<Texture2D> CardNameAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(CardNamePath);
                    public static Asset<Texture2D> CardNameImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(CardNamePath, AssetRequestMode.ImmediateLoad);
                    public const string CardNamePath = "Images/Card/Original/Summoner/CardName";
                    public static Asset<Texture2D> CardSampleAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(CardSamplePath);
                    public static Asset<Texture2D> CardSampleImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(CardSamplePath, AssetRequestMode.ImmediateLoad);
                    public const string CardSamplePath = "Images/Card/Original/Summoner/CardSample";
                    public static Asset<Texture2D> CardTipAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(CardTipPath);
                    public static Asset<Texture2D> CardTipImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(CardTipPath, AssetRequestMode.ImmediateLoad);
                    public const string CardTipPath = "Images/Card/Original/Summoner/CardTip";
                }

            }

        }

        public static class Effects
        {
            public static Asset<Texture2D> ballselfAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(ballselfPath);
            public static Asset<Texture2D> ballselfImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(ballselfPath, AssetRequestMode.ImmediateLoad);
            public const string ballselfPath = "Images/Effects/ballself";
            public static Asset<Texture2D> DecrateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(DecratePath);
            public static Asset<Texture2D> DecrateImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(DecratePath, AssetRequestMode.ImmediateLoad);
            public const string DecratePath = "Images/Effects/Decrate";
            public static Asset<Texture2D> Extra_189Asset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(Extra_189Path);
            public static Asset<Texture2D> Extra_189ImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(Extra_189Path, AssetRequestMode.ImmediateLoad);
            public const string Extra_189Path = "Images/Effects/Extra_189";
            public static Asset<Texture2D> Extra_190Asset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(Extra_190Path);
            public static Asset<Texture2D> Extra_190ImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(Extra_190Path, AssetRequestMode.ImmediateLoad);
            public const string Extra_190Path = "Images/Effects/Extra_190";
            public static Asset<Texture2D> Extra_197Asset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(Extra_197Path);
            public static Asset<Texture2D> Extra_197ImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(Extra_197Path, AssetRequestMode.ImmediateLoad);
            public const string Extra_197Path = "Images/Effects/Extra_197";
            public static Asset<Texture2D> Extra_198Asset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(Extra_198Path);
            public static Asset<Texture2D> Extra_198ImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(Extra_198Path, AssetRequestMode.ImmediateLoad);
            public const string Extra_198Path = "Images/Effects/Extra_198";
            public static Asset<Texture2D> Extra_199Asset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(Extra_199Path);
            public static Asset<Texture2D> Extra_199ImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(Extra_199Path, AssetRequestMode.ImmediateLoad);
            public const string Extra_199Path = "Images/Effects/Extra_199";
            public static Asset<Texture2D> Extra_200Asset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(Extra_200Path);
            public static Asset<Texture2D> Extra_200ImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(Extra_200Path, AssetRequestMode.ImmediateLoad);
            public const string Extra_200Path = "Images/Effects/Extra_200";
            public static Asset<Texture2D> Extra_201Asset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(Extra_201Path);
            public static Asset<Texture2D> Extra_201ImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(Extra_201Path, AssetRequestMode.ImmediateLoad);
            public const string Extra_201Path = "Images/Effects/Extra_201";
            public static Asset<Texture2D> Extra_202Asset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(Extra_202Path);
            public static Asset<Texture2D> Extra_202ImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(Extra_202Path, AssetRequestMode.ImmediateLoad);
            public const string Extra_202Path = "Images/Effects/Extra_202";
            public static Asset<Texture2D> FireBurstAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(FireBurstPath);
            public static Asset<Texture2D> FireBurstImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(FireBurstPath, AssetRequestMode.ImmediateLoad);
            public const string FireBurstPath = "Images/Effects/FireBurst";
            public static Asset<Texture2D> Flame0Asset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(Flame0Path);
            public static Asset<Texture2D> Flame0ImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(Flame0Path, AssetRequestMode.ImmediateLoad);
            public const string Flame0Path = "Images/Effects/Flame0";
            public static Asset<Texture2D> heatmapAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(heatmapPath);
            public static Asset<Texture2D> heatmapImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(heatmapPath, AssetRequestMode.ImmediateLoad);
            public const string heatmapPath = "Images/Effects/heatmap";
            public static Asset<Texture2D> heatmap2Asset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(heatmap2Path);
            public static Asset<Texture2D> heatmap2ImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(heatmap2Path, AssetRequestMode.ImmediateLoad);
            public const string heatmap2Path = "Images/Effects/heatmap2";
            public static Asset<Texture2D> heatmap3Asset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(heatmap3Path);
            public static Asset<Texture2D> heatmap3ImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(heatmap3Path, AssetRequestMode.ImmediateLoad);
            public const string heatmap3Path = "Images/Effects/heatmap3";
            public static Asset<Texture2D> heatmap4Asset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(heatmap4Path);
            public static Asset<Texture2D> heatmap4ImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(heatmap4Path, AssetRequestMode.ImmediateLoad);
            public const string heatmap4Path = "Images/Effects/heatmap4";
            public static Asset<Texture2D> heatmap5Asset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(heatmap5Path);
            public static Asset<Texture2D> heatmap5ImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(heatmap5Path, AssetRequestMode.ImmediateLoad);
            public const string heatmap5Path = "Images/Effects/heatmap5";
            public static Asset<Texture2D> heatmap6Asset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(heatmap6Path);
            public static Asset<Texture2D> heatmap6ImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(heatmap6Path, AssetRequestMode.ImmediateLoad);
            public const string heatmap6Path = "Images/Effects/heatmap6";
            public static Asset<Texture2D> LaserFxAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(LaserFxPath);
            public static Asset<Texture2D> LaserFxImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(LaserFxPath, AssetRequestMode.ImmediateLoad);
            public const string LaserFxPath = "Images/Effects/LaserFx";
            public static Asset<Texture2D> LockAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(LockPath);
            public static Asset<Texture2D> LockImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(LockPath, AssetRequestMode.ImmediateLoad);
            public const string LockPath = "Images/Effects/Lock";
            public static Asset<Texture2D> NightAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(NightPath);
            public static Asset<Texture2D> NightImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(NightPath, AssetRequestMode.ImmediateLoad);
            public const string NightPath = "Images/Effects/Night";
            public static Asset<Texture2D> SpAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(SpPath);
            public static Asset<Texture2D> SpImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(SpPath, AssetRequestMode.ImmediateLoad);
            public const string SpPath = "Images/Effects/Sp";
        }

        public static class Misc
        {
            public static Asset<Texture2D> NikoAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(NikoPath);
            public static Asset<Texture2D> NikoImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(NikoPath, AssetRequestMode.ImmediateLoad);
            public const string NikoPath = "Images/Misc/Niko";
        }

        public static class UI
        {
            public static Asset<Texture2D> HorizontalScrollbarAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(HorizontalScrollbarPath);
            public static Asset<Texture2D> HorizontalScrollbarImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(HorizontalScrollbarPath, AssetRequestMode.ImmediateLoad);
            public const string HorizontalScrollbarPath = "Images/UI/HorizontalScrollbar";
            public static Asset<Texture2D> HorizontalScrollbarInnerAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(HorizontalScrollbarInnerPath);
            public static Asset<Texture2D> HorizontalScrollbarInnerImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(HorizontalScrollbarInnerPath, AssetRequestMode.ImmediateLoad);
            public const string HorizontalScrollbarInnerPath = "Images/UI/HorizontalScrollbarInner";
            public static Asset<Texture2D> PanelAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(PanelPath);
            public static Asset<Texture2D> PanelImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(PanelPath, AssetRequestMode.ImmediateLoad);
            public const string PanelPath = "Images/UI/Panel";
            public static Asset<Texture2D> VerticalScrollbarAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(VerticalScrollbarPath);
            public static Asset<Texture2D> VerticalScrollbarImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(VerticalScrollbarPath, AssetRequestMode.ImmediateLoad);
            public const string VerticalScrollbarPath = "Images/UI/VerticalScrollbar";
            public static Asset<Texture2D> VerticalScrollbarInnerAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(VerticalScrollbarInnerPath);
            public static Asset<Texture2D> VerticalScrollbarInnerImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(VerticalScrollbarInnerPath, AssetRequestMode.ImmediateLoad);
            public const string VerticalScrollbarInnerPath = "Images/UI/VerticalScrollbarInner";
        }

    }

    public static class Items
    {
        public static class Misc
        {
            public static Asset<Texture2D> AyTsaoAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(AyTsaoPath);
            public static Asset<Texture2D> AyTsaoImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(AyTsaoPath, AssetRequestMode.ImmediateLoad);
            public const string AyTsaoPath = "Items/Misc/AyTsao";
            public static Asset<Texture2D> AyTsaoSeedAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(AyTsaoSeedPath);
            public static Asset<Texture2D> AyTsaoSeedImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(AyTsaoSeedPath, AssetRequestMode.ImmediateLoad);
            public const string AyTsaoSeedPath = "Items/Misc/AyTsaoSeed";
            public static Asset<Texture2D> BrokenGiantShovelAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(BrokenGiantShovelPath);
            public static Asset<Texture2D> BrokenGiantShovelImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(BrokenGiantShovelPath, AssetRequestMode.ImmediateLoad);
            public const string BrokenGiantShovelPath = "Items/Misc/BrokenGiantShovel";
            public static Asset<Texture2D> ChenAiAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(ChenAiPath);
            public static Asset<Texture2D> ChenAiImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(ChenAiPath, AssetRequestMode.ImmediateLoad);
            public const string ChenAiPath = "Items/Misc/ChenAi";
            public static Asset<Texture2D> LargePottedPlantAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(LargePottedPlantPath);
            public static Asset<Texture2D> LargePottedPlantImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(LargePottedPlantPath, AssetRequestMode.ImmediateLoad);
            public const string LargePottedPlantPath = "Items/Misc/LargePottedPlant";
            public static Asset<Texture2D> LightPromiseAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(LightPromisePath);
            public static Asset<Texture2D> LightPromiseImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(LightPromisePath, AssetRequestMode.ImmediateLoad);
            public const string LightPromisePath = "Items/Misc/LightPromise";
            public static Asset<Texture2D> MagicPowerFallingStarAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(MagicPowerFallingStarPath);
            public static Asset<Texture2D> MagicPowerFallingStarImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(MagicPowerFallingStarPath, AssetRequestMode.ImmediateLoad);
            public const string MagicPowerFallingStarPath = "Items/Misc/MagicPowerFallingStar";
            public static Asset<Texture2D> ParadiseAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(ParadisePath);
            public static Asset<Texture2D> ParadiseImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(ParadisePath, AssetRequestMode.ImmediateLoad);
            public const string ParadisePath = "Items/Misc/Paradise";
            public static Asset<Texture2D> RedmoonAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(RedmoonPath);
            public static Asset<Texture2D> RedmoonImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(RedmoonPath, AssetRequestMode.ImmediateLoad);
            public const string RedmoonPath = "Items/Misc/Redmoon";
            public static Asset<Texture2D> SimpleSteelCannonAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(SimpleSteelCannonPath);
            public static Asset<Texture2D> SimpleSteelCannonImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(SimpleSteelCannonPath, AssetRequestMode.ImmediateLoad);
            public const string SimpleSteelCannonPath = "Items/Misc/SimpleSteelCannon";
            public static Asset<Texture2D> StrangeDoorplateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(StrangeDoorplatePath);
            public static Asset<Texture2D> StrangeDoorplateImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(StrangeDoorplatePath, AssetRequestMode.ImmediateLoad);
            public const string StrangeDoorplatePath = "Items/Misc/StrangeDoorplate";
            public static Asset<Texture2D> SuicideRevolverAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(SuicideRevolverPath);
            public static Asset<Texture2D> SuicideRevolverImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(SuicideRevolverPath, AssetRequestMode.ImmediateLoad);
            public const string SuicideRevolverPath = "Items/Misc/SuicideRevolver";
            public static Asset<Texture2D> SunLightAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(SunLightPath);
            public static Asset<Texture2D> SunLightImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(SunLightPath, AssetRequestMode.ImmediateLoad);
            public const string SunLightPath = "Items/Misc/SunLight";
            public static Asset<Texture2D> TwistedSteelBladeAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(TwistedSteelBladePath);
            public static Asset<Texture2D> TwistedSteelBladeImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(TwistedSteelBladePath, AssetRequestMode.ImmediateLoad);
            public const string TwistedSteelBladePath = "Items/Misc/TwistedSteelBlade";
            public static Asset<Texture2D> WanAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(WanPath);
            public static Asset<Texture2D> WanImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(WanPath, AssetRequestMode.ImmediateLoad);
            public const string WanPath = "Items/Misc/Wan";
            public static Asset<Texture2D> WanGiftAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(WanGiftPath);
            public static Asset<Texture2D> WanGiftImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(WanGiftPath, AssetRequestMode.ImmediateLoad);
            public const string WanGiftPath = "Items/Misc/WanGift";
            public static class Accessories
            {
                public static Asset<Texture2D> HolyFlameCrownAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(HolyFlameCrownPath);
                public static Asset<Texture2D> HolyFlameCrownImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(HolyFlameCrownPath, AssetRequestMode.ImmediateLoad);
                public const string HolyFlameCrownPath = "Items/Misc/Accessories/HolyFlameCrown";
                public static Asset<Texture2D> LightningAmuletAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(LightningAmuletPath);
                public static Asset<Texture2D> LightningAmuletImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(LightningAmuletPath, AssetRequestMode.ImmediateLoad);
                public const string LightningAmuletPath = "Items/Misc/Accessories/LightningAmulet";
                public static Asset<Texture2D> MagicBoneShieldAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(MagicBoneShieldPath);
                public static Asset<Texture2D> MagicBoneShieldImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(MagicBoneShieldPath, AssetRequestMode.ImmediateLoad);
                public const string MagicBoneShieldPath = "Items/Misc/Accessories/MagicBoneShield";
            }

            public static class Materials
            {
                public static Asset<Texture2D> DarkMoonSoulAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(DarkMoonSoulPath);
                public static Asset<Texture2D> DarkMoonSoulImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(DarkMoonSoulPath, AssetRequestMode.ImmediateLoad);
                public const string DarkMoonSoulPath = "Items/Misc/Materials/DarkMoonSoul";
                public static Asset<Texture2D> GreenSoulAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(GreenSoulPath);
                public static Asset<Texture2D> GreenSoulImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(GreenSoulPath, AssetRequestMode.ImmediateLoad);
                public const string GreenSoulPath = "Items/Misc/Materials/GreenSoul";
                public static Asset<Texture2D> RelicSpiritStoneAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(RelicSpiritStonePath);
                public static Asset<Texture2D> RelicSpiritStoneImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(RelicSpiritStonePath, AssetRequestMode.ImmediateLoad);
                public const string RelicSpiritStonePath = "Items/Misc/Materials/RelicSpiritStone";
                public static Asset<Texture2D> SilkAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(SilkPath);
                public static Asset<Texture2D> SilkImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(SilkPath, AssetRequestMode.ImmediateLoad);
                public const string SilkPath = "Items/Misc/Materials/Silk";
            }

            public static class Weapons
            {
                public static Asset<Texture2D> BigTorchAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(BigTorchPath);
                public static Asset<Texture2D> BigTorchImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(BigTorchPath, AssetRequestMode.ImmediateLoad);
                public const string BigTorchPath = "Items/Misc/Weapons/BigTorch";
                public static Asset<Texture2D> BrilliantDragonAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(BrilliantDragonPath);
                public static Asset<Texture2D> BrilliantDragonImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(BrilliantDragonPath, AssetRequestMode.ImmediateLoad);
                public const string BrilliantDragonPath = "Items/Misc/Weapons/BrilliantDragon";
                public static Asset<Texture2D> BuleLightAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(BuleLightPath);
                public static Asset<Texture2D> BuleLightImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(BuleLightPath, AssetRequestMode.ImmediateLoad);
                public const string BuleLightPath = "Items/Misc/Weapons/BuleLight";
                public static Asset<Texture2D> ChaosBowAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(ChaosBowPath);
                public static Asset<Texture2D> ChaosBowImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(ChaosBowPath, AssetRequestMode.ImmediateLoad);
                public const string ChaosBowPath = "Items/Misc/Weapons/ChaosBow";
                public static Asset<Texture2D> ChillyMoonStaffAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(ChillyMoonStaffPath);
                public static Asset<Texture2D> ChillyMoonStaffImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(ChillyMoonStaffPath, AssetRequestMode.ImmediateLoad);
                public const string ChillyMoonStaffPath = "Items/Misc/Weapons/ChillyMoonStaff";
                public static Asset<Texture2D> DuskAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(DuskPath);
                public static Asset<Texture2D> DuskImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(DuskPath, AssetRequestMode.ImmediateLoad);
                public const string DuskPath = "Items/Misc/Weapons/Dusk";
                public static Asset<Texture2D> EnchantingStaffAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(EnchantingStaffPath);
                public static Asset<Texture2D> EnchantingStaffImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(EnchantingStaffPath, AssetRequestMode.ImmediateLoad);
                public const string EnchantingStaffPath = "Items/Misc/Weapons/EnchantingStaff";
                public static Asset<Texture2D> FantasyNightSkyAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(FantasyNightSkyPath);
                public static Asset<Texture2D> FantasyNightSkyImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(FantasyNightSkyPath, AssetRequestMode.ImmediateLoad);
                public const string FantasyNightSkyPath = "Items/Misc/Weapons/FantasyNightSky";
                public static Asset<Texture2D> FineRedAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(FineRedPath);
                public static Asset<Texture2D> FineRedImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(FineRedPath, AssetRequestMode.ImmediateLoad);
                public const string FineRedPath = "Items/Misc/Weapons/FineRed";
                public static Asset<Texture2D> FlickeringPhantomAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(FlickeringPhantomPath);
                public static Asset<Texture2D> FlickeringPhantomImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(FlickeringPhantomPath, AssetRequestMode.ImmediateLoad);
                public const string FlickeringPhantomPath = "Items/Misc/Weapons/FlickeringPhantom";
                public static Asset<Texture2D> HolyFireOfHeavenAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(HolyFireOfHeavenPath);
                public static Asset<Texture2D> HolyFireOfHeavenImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(HolyFireOfHeavenPath, AssetRequestMode.ImmediateLoad);
                public const string HolyFireOfHeavenPath = "Items/Misc/Weapons/HolyFireOfHeaven";
                public static Asset<Texture2D> MuddyWaterAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(MuddyWaterPath);
                public static Asset<Texture2D> MuddyWaterImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(MuddyWaterPath, AssetRequestMode.ImmediateLoad);
                public const string MuddyWaterPath = "Items/Misc/Weapons/MuddyWater";
                public static Asset<Texture2D> NaturalGiftAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(NaturalGiftPath);
                public static Asset<Texture2D> NaturalGiftImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(NaturalGiftPath, AssetRequestMode.ImmediateLoad);
                public const string NaturalGiftPath = "Items/Misc/Weapons/NaturalGift";
                public static Asset<Texture2D> PhantomTorchAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(PhantomTorchPath);
                public static Asset<Texture2D> PhantomTorchImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(PhantomTorchPath, AssetRequestMode.ImmediateLoad);
                public const string PhantomTorchPath = "Items/Misc/Weapons/PhantomTorch";
                public static Asset<Texture2D> PulseofThunderAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(PulseofThunderPath);
                public static Asset<Texture2D> PulseofThunderImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(PulseofThunderPath, AssetRequestMode.ImmediateLoad);
                public const string PulseofThunderPath = "Items/Misc/Weapons/PulseofThunder";
                public static Asset<Texture2D> PurpleSteelStaffAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(PurpleSteelStaffPath);
                public static Asset<Texture2D> PurpleSteelStaffImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(PurpleSteelStaffPath, AssetRequestMode.ImmediateLoad);
                public const string PurpleSteelStaffPath = "Items/Misc/Weapons/PurpleSteelStaff";
                public static Asset<Texture2D> ShadowGhostAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(ShadowGhostPath);
                public static Asset<Texture2D> ShadowGhostImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(ShadowGhostPath, AssetRequestMode.ImmediateLoad);
                public const string ShadowGhostPath = "Items/Misc/Weapons/ShadowGhost";
                public static Asset<Texture2D> SilkWandAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(SilkWandPath);
                public static Asset<Texture2D> SilkWandImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(SilkWandPath, AssetRequestMode.ImmediateLoad);
                public const string SilkWandPath = "Items/Misc/Weapons/SilkWand";
                public static Asset<Texture2D> SpecularWaterAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(SpecularWaterPath);
                public static Asset<Texture2D> SpecularWaterImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(SpecularWaterPath, AssetRequestMode.ImmediateLoad);
                public const string SpecularWaterPath = "Items/Misc/Weapons/SpecularWater";
                public static Asset<Texture2D> StarLockAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(StarLockPath);
                public static Asset<Texture2D> StarLockImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(StarLockPath, AssetRequestMode.ImmediateLoad);
                public const string StarLockPath = "Items/Misc/Weapons/StarLock";
                public static Asset<Texture2D> ___proAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(___proPath);
                public static Asset<Texture2D> ___proImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(___proPath, AssetRequestMode.ImmediateLoad);
                public const string ___proPath = "Items/Misc/Weapons/蚕丝团pro";
                public static Asset<Texture2D> ____Asset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(____Path);
                public static Asset<Texture2D> ____ImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(____Path, AssetRequestMode.ImmediateLoad);
                public const string ____Path = "Items/Misc/Weapons/蚕丝末端";
            }

        }

        public static class Series
        {
            public static class Brightiron
            {
                public static Asset<Texture2D> BrightironBarAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(BrightironBarPath);
                public static Asset<Texture2D> BrightironBarImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(BrightironBarPath, AssetRequestMode.ImmediateLoad);
                public const string BrightironBarPath = "Items/Series/Brightiron/BrightironBar";
                public static Asset<Texture2D> BrightironBreastplateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(BrightironBreastplatePath);
                public static Asset<Texture2D> BrightironBreastplateImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(BrightironBreastplatePath, AssetRequestMode.ImmediateLoad);
                public const string BrightironBreastplatePath = "Items/Series/Brightiron/BrightironBreastplate";
                public static Asset<Texture2D> BrightironBreastplate_BodyAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(BrightironBreastplate_BodyPath);
                public static Asset<Texture2D> BrightironBreastplate_BodyImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(BrightironBreastplate_BodyPath, AssetRequestMode.ImmediateLoad);
                public const string BrightironBreastplate_BodyPath = "Items/Series/Brightiron/BrightironBreastplate_Body";
                public static Asset<Texture2D> BrightironBreastplate_Body_CAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(BrightironBreastplate_Body_CPath);
                public static Asset<Texture2D> BrightironBreastplate_Body_CImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(BrightironBreastplate_Body_CPath, AssetRequestMode.ImmediateLoad);
                public const string BrightironBreastplate_Body_CPath = "Items/Series/Brightiron/BrightironBreastplate_Body_C";
                public static Asset<Texture2D> BrightironBreastplate_Female_BodyAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(BrightironBreastplate_Female_BodyPath);
                public static Asset<Texture2D> BrightironBreastplate_Female_BodyImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(BrightironBreastplate_Female_BodyPath, AssetRequestMode.ImmediateLoad);
                public const string BrightironBreastplate_Female_BodyPath = "Items/Series/Brightiron/BrightironBreastplate_Female_Body";
                public static Asset<Texture2D> BrightironBreastplate_Female_Body111Asset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(BrightironBreastplate_Female_Body111Path);
                public static Asset<Texture2D> BrightironBreastplate_Female_Body111ImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(BrightironBreastplate_Female_Body111Path, AssetRequestMode.ImmediateLoad);
                public const string BrightironBreastplate_Female_Body111Path = "Items/Series/Brightiron/BrightironBreastplate_Female_Body111";
                public static Asset<Texture2D> BrightironBroadswordAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(BrightironBroadswordPath);
                public static Asset<Texture2D> BrightironBroadswordImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(BrightironBroadswordPath, AssetRequestMode.ImmediateLoad);
                public const string BrightironBroadswordPath = "Items/Series/Brightiron/BrightironBroadsword";
                public static Asset<Texture2D> BrightironHeavyaxeAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(BrightironHeavyaxePath);
                public static Asset<Texture2D> BrightironHeavyaxeImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(BrightironHeavyaxePath, AssetRequestMode.ImmediateLoad);
                public const string BrightironHeavyaxePath = "Items/Series/Brightiron/BrightironHeavyaxe";
                public static Asset<Texture2D> BrightironHelmetAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(BrightironHelmetPath);
                public static Asset<Texture2D> BrightironHelmetImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(BrightironHelmetPath, AssetRequestMode.ImmediateLoad);
                public const string BrightironHelmetPath = "Items/Series/Brightiron/BrightironHelmet";
                public static Asset<Texture2D> BrightironHelmet_HeadAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(BrightironHelmet_HeadPath);
                public static Asset<Texture2D> BrightironHelmet_HeadImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(BrightironHelmet_HeadPath, AssetRequestMode.ImmediateLoad);
                public const string BrightironHelmet_HeadPath = "Items/Series/Brightiron/BrightironHelmet_Head";
                public static Asset<Texture2D> BrightironLeggingsAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(BrightironLeggingsPath);
                public static Asset<Texture2D> BrightironLeggingsImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(BrightironLeggingsPath, AssetRequestMode.ImmediateLoad);
                public const string BrightironLeggingsPath = "Items/Series/Brightiron/BrightironLeggings";
                public static Asset<Texture2D> BrightironLeggings_LegsAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(BrightironLeggings_LegsPath);
                public static Asset<Texture2D> BrightironLeggings_LegsImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(BrightironLeggings_LegsPath, AssetRequestMode.ImmediateLoad);
                public const string BrightironLeggings_LegsPath = "Items/Series/Brightiron/BrightironLeggings_Legs";
                public static Asset<Texture2D> BrightironOreAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(BrightironOrePath);
                public static Asset<Texture2D> BrightironOreImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(BrightironOrePath, AssetRequestMode.ImmediateLoad);
                public const string BrightironOrePath = "Items/Series/Brightiron/BrightironOre";
                public static Asset<Texture2D> BrightironPickaxeAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(BrightironPickaxePath);
                public static Asset<Texture2D> BrightironPickaxeImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(BrightironPickaxePath, AssetRequestMode.ImmediateLoad);
                public const string BrightironPickaxePath = "Items/Series/Brightiron/BrightironPickaxe";
                public static Asset<Texture2D> BrightironShortbowAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(BrightironShortbowPath);
                public static Asset<Texture2D> BrightironShortbowImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(BrightironShortbowPath, AssetRequestMode.ImmediateLoad);
                public const string BrightironShortbowPath = "Items/Series/Brightiron/BrightironShortbow";
                public static Asset<Texture2D> BrightironStaffAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(BrightironStaffPath);
                public static Asset<Texture2D> BrightironStaffImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(BrightironStaffPath, AssetRequestMode.ImmediateLoad);
                public const string BrightironStaffPath = "Items/Series/Brightiron/BrightironStaff";
                public static Asset<Texture2D> SpiritPiecesAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(SpiritPiecesPath);
                public static Asset<Texture2D> SpiritPiecesImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(SpiritPiecesPath, AssetRequestMode.ImmediateLoad);
                public const string SpiritPiecesPath = "Items/Series/Brightiron/SpiritPieces";
            }

            public static class Californium
            {
                public static Asset<Texture2D> CaliforniumBarAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(CaliforniumBarPath);
                public static Asset<Texture2D> CaliforniumBarImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(CaliforniumBarPath, AssetRequestMode.ImmediateLoad);
                public const string CaliforniumBarPath = "Items/Series/Californium/CaliforniumBar";
                public static Asset<Texture2D> GammaDisembowerAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(GammaDisembowerPath);
                public static Asset<Texture2D> GammaDisembowerImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(GammaDisembowerPath, AssetRequestMode.ImmediateLoad);
                public const string GammaDisembowerPath = "Items/Series/Californium/GammaDisembower";
                public static Asset<Texture2D> HallowRadianterAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(HallowRadianterPath);
                public static Asset<Texture2D> HallowRadianterImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(HallowRadianterPath, AssetRequestMode.ImmediateLoad);
                public const string HallowRadianterPath = "Items/Series/Californium/HallowRadianter";
                public static Asset<Texture2D> NeutronSourceAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(NeutronSourcePath);
                public static Asset<Texture2D> NeutronSourceImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(NeutronSourcePath, AssetRequestMode.ImmediateLoad);
                public const string NeutronSourcePath = "Items/Series/Californium/NeutronSource";
                public static Asset<Texture2D> PlaguebatterAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(PlaguebatterPath);
                public static Asset<Texture2D> PlaguebatterImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(PlaguebatterPath, AssetRequestMode.ImmediateLoad);
                public const string PlaguebatterPath = "Items/Series/Californium/Plaguebatter";
                public static Asset<Texture2D> RaydasonAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(RaydasonPath);
                public static Asset<Texture2D> RaydasonImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(RaydasonPath, AssetRequestMode.ImmediateLoad);
                public const string RaydasonPath = "Items/Series/Californium/Raydason";
                public static Asset<Texture2D> SpeedingTramcarAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(SpeedingTramcarPath);
                public static Asset<Texture2D> SpeedingTramcarImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(SpeedingTramcarPath, AssetRequestMode.ImmediateLoad);
                public const string SpeedingTramcarPath = "Items/Series/Californium/SpeedingTramcar";
            }

            public static class Chaos
            {
                public static Asset<Texture2D> BlackPageAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(BlackPagePath);
                public static Asset<Texture2D> BlackPageImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(BlackPagePath, AssetRequestMode.ImmediateLoad);
                public const string BlackPagePath = "Items/Series/Chaos/BlackPage";
                public static Asset<Texture2D> BrainAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(BrainPath);
                public static Asset<Texture2D> BrainImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(BrainPath, AssetRequestMode.ImmediateLoad);
                public const string BrainPath = "Items/Series/Chaos/Brain";
                public static Asset<Texture2D> CursedSoulAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(CursedSoulPath);
                public static Asset<Texture2D> CursedSoulImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(CursedSoulPath, AssetRequestMode.ImmediateLoad);
                public const string CursedSoulPath = "Items/Series/Chaos/CursedSoul";
                public static Asset<Texture2D> MagicTentacleAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(MagicTentaclePath);
                public static Asset<Texture2D> MagicTentacleImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(MagicTentaclePath, AssetRequestMode.ImmediateLoad);
                public const string MagicTentaclePath = "Items/Series/Chaos/MagicTentacle";
            }

            public static class Drawer
            {
                public static Asset<Texture2D> SnowaveAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(SnowavePath);
                public static Asset<Texture2D> SnowaveImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(SnowavePath, AssetRequestMode.ImmediateLoad);
                public const string SnowavePath = "Items/Series/Drawer/Snowave";
            }

            public static class Foods
            {
                public static Asset<Texture2D> ColaAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(ColaPath);
                public static Asset<Texture2D> ColaImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(ColaPath, AssetRequestMode.ImmediateLoad);
                public const string ColaPath = "Items/Series/Foods/Cola";
                public static Asset<Texture2D> DumplingAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(DumplingPath);
                public static Asset<Texture2D> DumplingImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(DumplingPath, AssetRequestMode.ImmediateLoad);
                public const string DumplingPath = "Items/Series/Foods/Dumpling";
                public static Asset<Texture2D> GlacierWaterAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(GlacierWaterPath);
                public static Asset<Texture2D> GlacierWaterImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(GlacierWaterPath, AssetRequestMode.ImmediateLoad);
                public const string GlacierWaterPath = "Items/Series/Foods/GlacierWater";
                public static Asset<Texture2D> GuoKuiAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(GuoKuiPath);
                public static Asset<Texture2D> GuoKuiImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(GuoKuiPath, AssetRequestMode.ImmediateLoad);
                public const string GuoKuiPath = "Items/Series/Foods/GuoKui";
                public static Asset<Texture2D> JiaoDuiAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(JiaoDuiPath);
                public static Asset<Texture2D> JiaoDuiImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(JiaoDuiPath, AssetRequestMode.ImmediateLoad);
                public const string JiaoDuiPath = "Items/Series/Foods/JiaoDui";
                public static Asset<Texture2D> KabobAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(KabobPath);
                public static Asset<Texture2D> KabobImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(KabobPath, AssetRequestMode.ImmediateLoad);
                public const string KabobPath = "Items/Series/Foods/Kabob";
                public static Asset<Texture2D> RiceNoodlesAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(RiceNoodlesPath);
                public static Asset<Texture2D> RiceNoodlesImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(RiceNoodlesPath, AssetRequestMode.ImmediateLoad);
                public const string RiceNoodlesPath = "Items/Series/Foods/RiceNoodles";
                public static Asset<Texture2D> ShadyShaddock_BAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(ShadyShaddock_BPath);
                public static Asset<Texture2D> ShadyShaddock_BImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(ShadyShaddock_BPath, AssetRequestMode.ImmediateLoad);
                public const string ShadyShaddock_BPath = "Items/Series/Foods/ShadyShaddock_B";
                public static Asset<Texture2D> ShadyShaddock_CAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(ShadyShaddock_CPath);
                public static Asset<Texture2D> ShadyShaddock_CImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(ShadyShaddock_CPath, AssetRequestMode.ImmediateLoad);
                public const string ShadyShaddock_CPath = "Items/Series/Foods/ShadyShaddock_C";
                public static Asset<Texture2D> StarLollipopAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(StarLollipopPath);
                public static Asset<Texture2D> StarLollipopImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(StarLollipopPath, AssetRequestMode.ImmediateLoad);
                public const string StarLollipopPath = "Items/Series/Foods/StarLollipop";
                public static Asset<Texture2D> SteamedBreadOfCornAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(SteamedBreadOfCornPath);
                public static Asset<Texture2D> SteamedBreadOfCornImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(SteamedBreadOfCornPath, AssetRequestMode.ImmediateLoad);
                public const string SteamedBreadOfCornPath = "Items/Series/Foods/SteamedBreadOfCorn";
                public static Asset<Texture2D> SteamedStuffedBunAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(SteamedStuffedBunPath);
                public static Asset<Texture2D> SteamedStuffedBunImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(SteamedStuffedBunPath, AssetRequestMode.ImmediateLoad);
                public const string SteamedStuffedBunPath = "Items/Series/Foods/SteamedStuffedBun";
            }

            public static class Frosted
            {
                public static Asset<Texture2D> FrostedFlyingcutterAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(FrostedFlyingcutterPath);
                public static Asset<Texture2D> FrostedFlyingcutterImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(FrostedFlyingcutterPath, AssetRequestMode.ImmediateLoad);
                public const string FrostedFlyingcutterPath = "Items/Series/Frosted/FrostedFlyingcutter";
                public static Asset<Texture2D> FrostedSickleAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(FrostedSicklePath);
                public static Asset<Texture2D> FrostedSickleImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(FrostedSicklePath, AssetRequestMode.ImmediateLoad);
                public const string FrostedSicklePath = "Items/Series/Frosted/FrostedSickle";
                public static Asset<Texture2D> FrostedThornAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(FrostedThornPath);
                public static Asset<Texture2D> FrostedThornImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(FrostedThornPath, AssetRequestMode.ImmediateLoad);
                public const string FrostedThornPath = "Items/Series/Frosted/FrostedThorn";
            }

            public static class HollowKnight
            {
                public static Asset<Texture2D> AbyssShriekAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(AbyssShriekPath);
                public static Asset<Texture2D> AbyssShriekImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(AbyssShriekPath, AssetRequestMode.ImmediateLoad);
                public const string AbyssShriekPath = "Items/Series/HollowKnight/AbyssShriek";
                public static Asset<Texture2D> DescendingDarkAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(DescendingDarkPath);
                public static Asset<Texture2D> DescendingDarkImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(DescendingDarkPath, AssetRequestMode.ImmediateLoad);
                public const string DescendingDarkPath = "Items/Series/HollowKnight/DescendingDark";
                public static Asset<Texture2D> DesolateDiveAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(DesolateDivePath);
                public static Asset<Texture2D> DesolateDiveImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(DesolateDivePath, AssetRequestMode.ImmediateLoad);
                public const string DesolateDivePath = "Items/Series/HollowKnight/DesolateDive";
                public static Asset<Texture2D> FocusAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(FocusPath);
                public static Asset<Texture2D> FocusImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(FocusPath, AssetRequestMode.ImmediateLoad);
                public const string FocusPath = "Items/Series/HollowKnight/Focus";
                public static Asset<Texture2D> HallownestSealAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(HallownestSealPath);
                public static Asset<Texture2D> HallownestSealImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(HallownestSealPath, AssetRequestMode.ImmediateLoad);
                public const string HallownestSealPath = "Items/Series/HollowKnight/HallownestSeal";
                public static Asset<Texture2D> HowlingWraithsAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(HowlingWraithsPath);
                public static Asset<Texture2D> HowlingWraithsImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(HowlingWraithsPath, AssetRequestMode.ImmediateLoad);
                public const string HowlingWraithsPath = "Items/Series/HollowKnight/HowlingWraiths";
                public static Asset<Texture2D> JetTraceAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(JetTracePath);
                public static Asset<Texture2D> JetTraceImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(JetTracePath, AssetRequestMode.ImmediateLoad);
                public const string JetTracePath = "Items/Series/HollowKnight/JetTrace";
                public static Asset<Texture2D> KingSwingAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(KingSwingPath);
                public static Asset<Texture2D> KingSwingImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(KingSwingPath, AssetRequestMode.ImmediateLoad);
                public const string KingSwingPath = "Items/Series/HollowKnight/KingSwing";
                public static Asset<Texture2D> MothwingCloakAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(MothwingCloakPath);
                public static Asset<Texture2D> MothwingCloakImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(MothwingCloakPath, AssetRequestMode.ImmediateLoad);
                public const string MothwingCloakPath = "Items/Series/HollowKnight/MothwingCloak";
                public static Asset<Texture2D> OldNailAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(OldNailPath);
                public static Asset<Texture2D> OldNailImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(OldNailPath, AssetRequestMode.ImmediateLoad);
                public const string OldNailPath = "Items/Series/HollowKnight/OldNail";
                public static Asset<Texture2D> ShadeCloakAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(ShadeCloakPath);
                public static Asset<Texture2D> ShadeCloakImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(ShadeCloakPath, AssetRequestMode.ImmediateLoad);
                public const string ShadeCloakPath = "Items/Series/HollowKnight/ShadeCloak";
                public static Asset<Texture2D> ShadeSoulAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(ShadeSoulPath);
                public static Asset<Texture2D> ShadeSoulImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(ShadeSoulPath, AssetRequestMode.ImmediateLoad);
                public const string ShadeSoulPath = "Items/Series/HollowKnight/ShadeSoul";
                public static Asset<Texture2D> VengefulSpiritAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(VengefulSpiritPath);
                public static Asset<Texture2D> VengefulSpiritImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(VengefulSpiritPath, AssetRequestMode.ImmediateLoad);
                public const string VengefulSpiritPath = "Items/Series/HollowKnight/VengefulSpirit";
            }

            public static class LightMushroom
            {
                public static Asset<Texture2D> LightMushroomAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(LightMushroomPath);
                public static Asset<Texture2D> LightMushroomImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(LightMushroomPath, AssetRequestMode.ImmediateLoad);
                public const string LightMushroomPath = "Items/Series/LightMushroom/LightMushroom";
                public static Asset<Texture2D> LightMushroomClawAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(LightMushroomClawPath);
                public static Asset<Texture2D> LightMushroomClawImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(LightMushroomClawPath, AssetRequestMode.ImmediateLoad);
                public const string LightMushroomClawPath = "Items/Series/LightMushroom/LightMushroomClaw";
                public static Asset<Texture2D> LightMushroomHammeraxeAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(LightMushroomHammeraxePath);
                public static Asset<Texture2D> LightMushroomHammeraxeImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(LightMushroomHammeraxePath, AssetRequestMode.ImmediateLoad);
                public const string LightMushroomHammeraxePath = "Items/Series/LightMushroom/LightMushroomHammeraxe";
                public static Asset<Texture2D> LightMushroomHolybowAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(LightMushroomHolybowPath);
                public static Asset<Texture2D> LightMushroomHolybowImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(LightMushroomHolybowPath, AssetRequestMode.ImmediateLoad);
                public const string LightMushroomHolybowPath = "Items/Series/LightMushroom/LightMushroomHolybow";
                public static Asset<Texture2D> LightMushroomHolyknifeAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(LightMushroomHolyknifePath);
                public static Asset<Texture2D> LightMushroomHolyknifeImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(LightMushroomHolyknifePath, AssetRequestMode.ImmediateLoad);
                public const string LightMushroomHolyknifePath = "Items/Series/LightMushroom/LightMushroomHolyknife";
                public static Asset<Texture2D> LightMushroomStaffAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(LightMushroomStaffPath);
                public static Asset<Texture2D> LightMushroomStaffImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(LightMushroomStaffPath, AssetRequestMode.ImmediateLoad);
                public const string LightMushroomStaffPath = "Items/Series/LightMushroom/LightMushroomStaff";
            }

            public static class Miracle
            {
                public static Asset<Texture2D> BrilliantPearlAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(BrilliantPearlPath);
                public static Asset<Texture2D> BrilliantPearlImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(BrilliantPearlPath, AssetRequestMode.ImmediateLoad);
                public const string BrilliantPearlPath = "Items/Series/Miracle/BrilliantPearl";
                public static Asset<Texture2D> CrystalSpurAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(CrystalSpurPath);
                public static Asset<Texture2D> CrystalSpurImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(CrystalSpurPath, AssetRequestMode.ImmediateLoad);
                public const string CrystalSpurPath = "Items/Series/Miracle/CrystalSpur";
                public static Asset<Texture2D> GloryAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(GloryPath);
                public static Asset<Texture2D> GloryImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(GloryPath, AssetRequestMode.ImmediateLoad);
                public const string GloryPath = "Items/Series/Miracle/Glory";
                public static Asset<Texture2D> HolyElementAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(HolyElementPath);
                public static Asset<Texture2D> HolyElementImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(HolyElementPath, AssetRequestMode.ImmediateLoad);
                public const string HolyElementPath = "Items/Series/Miracle/HolyElement";
                public static Asset<Texture2D> HolySpiritArrowAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(HolySpiritArrowPath);
                public static Asset<Texture2D> HolySpiritArrowImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(HolySpiritArrowPath, AssetRequestMode.ImmediateLoad);
                public const string HolySpiritArrowPath = "Items/Series/Miracle/HolySpiritArrow";
                public static Asset<Texture2D> HolySpiritBulletAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(HolySpiritBulletPath);
                public static Asset<Texture2D> HolySpiritBulletImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(HolySpiritBulletPath, AssetRequestMode.ImmediateLoad);
                public const string HolySpiritBulletPath = "Items/Series/Miracle/HolySpiritBullet";
                public static Asset<Texture2D> HolySpiritCurseAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(HolySpiritCursePath);
                public static Asset<Texture2D> HolySpiritCurseImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(HolySpiritCursePath, AssetRequestMode.ImmediateLoad);
                public const string HolySpiritCursePath = "Items/Series/Miracle/HolySpiritCurse";
                public static Asset<Texture2D> MiracleRecorderBossBagAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(MiracleRecorderBossBagPath);
                public static Asset<Texture2D> MiracleRecorderBossBagImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(MiracleRecorderBossBagPath, AssetRequestMode.ImmediateLoad);
                public const string MiracleRecorderBossBagPath = "Items/Series/Miracle/MiracleRecorderBossBag";
                public static Asset<Texture2D> ShiningSpiritAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(ShiningSpiritPath);
                public static Asset<Texture2D> ShiningSpiritImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(ShiningSpiritPath, AssetRequestMode.ImmediateLoad);
                public const string ShiningSpiritPath = "Items/Series/Miracle/ShiningSpirit";
            }

            public static class Mosscobble
            {
                public static Asset<Texture2D> MosscobbleAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(MosscobblePath);
                public static Asset<Texture2D> MosscobbleImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(MosscobblePath, AssetRequestMode.ImmediateLoad);
                public const string MosscobblePath = "Items/Series/Mosscobble/Mosscobble";
                public static Asset<Texture2D> MosscobbleAxeAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(MosscobbleAxePath);
                public static Asset<Texture2D> MosscobbleAxeImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(MosscobbleAxePath, AssetRequestMode.ImmediateLoad);
                public const string MosscobbleAxePath = "Items/Series/Mosscobble/MosscobbleAxe";
                public static Asset<Texture2D> MosscobbleBreastplateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(MosscobbleBreastplatePath);
                public static Asset<Texture2D> MosscobbleBreastplateImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(MosscobbleBreastplatePath, AssetRequestMode.ImmediateLoad);
                public const string MosscobbleBreastplatePath = "Items/Series/Mosscobble/MosscobbleBreastplate";
                public static Asset<Texture2D> MosscobbleBreastplate_ArmsAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(MosscobbleBreastplate_ArmsPath);
                public static Asset<Texture2D> MosscobbleBreastplate_ArmsImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(MosscobbleBreastplate_ArmsPath, AssetRequestMode.ImmediateLoad);
                public const string MosscobbleBreastplate_ArmsPath = "Items/Series/Mosscobble/MosscobbleBreastplate_Arms";
                public static Asset<Texture2D> MosscobbleBreastplate_BodyAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(MosscobbleBreastplate_BodyPath);
                public static Asset<Texture2D> MosscobbleBreastplate_BodyImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(MosscobbleBreastplate_BodyPath, AssetRequestMode.ImmediateLoad);
                public const string MosscobbleBreastplate_BodyPath = "Items/Series/Mosscobble/MosscobbleBreastplate_Body";
                public static Asset<Texture2D> MosscobbleBreastplate_FemaleBodyAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(MosscobbleBreastplate_FemaleBodyPath);
                public static Asset<Texture2D> MosscobbleBreastplate_FemaleBodyImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(MosscobbleBreastplate_FemaleBodyPath, AssetRequestMode.ImmediateLoad);
                public const string MosscobbleBreastplate_FemaleBodyPath = "Items/Series/Mosscobble/MosscobbleBreastplate_FemaleBody";
                public static Asset<Texture2D> MosscobbleHelmetAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(MosscobbleHelmetPath);
                public static Asset<Texture2D> MosscobbleHelmetImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(MosscobbleHelmetPath, AssetRequestMode.ImmediateLoad);
                public const string MosscobbleHelmetPath = "Items/Series/Mosscobble/MosscobbleHelmet";
                public static Asset<Texture2D> MosscobbleHelmet_HeadAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(MosscobbleHelmet_HeadPath);
                public static Asset<Texture2D> MosscobbleHelmet_HeadImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(MosscobbleHelmet_HeadPath, AssetRequestMode.ImmediateLoad);
                public const string MosscobbleHelmet_HeadPath = "Items/Series/Mosscobble/MosscobbleHelmet_Head";
                public static Asset<Texture2D> MosscobbleKnifeAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(MosscobbleKnifePath);
                public static Asset<Texture2D> MosscobbleKnifeImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(MosscobbleKnifePath, AssetRequestMode.ImmediateLoad);
                public const string MosscobbleKnifePath = "Items/Series/Mosscobble/MosscobbleKnife";
                public static Asset<Texture2D> MosscobbleLeggingsAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(MosscobbleLeggingsPath);
                public static Asset<Texture2D> MosscobbleLeggingsImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(MosscobbleLeggingsPath, AssetRequestMode.ImmediateLoad);
                public const string MosscobbleLeggingsPath = "Items/Series/Mosscobble/MosscobbleLeggings";
                public static Asset<Texture2D> MosscobbleLeggings_LegsAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(MosscobbleLeggings_LegsPath);
                public static Asset<Texture2D> MosscobbleLeggings_LegsImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(MosscobbleLeggings_LegsPath, AssetRequestMode.ImmediateLoad);
                public const string MosscobbleLeggings_LegsPath = "Items/Series/Mosscobble/MosscobbleLeggings_Legs";
                public static Asset<Texture2D> MosscobblePickaxeAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(MosscobblePickaxePath);
                public static Asset<Texture2D> MosscobblePickaxeImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(MosscobblePickaxePath, AssetRequestMode.ImmediateLoad);
                public const string MosscobblePickaxePath = "Items/Series/Mosscobble/MosscobblePickaxe";
            }

            public static class Recharge
            {
                public static Asset<Texture2D> ElectrifiedBlasterCannonAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(ElectrifiedBlasterCannonPath);
                public static Asset<Texture2D> ElectrifiedBlasterCannonImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(ElectrifiedBlasterCannonPath, AssetRequestMode.ImmediateLoad);
                public const string ElectrifiedBlasterCannonPath = "Items/Series/Recharge/ElectrifiedBlasterCannon";
                public static Asset<Texture2D> ElectrifiedBlasterCannonBarAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(ElectrifiedBlasterCannonBarPath);
                public static Asset<Texture2D> ElectrifiedBlasterCannonBarImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(ElectrifiedBlasterCannonBarPath, AssetRequestMode.ImmediateLoad);
                public const string ElectrifiedBlasterCannonBarPath = "Items/Series/Recharge/ElectrifiedBlasterCannonBar";
                public static Asset<Texture2D> ElectrifiedBlasterCannonBarInnerAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(ElectrifiedBlasterCannonBarInnerPath);
                public static Asset<Texture2D> ElectrifiedBlasterCannonBarInnerImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(ElectrifiedBlasterCannonBarInnerPath, AssetRequestMode.ImmediateLoad);
                public const string ElectrifiedBlasterCannonBarInnerPath = "Items/Series/Recharge/ElectrifiedBlasterCannonBarInner";
                public static Asset<Texture2D> HeavenFragmentAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(HeavenFragmentPath);
                public static Asset<Texture2D> HeavenFragmentImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(HeavenFragmentPath, AssetRequestMode.ImmediateLoad);
                public const string HeavenFragmentPath = "Items/Series/Recharge/HeavenFragment";
                public static Asset<Texture2D> LaserGunAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(LaserGunPath);
                public static Asset<Texture2D> LaserGunImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(LaserGunPath, AssetRequestMode.ImmediateLoad);
                public const string LaserGunPath = "Items/Series/Recharge/LaserGun";
                public static Asset<Texture2D> LaserGunBarAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(LaserGunBarPath);
                public static Asset<Texture2D> LaserGunBarImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(LaserGunBarPath, AssetRequestMode.ImmediateLoad);
                public const string LaserGunBarPath = "Items/Series/Recharge/LaserGunBar";
                public static Asset<Texture2D> LaserGunBarInnerAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(LaserGunBarInnerPath);
                public static Asset<Texture2D> LaserGunBarInnerImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(LaserGunBarInnerPath, AssetRequestMode.ImmediateLoad);
                public const string LaserGunBarInnerPath = "Items/Series/Recharge/LaserGunBarInner";
                public static Asset<Texture2D> LuminousReactorAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(LuminousReactorPath);
                public static Asset<Texture2D> LuminousReactorImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(LuminousReactorPath, AssetRequestMode.ImmediateLoad);
                public const string LuminousReactorPath = "Items/Series/Recharge/LuminousReactor";
                public static Asset<Texture2D> MarsBatteryAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(MarsBatteryPath);
                public static Asset<Texture2D> MarsBatteryImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(MarsBatteryPath, AssetRequestMode.ImmediateLoad);
                public const string MarsBatteryPath = "Items/Series/Recharge/MarsBattery";
                public static Asset<Texture2D> PhotonicComponentAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(PhotonicComponentPath);
                public static Asset<Texture2D> PhotonicComponentImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(PhotonicComponentPath, AssetRequestMode.ImmediateLoad);
                public const string PhotonicComponentPath = "Items/Series/Recharge/PhotonicComponent";
                public static Asset<Texture2D> PrismRevolverAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(PrismRevolverPath);
                public static Asset<Texture2D> PrismRevolverImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(PrismRevolverPath, AssetRequestMode.ImmediateLoad);
                public const string PrismRevolverPath = "Items/Series/Recharge/PrismRevolver";
                public static Asset<Texture2D> PrismRevolverBarAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(PrismRevolverBarPath);
                public static Asset<Texture2D> PrismRevolverBarImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(PrismRevolverBarPath, AssetRequestMode.ImmediateLoad);
                public const string PrismRevolverBarPath = "Items/Series/Recharge/PrismRevolverBar";
                public static Asset<Texture2D> PrismRevolverBarInnerAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(PrismRevolverBarInnerPath);
                public static Asset<Texture2D> PrismRevolverBarInnerImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(PrismRevolverBarInnerPath, AssetRequestMode.ImmediateLoad);
                public const string PrismRevolverBarInnerPath = "Items/Series/Recharge/PrismRevolverBarInner";
                public static Asset<Texture2D> StarAngelAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(StarAngelPath);
                public static Asset<Texture2D> StarAngelImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(StarAngelPath, AssetRequestMode.ImmediateLoad);
                public const string StarAngelPath = "Items/Series/Recharge/StarAngel";
                public static Asset<Texture2D> StarAngelBarAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(StarAngelBarPath);
                public static Asset<Texture2D> StarAngelBarImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(StarAngelBarPath, AssetRequestMode.ImmediateLoad);
                public const string StarAngelBarPath = "Items/Series/Recharge/StarAngelBar";
                public static Asset<Texture2D> StarAngelBarInnerAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(StarAngelBarInnerPath);
                public static Asset<Texture2D> StarAngelBarInnerImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(StarAngelBarInnerPath, AssetRequestMode.ImmediateLoad);
                public const string StarAngelBarInnerPath = "Items/Series/Recharge/StarAngelBarInner";
                public static Asset<Texture2D> Thomas_ZeroAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(Thomas_ZeroPath);
                public static Asset<Texture2D> Thomas_ZeroImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(Thomas_ZeroPath, AssetRequestMode.ImmediateLoad);
                public const string Thomas_ZeroPath = "Items/Series/Recharge/Thomas_Zero";
                public static Asset<Texture2D> Thomas_ZeroBarAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(Thomas_ZeroBarPath);
                public static Asset<Texture2D> Thomas_ZeroBarImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(Thomas_ZeroBarPath, AssetRequestMode.ImmediateLoad);
                public const string Thomas_ZeroBarPath = "Items/Series/Recharge/Thomas_ZeroBar";
                public static Asset<Texture2D> Thomas_ZeroBarInnerAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(Thomas_ZeroBarInnerPath);
                public static Asset<Texture2D> Thomas_ZeroBarInnerImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(Thomas_ZeroBarInnerPath, AssetRequestMode.ImmediateLoad);
                public const string Thomas_ZeroBarInnerPath = "Items/Series/Recharge/Thomas_ZeroBarInner";
                public static Asset<Texture2D> TwilightGatlingAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(TwilightGatlingPath);
                public static Asset<Texture2D> TwilightGatlingImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(TwilightGatlingPath, AssetRequestMode.ImmediateLoad);
                public const string TwilightGatlingPath = "Items/Series/Recharge/TwilightGatling";
                public static Asset<Texture2D> TwilightGatlingBarAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(TwilightGatlingBarPath);
                public static Asset<Texture2D> TwilightGatlingBarImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(TwilightGatlingBarPath, AssetRequestMode.ImmediateLoad);
                public const string TwilightGatlingBarPath = "Items/Series/Recharge/TwilightGatlingBar";
                public static Asset<Texture2D> TwilightGatlingBarInnerAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(TwilightGatlingBarInnerPath);
                public static Asset<Texture2D> TwilightGatlingBarInnerImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(TwilightGatlingBarInnerPath, AssetRequestMode.ImmediateLoad);
                public const string TwilightGatlingBarInnerPath = "Items/Series/Recharge/TwilightGatlingBarInner";
                public static Asset<Texture2D> TwilightGatlingBar_SideAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(TwilightGatlingBar_SidePath);
                public static Asset<Texture2D> TwilightGatlingBar_SideImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(TwilightGatlingBar_SidePath, AssetRequestMode.ImmediateLoad);
                public const string TwilightGatlingBar_SidePath = "Items/Series/Recharge/TwilightGatlingBar_Side";
                public static Asset<Texture2D> ZapinatorAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(ZapinatorPath);
                public static Asset<Texture2D> ZapinatorImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(ZapinatorPath, AssetRequestMode.ImmediateLoad);
                public const string ZapinatorPath = "Items/Series/Recharge/Zapinator";
                public static Asset<Texture2D> ZapinatorBarAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(ZapinatorBarPath);
                public static Asset<Texture2D> ZapinatorBarImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(ZapinatorBarPath, AssetRequestMode.ImmediateLoad);
                public const string ZapinatorBarPath = "Items/Series/Recharge/ZapinatorBar";
                public static Asset<Texture2D> ZapinatorBarInnerAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(ZapinatorBarInnerPath);
                public static Asset<Texture2D> ZapinatorBarInnerImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(ZapinatorBarInnerPath, AssetRequestMode.ImmediateLoad);
                public const string ZapinatorBarInnerPath = "Items/Series/Recharge/ZapinatorBarInner";
            }

            public static class RiftValley
            {
                public static Asset<Texture2D> AncientSwordAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(AncientSwordPath);
                public static Asset<Texture2D> AncientSwordImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(AncientSwordPath, AssetRequestMode.ImmediateLoad);
                public const string AncientSwordPath = "Items/Series/RiftValley/AncientSword";
                public static Asset<Texture2D> DarkBlueMantraStoneAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(DarkBlueMantraStonePath);
                public static Asset<Texture2D> DarkBlueMantraStoneImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(DarkBlueMantraStonePath, AssetRequestMode.ImmediateLoad);
                public const string DarkBlueMantraStonePath = "Items/Series/RiftValley/DarkBlueMantraStone";
                public static Asset<Texture2D> DarkBlueStingAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(DarkBlueStingPath);
                public static Asset<Texture2D> DarkBlueStingImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(DarkBlueStingPath, AssetRequestMode.ImmediateLoad);
                public const string DarkBlueStingPath = "Items/Series/RiftValley/DarkBlueSting";
                public static Asset<Texture2D> DisasterCurseSilverIngotAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(DisasterCurseSilverIngotPath);
                public static Asset<Texture2D> DisasterCurseSilverIngotImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(DisasterCurseSilverIngotPath, AssetRequestMode.ImmediateLoad);
                public const string DisasterCurseSilverIngotPath = "Items/Series/RiftValley/DisasterCurseSilverIngot";
                public static Asset<Texture2D> DisasterSilverOreAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(DisasterSilverOrePath);
                public static Asset<Texture2D> DisasterSilverOreImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(DisasterSilverOrePath, AssetRequestMode.ImmediateLoad);
                public const string DisasterSilverOrePath = "Items/Series/RiftValley/DisasterSilverOre";
                public static Asset<Texture2D> EmeraldLeafAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(EmeraldLeafPath);
                public static Asset<Texture2D> EmeraldLeafImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(EmeraldLeafPath, AssetRequestMode.ImmediateLoad);
                public const string EmeraldLeafPath = "Items/Series/RiftValley/EmeraldLeaf";
                public static Asset<Texture2D> FantasyStoneAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(FantasyStonePath);
                public static Asset<Texture2D> FantasyStoneImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(FantasyStonePath, AssetRequestMode.ImmediateLoad);
                public const string FantasyStonePath = "Items/Series/RiftValley/FantasyStone";
                public static Asset<Texture2D> FlamehappyAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(FlamehappyPath);
                public static Asset<Texture2D> FlamehappyImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(FlamehappyPath, AssetRequestMode.ImmediateLoad);
                public const string FlamehappyPath = "Items/Series/RiftValley/Flamehappy";
                public static Asset<Texture2D> FlowFireGoldAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(FlowFireGoldPath);
                public static Asset<Texture2D> FlowFireGoldImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(FlowFireGoldPath, AssetRequestMode.ImmediateLoad);
                public const string FlowFireGoldPath = "Items/Series/RiftValley/FlowFireGold";
                public static Asset<Texture2D> FlowFireGoldBrickAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(FlowFireGoldBrickPath);
                public static Asset<Texture2D> FlowFireGoldBrickImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(FlowFireGoldBrickPath, AssetRequestMode.ImmediateLoad);
                public const string FlowFireGoldBrickPath = "Items/Series/RiftValley/FlowFireGoldBrick";
                public static Asset<Texture2D> HolyLightRotatingBladeAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(HolyLightRotatingBladePath);
                public static Asset<Texture2D> HolyLightRotatingBladeImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(HolyLightRotatingBladePath, AssetRequestMode.ImmediateLoad);
                public const string HolyLightRotatingBladePath = "Items/Series/RiftValley/HolyLightRotatingBlade";
                public static Asset<Texture2D> HolySpiritCutterAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(HolySpiritCutterPath);
                public static Asset<Texture2D> HolySpiritCutterImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(HolySpiritCutterPath, AssetRequestMode.ImmediateLoad);
                public const string HolySpiritCutterPath = "Items/Series/RiftValley/HolySpiritCutter";
                public static Asset<Texture2D> LifeShieldAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(LifeShieldPath);
                public static Asset<Texture2D> LifeShieldImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(LifeShieldPath, AssetRequestMode.ImmediateLoad);
                public const string LifeShieldPath = "Items/Series/RiftValley/LifeShield";
                public static Asset<Texture2D> LifeStarAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(LifeStarPath);
                public static Asset<Texture2D> LifeStarImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(LifeStarPath, AssetRequestMode.ImmediateLoad);
                public const string LifeStarPath = "Items/Series/RiftValley/LifeStar";
                public static Asset<Texture2D> PredatorSoulAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(PredatorSoulPath);
                public static Asset<Texture2D> PredatorSoulImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(PredatorSoulPath, AssetRequestMode.ImmediateLoad);
                public const string PredatorSoulPath = "Items/Series/RiftValley/PredatorSoul";
                public static Asset<Texture2D> RosewoodCrystalStaffAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(RosewoodCrystalStaffPath);
                public static Asset<Texture2D> RosewoodCrystalStaffImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(RosewoodCrystalStaffPath, AssetRequestMode.ImmediateLoad);
                public const string RosewoodCrystalStaffPath = "Items/Series/RiftValley/RosewoodCrystalStaff";
                public static Asset<Texture2D> RottenMeatcsAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(RottenMeatcsPath);
                public static Asset<Texture2D> RottenMeatcsImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(RottenMeatcsPath, AssetRequestMode.ImmediateLoad);
                public const string RottenMeatcsPath = "Items/Series/RiftValley/RottenMeatcs";
                public static Asset<Texture2D> SoulHaystackAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(SoulHaystackPath);
                public static Asset<Texture2D> SoulHaystackImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(SoulHaystackPath, AssetRequestMode.ImmediateLoad);
                public const string SoulHaystackPath = "Items/Series/RiftValley/SoulHaystack";
                public static Asset<Texture2D> SpiritualPowerRosewoodAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(SpiritualPowerRosewoodPath);
                public static Asset<Texture2D> SpiritualPowerRosewoodImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(SpiritualPowerRosewoodPath, AssetRequestMode.ImmediateLoad);
                public const string SpiritualPowerRosewoodPath = "Items/Series/RiftValley/SpiritualPowerRosewood";
                public static Asset<Texture2D> TestAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(TestPath);
                public static Asset<Texture2D> TestImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(TestPath, AssetRequestMode.ImmediateLoad);
                public const string TestPath = "Items/Series/RiftValley/Test";
            }

            public static class Sharpsand
            {
                public static Asset<Texture2D> SharpsandBowAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(SharpsandBowPath);
                public static Asset<Texture2D> SharpsandBowImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(SharpsandBowPath, AssetRequestMode.ImmediateLoad);
                public const string SharpsandBowPath = "Items/Series/Sharpsand/SharpsandBow";
                public static Asset<Texture2D> SharpsandGiantbladeAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(SharpsandGiantbladePath);
                public static Asset<Texture2D> SharpsandGiantbladeImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(SharpsandGiantbladePath, AssetRequestMode.ImmediateLoad);
                public const string SharpsandGiantbladePath = "Items/Series/Sharpsand/SharpsandGiantblade";
                public static Asset<Texture2D> SharpsandStaffAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(SharpsandStaffPath);
                public static Asset<Texture2D> SharpsandStaffImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(SharpsandStaffPath, AssetRequestMode.ImmediateLoad);
                public const string SharpsandStaffPath = "Items/Series/Sharpsand/SharpsandStaff";
                public static Asset<Texture2D> ____Asset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(____Path);
                public static Asset<Texture2D> ____ImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(____Path, AssetRequestMode.ImmediateLoad);
                public const string ____Path = "Items/Series/Sharpsand/纯砂炸弹";
            }

            public static class SoulCemetery
            {
                public static Asset<Texture2D> BlueIronOreAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(BlueIronOrePath);
                public static Asset<Texture2D> BlueIronOreImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(BlueIronOrePath, AssetRequestMode.ImmediateLoad);
                public const string BlueIronOrePath = "Items/Series/SoulCemetery/BlueIronOre";
                public static Asset<Texture2D> BoneOakAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(BoneOakPath);
                public static Asset<Texture2D> BoneOakImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(BoneOakPath, AssetRequestMode.ImmediateLoad);
                public const string BoneOakPath = "Items/Series/SoulCemetery/BoneOak";
                public static Asset<Texture2D> DelivererAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(DelivererPath);
                public static Asset<Texture2D> DelivererImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(DelivererPath, AssetRequestMode.ImmediateLoad);
                public const string DelivererPath = "Items/Series/SoulCemetery/Deliverer";
                public static Asset<Texture2D> EvilSoulJudgmentAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(EvilSoulJudgmentPath);
                public static Asset<Texture2D> EvilSoulJudgmentImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(EvilSoulJudgmentPath, AssetRequestMode.ImmediateLoad);
                public const string EvilSoulJudgmentPath = "Items/Series/SoulCemetery/EvilSoulJudgment";
                public static Asset<Texture2D> JinxedAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(JinxedPath);
                public static Asset<Texture2D> JinxedImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(JinxedPath, AssetRequestMode.ImmediateLoad);
                public const string JinxedPath = "Items/Series/SoulCemetery/Jinxed";
                public static Asset<Texture2D> PhantomBacteriaAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(PhantomBacteriaPath);
                public static Asset<Texture2D> PhantomBacteriaImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(PhantomBacteriaPath, AssetRequestMode.ImmediateLoad);
                public const string PhantomBacteriaPath = "Items/Series/SoulCemetery/PhantomBacteria";
                public static Asset<Texture2D> SoulCongealingSoilAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(SoulCongealingSoilPath);
                public static Asset<Texture2D> SoulCongealingSoilImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(SoulCongealingSoilPath, AssetRequestMode.ImmediateLoad);
                public const string SoulCongealingSoilPath = "Items/Series/SoulCemetery/SoulCongealingSoil";
                public static Asset<Texture2D> SpiritOfDefilementAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(SpiritOfDefilementPath);
                public static Asset<Texture2D> SpiritOfDefilementImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(SpiritOfDefilementPath, AssetRequestMode.ImmediateLoad);
                public const string SpiritOfDefilementPath = "Items/Series/SoulCemetery/SpiritOfDefilement";
                public static Asset<Texture2D> WhiteSoulStoneAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(WhiteSoulStonePath);
                public static Asset<Texture2D> WhiteSoulStoneImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(WhiteSoulStonePath, AssetRequestMode.ImmediateLoad);
                public const string WhiteSoulStonePath = "Items/Series/SoulCemetery/WhiteSoulStone";
            }

            public static class StarlightDargon
            {
                public static Asset<Texture2D> FallingStarAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(FallingStarPath);
                public static Asset<Texture2D> FallingStarImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(FallingStarPath, AssetRequestMode.ImmediateLoad);
                public const string FallingStarPath = "Items/Series/StarlightDargon/FallingStar";
            }

        }

    }

    public static class NPCs
    {
        public static class Boss
        {
            public static class DargonDemo
            {
                public static Asset<Texture2D> DargonDemoAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(DargonDemoPath);
                public static Asset<Texture2D> DargonDemoImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(DargonDemoPath, AssetRequestMode.ImmediateLoad);
                public const string DargonDemoPath = "NPCs/Boss/DargonDemo/DargonDemo";
            }

            public static class MiracleRecorder
            {
                public static Asset<Texture2D> MiracleRecorderAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(MiracleRecorderPath);
                public static Asset<Texture2D> MiracleRecorderImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(MiracleRecorderPath, AssetRequestMode.ImmediateLoad);
                public const string MiracleRecorderPath = "NPCs/Boss/MiracleRecorder/MiracleRecorder";
                public static Asset<Texture2D> MiracleRecorderDrawerAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(MiracleRecorderDrawerPath);
                public static Asset<Texture2D> MiracleRecorderDrawerImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(MiracleRecorderDrawerPath, AssetRequestMode.ImmediateLoad);
                public const string MiracleRecorderDrawerPath = "NPCs/Boss/MiracleRecorder/MiracleRecorderDrawer";
                public static Asset<Texture2D> MiracleRecorderGhostAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(MiracleRecorderGhostPath);
                public static Asset<Texture2D> MiracleRecorderGhostImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(MiracleRecorderGhostPath, AssetRequestMode.ImmediateLoad);
                public const string MiracleRecorderGhostPath = "NPCs/Boss/MiracleRecorder/MiracleRecorderGhost";
                public static Asset<Texture2D> MiracleRecorderGlowAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(MiracleRecorderGlowPath);
                public static Asset<Texture2D> MiracleRecorderGlowImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(MiracleRecorderGlowPath, AssetRequestMode.ImmediateLoad);
                public const string MiracleRecorderGlowPath = "NPCs/Boss/MiracleRecorder/MiracleRecorderGlow";
                public static Asset<Texture2D> MiracleRecorder_Head_BossAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(MiracleRecorder_Head_BossPath);
                public static Asset<Texture2D> MiracleRecorder_Head_BossImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(MiracleRecorder_Head_BossPath, AssetRequestMode.ImmediateLoad);
                public const string MiracleRecorder_Head_BossPath = "NPCs/Boss/MiracleRecorder/MiracleRecorder_Head_Boss";
            }

        }

        public static class Chaos
        {
            public static Asset<Texture2D> ChaoticAuroraAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(ChaoticAuroraPath);
            public static Asset<Texture2D> ChaoticAuroraImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(ChaoticAuroraPath, AssetRequestMode.ImmediateLoad);
            public const string ChaoticAuroraPath = "NPCs/Chaos/ChaoticAurora";
            public static Asset<Texture2D> ChaoticBiteAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(ChaoticBitePath);
            public static Asset<Texture2D> ChaoticBiteImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(ChaoticBitePath, AssetRequestMode.ImmediateLoad);
            public const string ChaoticBitePath = "NPCs/Chaos/ChaoticBite";
            public static Asset<Texture2D> ChaoticLickerAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(ChaoticLickerPath);
            public static Asset<Texture2D> ChaoticLickerImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(ChaoticLickerPath, AssetRequestMode.ImmediateLoad);
            public const string ChaoticLickerPath = "NPCs/Chaos/ChaoticLicker";
            public static Asset<Texture2D> ChaoticMiteAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(ChaoticMitePath);
            public static Asset<Texture2D> ChaoticMiteImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(ChaoticMitePath, AssetRequestMode.ImmediateLoad);
            public const string ChaoticMitePath = "NPCs/Chaos/ChaoticMite";
            public static Asset<Texture2D> MistAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(MistPath);
            public static Asset<Texture2D> MistImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(MistPath, AssetRequestMode.ImmediateLoad);
            public const string MistPath = "NPCs/Chaos/Mist";
            public static Asset<Texture2D> RumiaAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(RumiaPath);
            public static Asset<Texture2D> RumiaImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(RumiaPath, AssetRequestMode.ImmediateLoad);
            public const string RumiaPath = "NPCs/Chaos/Rumia";
            public static Asset<Texture2D> SoftGlueDragonAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(SoftGlueDragonPath);
            public static Asset<Texture2D> SoftGlueDragonImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(SoftGlueDragonPath, AssetRequestMode.ImmediateLoad);
            public const string SoftGlueDragonPath = "NPCs/Chaos/SoftGlueDragon";
        }

        public static class Enemies
        {
            public static Asset<Texture2D> FireSlimAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(FireSlimPath);
            public static Asset<Texture2D> FireSlimImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(FireSlimPath, AssetRequestMode.ImmediateLoad);
            public const string FireSlimPath = "NPCs/Enemies/FireSlim";
            public static Asset<Texture2D> RaiderAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(RaiderPath);
            public static Asset<Texture2D> RaiderImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(RaiderPath, AssetRequestMode.ImmediateLoad);
            public const string RaiderPath = "NPCs/Enemies/Raider";
            public static Asset<Texture2D> ShadowSwingAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(ShadowSwingPath);
            public static Asset<Texture2D> ShadowSwingImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(ShadowSwingPath, AssetRequestMode.ImmediateLoad);
            public const string ShadowSwingPath = "NPCs/Enemies/ShadowSwing";
            public static Asset<Texture2D> WildSilkwormAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(WildSilkwormPath);
            public static Asset<Texture2D> WildSilkwormImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(WildSilkwormPath, AssetRequestMode.ImmediateLoad);
            public const string WildSilkwormPath = "NPCs/Enemies/WildSilkworm";
        }

    }

    public static class Projectiles
    {
        public static class Misc
        {
            public static Asset<Texture2D> AccumulationAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(AccumulationPath);
            public static Asset<Texture2D> AccumulationImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(AccumulationPath, AssetRequestMode.ImmediateLoad);
            public const string AccumulationPath = "Projectiles/Misc/Accumulation";
            public static Asset<Texture2D> BigTouchProAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(BigTouchProPath);
            public static Asset<Texture2D> BigTouchProImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(BigTouchProPath, AssetRequestMode.ImmediateLoad);
            public const string BigTouchProPath = "Projectiles/Misc/BigTouchPro";
            public static Asset<Texture2D> BlueLightProAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(BlueLightProPath);
            public static Asset<Texture2D> BlueLightProImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(BlueLightProPath, AssetRequestMode.ImmediateLoad);
            public const string BlueLightProPath = "Projectiles/Misc/BlueLightPro";
            public static Asset<Texture2D> CannonBaseAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(CannonBasePath);
            public static Asset<Texture2D> CannonBaseImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(CannonBasePath, AssetRequestMode.ImmediateLoad);
            public const string CannonBasePath = "Projectiles/Misc/CannonBase";
            public static Asset<Texture2D> CannonBaseGhostAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(CannonBaseGhostPath);
            public static Asset<Texture2D> CannonBaseGhostImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(CannonBaseGhostPath, AssetRequestMode.ImmediateLoad);
            public const string CannonBaseGhostPath = "Projectiles/Misc/CannonBaseGhost";
            public static Asset<Texture2D> CannonBowAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(CannonBowPath);
            public static Asset<Texture2D> CannonBowImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(CannonBowPath, AssetRequestMode.ImmediateLoad);
            public const string CannonBowPath = "Projectiles/Misc/CannonBow";
            public static Asset<Texture2D> CannonBowGhostAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(CannonBowGhostPath);
            public static Asset<Texture2D> CannonBowGhostImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(CannonBowGhostPath, AssetRequestMode.ImmediateLoad);
            public const string CannonBowGhostPath = "Projectiles/Misc/CannonBowGhost";
            public static Asset<Texture2D> CrystalSentenceAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(CrystalSentencePath);
            public static Asset<Texture2D> CrystalSentenceImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(CrystalSentencePath, AssetRequestMode.ImmediateLoad);
            public const string CrystalSentencePath = "Projectiles/Misc/CrystalSentence";
            public static Asset<Texture2D> EnchantingProAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(EnchantingProPath);
            public static Asset<Texture2D> EnchantingProImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(EnchantingProPath, AssetRequestMode.ImmediateLoad);
            public const string EnchantingProPath = "Projectiles/Misc/EnchantingPro";
            public static Asset<Texture2D> FriendlyStarAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(FriendlyStarPath);
            public static Asset<Texture2D> FriendlyStarImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(FriendlyStarPath, AssetRequestMode.ImmediateLoad);
            public const string FriendlyStarPath = "Projectiles/Misc/FriendlyStar";
            public static Asset<Texture2D> HolyFlameAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(HolyFlamePath);
            public static Asset<Texture2D> HolyFlameImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(HolyFlamePath, AssetRequestMode.ImmediateLoad);
            public const string HolyFlamePath = "Projectiles/Misc/HolyFlame";
            public static Asset<Texture2D> LightAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(LightPath);
            public static Asset<Texture2D> LightImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(LightPath, AssetRequestMode.ImmediateLoad);
            public const string LightPath = "Projectiles/Misc/Light";
            public static Asset<Texture2D> PhantomTorchAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(PhantomTorchPath);
            public static Asset<Texture2D> PhantomTorchImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(PhantomTorchPath, AssetRequestMode.ImmediateLoad);
            public const string PhantomTorchPath = "Projectiles/Misc/PhantomTorch";
            public static Asset<Texture2D> PurpleSteelAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(PurpleSteelPath);
            public static Asset<Texture2D> PurpleSteelImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(PurpleSteelPath, AssetRequestMode.ImmediateLoad);
            public const string PurpleSteelPath = "Projectiles/Misc/PurpleSteel";
            public static Asset<Texture2D> RedarrowAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(RedarrowPath);
            public static Asset<Texture2D> RedarrowImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(RedarrowPath, AssetRequestMode.ImmediateLoad);
            public const string RedarrowPath = "Projectiles/Misc/Redarrow";
            public static Asset<Texture2D> Redarrow2Asset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(Redarrow2Path);
            public static Asset<Texture2D> Redarrow2ImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(Redarrow2Path, AssetRequestMode.ImmediateLoad);
            public const string Redarrow2Path = "Projectiles/Misc/Redarrow2";
            public static Asset<Texture2D> StickyGhostAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(StickyGhostPath);
            public static Asset<Texture2D> StickyGhostImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(StickyGhostPath, AssetRequestMode.ImmediateLoad);
            public const string StickyGhostPath = "Projectiles/Misc/StickyGhost";
            public static Asset<Texture2D> SuicideAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(SuicidePath);
            public static Asset<Texture2D> SuicideImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(SuicidePath, AssetRequestMode.ImmediateLoad);
            public const string SuicidePath = "Projectiles/Misc/Suicide";
            public static Asset<Texture2D> WanAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(WanPath);
            public static Asset<Texture2D> WanImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(WanPath, AssetRequestMode.ImmediateLoad);
            public const string WanPath = "Projectiles/Misc/Wan";
            public static Asset<Texture2D> YaoAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(YaoPath);
            public static Asset<Texture2D> YaoImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(YaoPath, AssetRequestMode.ImmediateLoad);
            public const string YaoPath = "Projectiles/Misc/Yao";
            public static class Draw_textures
            {
                public static Asset<Texture2D> light1Asset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(light1Path);
                public static Asset<Texture2D> light1ImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(light1Path, AssetRequestMode.ImmediateLoad);
                public const string light1Path = "Projectiles/Misc/Draw_textures/light1";
                public static Asset<Texture2D> shade0Asset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(shade0Path);
                public static Asset<Texture2D> shade0ImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(shade0Path, AssetRequestMode.ImmediateLoad);
                public const string shade0Path = "Projectiles/Misc/Draw_textures/shade0";
            }

        }

        public static class Series
        {
            public static class Boss
            {
                public static class MiracleRecorder
                {
                    public static Asset<Texture2D> BlackMistAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(BlackMistPath);
                    public static Asset<Texture2D> BlackMistImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(BlackMistPath, AssetRequestMode.ImmediateLoad);
                    public const string BlackMistPath = "Projectiles/Series/Boss/MiracleRecorder/BlackMist";
                    public static Asset<Texture2D> Circle0Asset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(Circle0Path);
                    public static Asset<Texture2D> Circle0ImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(Circle0Path, AssetRequestMode.ImmediateLoad);
                    public const string Circle0Path = "Projectiles/Series/Boss/MiracleRecorder/Circle0";
                    public static Asset<Texture2D> Circle1Asset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(Circle1Path);
                    public static Asset<Texture2D> Circle1ImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(Circle1Path, AssetRequestMode.ImmediateLoad);
                    public const string Circle1Path = "Projectiles/Series/Boss/MiracleRecorder/Circle1";
                    public static Asset<Texture2D> Circle2Asset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(Circle2Path);
                    public static Asset<Texture2D> Circle2ImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(Circle2Path, AssetRequestMode.ImmediateLoad);
                    public const string Circle2Path = "Projectiles/Series/Boss/MiracleRecorder/Circle2";
                    public static Asset<Texture2D> Circle3Asset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(Circle3Path);
                    public static Asset<Texture2D> Circle3ImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(Circle3Path, AssetRequestMode.ImmediateLoad);
                    public const string Circle3Path = "Projectiles/Series/Boss/MiracleRecorder/Circle3";
                    public static Asset<Texture2D> DamageCircleAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(DamageCirclePath);
                    public static Asset<Texture2D> DamageCircleImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(DamageCirclePath, AssetRequestMode.ImmediateLoad);
                    public const string DamageCirclePath = "Projectiles/Series/Boss/MiracleRecorder/DamageCircle";
                    public static Asset<Texture2D> DamageCircle2Asset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(DamageCircle2Path);
                    public static Asset<Texture2D> DamageCircle2ImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(DamageCircle2Path, AssetRequestMode.ImmediateLoad);
                    public const string DamageCircle2Path = "Projectiles/Series/Boss/MiracleRecorder/DamageCircle2";
                    public static Asset<Texture2D> DamageCircle3Asset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(DamageCircle3Path);
                    public static Asset<Texture2D> DamageCircle3ImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(DamageCircle3Path, AssetRequestMode.ImmediateLoad);
                    public const string DamageCircle3Path = "Projectiles/Series/Boss/MiracleRecorder/DamageCircle3";
                    public static Asset<Texture2D> GoldCircle1Asset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(GoldCircle1Path);
                    public static Asset<Texture2D> GoldCircle1ImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(GoldCircle1Path, AssetRequestMode.ImmediateLoad);
                    public const string GoldCircle1Path = "Projectiles/Series/Boss/MiracleRecorder/GoldCircle1";
                    public static Asset<Texture2D> GoldCircle2Asset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(GoldCircle2Path);
                    public static Asset<Texture2D> GoldCircle2ImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(GoldCircle2Path, AssetRequestMode.ImmediateLoad);
                    public const string GoldCircle2Path = "Projectiles/Series/Boss/MiracleRecorder/GoldCircle2";
                    public static Asset<Texture2D> GoldDamageCircle0Asset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(GoldDamageCircle0Path);
                    public static Asset<Texture2D> GoldDamageCircle0ImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(GoldDamageCircle0Path, AssetRequestMode.ImmediateLoad);
                    public const string GoldDamageCircle0Path = "Projectiles/Series/Boss/MiracleRecorder/GoldDamageCircle0";
                    public static Asset<Texture2D> HolyFireAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(HolyFirePath);
                    public static Asset<Texture2D> HolyFireImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(HolyFirePath, AssetRequestMode.ImmediateLoad);
                    public const string HolyFirePath = "Projectiles/Series/Boss/MiracleRecorder/HolyFire";
                    public static Asset<Texture2D> HolyprojAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(HolyprojPath);
                    public static Asset<Texture2D> HolyprojImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(HolyprojPath, AssetRequestMode.ImmediateLoad);
                    public const string HolyprojPath = "Projectiles/Series/Boss/MiracleRecorder/Holyproj";
                    public static Asset<Texture2D> Laser01Asset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(Laser01Path);
                    public static Asset<Texture2D> Laser01ImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(Laser01Path, AssetRequestMode.ImmediateLoad);
                    public const string Laser01Path = "Projectiles/Series/Boss/MiracleRecorder/Laser01";
                    public static Asset<Texture2D> Laser02Asset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(Laser02Path);
                    public static Asset<Texture2D> Laser02ImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(Laser02Path, AssetRequestMode.ImmediateLoad);
                    public const string Laser02Path = "Projectiles/Series/Boss/MiracleRecorder/Laser02";
                    public static Asset<Texture2D> Laser03Asset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(Laser03Path);
                    public static Asset<Texture2D> Laser03ImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(Laser03Path, AssetRequestMode.ImmediateLoad);
                    public const string Laser03Path = "Projectiles/Series/Boss/MiracleRecorder/Laser03";
                    public static Asset<Texture2D> LightDotAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(LightDotPath);
                    public static Asset<Texture2D> LightDotImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(LightDotPath, AssetRequestMode.ImmediateLoad);
                    public const string LightDotPath = "Projectiles/Series/Boss/MiracleRecorder/LightDot";
                    public static Asset<Texture2D> PinAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(PinPath);
                    public static Asset<Texture2D> PinImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(PinPath, AssetRequestMode.ImmediateLoad);
                    public const string PinPath = "Projectiles/Series/Boss/MiracleRecorder/Pin";
                    public static Asset<Texture2D> projTexAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(projTexPath);
                    public static Asset<Texture2D> projTexImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(projTexPath, AssetRequestMode.ImmediateLoad);
                    public const string projTexPath = "Projectiles/Series/Boss/MiracleRecorder/projTex";
                    public static Asset<Texture2D> RankRoundAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(RankRoundPath);
                    public static Asset<Texture2D> RankRoundImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(RankRoundPath, AssetRequestMode.ImmediateLoad);
                    public const string RankRoundPath = "Projectiles/Series/Boss/MiracleRecorder/RankRound";
                    public static Asset<Texture2D> RoundAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(RoundPath);
                    public static Asset<Texture2D> RoundImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(RoundPath, AssetRequestMode.ImmediateLoad);
                    public const string RoundPath = "Projectiles/Series/Boss/MiracleRecorder/Round";
                    public static Asset<Texture2D> Round2Asset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(Round2Path);
                    public static Asset<Texture2D> Round2ImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(Round2Path, AssetRequestMode.ImmediateLoad);
                    public const string Round2Path = "Projectiles/Series/Boss/MiracleRecorder/Round2";
                    public static Asset<Texture2D> Round3Asset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(Round3Path);
                    public static Asset<Texture2D> Round3ImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(Round3Path, AssetRequestMode.ImmediateLoad);
                    public const string Round3Path = "Projectiles/Series/Boss/MiracleRecorder/Round3";
                    public static Asset<Texture2D> ServantsAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(ServantsPath);
                    public static Asset<Texture2D> ServantsImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(ServantsPath, AssetRequestMode.ImmediateLoad);
                    public const string ServantsPath = "Projectiles/Series/Boss/MiracleRecorder/Servants";
                    public static Asset<Texture2D> SpaceAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(SpacePath);
                    public static Asset<Texture2D> SpaceImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(SpacePath, AssetRequestMode.ImmediateLoad);
                    public const string SpacePath = "Projectiles/Series/Boss/MiracleRecorder/Space";
                    public static Asset<Texture2D> SparkAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(SparkPath);
                    public static Asset<Texture2D> SparkImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(SparkPath, AssetRequestMode.ImmediateLoad);
                    public const string SparkPath = "Projectiles/Series/Boss/MiracleRecorder/Spark";
                    public static Asset<Texture2D> SparkleAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(SparklePath);
                    public static Asset<Texture2D> SparkleImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(SparklePath, AssetRequestMode.ImmediateLoad);
                    public const string SparklePath = "Projectiles/Series/Boss/MiracleRecorder/Sparkle";
                    public static Asset<Texture2D> StarAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(StarPath);
                    public static Asset<Texture2D> StarImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(StarPath, AssetRequestMode.ImmediateLoad);
                    public const string StarPath = "Projectiles/Series/Boss/MiracleRecorder/Star";
                    public static Asset<Texture2D> Star2Asset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(Star2Path);
                    public static Asset<Texture2D> Star2ImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(Star2Path, AssetRequestMode.ImmediateLoad);
                    public const string Star2Path = "Projectiles/Series/Boss/MiracleRecorder/Star2";
                }

            }

            public static class Items
            {
                public static class Frosted
                {
                    public static Asset<Texture2D> Flycutter1Asset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(Flycutter1Path);
                    public static Asset<Texture2D> Flycutter1ImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(Flycutter1Path, AssetRequestMode.ImmediateLoad);
                    public const string Flycutter1Path = "Projectiles/Series/Items/Frosted/Flycutter1";
                    public static Asset<Texture2D> Flycutter2Asset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(Flycutter2Path);
                    public static Asset<Texture2D> Flycutter2ImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(Flycutter2Path, AssetRequestMode.ImmediateLoad);
                    public const string Flycutter2Path = "Projectiles/Series/Items/Frosted/Flycutter2";
                    public static Asset<Texture2D> Flycutter3Asset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(Flycutter3Path);
                    public static Asset<Texture2D> Flycutter3ImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(Flycutter3Path, AssetRequestMode.ImmediateLoad);
                    public const string Flycutter3Path = "Projectiles/Series/Items/Frosted/Flycutter3";
                    public static Asset<Texture2D> IcelightAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(IcelightPath);
                    public static Asset<Texture2D> IcelightImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(IcelightPath, AssetRequestMode.ImmediateLoad);
                    public const string IcelightPath = "Projectiles/Series/Items/Frosted/Icelight";
                    public static Asset<Texture2D> ProFrostedThornAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(ProFrostedThornPath);
                    public static Asset<Texture2D> ProFrostedThornImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(ProFrostedThornPath, AssetRequestMode.ImmediateLoad);
                    public const string ProFrostedThornPath = "Projectiles/Series/Items/Frosted/ProFrostedThorn";
                }

                public static class HollowKnight
                {
                    public static Asset<Texture2D> AbyssShriekAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(AbyssShriekPath);
                    public static Asset<Texture2D> AbyssShriekImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(AbyssShriekPath, AssetRequestMode.ImmediateLoad);
                    public const string AbyssShriekPath = "Projectiles/Series/Items/HollowKnight/AbyssShriek";
                    public static Asset<Texture2D> BeFallAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(BeFallPath);
                    public static Asset<Texture2D> BeFallImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(BeFallPath, AssetRequestMode.ImmediateLoad);
                    public const string BeFallPath = "Projectiles/Series/Items/HollowKnight/BeFall";
                    public static Asset<Texture2D> DescendingDarkAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(DescendingDarkPath);
                    public static Asset<Texture2D> DescendingDarkImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(DescendingDarkPath, AssetRequestMode.ImmediateLoad);
                    public const string DescendingDarkPath = "Projectiles/Series/Items/HollowKnight/DescendingDark";
                    public static Asset<Texture2D> DescendingDark_F2Asset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(DescendingDark_F2Path);
                    public static Asset<Texture2D> DescendingDark_F2ImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(DescendingDark_F2Path, AssetRequestMode.ImmediateLoad);
                    public const string DescendingDark_F2Path = "Projectiles/Series/Items/HollowKnight/DescendingDark_F2";
                    public static Asset<Texture2D> DescendingDark_TwoAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(DescendingDark_TwoPath);
                    public static Asset<Texture2D> DescendingDark_TwoImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(DescendingDark_TwoPath, AssetRequestMode.ImmediateLoad);
                    public const string DescendingDark_TwoPath = "Projectiles/Series/Items/HollowKnight/DescendingDark_Two";
                    public static Asset<Texture2D> DesolateDiveAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(DesolateDivePath);
                    public static Asset<Texture2D> DesolateDiveImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(DesolateDivePath, AssetRequestMode.ImmediateLoad);
                    public const string DesolateDivePath = "Projectiles/Series/Items/HollowKnight/DesolateDive";
                    public static Asset<Texture2D> DesolateDive_F1Asset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(DesolateDive_F1Path);
                    public static Asset<Texture2D> DesolateDive_F1ImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(DesolateDive_F1Path, AssetRequestMode.ImmediateLoad);
                    public const string DesolateDive_F1Path = "Projectiles/Series/Items/HollowKnight/DesolateDive_F1";
                    public static Asset<Texture2D> DesolateDive_F2Asset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(DesolateDive_F2Path);
                    public static Asset<Texture2D> DesolateDive_F2ImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(DesolateDive_F2Path, AssetRequestMode.ImmediateLoad);
                    public const string DesolateDive_F2Path = "Projectiles/Series/Items/HollowKnight/DesolateDive_F2";
                    public static Asset<Texture2D> DesolateDive_F3Asset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(DesolateDive_F3Path);
                    public static Asset<Texture2D> DesolateDive_F3ImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(DesolateDive_F3Path, AssetRequestMode.ImmediateLoad);
                    public const string DesolateDive_F3Path = "Projectiles/Series/Items/HollowKnight/DesolateDive_F3";
                    public static Asset<Texture2D> DesolateDive_LightAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(DesolateDive_LightPath);
                    public static Asset<Texture2D> DesolateDive_LightImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(DesolateDive_LightPath, AssetRequestMode.ImmediateLoad);
                    public const string DesolateDive_LightPath = "Projectiles/Series/Items/HollowKnight/DesolateDive_Light";
                    public static Asset<Texture2D> FocusAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(FocusPath);
                    public static Asset<Texture2D> FocusImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(FocusPath, AssetRequestMode.ImmediateLoad);
                    public const string FocusPath = "Projectiles/Series/Items/HollowKnight/Focus";
                    public static Asset<Texture2D> HallownestAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(HallownestPath);
                    public static Asset<Texture2D> HallownestImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(HallownestPath, AssetRequestMode.ImmediateLoad);
                    public const string HallownestPath = "Projectiles/Series/Items/HollowKnight/Hallownest";
                    public static Asset<Texture2D> HowlingWraithsAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(HowlingWraithsPath);
                    public static Asset<Texture2D> HowlingWraithsImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(HowlingWraithsPath, AssetRequestMode.ImmediateLoad);
                    public const string HowlingWraithsPath = "Projectiles/Series/Items/HollowKnight/HowlingWraiths";
                    public static Asset<Texture2D> HowlingWraiths2Asset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(HowlingWraiths2Path);
                    public static Asset<Texture2D> HowlingWraiths2ImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(HowlingWraiths2Path, AssetRequestMode.ImmediateLoad);
                    public const string HowlingWraiths2Path = "Projectiles/Series/Items/HollowKnight/HowlingWraiths2";
                    public static Asset<Texture2D> KnifeLightAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(KnifeLightPath);
                    public static Asset<Texture2D> KnifeLightImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(KnifeLightPath, AssetRequestMode.ImmediateLoad);
                    public const string KnifeLightPath = "Projectiles/Series/Items/HollowKnight/KnifeLight";
                    public static Asset<Texture2D> KnifeLight01Asset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(KnifeLight01Path);
                    public static Asset<Texture2D> KnifeLight01ImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(KnifeLight01Path, AssetRequestMode.ImmediateLoad);
                    public const string KnifeLight01Path = "Projectiles/Series/Items/HollowKnight/KnifeLight01";
                    public static Asset<Texture2D> KnifeLight2Asset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(KnifeLight2Path);
                    public static Asset<Texture2D> KnifeLight2ImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(KnifeLight2Path, AssetRequestMode.ImmediateLoad);
                    public const string KnifeLight2Path = "Projectiles/Series/Items/HollowKnight/KnifeLight2";
                    public static Asset<Texture2D> KnifeLight3Asset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(KnifeLight3Path);
                    public static Asset<Texture2D> KnifeLight3ImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(KnifeLight3Path, AssetRequestMode.ImmediateLoad);
                    public const string KnifeLight3Path = "Projectiles/Series/Items/HollowKnight/KnifeLight3";
                    public static Asset<Texture2D> ShadeSoulAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(ShadeSoulPath);
                    public static Asset<Texture2D> ShadeSoulImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(ShadeSoulPath, AssetRequestMode.ImmediateLoad);
                    public const string ShadeSoulPath = "Projectiles/Series/Items/HollowKnight/ShadeSoul";
                    public static Asset<Texture2D> ShadeSoul2Asset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(ShadeSoul2Path);
                    public static Asset<Texture2D> ShadeSoul2ImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(ShadeSoul2Path, AssetRequestMode.ImmediateLoad);
                    public const string ShadeSoul2Path = "Projectiles/Series/Items/HollowKnight/ShadeSoul2";
                    public static Asset<Texture2D> ShockAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(ShockPath);
                    public static Asset<Texture2D> ShockImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(ShockPath, AssetRequestMode.ImmediateLoad);
                    public const string ShockPath = "Projectiles/Series/Items/HollowKnight/Shock";
                    public static Asset<Texture2D> SmashRangeAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(SmashRangePath);
                    public static Asset<Texture2D> SmashRangeImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(SmashRangePath, AssetRequestMode.ImmediateLoad);
                    public const string SmashRangePath = "Projectiles/Series/Items/HollowKnight/SmashRange";
                    public static Asset<Texture2D> SwingAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(SwingPath);
                    public static Asset<Texture2D> SwingImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(SwingPath, AssetRequestMode.ImmediateLoad);
                    public const string SwingPath = "Projectiles/Series/Items/HollowKnight/Swing";
                    public static Asset<Texture2D> VengefulSpiritAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(VengefulSpiritPath);
                    public static Asset<Texture2D> VengefulSpiritImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(VengefulSpiritPath, AssetRequestMode.ImmediateLoad);
                    public const string VengefulSpiritPath = "Projectiles/Series/Items/HollowKnight/VengefulSpirit";
                    public static Asset<Texture2D> VengefulSpirit2Asset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(VengefulSpirit2Path);
                    public static Asset<Texture2D> VengefulSpirit2ImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(VengefulSpirit2Path, AssetRequestMode.ImmediateLoad);
                    public const string VengefulSpirit2Path = "Projectiles/Series/Items/HollowKnight/VengefulSpirit2";
                    public static Asset<Texture2D> WhiteScreenAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(WhiteScreenPath);
                    public static Asset<Texture2D> WhiteScreenImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(WhiteScreenPath, AssetRequestMode.ImmediateLoad);
                    public const string WhiteScreenPath = "Projectiles/Series/Items/HollowKnight/WhiteScreen";
                    public static Asset<Texture2D> __3Asset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(__3Path);
                    public static Asset<Texture2D> __3ImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(__3Path, AssetRequestMode.ImmediateLoad);
                    public const string __3Path = "Projectiles/Series/Items/HollowKnight/刀光3";
                    public static Asset<Texture2D> __4Asset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(__4Path);
                    public static Asset<Texture2D> __4ImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(__4Path, AssetRequestMode.ImmediateLoad);
                    public const string __4Path = "Projectiles/Series/Items/HollowKnight/刀光4";
                    public static Asset<Texture2D> ____Asset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(____Path);
                    public static Asset<Texture2D> ____ImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(____Path, AssetRequestMode.ImmediateLoad);
                    public const string ____Path = "Projectiles/Series/Items/HollowKnight/黑砸粒子";
                    public static Asset<Texture2D> ____2Asset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(____2Path);
                    public static Asset<Texture2D> ____2ImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(____2Path, AssetRequestMode.ImmediateLoad);
                    public const string ____2Path = "Projectiles/Series/Items/HollowKnight/黑砸粒子2";
                }

                public static class Miracle
                {
                    public static Asset<Texture2D> GloryLightAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(GloryLightPath);
                    public static Asset<Texture2D> GloryLightImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(GloryLightPath, AssetRequestMode.ImmediateLoad);
                    public const string GloryLightPath = "Projectiles/Series/Items/Miracle/GloryLight";
                    public static Asset<Texture2D> GloryLight2Asset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(GloryLight2Path);
                    public static Asset<Texture2D> GloryLight2ImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(GloryLight2Path, AssetRequestMode.ImmediateLoad);
                    public const string GloryLight2Path = "Projectiles/Series/Items/Miracle/GloryLight2";
                    public static Asset<Texture2D> GloryProjAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(GloryProjPath);
                    public static Asset<Texture2D> GloryProjImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(GloryProjPath, AssetRequestMode.ImmediateLoad);
                    public const string GloryProjPath = "Projectiles/Series/Items/Miracle/GloryProj";
                    public static Asset<Texture2D> GloryProjGhostAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(GloryProjGhostPath);
                    public static Asset<Texture2D> GloryProjGhostImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(GloryProjGhostPath, AssetRequestMode.ImmediateLoad);
                    public const string GloryProjGhostPath = "Projectiles/Series/Items/Miracle/GloryProjGhost";
                }

                public static class ReCharge
                {
                    public static Asset<Texture2D> AngelAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(AngelPath);
                    public static Asset<Texture2D> AngelImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(AngelPath, AssetRequestMode.ImmediateLoad);
                    public const string AngelPath = "Projectiles/Series/Items/ReCharge/Angel";
                    public static Asset<Texture2D> AngelBoomAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(AngelBoomPath);
                    public static Asset<Texture2D> AngelBoomImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(AngelBoomPath, AssetRequestMode.ImmediateLoad);
                    public const string AngelBoomPath = "Projectiles/Series/Items/ReCharge/AngelBoom";
                    public static Asset<Texture2D> Laser1Asset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(Laser1Path);
                    public static Asset<Texture2D> Laser1ImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(Laser1Path, AssetRequestMode.ImmediateLoad);
                    public const string Laser1Path = "Projectiles/Series/Items/ReCharge/Laser1";
                    public static Asset<Texture2D> Laser2Asset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(Laser2Path);
                    public static Asset<Texture2D> Laser2ImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(Laser2Path, AssetRequestMode.ImmediateLoad);
                    public const string Laser2Path = "Projectiles/Series/Items/ReCharge/Laser2";
                    public static Asset<Texture2D> Laser3Asset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(Laser3Path);
                    public static Asset<Texture2D> Laser3ImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(Laser3Path, AssetRequestMode.ImmediateLoad);
                    public const string Laser3Path = "Projectiles/Series/Items/ReCharge/Laser3";
                    public static Asset<Texture2D> LaserGunAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(LaserGunPath);
                    public static Asset<Texture2D> LaserGunImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(LaserGunPath, AssetRequestMode.ImmediateLoad);
                    public const string LaserGunPath = "Projectiles/Series/Items/ReCharge/LaserGun";
                    public static Asset<Texture2D> PrismAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(PrismPath);
                    public static Asset<Texture2D> PrismImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(PrismPath, AssetRequestMode.ImmediateLoad);
                    public const string PrismPath = "Projectiles/Series/Items/ReCharge/Prism";
                    public static Asset<Texture2D> pro1Asset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(pro1Path);
                    public static Asset<Texture2D> pro1ImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(pro1Path, AssetRequestMode.ImmediateLoad);
                    public const string pro1Path = "Projectiles/Series/Items/ReCharge/pro1";
                    public static Asset<Texture2D> pro2Asset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(pro2Path);
                    public static Asset<Texture2D> pro2ImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(pro2Path, AssetRequestMode.ImmediateLoad);
                    public const string pro2Path = "Projectiles/Series/Items/ReCharge/pro2";
                    public static Asset<Texture2D> StarAngelAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(StarAngelPath);
                    public static Asset<Texture2D> StarAngelImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(StarAngelPath, AssetRequestMode.ImmediateLoad);
                    public const string StarAngelPath = "Projectiles/Series/Items/ReCharge/StarAngel";
                    public static Asset<Texture2D> StarAngelBoomAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(StarAngelBoomPath);
                    public static Asset<Texture2D> StarAngelBoomImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(StarAngelBoomPath, AssetRequestMode.ImmediateLoad);
                    public const string StarAngelBoomPath = "Projectiles/Series/Items/ReCharge/StarAngelBoom";
                    public static Asset<Texture2D> ThomasAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(ThomasPath);
                    public static Asset<Texture2D> ThomasImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(ThomasPath, AssetRequestMode.ImmediateLoad);
                    public const string ThomasPath = "Projectiles/Series/Items/ReCharge/Thomas";
                    public static Asset<Texture2D> ZapinatorAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(ZapinatorPath);
                    public static Asset<Texture2D> ZapinatorImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(ZapinatorPath, AssetRequestMode.ImmediateLoad);
                    public const string ZapinatorPath = "Projectiles/Series/Items/ReCharge/Zapinator";
                }

                public static class RiftValley
                {
                    public static Asset<Texture2D> RosewoodCrystalAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(RosewoodCrystalPath);
                    public static Asset<Texture2D> RosewoodCrystalImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(RosewoodCrystalPath, AssetRequestMode.ImmediateLoad);
                    public const string RosewoodCrystalPath = "Projectiles/Series/Items/RiftValley/RosewoodCrystal";
                    public static Asset<Texture2D> RosewoodCrystal2Asset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(RosewoodCrystal2Path);
                    public static Asset<Texture2D> RosewoodCrystal2ImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(RosewoodCrystal2Path, AssetRequestMode.ImmediateLoad);
                    public const string RosewoodCrystal2Path = "Projectiles/Series/Items/RiftValley/RosewoodCrystal2";
                    public static Asset<Texture2D> RosewoodCrystal3Asset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(RosewoodCrystal3Path);
                    public static Asset<Texture2D> RosewoodCrystal3ImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(RosewoodCrystal3Path, AssetRequestMode.ImmediateLoad);
                    public const string RosewoodCrystal3Path = "Projectiles/Series/Items/RiftValley/RosewoodCrystal3";
                }

                public static class Sharpsand
                {
                    public static Asset<Texture2D> BombAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(BombPath);
                    public static Asset<Texture2D> BombImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(BombPath, AssetRequestMode.ImmediateLoad);
                    public const string BombPath = "Projectiles/Series/Items/Sharpsand/Bomb";
                    public static Asset<Texture2D> SharpsandBOOMAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(SharpsandBOOMPath);
                    public static Asset<Texture2D> SharpsandBOOMImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(SharpsandBOOMPath, AssetRequestMode.ImmediateLoad);
                    public const string SharpsandBOOMPath = "Projectiles/Series/Items/Sharpsand/SharpsandBOOM";
                    public static Asset<Texture2D> SharpsandFireAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(SharpsandFirePath);
                    public static Asset<Texture2D> SharpsandFireImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(SharpsandFirePath, AssetRequestMode.ImmediateLoad);
                    public const string SharpsandFirePath = "Projectiles/Series/Items/Sharpsand/SharpsandFire";
                    public static Asset<Texture2D> SharpsandSwordgasAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(SharpsandSwordgasPath);
                    public static Asset<Texture2D> SharpsandSwordgasImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(SharpsandSwordgasPath, AssetRequestMode.ImmediateLoad);
                    public const string SharpsandSwordgasPath = "Projectiles/Series/Items/Sharpsand/SharpsandSwordgas";
                }

            }

        }

    }

    public static class Tiles
    {
        public static Asset<Texture2D> AbandonedAltarAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(AbandonedAltarPath);
        public static Asset<Texture2D> AbandonedAltarImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(AbandonedAltarPath, AssetRequestMode.ImmediateLoad);
        public const string AbandonedAltarPath = "Tiles/AbandonedAltar";
        public static Asset<Texture2D> AyTsaoAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(AyTsaoPath);
        public static Asset<Texture2D> AyTsaoImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(AyTsaoPath, AssetRequestMode.ImmediateLoad);
        public const string AyTsaoPath = "Tiles/AyTsao";
        public static Asset<Texture2D> HolySpiritCurseAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(HolySpiritCursePath);
        public static Asset<Texture2D> HolySpiritCurseImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(HolySpiritCursePath, AssetRequestMode.ImmediateLoad);
        public const string HolySpiritCursePath = "Tiles/HolySpiritCurse";
        public static Asset<Texture2D> LargePottedPlantAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(LargePottedPlantPath);
        public static Asset<Texture2D> LargePottedPlantImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(LargePottedPlantPath, AssetRequestMode.ImmediateLoad);
        public const string LargePottedPlantPath = "Tiles/LargePottedPlant";
        public static Asset<Texture2D> MagicPowerFallingStarAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(MagicPowerFallingStarPath);
        public static Asset<Texture2D> MagicPowerFallingStarImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(MagicPowerFallingStarPath, AssetRequestMode.ImmediateLoad);
        public const string MagicPowerFallingStarPath = "Tiles/MagicPowerFallingStar";
        public static class RiftValley
        {
            public static Asset<Texture2D> AncientPlantBlocktileAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(AncientPlantBlocktilePath);
            public static Asset<Texture2D> AncientPlantBlocktileImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(AncientPlantBlocktilePath, AssetRequestMode.ImmediateLoad);
            public const string AncientPlantBlocktilePath = "Tiles/RiftValley/AncientPlantBlocktile";
            public static Asset<Texture2D> DarkBlueMantraStoneAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(DarkBlueMantraStonePath);
            public static Asset<Texture2D> DarkBlueMantraStoneImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(DarkBlueMantraStonePath, AssetRequestMode.ImmediateLoad);
            public const string DarkBlueMantraStonePath = "Tiles/RiftValley/DarkBlueMantraStone";
            public static Asset<Texture2D> DisasterSilverBarAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(DisasterSilverBarPath);
            public static Asset<Texture2D> DisasterSilverBarImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(DisasterSilverBarPath, AssetRequestMode.ImmediateLoad);
            public const string DisasterSilverBarPath = "Tiles/RiftValley/DisasterSilverBar";
            public static Asset<Texture2D> DisasterSilverOreAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(DisasterSilverOrePath);
            public static Asset<Texture2D> DisasterSilverOreImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(DisasterSilverOrePath, AssetRequestMode.ImmediateLoad);
            public const string DisasterSilverOrePath = "Tiles/RiftValley/DisasterSilverOre";
            public static Asset<Texture2D> DreamMosstileAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(DreamMosstilePath);
            public static Asset<Texture2D> DreamMosstileImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(DreamMosstilePath, AssetRequestMode.ImmediateLoad);
            public const string DreamMosstilePath = "Tiles/RiftValley/DreamMosstile";
            public static Asset<Texture2D> DreamTurftileAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(DreamTurftilePath);
            public static Asset<Texture2D> DreamTurftileImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(DreamTurftilePath, AssetRequestMode.ImmediateLoad);
            public const string DreamTurftilePath = "Tiles/RiftValley/DreamTurftile";
            public static Asset<Texture2D> ExoticFlowerBlocktileAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(ExoticFlowerBlocktilePath);
            public static Asset<Texture2D> ExoticFlowerBlocktileImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(ExoticFlowerBlocktilePath, AssetRequestMode.ImmediateLoad);
            public const string ExoticFlowerBlocktilePath = "Tiles/RiftValley/ExoticFlowerBlocktile";
            public static Asset<Texture2D> FantasyStoneAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(FantasyStonePath);
            public static Asset<Texture2D> FantasyStoneImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(FantasyStonePath, AssetRequestMode.ImmediateLoad);
            public const string FantasyStonePath = "Tiles/RiftValley/FantasyStone";
            public static Asset<Texture2D> FlowFireGoldAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(FlowFireGoldPath);
            public static Asset<Texture2D> FlowFireGoldImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(FlowFireGoldPath, AssetRequestMode.ImmediateLoad);
            public const string FlowFireGoldPath = "Tiles/RiftValley/FlowFireGold";
            public static Asset<Texture2D> FlowFireGoldBrickAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(FlowFireGoldBrickPath);
            public static Asset<Texture2D> FlowFireGoldBrickImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(FlowFireGoldBrickPath, AssetRequestMode.ImmediateLoad);
            public const string FlowFireGoldBrickPath = "Tiles/RiftValley/FlowFireGoldBrick";
            public static Asset<Texture2D> LifeMosstileAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(LifeMosstilePath);
            public static Asset<Texture2D> LifeMosstileImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(LifeMosstilePath, AssetRequestMode.ImmediateLoad);
            public const string LifeMosstilePath = "Tiles/RiftValley/LifeMosstile";
            public static Asset<Texture2D> LifeStartileAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(LifeStartilePath);
            public static Asset<Texture2D> LifeStartileImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(LifeStartilePath, AssetRequestMode.ImmediateLoad);
            public const string LifeStartilePath = "Tiles/RiftValley/LifeStartile";
            public static Asset<Texture2D> LifeTurftileAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(LifeTurftilePath);
            public static Asset<Texture2D> LifeTurftileImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(LifeTurftilePath, AssetRequestMode.ImmediateLoad);
            public const string LifeTurftilePath = "Tiles/RiftValley/LifeTurftile";
            public static Asset<Texture2D> LossLeaftileAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(LossLeaftilePath);
            public static Asset<Texture2D> LossLeaftileImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(LossLeaftilePath, AssetRequestMode.ImmediateLoad);
            public const string LossLeaftilePath = "Tiles/RiftValley/LossLeaftile";
            public static Asset<Texture2D> MossSkintileAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(MossSkintilePath);
            public static Asset<Texture2D> MossSkintileImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(MossSkintilePath, AssetRequestMode.ImmediateLoad);
            public const string MossSkintilePath = "Tiles/RiftValley/MossSkintile";
            public static Asset<Texture2D> PhantomMosstileAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(PhantomMosstilePath);
            public static Asset<Texture2D> PhantomMosstileImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(PhantomMosstilePath, AssetRequestMode.ImmediateLoad);
            public const string PhantomMosstilePath = "Tiles/RiftValley/PhantomMosstile";
            public static Asset<Texture2D> PhantomTurftileAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(PhantomTurftilePath);
            public static Asset<Texture2D> PhantomTurftileImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(PhantomTurftilePath, AssetRequestMode.ImmediateLoad);
            public const string PhantomTurftilePath = "Tiles/RiftValley/PhantomTurftile";
            public static Asset<Texture2D> RottenMeatcsAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(RottenMeatcsPath);
            public static Asset<Texture2D> RottenMeatcsImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(RottenMeatcsPath, AssetRequestMode.ImmediateLoad);
            public const string RottenMeatcsPath = "Tiles/RiftValley/RottenMeatcs";
            public static Asset<Texture2D> SacredLeaftileAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(SacredLeaftilePath);
            public static Asset<Texture2D> SacredLeaftileImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(SacredLeaftilePath, AssetRequestMode.ImmediateLoad);
            public const string SacredLeaftilePath = "Tiles/RiftValley/SacredLeaftile";
            public static Asset<Texture2D> ShineBlockAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(ShineBlockPath);
            public static Asset<Texture2D> ShineBlockImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(ShineBlockPath, AssetRequestMode.ImmediateLoad);
            public const string ShineBlockPath = "Tiles/RiftValley/ShineBlock";
            public static Asset<Texture2D> SoulHaystackAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(SoulHaystackPath);
            public static Asset<Texture2D> SoulHaystackImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(SoulHaystackPath, AssetRequestMode.ImmediateLoad);
            public const string SoulHaystackPath = "Tiles/RiftValley/SoulHaystack";
            public static class SoulCemetery
            {
                public static Asset<Texture2D> BlueIronOreAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(BlueIronOrePath);
                public static Asset<Texture2D> BlueIronOreImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(BlueIronOrePath, AssetRequestMode.ImmediateLoad);
                public const string BlueIronOrePath = "Tiles/RiftValley/SoulCemetery/BlueIronOre";
                public static Asset<Texture2D> BoneOakCrownAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(BoneOakCrownPath);
                public static Asset<Texture2D> BoneOakCrownImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(BoneOakCrownPath, AssetRequestMode.ImmediateLoad);
                public const string BoneOakCrownPath = "Tiles/RiftValley/SoulCemetery/BoneOakCrown";
                public static Asset<Texture2D> BoneOakLeaveAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(BoneOakLeavePath);
                public static Asset<Texture2D> BoneOakLeaveImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(BoneOakLeavePath, AssetRequestMode.ImmediateLoad);
                public const string BoneOakLeavePath = "Tiles/RiftValley/SoulCemetery/BoneOakLeave";
                public static Asset<Texture2D> BoneOakTrunkAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(BoneOakTrunkPath);
                public static Asset<Texture2D> BoneOakTrunkImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(BoneOakTrunkPath, AssetRequestMode.ImmediateLoad);
                public const string BoneOakTrunkPath = "Tiles/RiftValley/SoulCemetery/BoneOakTrunk";
                public static Asset<Texture2D> Grass_1Asset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(Grass_1Path);
                public static Asset<Texture2D> Grass_1ImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(Grass_1Path, AssetRequestMode.ImmediateLoad);
                public const string Grass_1Path = "Tiles/RiftValley/SoulCemetery/Grass_1";
                public static Asset<Texture2D> Grass_2Asset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(Grass_2Path);
                public static Asset<Texture2D> Grass_2ImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(Grass_2Path, AssetRequestMode.ImmediateLoad);
                public const string Grass_2Path = "Tiles/RiftValley/SoulCemetery/Grass_2";
                public static Asset<Texture2D> PlantAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(PlantPath);
                public static Asset<Texture2D> PlantImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(PlantPath, AssetRequestMode.ImmediateLoad);
                public const string PlantPath = "Tiles/RiftValley/SoulCemetery/Plant";
                public static Asset<Texture2D> SoulCongealingSoilAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(SoulCongealingSoilPath);
                public static Asset<Texture2D> SoulCongealingSoilImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(SoulCongealingSoilPath, AssetRequestMode.ImmediateLoad);
                public const string SoulCongealingSoilPath = "Tiles/RiftValley/SoulCemetery/SoulCongealingSoil";
                public static Asset<Texture2D> WhiteSoulStoneAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(WhiteSoulStonePath);
                public static Asset<Texture2D> WhiteSoulStoneImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(WhiteSoulStonePath, AssetRequestMode.ImmediateLoad);
                public const string WhiteSoulStonePath = "Tiles/RiftValley/SoulCemetery/WhiteSoulStone";
                public static Asset<Texture2D> _____tileAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(_____tilePath);
                public static Asset<Texture2D> _____tileImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(_____tilePath, AssetRequestMode.ImmediateLoad);
                public const string _____tilePath = "Tiles/RiftValley/SoulCemetery/凝魂土草皮tile";
            }

        }

    }

    public static class UI
    {
        public static class OdeUISystem
        {
            public static class Containers
            {
                public static class QuickBar
                {
                    public static class Images
                    {
                        public static Asset<Texture2D> arrayAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(arrayPath);
                        public static Asset<Texture2D> arrayImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(arrayPath, AssetRequestMode.ImmediateLoad);
                        public const string arrayPath = "UI/OdeUISystem/Containers/QuickBar/Images/array";
                    }

                }

                public static class Recharge
                {
                    public static class Images
                    {
                        public static Asset<Texture2D> CloseButtonDownAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(CloseButtonDownPath);
                        public static Asset<Texture2D> CloseButtonDownImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(CloseButtonDownPath, AssetRequestMode.ImmediateLoad);
                        public const string CloseButtonDownPath = "UI/OdeUISystem/Containers/Recharge/Images/CloseButtonDown";
                        public static Asset<Texture2D> CloseButtonUpAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(CloseButtonUpPath);
                        public static Asset<Texture2D> CloseButtonUpImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(CloseButtonUpPath, AssetRequestMode.ImmediateLoad);
                        public const string CloseButtonUpPath = "UI/OdeUISystem/Containers/Recharge/Images/CloseButtonUp";
                        public static Asset<Texture2D> ItemSlotAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(ItemSlotPath);
                        public static Asset<Texture2D> ItemSlotImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(ItemSlotPath, AssetRequestMode.ImmediateLoad);
                        public const string ItemSlotPath = "UI/OdeUISystem/Containers/Recharge/Images/ItemSlot";
                        public static Asset<Texture2D> PaintAreaAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(PaintAreaPath);
                        public static Asset<Texture2D> PaintAreaImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(PaintAreaPath, AssetRequestMode.ImmediateLoad);
                        public const string PaintAreaPath = "UI/OdeUISystem/Containers/Recharge/Images/PaintArea";
                        public static Asset<Texture2D> PanelAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(PanelPath);
                        public static Asset<Texture2D> PanelImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(PanelPath, AssetRequestMode.ImmediateLoad);
                        public const string PanelPath = "UI/OdeUISystem/Containers/Recharge/Images/Panel";
                        public static Asset<Texture2D> SnowavePaperAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(SnowavePaperPath);
                        public static Asset<Texture2D> SnowavePaperImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(SnowavePaperPath, AssetRequestMode.ImmediateLoad);
                        public const string SnowavePaperPath = "UI/OdeUISystem/Containers/Recharge/Images/SnowavePaper";
                        public static Asset<Texture2D> XButtonDownAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(XButtonDownPath);
                        public static Asset<Texture2D> XButtonDownImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(XButtonDownPath, AssetRequestMode.ImmediateLoad);
                        public const string XButtonDownPath = "UI/OdeUISystem/Containers/Recharge/Images/XButtonDown";
                        public static Asset<Texture2D> XButtonUpAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(XButtonUpPath);
                        public static Asset<Texture2D> XButtonUpImmediateAsset => ModAssets_Utils.Mod.Assets.Request<Texture2D>(XButtonUpPath, AssetRequestMode.ImmediateLoad);
                        public const string XButtonUpPath = "UI/OdeUISystem/Containers/Recharge/Images/XButtonUp";
                    }

                }

            }

        }

    }

}

