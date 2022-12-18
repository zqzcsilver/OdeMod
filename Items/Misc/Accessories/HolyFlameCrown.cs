using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using OdeMod.Players;

namespace OdeMod.Items.Misc.Accessories
{
    internal class HolyFlameCrown : ModItem, IMiscItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            //Tooltip.SetDefault("圣火洗礼\n你的每一次攻击都有几率会附加一次圣火爆炸");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 28;
            Item.height = 34;
            Item.maxStack = 1;
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<OdePlayer>().HolyFlameCrown = true;
            base.UpdateAccessory(player, hideVisual);
        }
    }
}
