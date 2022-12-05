using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Items.Series.Recharge
{
    //天堂配件
    internal class HeavenFragment : ModItem
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.maxStack = 1;
            Item.width = 28;
            Item.height = 22;
            Item.rare = ItemRarityID.Pink;
        }
        public override void AddRecipes()
        {
            base.AddRecipes();
            CreateRecipe()
                .AddIngredient(ItemID.HallowedBar, 40)
                .AddIngredient(ItemID.FragmentVortex, 20)
                .AddIngredient(ItemID.FragmentNebula, 20)
                .AddIngredient(ItemID.FragmentSolar, 20)
                .AddIngredient(ItemID.FragmentStardust, 20)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }
    }
}
