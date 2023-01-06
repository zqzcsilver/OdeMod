using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Items.Series.Miracle
{
    internal class HolySpiritBullet : ModItem, IMiracle
    {
        public override void SetDefaults()
        {
            Item.width = 12;
            Item.height = 12;
            Item.DamageType = DamageClass.Ranged;
            Item.damage = 35;
            Item.crit = 4;
            Item.knockBack = 1.5f;
            Item.ammo = AmmoID.Bullet;
            //Item.shoot = ModContent.ProjectileType < Projectiles.Series.Miracle.HolySpiritBullet>();
            //Item.shootSpeed = 10f;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.rare = ItemRarityID.LightPurple;
            Item.value = Item.sellPrice(0, 0, 1, 0);
            Item.maxStack = 9999;
            base.SetDefaults();
        }
        public override void AddRecipes()
        {
            base.AddRecipes();
            CreateRecipe(70)//参照了叶绿弹的合成数量
                .AddIngredient(ModContent.ItemType<HolyElement>(), 1)
                .AddIngredient(ItemID.MusketBall, 60)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}