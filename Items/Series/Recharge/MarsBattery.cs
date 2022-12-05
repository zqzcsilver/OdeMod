using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Items.Series.Recharge
{
    //火星电池
    internal class MarsBattery : ModItem, IRechargeAccessory
    {
        public float RechargeSpeedMult => 1f;
        public float RechargeSpeedAdd => 0f;
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.maxStack = 1;
            Item.width = 28;
            Item.height = 22;
            Item.rare = ItemRarityID.Pink;
        }
    }
}
