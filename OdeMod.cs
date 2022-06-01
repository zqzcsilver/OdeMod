using Terraria;

using OdeMod.Utils;

using Terraria.ModLoader;
using System;
using System.Resources;
using Terraria.Localization;
using System.Reflection;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace OdeMod
{
    public class OdeMod : Mod, IOde
    {
        internal static OdeMod Instance { get; private set; }
        public override void Load()
        {
            Instance = this;
            LanguageType.LoadCulture();

            //On.Terraria.Localization.LanguageManager.SetLanguage_GameCulture += (orig, self, gameCulture) =>
            //{
            //    orig(self, gameCulture);
            //};

            base.Load();
        }
        public override void Unload()
        {
            base.Unload();
            Instance = null;
        }
    }
}