using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OdeMod.Utils;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Items.Series.Sharpsand
{
    internal class SharpsandGiantblade : ModItem, ISharpsand
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Sharpsand Giantblade");
            DisplayName.AddTranslation(LanguageType.Chinese, "纯砂巨剑");
            Tooltip.SetDefault("");
            Tooltip.AddTranslation(LanguageType.Chinese, "");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 42;
            Item.height = 52;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 5f;
            Item.damage = 86;
            Item.crit = 10;
            Item.useTime = 23;
            Item.useAnimation = 23;
            Item.useTurn = false;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.rare = ItemRarityID.Pink;
            Item.value = Item.sellPrice(0, 7, 50, 0);
            Item.autoReuse = true;
            Item.shoot = 451;//
            Item.shootSpeed = 30;
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
