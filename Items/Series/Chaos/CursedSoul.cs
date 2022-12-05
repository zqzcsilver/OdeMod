using OdeMod.Utils;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Items.Series.Chaos
{
    internal class CursedSoul : ModItem, IChaos
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("CursedSoul");
            DisplayName.AddTranslation(LanguageType.Chinese, "被诅咒的灵魂");
            Tooltip.SetDefault("他试图挣脱你的掌握");
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 3));
            ItemID.Sets.AnimatesAsSoul[Item.type] = true;
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 20;
            Item.height = 38;
            Item.maxStack = 99;
            Item.rare = ItemRarityID.LightRed;
        }
    }
}
