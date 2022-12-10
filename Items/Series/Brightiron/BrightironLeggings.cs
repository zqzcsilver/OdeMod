using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Items.Series.Brightiron
{
    [AutoloadEquip(EquipType.Legs)]
    internal class BrightironLeggings : ModItem,IBrightiron
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Kick");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 18;
			Item.rare = ItemRarityID.Green;
			Item.defense = 15;
		}

		public override void UpdateEquip(Player player)
		{
			player.moveSpeed += 0.05f;
		}

		public override void AddRecipes()
		{
			CreateRecipe().
				AddIngredient(ModContent.ItemType<BrightironOre>(), 10).
				AddTile(TileID.Furnaces).
				Register();
		}
	}
}
