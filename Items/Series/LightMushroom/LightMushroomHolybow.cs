using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OdeMod.Utils;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Items.Series.LightMushroom
{
    /// <summary>
    /// 明菇弓矢，属于<see cref="LightMushroomItemInterface"/>
    /// </summary>
    internal class LightMushroomHolybow: ModItem, LightMushroomItemInterface
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Sunshine Mushroom Bow");
            DisplayName.AddTranslation(LanguageType.Chinese, "明菇弓矢");
            Tooltip.SetDefault("Hit enemies with a bright strike!");
            Tooltip.AddTranslation(LanguageType.Chinese, "朝着敌人发出光明的一击！");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 32;
            Item.height = 44;
            Item.damage = 41;
            Item.crit = 18;
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.sellPrice(0, 4, 10, 65);
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.useAmmo = AmmoID.Arrow;
            Item.useTime = 19;
            Item.useTurn = false;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.autoReuse = true;
            Item.knockBack = 3.7f;
            Item.shoot = ProjectileID.WoodenArrowFriendly;
            Item.shootSpeed = 52f;
            Item.useAnimation = 19;
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
                .AddIngredient(ModContent.ItemType<LightMushroom>(), 9)
                .AddIngredient(ItemID.HellstoneBar, 8)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
