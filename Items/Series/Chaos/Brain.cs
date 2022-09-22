using OdeMod.Utils;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Items.Series.Chaos
{
    internal class Brain : ModItem, IChaos
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Brain");
            DisplayName.AddTranslation(LanguageType.Chinese, "脑");
            Tooltip.SetDefault("可食用");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 32;
            Item.height = 24;
            Item.maxStack = 30;
            Item.rare = ItemRarityID.LightRed;
        }
    }
}
