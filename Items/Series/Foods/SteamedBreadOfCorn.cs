using Terraria.DataStructures;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace OdeMod.Items.Series.Foods
{
    internal class SteamedBreadOfCorn : ModItem, IFoods
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Tooltip.SetDefault("窝窝头，窝窝头，一块钱两个（");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.scale = 0.5f;
            Item.width = 24;
            Item.height = 24;
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.useAnimation = 60;
            Item.useTime = 60;
            Item.consumable = true;
            Item.maxStack = 99;
        }
        public override bool CanUseItem(Player player)
        {
            return base.CanUseItem(player);
        }
    }
}
