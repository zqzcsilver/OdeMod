using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace OdeMod.Items.Series.Recharge
{
    //贴图格式不一 之后修改
    internal class TwilightGatling : ModItem, IRechargeableWeapon
    {
        public Texture2D RechargeBar => ModContent.Request<Texture2D>("OdeMod/Items/Series/Recharge/TwilightGatlingBar", AssetRequestMode.ImmediateLoad).Value;
        public Texture2D RechargeBarInner => ModContent.Request<Texture2D>("OdeMod/Items/Series/Recharge/TwilightGatlingBarInner", AssetRequestMode.ImmediateLoad).Value;
        public Vector2 RechargeBarInnerOffset => new Vector2(0f, 0f);
        public Rectangle RechargeBarInnerSource => new Rectangle(0, 0, 92, 52);
        public IRechargeAccessory[] RechargeAccessories => rechargeAccessories;
        public float EnergyMax => 180f;
        float IRechargeableWeapon.Energy { get => energy; set => energy = value; }
        public float RechargeSpeed => 0.1f;
        public float EnergyConsumption => 6f;

        private IRechargeAccessory[] rechargeAccessories = new IRechargeAccessory[2];
        private float energy = 120f;
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.CloneDefaults(ItemID.Revolver);//未设计数据
        }
        public override bool CanUseItem(Player player)
        {
            bool flag = ((IRechargeableWeapon)this).HaveEnergyToUse();
            if (flag)
                energy -= EnergyConsumption;
            return flag;
        }
        public override void SaveData(TagCompound tag)
        {
            base.SaveData(tag);
            ((IRechargeableWeapon)this).SaveRechargeAccessories(tag);
        }
        public override void LoadData(TagCompound tag)
        {
            base.LoadData(tag);
            ((IRechargeableWeapon)this).LoadRechargeAccessories(tag);
        }
    }
}
