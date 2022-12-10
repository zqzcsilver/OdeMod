using Terraria.DataStructures;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Items.Series.Foods
{
    internal class GuoKui : ModItem,IFoods
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Tooltip.SetDefault("好吃");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 28;
            Item.height = 28;
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.useAnimation = 20;
            Item.useTime = 20;
            Item.consumable = true;
            Item.maxStack = 99;
        }
        public override bool CanUseItem(Player player)
        {
            return base.CanUseItem(player);
        }
    }
}
