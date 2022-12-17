using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Items.Misc.Materials
{
    internal class GreenSoul : ModItem,IMiscItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 8));
            ItemID.Sets.AnimatesAsSoul[Item.type] = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 28;
            Item.height = 28;
            Item.maxStack = 99;
            Item.rare = ItemRarityID.Red;
        }
    }
}
