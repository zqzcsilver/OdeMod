using Microsoft.Xna.Framework;

using OdeMod.Players;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Items.Series.HollowKnight
{
    internal class ShadeSoul : ModItem, IHollowKnight
    {
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("ShadeSoul");
            //DisplayName.AddTranslation(LanguageType.Chinese, "暗影之魂");
            //Tooltip.SetDefault("召唤一个向前飞行的阴影，燃烧路径上的敌人。");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            //规范<固定>顺序
            Item.width = 54;
            Item.height = 32;
            Item.DamageType = DamageClass.Magic;
            Item.damage = 100;
            Item.crit = -4;
            Item.knockBack = 9;
            Item.mana = 25;
            Item.rare = ItemRarityID.Blue;
            Item.shoot = ModContent.ProjectileType<Projectiles.Series.Items.HollowKnight.ShadeSoul>();
            Item.shootSpeed = 5.5f;
            Item.UseSound = SoundID.Item1;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 22;
            Item.useTime = 22;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.autoReuse = false;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<Projectiles.Series.Items.HollowKnight.ShadeSoul>(), damage, knockback, player.whoAmI, 1f);
            return false;
        }
        public override bool CanUseItem(Player player)
        {
            return !player.GetModPlayer<OdePlayer>().OnHollowKnightItemUsing;
        }
    }
}