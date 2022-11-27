using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using OdeMod.Players;

namespace OdeMod.Items.Misc
{
    internal class Wan : ModItem, IMiscItem
    {
        //BUG居多 在修
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 32;
            Item.height = 38;
            Item.DamageType = DamageClass.Ranged;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.damage = 10;
            Item.crit = 1;
            Item.shoot = ModContent.ProjectileType<Projectiles.Misc.Wan>();
            Item.shootSpeed = 10f;
            Item.useAnimation = 10;
            Item.useTime = 10;
            Item.consumable = true;
            Item.maxStack = 99;
        }
        public override bool? UseItem(Player player)
        {
            Item.damage = Main.rand.Next(1, 100);
            Item.crit = Main.rand.Next(1, 50);
            var pls = player.GetModPlayer<EggPlayer>();
            pls.Wan++;
            if (pls.WanAngery)
            {
                Item.damage = 1;
            }
            return base.UseItem(player);
        }
        //public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        //{

        //    return base.Shoot(player, source, position, velocity, type, damage, knockback);
        //}
    }
}
