using OdeMod.Players;
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
            //DisplayName.SetDefault("Brain");
            //DisplayName.AddTranslation(LanguageType.Chinese, "脑");
            //Tooltip.SetDefault("可食用");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 32;
            Item.height = 24;
            Item.maxStack = 99;
            Item.rare = ItemRarityID.LightRed;
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.useAnimation = 15;
            Item.consumable = true;
        }
        public override bool? UseItem(Player player)
        {
            var pls = player.GetModPlayer<EggPlayer>();
            if(pls.BrainNum < 99)
            {
                pls.BrainNum++;
            }
            
            return base.UseItem(player);
        }
        
    }
}
