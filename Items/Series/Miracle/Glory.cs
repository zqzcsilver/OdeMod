using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Items.Series.Miracle
{
    internal class Glory : ModItem, IMiracle
    {
        public override void SetDefaults()
        {
            Item.width = 56;
            Item.height = 54;
            Item.DamageType = DamageClass.Melee;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.damage = 80;
            Item.crit = 4;
            Item.knockBack = 1.5f;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.shoot = ModContent.ProjectileType<Projectiles.Misc.Accumulation>();
            Item.shootSpeed = 4;
            Item.noUseGraphic = true;
            Item.rare = ItemRarityID.Pink;
            Item.channel = true;
            Item.value = Item.sellPrice(0, 2, 0, 0);
            base.SetDefaults();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, player.Center, Vector2.Zero, ModContent.ProjectileType<Projectiles.Series.Items.Miracle.GloryProj>(), damage, knockback, player.whoAmI);
            
            return false;
        }
    }
}
