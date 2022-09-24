using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OdeMod.Players;
using OdeMod.UI.OdeUISystem;
using OdeMod.Utils;

using System;
using System.Reflection;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
//using Candlight.Professions;

namespace OdeMod
{
    public class OdeMod : Mod, IOde
    {
        /// <summary>
        /// OdeMod��ʵ��
        /// </summary>
        internal static OdeMod Instance { get => ModContent.GetInstance<OdeMod>(); }
        /// <summary>
        /// Ode��UI����ϵͳʵ��
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
        /// ������Ϣ����ϵͳ��ʵ��
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
        public override void Load()
        {
            base.Load();
            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                //On.Terraria.Main.DrawPlayer += Main_DrawPlayer;
                //����LanguageType�ڵ���������ʵ��
                LanguageType.LoadCulture();

                //���Hook
                MonoModHooks.RequestNativeAccess();
                MonoMod.RuntimeDetour.IDetour detour = new MonoMod.RuntimeDetour.Hook(
                    typeof(ModDust).GetMethod("Draw", BindingFlags.Instance | BindingFlags.NonPublic),
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
                detour.Apply();
            }
        }

    }
}