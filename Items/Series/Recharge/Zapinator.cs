using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace OdeMod.Items.Series.Recharge
{
    //真红冲击波 移除的武器
    internal class Zapinator : ModItem, IRechargeableWeapon
    {
        public Texture2D RechargeBar => ModContent.Request<Texture2D>("OdeMod/Items/Series/Recharge/ZapinatorBar", AssetRequestMode.ImmediateLoad).Value;
        public Texture2D RechargeBarInner => ModContent.Request<Texture2D>("OdeMod/Items/Series/Recharge/ZapinatorBarInner", AssetRequestMode.ImmediateLoad).Value;
        public Vector2 RechargeBarInnerOffset => new Vector2(10f, 12f);
        public Rectangle RechargeBarInnerSource => new Rectangle(0, 0, 62, 8);
        public IRechargeAccessory[] RechargeAccessories => rechargeAccessories;
        public float EnergyMax => 180f;
        float IRechargeableWeapon.Energy { get => energy; set => energy = value; }
        public float RechargeSpeed => 0.1f;
        public float EnergyConsumption => 40f;

        private IRechargeAccessory[] rechargeAccessories = new IRechargeAccessory[2];
        private float energy = 180f;
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 56;
            Item.height = 32;
            Item.damage = 50;
            Item.DamageType = DamageClass.Ranged;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.shoot = ModContent.ProjectileType<Projectiles.Series.Items.ReCharge.Zapinator>();
            Item.shootSpeed = 20f;
            Item.useTime = 7;
            Item.useAnimation = 7;
            Item.noMelee = true;
            Item.knockBack = 3;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse != 2)
            {
                Item.mana = 4;
                Item.damage = 50;
                Item.useAnimation = 12;
                Item.useTime = 12;
                Item.shoot = ProjectileID.PurpleLaser;
                Item.shootSpeed = 25f;
                return true;
            }
            bool flag = ((IRechargeableWeapon)this).HaveEnergyToUse();
            if (flag && player.altFunctionUse == 2)
            {
                energy -= EnergyConsumption;
                Item.damage = 150;
                Item.mana = 0;
                Item.useAnimation = 12;
                Item.useTime = 6;
                Item.shoot = ModContent.ProjectileType<Projectiles.Series.Items.ReCharge.Zapinator>();
                Item.shootSpeed = 20f;
            }
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

