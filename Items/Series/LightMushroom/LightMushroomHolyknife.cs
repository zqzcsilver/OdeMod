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
    /// <summary>
    /// 明菇狂热者，属于<see cref="LightMushroomItemInterface"/>
    /// </summary>
    internal class LightMushroomHolyknife : ModItem, LightMushroomItemInterface
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Sunshine Mushroom Sword");
            DisplayName.AddTranslation(LanguageType.Chinese, "明菇狂热者");
            Tooltip.SetDefault("I really don't understand why someone would use mushrooms as a material for sword");
            Tooltip.AddTranslation(LanguageType.Chinese, "真是搞不懂为什么会有人用蘑菇作为剑的材料");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 44;
            Item.height = 44;
            Item.damage = 69;
            Item.crit = 18;
            Item.rare = ItemRarityID.Orange;
            Item.DamageType = DamageClass.Melee;
            Item.value = Item.sellPrice(0, 5, 10, 20);
            Item.useTime = 18;
            Item.useTurn = true;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.autoReuse = true;
            Item.knockBack = 3.7f;
            Item.useAnimation = 18;
        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            base.MeleeEffects(player, hitbox);
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
                .AddIngredient(ModContent.ItemType<LightMushroom>(), 7)
                .AddIngredient(ItemID.HellstoneBar, 5)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
