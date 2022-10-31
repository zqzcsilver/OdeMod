using Microsoft.Xna.Framework.Graphics;
using OdeMod.UI.OdeUISystem;
using OdeMod.Utils;

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
        public static Effect npcEffect;
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
            }
            Filters.Scene["TemplateMod2:GBlur"] = new Filter(new BossSSD(new Ref<Effect>(ModContent.Request<Effect>("OdeMod/Effects/Content/SSD1", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value), "Rotate"), EffectPriority.Medium);
            Filters.Scene["TemplateMod2:GBlur"].Load();

        }
    }
}