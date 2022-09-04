using Microsoft.Xna.Framework;

using OdeMod.Utils;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Items.Series.HollowKnight
{
    internal class DescendingDark : ModItem, IHollowKnight
    {
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("DescendingDark");
            //DisplayName.AddTranslation(LanguageType.Chinese, "黑暗降临");
            //Tooltip.SetDefault("集中灵魂和暗影的力量打击地面。");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            //规范<固定>顺序 
            Item.width = 54;
            Item.height = 32;
            Item.DamageType = DamageClass.Magic;
            Item.damage = 200;
            Item.crit = -4;
            Item.knockBack = 9;
            Item.mana = 25;
            Item.rare = ItemRarityID.Blue;
            Item.shoot = ModContent.ProjectileType<Projectiles.Series.Items.HollowKnight.DescendingDark>();//我一直好奇为什么银烛这里写的都是同一个PRO
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
            Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<Projectiles.Series.Items.HollowKnight.DescendingDark>(), damage, knockback, player.whoAmI, 1f);
            return false;
        }
    }
}
