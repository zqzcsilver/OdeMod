using Microsoft.Xna.Framework;

using OdeMod.Utils;

using System;
using System.Reflection;

using Terraria;
using Terraria.ModLoader;

namespace OdeMod
{
    public class OdeMod : Mod, IOde
    {
        public override void Load()
        {
            base.Load();

            //加载LanguageType内的语言种类实例
            LanguageType.LoadCulture();

            //添加Hook
            MonoModHooks.RequestNativeAccess();
            MonoMod.RuntimeDetour.IDetour detour = new MonoMod.RuntimeDetour.Hook(
                typeof(ModDust).GetMethod("Draw", BindingFlags.Instance | BindingFlags.NonPublic),
               new Action<Action<ModDust, Dust, Color, float>, ModDust, Dust, Color, float>(
                   (orig, self, dust, alpha, scale) =>
               {
                   if (self is Dusts.IOdeDusts && ((Dusts.IOdeDusts)self).UseMyDraw)
                   {
                       ((Dusts.IOdeDusts)self).Draw(self, dust, alpha, scale, Main.spriteBatch);
                   }
                   else
                       orig(self, dust, alpha, scale);
               }));
            detour.Apply();

        }
    }
}