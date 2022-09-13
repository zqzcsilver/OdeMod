using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace OdeMod.Items.Series.Recharge
{
    internal class Thomas_Zero : ModItem, IRechargeableWeapon
    {
        public Texture2D RechargeBar => ModContent.Request<Texture2D>("OdeMod/Items/Series/Recharge/Thomas_ZeroBar", AssetRequestMode.ImmediateLoad).Value;
        public Texture2D RechargeBarInner => ModContent.Request<Texture2D>("OdeMod/Items/Series/Recharge/Thomas_ZeroBarInner", AssetRequestMode.ImmediateLoad).Value;
        public Vector2 RechargeBarInnerOffset => new Vector2(28f, 14f);
        public Rectangle RechargeBarInnerSource => new Rectangle(0, 0, 44, 12);
        public IRechargeAccessory[] RechargeAccessories => rechargeAccessories;
        public float EnergyMax => 120f;
        float IRechargeableWeapon.Energy { get => energy; set => energy = value; }
        public float RechargeSpeed => 0.1f;
        public float EnergyConsumption => 30f;

        private IRechargeAccessory[] rechargeAccessories = new IRechargeAccessory[2];
        private float energy = 120f;
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 56;
            Item.height = 32;
            Item.damage = 70;
            Item.DamageType = DamageClass.Ranged;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.shoot = ModContent.ProjectileType<Projectiles.Series.Items.ReCharge.Thomas>();
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
                .AddIngredient(ModContent.ItemType<PhotonicComponent>(), 1)//这里应该是反物质组件，但是贴图只有光子组件的
                .AddIngredient(ItemID.IllegalGunParts, 2)
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

