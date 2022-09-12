using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria.ID;

namespace OdeMod.Items.Series.Recharge
{
    internal class PhotonicComponent : ModItem
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 26;
            Item.height = 20;
            Item.rare = ItemRarityID.Pink;
            Item.maxStack = 99;
        }
    }
}
