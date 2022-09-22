using Microsoft.Xna.Framework;
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
        public override void Load()
        {
            base.Load();
            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                //On.Terraria.Main.DrawPlayer += Main_DrawPlayer;
                //加载LanguageType内的语言种类实例
                LanguageType.LoadCulture();

                //添加Hook
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

        /*private void Main_DrawPlayer(On.Terraria.Main.orig_DrawPlayer orig, Main self, Player drawPlayer, Vector2 Position, float rotation, Vector2 rotationOrigin, float shadow)
        {
            orig(self, drawPlayer, Position, rotation, rotationOrigin, shadow);
            Player player = drawPlayer;
            if (player.GetModPlayer<OdePlayer>().)
            {
                player.AddBuff(ModContent.BuffType<Buffs.圣巢模式>(), 1);
            }
            if (Static.hallow > 0)
            {
                
                Texture2D texture = ModContent.GetTexture("Candlight/Projectiles/nest");
                Texture2D texture2 = ModContent.GetTexture("Candlight/Projectiles/白屏");
                Vector2 drawPos = player.position - Main.screenPosition + new Vector2(6f, 0f);
                Color color = new Color(5 + Static.hallow * 5, 5 + Static.hallow * 5, 5 + Static.hallow * 5, 5 + Static.hallow * 5);
                Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, texture.Height * 0.5f);
                Vector2 drawOrigin2 = new Vector2(texture2.Width * 0.5f, texture2.Height * 0.5f);
                float range = 1;
                float range2 = 1;
                if (Static.hallow <= 50)
                {
                    Main.spriteBatch.Draw(texture, drawPos, null, color, rotation, drawOrigin, range, SpriteEffects.None, 0f);
                    for (int i = 1; i <= 10; i++)
                    {
                        color *= 0.9f;
                        range += 0.01f;
                        Main.spriteBatch.Draw(texture, drawPos, null, color, rotation, drawOrigin, range, SpriteEffects.None, 0f);
                    }
                }
                if (Static.hallow > 50 && Static.hallow <= 140)
                {
                    for (int i = 1; i <= (Static.hallow - 50) * 0.7f; i++)
                    {
                        range *= 1.05f;
                        range2 *= 0.93f;
                        color *= 0.9f;
                        Main.spriteBatch.Draw(texture, drawPos, null, color, rotation, drawOrigin, range, SpriteEffects.None, 0f);
                        Main.spriteBatch.Draw(texture, drawPos, null, new Color(255, 255, 255, 255), rotation, drawOrigin, range2, SpriteEffects.None, 0f);
                        range2 *= 0.93f;
                        Main.spriteBatch.Draw(texture, drawPos, null, new Color(255, 255, 255, 255), rotation, drawOrigin, range2, SpriteEffects.None, 0f);
                    }
                }
                if (Static.hallow > 80 && Static.hallow <= 140)
                {
                    Main.spriteBatch.Draw(texture2, drawPos, null, new Color(15 + (Static.hallow - 80) * 4, 15 + (Static.hallow - 80) * 4, 15 + (Static.hallow - 80) * 4, 15 + (Static.hallow - 80) * 4), rotation, drawOrigin2, 2, SpriteEffects.None, 0f);

                }
                if (Static.hallow > 140 && Static.hallow <= 160)
                {
                    Main.spriteBatch.Draw(texture2, drawPos, null, new Color(255, 255, 255, 255), rotation, drawOrigin2, 2, SpriteEffects.None, 0f);
                }
                if (Static.hallow > 160 && Static.hallow <= 200)
                {
                    Main.spriteBatch.Draw(texture2, drawPos, null, new Color(255 - (Static.hallow - 160) * 6, 255 - (Static.hallow - 160) * 6, 255 - (Static.hallow - 160) * 6, 255 - (Static.hallow - 160) * 6), rotation, drawOrigin2, 2, SpriteEffects.None, 0f);

                }
                if (Static.hallow == 202)
                {
                    Static.mode = true;
                    player.AddBuff(ModContent.BuffType<Buffs.圣巢模式>(), 1);
                    Main.NewText("圣巢模式已开启");
                    Main.NewText("玩得愉快！");
                }
            }
        }*/

    }
}