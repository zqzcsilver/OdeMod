using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Items.Misc
{
    internal class AyTsao : ModItem, IMiscItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Tooltip.SetDefault("辟邪");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 28;
            Item.height = 32;
            Item.maxStack = 99;
            Item.rare = ItemRarityID.Green;
        }
    }
}
