using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace OdeMod.Items.Series.Miracle
{
    internal class HolySpiritArrow : ModItem, IMiracle
    {
        public override void SetDefaults()
        {
            Item.width = 14;
            Item.height = 32;
            Item.DamageType = DamageClass.Ranged;
            Item.damage = 35;
            Item.crit = 4;
            Item.knockBack = 1.5f;
            Item.ammo = AmmoID.Arrow;
            //Item.shoot = ModContent.ProjectileType < Projectiles.Series.Miracle.HolySpiritArrow>();
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
            CreateRecipe(150)//参照了叶绿箭的合成数量 气抖冷 为什么子弹那么难做（x
                .AddIngredient(ModContent.ItemType<HolyElement>(), 1)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}
