using Microsoft.Xna.Framework;

using OdeMod.Players;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static OdeMod.QuickAssetReference.ModAssets_Hjson;

namespace OdeMod.Items.Series.Drawer.Pens
{
    internal class CopperPen : BasePen, IPens
    {

        public override void SetDefaults()
        {
            BaseDamage = 8;
            Item.width = 34;
            Item.height = 34;
        }

    }
}