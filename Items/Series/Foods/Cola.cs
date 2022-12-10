using Terraria.DataStructures;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using OdeMod.Buffs.Foods;
using OdeMod.Players;

namespace OdeMod.Items.Series.Foods
{
    internal class Cola : ModItem, IFoods
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Tooltip.SetDefault("别喝太多");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 38;
            Item.height = 38;
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.useAnimation = 10;
            Item.useTime = 10;
            Item.consumable = true;
            Item.maxStack = 99;
        }
        public override bool CanUseItem(Player player)
        {
            player.GetModPlayer<OdeFoodPlayer>().Food_Variety_Test(Item);
            player.AddBuff(ModContent.BuffType<Happy>(), 6000);
            return base.CanUseItem(player);
        }
    }
}
