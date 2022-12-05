using OdeMod.Utils;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Items.Series.Chaos
{
    internal class MagicTentacle : ModItem, IChaos
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("MagicTentacle");
            DisplayName.AddTranslation(LanguageType.Chinese, "魔化触手");
            Tooltip.SetDefault("暂无");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 34;
            Item.height = 34;
            Item.maxStack = 99;
            Item.rare = ItemRarityID.Purple;
        }
    }
}
