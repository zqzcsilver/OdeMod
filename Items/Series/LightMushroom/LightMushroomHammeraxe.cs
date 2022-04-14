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
    /// 明菇斧锤，属于<see cref="LightMushroomInterface"/>
    /// </summary>
    internal class LightMushroomHammeraxe : ModItem, LightMushroomInterface
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Light Mushroom Hammeraxe");
            DisplayName.AddTranslation(LanguageType.Chinese, "明菇斧锤");
            Tooltip.SetDefault("Soft to the touch, but shiny");
            Tooltip.AddTranslation(LanguageType.Chinese, "软绵绵的手感，但是会发亮");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 40;
            Item.height = 38;
            Item.axe = 10;
            Item.hammer = 80;
            Item.crit = 8;
            Item.rare = ItemRarityID.Orange;
            Item.value = Terraria.Item.sellPrice(0, 4, 30, 0);
            Item.damage = 34;
            Item.useTime = 17;
            Item.useTurn = true;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.autoReuse = true;
            Item.knockBack = 7.2f;
            Item.useAnimation = 17;
            Item.DamageType = DamageClass.Melee;
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
                .AddIngredient(ModContent.ItemType<LightMushroom>(), 8)
                .AddIngredient(ItemID.HellstoneBar, 5)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
