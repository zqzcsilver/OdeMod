using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace OdeMod.Items.Series.Miracle
{
    internal class BrilliantPearl : ModItem, IMiracle
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 26;
            Item.height = 28;
            Item.maxStack = 1;
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.ZoneHallow)
            {

            }
            base.UpdateAccessory(player, hideVisual);
        }
    }
}
