using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OdeMod.Utils;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Items.Series.Californium
{
    internal class Raydason : ModItem, ICalifornium
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Raydason");
            DisplayName.AddTranslation(LanguageType.Chinese, "瑞达森");
            Tooltip.SetDefault("");
            Tooltip.AddTranslation(LanguageType.Chinese, "");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 34;
            Item.height = 30;
            Item.DamageType = DamageClass.Melee;
            Item.channel = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.knockBack = 5;
            Item.damage = 118;
            Item.useTime = 32;
            Item.useAnimation = 32;
            Item.useTurn = false;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Cyan;
            Item.value = Item.sellPrice(0, 9, 60, 10);
            Item.autoReuse = true;
            Item.shoot = ProjectileID.RedsYoyo;
            Item.shootSpeed = 50;
        }
        public override void AddRecipes()
        {
            base.AddRecipes();
            CreateRecipe()
               .AddIngredient(ModContent.ItemType<CaliforniumBar>(), 16)
               .AddTile(TileID.LunarCraftingStation)
               .Register();
        }
    }
}
