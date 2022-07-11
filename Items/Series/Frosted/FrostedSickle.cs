using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using OdeMod.Utils;
namespace OdeMod.Items.Series.Frosted
{
    internal class FrostedSickle : ModItem, IFrosted
    {
        public override void SetStaticDefaults()
        {
            /*
            base.SetStaticDefaults();
            DisplayName.SetDefault("Forsted Sickle");
            DisplayName.AddTranslation(LanguageType.Chinese, "凝霜镰刀");
            Tooltip.SetDefault("");
            Tooltip.AddTranslation(LanguageType.Chinese, "");
            */
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 50;
            Item.height = 50;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 5f;
            Item.damage = 83;
            Item.crit = 16;
            Item.useTime = 28;
            Item.useAnimation = 28;
            Item.useTurn = false;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.rare = ItemRarityID.LightRed;
            Item.scale = 1.2f;
            Item.value = Item.sellPrice(0, 8, 0, 0);
            Item.autoReuse = true;
            Item.shoot = ProjectileID.IceSickle;//ModContent.ProjectileType<ProFrostedSickle>();
            Item.shootSpeed = 30;
        }
        public override void AddRecipes()
        {
            base.AddRecipes();
            CreateRecipe()
               .AddIngredient(ItemID.FrostCore, 3)
               .AddIngredient(ItemID.ChlorophyteBar, 20)
               .AddIngredient(ItemID.SoulofFright, 8)
               .AddIngredient(ItemID.SoulofMight, 15)
               .AddIngredient(ItemID.SoulofSight, 8)
               .AddTile(TileID.MythrilAnvil)
               .Register();
        }
    }
}
