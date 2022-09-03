using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terraria.ModLoader;
using Terraria.ID;

namespace OdeMod.Items.Series.Recharge
{
    internal class LuminousReactor : ModItem, IRechargeAccessory
    {
        public float RechargeSpeedMult => 1.25f;
        public float RechargeSpeedAdd => 0f;
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.CloneDefaults(ItemID.PixieDust);
            Item.maxStack = 1;
        }
    }
}
