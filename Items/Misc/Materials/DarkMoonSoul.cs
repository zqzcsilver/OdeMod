using Terraria.DataStructures;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Items.Misc.Materials
{
    internal class DarkMoonSoul : ModItem,IMiscItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 22));
            ItemID.Sets.AnimatesAsSoul[Item.type] = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Tooltip.SetDefault("现于暗月\n月后夜晚天空、混沌之蚀掉落");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 36;
            Item.height = 40;
            Item.maxStack = 99;
            Item.rare = ItemRarityID.Red;
            Item.value = Item.sellPrice(0, 0, 1, 0);
        }
    }
}
