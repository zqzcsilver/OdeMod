using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terraria.Localization;

namespace OdeMod.Utils
{
    internal static class LanguageType
    {
        /// <summary>
        /// 中文
        /// </summary>
        internal static GameCulture Chinese;
        /// <summary>
        /// 英文
        /// </summary>
        internal static GameCulture English;
        /// <summary>
        /// 加载语言种类
        /// </summary>
        internal static void LoadCulture()
        {
            Chinese = GameCulture.FromCultureName(GameCulture.CultureName.Chinese);
            English = GameCulture.FromCultureName(GameCulture.CultureName.English);
        }
    }
}
