using Terraria.DataStructures;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using OdeMod.Buffs;

namespace OdeMod.Items.Misc
{
    internal class ChenAi : ModItem,IMiscItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Tooltip.SetDefault("用于制作艾叶护符和艾汁酒\n流通在泰拉大陆通常都只有三年陈艾");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 30;
            Item.height = 28;
            Item.maxStack = 99;
            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(0, 0, 1, 0);
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.useAnimation = 25;
            Item.useTime = 25;
        }
        public override bool? UseItem(Player player)
        {
            return base.UseItem(player);
        }
    }
}
