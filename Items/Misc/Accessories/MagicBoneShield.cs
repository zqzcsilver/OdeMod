using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using OdeMod.Players;

namespace OdeMod.Items.Misc.Accessories
{
    internal class MagicBoneShield : ModItem,IMiscItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            //Tooltip.SetDefault("化敌为友");
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
            player.GetModPlayer<OdePlayer>().MagicBoneShield = true;
            base.UpdateAccessory(player, hideVisual);
        }
    }
}
