using System;
using System.Reflection;

using FontStashSharp;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OdeMod.CardMode;
using OdeMod.Professions.Painter;
using OdeMod.ShaderDatas.ScreenShaderDatas;
using OdeMod.UI.OdeUISystem;
using OdeMod.Utils;

using rail;

using Steamworks;

using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod
{
    /// <summary>
    /// 模组主类
    /// </summary>
    public class OdeMod : Mod, IOde
    {
        /// <summary>
        /// OdeMod的实例
        /// </summary>
        internal static OdeMod Instance { get => ModContent.GetInstance<OdeMod>(); }

        /// <summary>
        /// Ode的UI管理系统实例
        /// </summary>
        internal static OdeUISystem OdeUISystem
        {
            get
            {
                if (Instance.uiSystem == null)
                    Instance.uiSystem = new OdeUISystem();
                return Instance.uiSystem;
            }
        }

        private OdeUISystem uiSystem;

        /// <summary>
        /// 字体信息管理系统的实例
        /// </summary>
        internal static Utils.FontInfos.DynamicSpriteFontInfoManager DynamicSpriteFontInfoManager
        {
            get
            {
                if (Instance.infoManager == null)
                    Instance.infoManager = new Utils.FontInfos.DynamicSpriteFontInfoManager();
                return Instance.infoManager;
            }
        }

        private Utils.FontInfos.DynamicSpriteFontInfoManager infoManager;

        internal static RenderTarget2DPool RenderTarget2DPool
        {
            get
            {
                if (Instance.renderTarget2DPool == null)
                    Instance.renderTarget2DPool = new RenderTarget2DPool();
                return Instance.renderTarget2DPool;
            }
        }

        private Utils.RenderTarget2DPool renderTarget2DPool;

        internal static OdeScreenShaderDataManager ScreenShaderDataManager
        {
            get
            {
                if (Instance._screenShaderDataManager == null)
                    Instance._screenShaderDataManager = new OdeScreenShaderDataManager();
                return Instance._screenShaderDataManager;
            }
        }

        private OdeScreenShaderDataManager _screenShaderDataManager;

        internal static FontManager FontManager
        {
            get
            {
                if (Instance._fontManager == null)
                    Instance._fontManager = new FontManager();
                return Instance._fontManager;
            }
        }

        private FontManager _fontManager;

        internal static BinaryProcessed BinaryProcessed
        {
            get
            {
                if (Instance._binaryProcessed == null)
                {
                    Instance._binaryProcessed = new BinaryProcessed();
                    Instance._binaryProcessed.LoadProcessed();
                }
                return Instance._binaryProcessed;
            }
        }

        private BinaryProcessed _binaryProcessed;

        /// <summary>
        /// 默认字体大小
        /// </summary>
        public const float DEFAULT_FONT_SIZE = 40f;

        /// <summary>
        /// 默认字体系统
        /// </summary>
        public static FontSystem DefaultFontSystem => FontManager["Fonts/SourceHanSansHWSC_VF.ttf"];

        /// <summary>
        /// 默认字体
        /// </summary>
        public static DynamicSpriteFont DefaultFont = DefaultFontSystem.GetFont(DEFAULT_FONT_SIZE);

        /// <summary>
        /// 加载
        /// </summary>
        public override void Load()
        {
            base.Load();
            ContentInstance.Register(new object());
            MasterpieceCurator masterpieceCurator = new MasterpieceCurator();
            masterpieceCurator.Load();
            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                //On.Terraria.Main.DrawPlayer += Main_DrawPlayer;
                //加载LanguageType内的语言种类实例
                LanguageType.LoadCulture();

                //添加Hook
                MonoMod.RuntimeDetour.Hook hook = new MonoMod.RuntimeDetour.Hook(typeof(ModDust).GetMethod("Draw", BindingFlags.Instance | BindingFlags.NonPublic),
                   new Action<Action<ModDust, Dust, Color, float>, ModDust, Dust, Color, float>(
                       (orig, self, dust, alpha, scale) =>
                   {
                       if (self is Dusts.IOdeDusts dusts && dusts.UseMyDraw)
                       {
                           dusts.Draw(self, dust, alpha, scale, Main.spriteBatch);
                       }
                       else
                           orig(self, dust, alpha, scale);
                   }));
                hook.Apply();

                On_Main.Draw += On_Main_Draw;
                On_Main.Update += On_Main_Update;
            }

            ScreenShaderDataManager.Register("OdeMod:MiracleRecorder", new BossSSD(new Ref<Effect>(
                        ModContent.Request<Effect>("OdeMod/Effects/PixelShaders/ScreenShaders/SSD1",
                        ReLogic.Content.AssetRequestMode.ImmediateLoad).Value), "Rotate"), EffectPriority.Medium);
        }

        private void On_Main_Update(On_Main.orig_Update orig, Main self, GameTime gameTime)
        {
            if (CardSystem.Instance.CardModeVisible)
            {
                Main.gamePaused = true;
                Main.graphics.GraphicsDevice.SetRenderTarget(Main.screenTarget);
                Main.graphics.GraphicsDevice.Clear(Color.Transparent);
                Main.spriteBatch.Begin();

                CardSystem.Instance.Draw(Main.spriteBatch);

                Main.spriteBatch.End();
                Main.graphics.GraphicsDevice.SetRenderTarget(null);
                Main.spriteBatch.Begin();
                Main.spriteBatch.Draw(Main.screenTarget, Vector2.Zero, Color.White);
                Main.spriteBatch.End();
            }
            else
                orig(self, gameTime);
        }

        private void On_Main_Draw(On_Main.orig_Draw orig, Main self, GameTime gameTime)
        {
            if (CardSystem.Instance.CardModeVisible)
            {
                Main.gamePaused = true;

                CardSystem.Instance.Update(gameTime);
            }
            else
                orig(self, gameTime);
        }
    }
}