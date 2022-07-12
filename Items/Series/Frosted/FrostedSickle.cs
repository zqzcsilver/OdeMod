using Microsoft.Xna.Framework;

using System;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Items.Series.Frosted
{
    internal class FrostedSickle : ModItem, IFrosted
    {
        public override void SetStaticDefaults()
        {
            /*
            base.SetStaticDefaults();
            DisplayName.SetDefault("Forsted Sickle");
            DisplayName.AddTranslation(LanguageType.Chinese, "凝霜镰刀");
            Tooltip.SetDefault("");
            Tooltip.AddTranslation(LanguageType.Chinese, "");
            */
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 50;
            Item.height = 50;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 5f;
            Item.damage = 57;
            Item.crit = 4;
            Item.useTime = 12;
            Item.useAnimation = 36;
            Item.useTurn = false;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.rare = ItemRarityID.LightRed;
            Item.scale = 1.2f;
            Item.value = Item.sellPrice(0, 8, 0, 0);
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Series.Items.Frosted.icelight>();
            Item.shootSpeed = 12;
        }
        int times = -2;
        Vector2 plrToMouse;
        float r;
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.direction == 1)
            {
                if (times == -2)
                {
                    plrToMouse = Main.MouseWorld - player.Center;
                    r = (float)Math.Atan2(plrToMouse.Y, plrToMouse.X);
                }
                float r2 = r + times * MathHelper.Pi / 6f;
                Vector2 shootVel = r2.ToRotationVector2() * 10;
                Projectile.NewProjectile(source, player.Center, shootVel, ModContent.ProjectileType<Projectiles.Series.Items.Frosted.icelight>(), (int)(damage * 0.5f), knockback, player.whoAmI);
                times++;
                if (times == 1)
                {
                    times = -2;
                }
            }
            else
            {
                if (times == -2)
                {
                    plrToMouse = Main.MouseWorld - player.Center;
                    r = (float)Math.Atan2(plrToMouse.Y, plrToMouse.X);
                }
                float r2 = r - times * MathHelper.Pi / 5f;
                Vector2 shootVel = r2.ToRotationVector2() * 10;
                Projectile.NewProjectile(source, player.Center, shootVel, ModContent.ProjectileType<Projectiles.Series.Items.Frosted.icelight>(), (int)(damage * 0.5f), knockback, player.whoAmI);
                times++;
                if (times == 1)
                {
                    times = -2;
                }
            }
            return false;
        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            for (int i = 1; i <= 3; i++)
            {
                Dust d = Dust.NewDustDirect(hitbox.TopLeft(), hitbox.Width, hitbox.Height, DustID.IceTorch, 0, 0, 100, Color.White, 1.2f);
                d.noGravity = true;
            }
        }
        public override void AddRecipes()
        {
            base.AddRecipes();
            CreateRecipe()
               .AddIngredient(ItemID.FrostCore, 3)
               .AddIngredient(ItemID.ChlorophyteBar, 20)
               .AddIngredient(ItemID.SoulofFright, 8)
               .AddIngredient(ItemID.SoulofMight, 15)
               .AddIngredient(ItemID.SoulofSight, 8)
               .AddTile(TileID.MythrilAnvil)
               .Register();
        }
    }
}
