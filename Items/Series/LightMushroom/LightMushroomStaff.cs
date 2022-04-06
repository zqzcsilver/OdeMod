using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OdeMod.Utils;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Items.Series.LightMushroom
{
    internal class LightMushroomStaff: ModItem, LightMushroomInterface
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Light Mushroom Staff");
            DisplayName.AddTranslation(LanguageType.Chinese, "明菇魔杖");
            Tooltip.SetDefault("The magic stimulates the mushroom, and the mushroom releases the sunlight it stores");
            Tooltip.AddTranslation(LanguageType.Chinese, "魔力刺激明菇，明菇放出它储存的阳光");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 36;
            Item.height = 42;
            Item.damage = 52;
            Item.crit = 12;
            Item.mana = 4;
            Item.rare = ItemRarityID.Orange;
            Item.DamageType = DamageClass.Magic;
            Item.shoot = 4;
            Item.value = Item.sellPrice(0, 4, 90, 30);
            Item.useTime = 20;
            Item.useTurn = false;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.staff[Item.type] = true;
            Item.autoReuse = true;
            Item.knockBack = 2.6f;
            Item.shootSpeed = 13f;
            Item.useAnimation = 20;
        }
        public override void HoldItem(Player player)
        {
            base.HoldItem(player);
            if (player.itemAnimation != 0)
                Lighting.AddLight(player.Center, new Vector3(234, 217, 124) / 255f);
        }
        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Lighting.AddLight(Item.Center, new Vector3(234, 217, 124) / 255f * 0.6f);
            base.PostDrawInWorld(spriteBatch, lightColor, alphaColor, rotation, scale, whoAmI);
        }
        public override void AddRecipes()
        {
            base.AddRecipes();
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<LightMushroom>(), 10)
                .AddIngredient(ItemID.HellstoneBar, 6)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
