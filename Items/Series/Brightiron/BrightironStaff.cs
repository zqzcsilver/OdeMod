using OdeMod.Utils;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Items.Series.Brightiron
{
    internal class BrightironStaff : ModItem, BrightironInterface
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Brightiron Staff");
            DisplayName.AddTranslation(LanguageType.Chinese, "熙铁法杖");
            Tooltip.SetDefault("");
            Tooltip.AddTranslation(LanguageType.Chinese, "");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 42;
            Item.height = 42;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 6;
            Item.staff[Item.type] = true;
            Item.mana = 18;
            Item.knockBack = 5f;
            Item.damage = 41;
            Item.crit = 6;
            Item.useTime = 14;
            Item.useAnimation = 14;
            Item.useTurn = false;
            Item.noMelee = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(0, 2, 10, 0);
            Item.autoReuse = true;
            Item.shoot = 424;//ModContent.ProjectileType<ProBrightironSpiritPieces>()
            Item.shootSpeed = 10;
            Item.UseSound = SoundID.Item43;
        }
        /*public override bool IsArmorSet(Item head, Item body, Item legs)我很好奇这为什么不直接写在盔甲里
       {
           return body.type == ModContent.ItemType<BrightironBreastplate>() && legs.type == ModContent.ItemType<BrightironLeggings>();
       }*/
        public override void UpdateArmorSet(Player player) => Item.damage = (int)(Item.damage * 1.3);
        public override void AddRecipes()
        {
            base.AddRecipes();
            CreateRecipe().
                AddIngredient(ModContent.ItemType<BrightironBar>(), 10).
                AddIngredient(ModContent.ItemType<SpiritPieces>(), 15).
                AddTile(TileID.Anvils).
                Register();
        }
    }
}
