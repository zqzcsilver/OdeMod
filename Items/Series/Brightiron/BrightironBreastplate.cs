using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Items.Series.Brightiron
{
    [AutoloadEquip(EquipType.Body)]
    internal class BrightironBreastplate : ModItem, IBrightiron
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Kick");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 20;
			Item.rare = ItemRarityID.Green;
			Item.defense = 25;
		}

		public override void UpdateEquip(Player player)
		{

		}
		public override void AddRecipes()
		{
			CreateRecipe().
				AddIngredient(ModContent.ItemType<BrightironOre>(), 18).
				AddTile(TileID.Furnaces).
				Register();
		}
	}
}
