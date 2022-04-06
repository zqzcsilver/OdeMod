using OdeMod.Utils;

using Terraria.ModLoader;

namespace OdeMod
{
	public class OdeMod : Mod
	{
        public override void Load()
        {
            LanguageType.LoadCulture();
            base.Load();
        }
    }
}