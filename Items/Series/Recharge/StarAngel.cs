using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using ReLogic.Content;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace OdeMod.Items.Series.Recharge
{
    //这个武器有着跟他名字不符的外型
    internal class StarAngel : ModItem, IRechargeableWeapon
    {
        public Texture2D RechargeBar => ModContent.Request<Texture2D>("OdeMod/Items/Series/Recharge/StarAngelBar", AssetRequestMode.ImmediateLoad).Value;
        public Texture2D RechargeBarInner => ModContent.Request<Texture2D>("OdeMod/Items/Series/Recharge/StarAngelBarInner", AssetRequestMode.ImmediateLoad).Value;
        public Vector2 RechargeBarInnerOffset => new Vector2(46f, 20f);
        public Rectangle RechargeBarInnerSource => new Rectangle(0, 0, 32, 8);
        public IRechargeAccessory[] RechargeAccessories => rechargeAccessories;
        public float EnergyMax => 210f;
        float IRechargeableWeapon.Energy { get => energy; set => energy = value; }
        public float RechargeSpeed => 0.1f;
        public float EnergyConsumption => 40f;

        private IRechargeAccessory[] rechargeAccessories = new IRechargeAccessory[2];
        private float energy = 210f;
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 78;
            Item.height = 40;
            Item.damage = 580;//简单粗暴
            Item.useTurn = false;
            Item.autoReuse = false;
            Item.DamageType = DamageClass.Ranged;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.shoot = ModContent.ProjectileType<Projectiles.Series.Items.ReCharge.Angel>();
            Item.shootSpeed = 20f;
            Item.useTime = 30;
            Item.useAnimation = 30;
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
                .AddIngredient(ModContent.ItemType<HeavenFragment>(), 1)
                .AddIngredient(ItemID.SoulofLight, 40)
                .AddIngredient(ItemID.IllegalGunParts, 4)
                .AddIngredient(ItemID.Celeb2, 1)
                .AddTile(TileID.LunarCraftingStation)
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

