using Microsoft.Xna.Framework;

using OdeMod.Players;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Items.Series.HollowKnight
{
    internal class OldNail : ModItem, IHollowKnight
    {
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("OldNail");
            //DisplayName.AddTranslation(LanguageType.Chinese, "旧骨钉");
            //Tooltip.SetDefault("圣巢的传统武器。它的刀刃因老化和磨损变钝了。");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            //规范<固定>顺序 
            Item.width = 24;
            Item.height = 72;
            Item.DamageType = DamageClass.Melee;
            Item.damage = 25;
            Item.crit = -4;
            Item.knockBack = 4;
            Item.rare = ItemRarityID.Blue;
            Item.shoot = ModContent.ProjectileType<Projectiles.Series.Items.HollowKnight.KnifeLight>();
            Item.shootSpeed = 5.5f;
            Item.UseSound = SoundID.Item1;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 19;
            Item.useTime = 19;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.autoReuse = false;
        }
        int m = 0;
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {

            if (m % 2 == 0)
            {
                type = ModContent.ProjectileType<Projectiles.Series.Items.HollowKnight.KnifeLight>();
            }
            else
            {
                type = ModContent.ProjectileType<Projectiles.Series.Items.HollowKnight.KnifeLight2>();
            }
            m++;
            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, Main.myPlayer, 0f, 0f);
            return false;
        }
        public override bool CanUseItem(Player player)
        {
            return !player.GetModPlayer<OdePlayer>().OnHollowKnightItemUsing;
        }
    }
}
