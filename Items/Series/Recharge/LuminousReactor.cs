using Terraria.ID;
using Terraria.ModLoader;

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
