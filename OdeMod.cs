using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OdeMod.CardMode;
using OdeMod.ShaderDatas.ScreenShaderDatas;
using OdeMod.UI.OdeUISystem;
using OdeMod.Utils;

using System;
using System.Reflection;

using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;

//using Candlight.Professions;

namespace OdeMod
{
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

        public override void Load()
        {
            base.Load();

            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                //On.Terraria.Main.DrawPlayer += Main_DrawPlayer;
                //加载LanguageType内的语言种类实例
                LanguageType.LoadCulture();

                //添加Hook
                //MonoModHooks.RequestNativeAccess();
                //MonoMod.RuntimeDetour.IDetour detour = new MonoMod.RuntimeDetour.Hook(
                //    typeof(ModDust).GetMethod("Draw", BindingFlags.Instance | BindingFlags.NonPublic),
                //   new Action<Action<ModDust, Dust, Color, float>, ModDust, Dust, Color, float>(
                //       (orig, self, dust, alpha, scale) =>
                //   {
                //       if (self is Dusts.IOdeDusts dusts && dusts.UseMyDraw)
                //       {
                //           dusts.Draw(self, dust, alpha, scale, Main.spriteBatch);
                //       }
                //       else
                //           orig(self, dust, alpha, scale);
                //   }));
                //detour.Apply();

                On.Terraria.Main.Draw += Main_Draw;
            }

            ScreenShaderDataManager.Register("OdeMod:MiracleRecorder", new BossSSD(new Ref<Effect>(
                        ModContent.Request<Effect>("OdeMod/Effects/PixelShaders/ScreenShaders/SSD1",
                        ReLogic.Content.AssetRequestMode.ImmediateLoad).Value), "Rotate"), EffectPriority.Medium);
        }

        private void Main_Draw(On.Terraria.Main.orig_Draw orig, Main self, GameTime gameTime)
        {
            if (CardSystem.Instance.OpenCardMode)
            {
                Main.gamePaused = true;
                Main.graphics.GraphicsDevice.SetRenderTarget(Main.screenTarget);
                Main.graphics.GraphicsDevice.Clear(Color.Transparent);
                Main.spriteBatch.Begin();

               // CardSystem.Instance.Draw(Main.spriteBatch);

                Main.spriteBatch.End();
                Main.graphics.GraphicsDevice.SetRenderTarget(null);
                Main.spriteBatch.Begin();
                Main.spriteBatch.Draw(Main.screenTarget, Vector2.Zero, Color.White);
                Main.spriteBatch.End();
            }
            else
                orig(self, gameTime);
        }
    }
}