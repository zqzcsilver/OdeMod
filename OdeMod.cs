using OdeMod.Utils;

using Terraria.ModLoader;

namespace OdeMod
{
	public class OdeMod : Mod
	{
        internal static OdeMod Instance { get; private set; }
        public override void Load()
        {
            Instance = this;
            LanguageType.LoadCulture();
            base.Load();
        }
        public override void Unload()
        {
            base.Unload();
            Instance = null;
        }
    }
}