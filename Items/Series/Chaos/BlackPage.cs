using OdeMod.Utils;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Items.Series.Chaos
{
    internal class BlackPage : ModItem, IChaos
    {
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("BlackPage");
            //DisplayName.AddTranslation(LanguageType.Chinese, "黑暗书页");
            //Tooltip.SetDefault("写满了符文");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 26;
            Item.height = 30;
            Item.maxStack = 666;
            Item.rare = ItemRarityID.Gray;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.consumable = true;
        }
        public override bool CanUseItem(Player player)
        {
            if (!ChaosEvent.Chaos && Main.dayTime)
            {
                ChaosEvent.StarChaos();
                ChaosEvent.ChaosScore = 0;
                ChaosEvent.ChaosNum = 0;
            }
            else
            {
                ChaosEvent.StopChaos();
            }
            return base.CanUseItem(player);
        }
    }
}
