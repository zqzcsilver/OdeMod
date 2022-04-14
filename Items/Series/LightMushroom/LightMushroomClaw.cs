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
    /// 明菇爪套，属于<see cref="LightMushroomItemInterface"/>
    /// </summary>
    internal class LightMushroomClaw : ModItem, LightMushroomItemInterface
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Light Mushroom Claw");
            DisplayName.AddTranslation(LanguageType.Chinese, "明菇爪套");
            Tooltip.SetDefault("Emit reassuring sun rays");
            Tooltip.AddTranslation(LanguageType.Chinese, "发出令人安心的太阳光芒");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 30;
            Item.height = 26;
            Item.damage = 34;
            Item.crit = 12;
            Item.pick = 130;
            Item.rare = ItemRarityID.Orange;
            Item.value = Terraria.Item.sellPrice(0, 5, 0, 10);
            Item.useTime = 18;
            Item.useTurn = true;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.autoReuse = true;
            Item.knockBack = 3.2f;
            Item.useAnimation = 18;
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
                .AddIngredient(ModContent.ItemType<LightMushroom>(), 10)
                .AddIngredient(ItemID.HellstoneBar, 6)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
