using Microsoft.Xna.Framework;

using OdeMod.Utils;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Items.Series.HollowKnight
{
    internal class AbyssShriek : ModItem, IHollowKnight
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("AbyssShriek");
            DisplayName.AddTranslation(LanguageType.Chinese, "深渊尖啸");
            Tooltip.SetDefault("用嚎叫的灵魂和暗影轰击敌人。");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            //规范<固定>顺序 话说银烛是不是没怎么改数值
            Item.width = 54;
            Item.height = 32;
            Item.DamageType = DamageClass.Magic;
            Item.damage = 120;
            Item.crit = -4;
            Item.knockBack = 9;
            Item.mana = 25;
            Item.rare = ItemRarityID.Blue;
            Item.shoot = ModContent.ProjectileType<Projectiles.Series.Items.HollowKnight.AbyssShriek>();
            Item.shootSpeed = 5.5f;
            Item.UseSound = SoundID.Item1;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 60;
            Item.useTime = 60;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.autoReuse = false;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<Projectiles.Series.Items.HollowKnight.AbyssShriek>(), damage, knockback, player.whoAmI, 1f);
            return false;
        }
    }
}