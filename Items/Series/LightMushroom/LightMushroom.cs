using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Mono.Cecil.Cil;

using OdeMod.Utils;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace OdeMod.Items.Series.LightMushroom
{
    internal class LightMushroom : ModItem, LightMushroomInterface
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Light Mushroom");
            DisplayName.AddTranslation(LanguageType.Chinese, "光明蘑菇");
            Tooltip.SetDefault("Absorbs sunlight during the day and emits sunlight at night...");
            Tooltip.AddTranslation(LanguageType.Chinese, "在白天吸收阳光，在夜晚放出阳光...");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 22;
            Item.height = 24;
            Item.maxStack = 99;
            Item.rare = ItemRarityID.Blue;
            Item.value = Terraria.Item.sellPrice(0, 0, 2, 10);
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
                .AddIngredient(ItemID.SoulofLight, 1)
                .AddIngredient(ItemID.Mushroom, 2)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}
