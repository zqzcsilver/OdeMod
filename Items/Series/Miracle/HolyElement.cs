using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace OdeMod.Items.Series.Miracle
{
    internal class HolyElement : ModItem, IMiracle
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 26;
            Item.height = 28;
            Item.maxStack = 99;
            Item.rare = ItemRarityID.LightPurple;
            Item.value = Item.sellPrice(0, 0, 10, 0);
        }
    }
}
