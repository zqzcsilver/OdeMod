using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OdeMod.Utils;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Items.Series.Sharpsand
{
    internal class SharpsandStaff : ModItem,ISharpsand
    {
        public override void SetStaticDefaults()
        {
            /*
            base.SetStaticDefaults();
            DisplayName.SetDefault("Sharpsand Staff");
            DisplayName.AddTranslation(LanguageType.Chinese, "纯砂法杖");
            Tooltip.SetDefault("");
            Tooltip.AddTranslation(LanguageType.Chinese, "");
            */
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 38;
            Item.height = 52;
            Item.DamageType = DamageClass.Magic;
            Item.staff[Item.type] = true;
            Item.mana = 18;
            Item.knockBack = 2f;
            Item.damage = 86;
            Item.crit = 9;
            Item.useTime = 32;
            Item.useAnimation = 32;
            Item.useTurn = false;
            Item.noMelee = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Pink;
            Item.value = Item.sellPrice(0, 6, 50, 0);
            Item.autoReuse = true;
            Item.shoot = 424;//ModContent.ProjectileType<ProSharpsandFire>()
            Item.shootSpeed = 42;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            for (int i = 0; i <= Main.rand.Next(3, 5); i++)
            {
                Vector2 pVEC = new Vector2(Main.MouseWorld.X, Main.screenPosition.Y - Main.rand.Next(50, 100)) +
                    new Vector2(Main.rand.Next(-60, 60), Main.rand.Next(-60, 60));
                Vector2 tVEC = Vector2.Normalize(new Vector2(Main.MouseWorld.X + Main.rand.Next(-40, 40), Main.MouseWorld.Y +
                    Main.rand.NextFloat(-80f, -50f)) - pVEC) * Item.shootSpeed;
                Projectile.NewProjectile(source,pVEC, tVEC, 424, damage, knockback, player.whoAmI);
            }
            return false;
        }
    }
}
