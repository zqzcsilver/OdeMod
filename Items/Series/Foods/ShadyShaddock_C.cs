using Terraria.DataStructures;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace OdeMod.Items.Series.Foods
{
    internal class ShadyShaddock_C : ModItem, IFoods
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Tooltip.SetDefault("沾满鲜血的柚子");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 24;
            Item.height = 24;
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
