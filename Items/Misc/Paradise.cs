using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Items.Misc
{
    internal class Paradise : ModItem, IMiscItem
    {

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 70;
            Item.height = 70;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 2f;
            Item.damage = 114;
            Item.crit = 6;
            Item.useTime = 26;
            Item.useAnimation = 26;
            Item.useTurn = false;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.rare = ItemRarityID.Pink;
            Item.shoot = ModContent.ProjectileType<Projectiles.Series.Items.Sharpsand.SharpsandSwordgas>();
            Item.value = Item.sellPrice(0, 7, 50, 0);
            Item.autoReuse = true;
            Item.shootSpeed = 5;
        }
        public override void AddRecipes()
        {
            base.AddRecipes();
            CreateRecipe()
               .AddIngredient(ItemID.AncientBattleArmorMaterial, 3)
               .AddIngredient(ItemID.ChlorophyteBar, 16)
               .AddIngredient(ItemID.SoulofFright, 8)
               .AddIngredient(ItemID.SoulofMight, 15)
               .AddIngredient(ItemID.SoulofSight, 8)
               .AddTile(TileID.MythrilAnvil)
               .Register();
        }
    }
}
