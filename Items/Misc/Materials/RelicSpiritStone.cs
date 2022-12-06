using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Items.Misc.Materials
{
    internal class RelicSpiritStone : ModItem, IMiscItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Tooltip.SetDefault("花后神庙怪物掉落");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 32;
            Item.height = 38;
            Item.maxStack = 999;
            Item.rare = ItemRarityID.Orange;
        }
    }
}
