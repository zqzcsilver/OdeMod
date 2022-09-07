using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace OdeMod.Items.Series.Recharge
{
    //这个名字太普通了 应该考虑换一个吧ORZ
    internal class LaserGun : ModItem, IRechargeableWeapon
    {
        public Texture2D RechargeBar => ModContent.Request<Texture2D>("OdeMod/Items/Series/Recharge/LaserGunBar", AssetRequestMode.ImmediateLoad).Value;
        public Texture2D RechargeBarInner => ModContent.Request<Texture2D>("OdeMod/Items/Series/Recharge/LaserGunBarInner", AssetRequestMode.ImmediateLoad).Value;
        public Vector2 RechargeBarInnerOffset => new Vector2(10f, 8f);
        public Rectangle RechargeBarInnerSource => new Rectangle(0, 0, 66, 10);
        public IRechargeAccessory[] RechargeAccessories => rechargeAccessories;
        public float EnergyMax => 240f;
        float IRechargeableWeapon.Energy { get => energy; set => energy = value; }
        public float RechargeSpeed => 0.1f;
        public float EnergyConsumption => 80f;

        private IRechargeAccessory[] rechargeAccessories = new IRechargeAccessory[2];
        private float energy = 240f;
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 40;
            Item.height = 24;
            Item.damage = 30;
            Item.useTurn = false;
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Magic;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.shootSpeed = 20f;
            Item.useTime = 12;
            Item.useAnimation = 12;
            Item.noMelee = true;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(4f, 4f);
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            //这里右键发射充能弹幕前提是 先充满 然后发射三次 再充 未充满无法发射 麻烦写个bool判断一下能量达到最大值
            //另外这个的设定太过于普通了 不如充能子弹给怪加个过载？
            if(player.altFunctionUse != 2)
            {
                Item.mana = 10;
                Item.damage = 30;
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
                Item.damage = 60;
                Item.mana = 0;
                Item.useAnimation = 12;
                Item.useTime = 6;
                Item.shoot = ModContent.ProjectileType<Projectiles.Series.Items.ReCharge.LaserGun>();
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

