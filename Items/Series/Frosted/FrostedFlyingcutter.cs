using Microsoft.Xna.Framework;

using System;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Items.Series.Frosted
{
    internal class FrostedFlyingcutter : ModItem, IFrosted
    {
        public override void SetStaticDefaults()
        {
            /*
            base.SetStaticDefaults();
            DisplayName.SetDefault("Frosted Flyingcutter");
            DisplayName.AddTranslation(LanguageType.Chinese, "凝霜飞刀");
            Tooltip.SetDefault("");
            Tooltip.AddTranslation(LanguageType.Chinese, "");
            */
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 32;
            Item.height = 32;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 4.5f;
            Item.damage = 39;
            Item.crit = 6;
            Item.useTime = 25;
            Item.useAnimation = 25;
            //Item.UseSound=SoundID.item 幽银帮我加个音效
            Item.useTurn = false;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noUseGraphic = true;
            Item.rare = ItemRarityID.LightRed;
            Item.value = Item.sellPrice(0, 6, 50, 0);
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Series.Items.Frosted.Flycutter1_>();
            Item.shootSpeed = 11;
        }
        private int ThrownTime;
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 tVec = Vector2.Normalize(Main.MouseWorld - player.Center) * Item.shootSpeed;
            if (ThrownTime < 3)
            {
                Item.shootSpeed = 11;
                for (int i = -2; i <= 2; i++)
                {
                    Vector2 tVecl = tVec + new Vector2(-tVec.Y * 0.12f, tVec.X * 0.12f) * i;
                    tVecl.RotatedBy(i * 0.03);
                    Projectile.NewProjectile(source, player.Center, tVecl, ModContent.ProjectileType<Projectiles.Series.Items.Frosted.Flycutter1_>(), damage, knockback, player.whoAmI);
                }
                ThrownTime++;
            }
            else if (ThrownTime >= 3)
            {
                for (int i = -1; i <= 1; i++)
                {
                    Vector2 tVecl = tVec + new Vector2(-tVec.Y * 0.1f, tVec.X * 0.1f) * i;
                    tVecl.RotatedBy(i * 0.01);
                    Projectile.NewProjectile(source, player.Center, tVecl, ModContent.ProjectileType<Projectiles.Series.Items.Frosted.Flycutter2_>(), damage * 2, knockback, player.whoAmI);
                }
                Vector2 plrToMouse = Main.MouseWorld - player.Center;
                // 计算玩家到鼠标的向量弧度
                float r = (float)Math.Atan2(plrToMouse.Y, plrToMouse.X);
                for (int i = 1; i <= 60; i++)
                {
                    float r2 = r + (Main.rand.Next(-10, 11) * 0.08f);
                    Vector2 shootVel = r2.ToRotationVector2() * Main.rand.Next(40, 200) * 0.1f;
                    int num = Dust.NewDust(player.position, player.width, player.height, DustID.IceTorch, 0, 0, 100, default, 1.5f);

                    Main.dust[num].velocity = shootVel;

                    Main.dust[num].noGravity = true;
                    Main.dust[num].scale *= 1.02f;
                }

                ThrownTime = 0;
            }

            return false;
        }
        public override void AddRecipes()
        {
            base.AddRecipes();
            CreateRecipe()
               .AddIngredient(ItemID.FrostCore, 4)
               .AddIngredient(ItemID.ChlorophyteBar, 16)
               .AddIngredient(ItemID.SoulofFright, 14)
               .AddIngredient(ItemID.SoulofMight, 10)
               .AddIngredient(ItemID.SoulofSight, 10)
               .AddTile(TileID.MythrilAnvil)
               .Register();
        }
    }
}
