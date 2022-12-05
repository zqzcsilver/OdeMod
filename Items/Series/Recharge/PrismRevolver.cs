using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using ReLogic.Content;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace OdeMod.Items.Series.Recharge
{
    //名字中的Prism直译棱镜 原为光棱左轮 分光棱和左轮两部分 光棱翻译Prism取自RA2中的光棱坦克的英文名
    //中文名由HW提供
    internal class PrismRevolver : ModItem, IRechargeableWeapon
    {
        public Texture2D RechargeBar => ModContent.Request<Texture2D>("OdeMod/Items/Series/Recharge/PrismRevolverBar", AssetRequestMode.ImmediateLoad).Value;
        public Texture2D RechargeBarInner => ModContent.Request<Texture2D>("OdeMod/Items/Series/Recharge/PrismRevolverBarInner", AssetRequestMode.ImmediateLoad).Value;
        public Vector2 RechargeBarInnerOffset => new Vector2(16f, 6f);
        public Rectangle RechargeBarInnerSource => new Rectangle(0, 0, 66, 14);
        public IRechargeAccessory[] RechargeAccessories => rechargeAccessories;
        public float EnergyMax => 240f;
        float IRechargeableWeapon.Energy { get => energy; set => energy = value; }
        public float RechargeSpeed => 0.1f;
        public float EnergyConsumption => 24f;

        private IRechargeAccessory[] rechargeAccessories = new IRechargeAccessory[2];
        private float energy = 200f;
        //充能条分左右两边，左边充能完毕后面板图标显示0+100%，此时按右键可以将其转换至右边的充能条（100%）（转换后左边会重新充能），一个条总共可以打十枪
        //连续打了四枪然后停止的话，剩下两枪的攻击力会降低为50 
        //有趣的是 左轮一直有人竞速 但是设定上较慢 不如加个成就 多少时间区间内射出6发？
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 50;
            Item.height = 26;
            Item.damage = 70;
            Item.DamageType = DamageClass.Ranged;//这里Wan的设定应该是要加充能伤害
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.shoot = ModContent.ProjectileType<Projectiles.Series.Items.ReCharge.Prism>();
            Item.shootSpeed = 20f;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.noMelee = true;
        }
        public override bool CanUseItem(Player player)
        {
            bool flag = ((IRechargeableWeapon)this).HaveEnergyToUse();
            if (flag)
                energy -= EnergyConsumption;
            return flag;
        }
        public override void AddRecipes()
        {
            base.AddRecipes();
            CreateRecipe()
                .AddIngredient(ItemID.Revolver, 1)
                .AddIngredient(ItemID.SoulofLight, 25)
                .AddIngredient(ItemID.IllegalGunParts, 2)
                .AddTile(TileID.MythrilAnvil)
                .Register();
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

