using OdeMod.Utils;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Items.Series.Californium
{
    internal class CaliforniumBar : ModItem, ICalifornium
    {
        public override void SetStaticDefaults()
        {
            /*
            base.SetStaticDefaults();
            DisplayName.SetDefault("CaliforniumBar");
            DisplayName.AddTranslation(LanguageType.Chinese, "锎锭");
            Tooltip.SetDefault("Post-Moon Lord, Pirate Event");
            Tooltip.AddTranslation(LanguageType.Chinese, "月总后海盗事件");
            */
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 30;
            Item.height = 24;
            Item.maxStack = 99;
            Item.rare = ItemRarityID.LightPurple;
            Item.value = Item.sellPrice(0, 3, 0, 0);
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 18;
            Item.useTime = 18;
            Item.autoReuse = true;
            //Item.createTile = ModContent.TileType<TileCaliforniumBar>();
        }
    }
}
