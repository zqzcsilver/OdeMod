using Microsoft.Xna.Framework;

using System;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Items.Misc
{
    internal class LightPromise : ModItem, IMiscItem
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 62;
            Item.height = 54;
            Item.DamageType = DamageClass.Magic;
            Item.knockBack = 4f;
            Item.damage = 78;
            Item.crit = 16;
            Item.mana = 8;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useTurn = false;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Pink;
            Item.value = Item.sellPrice(0, 10, 0, 0);
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Misc.Accumulation>();
            Item.shootSpeed = 0;
            Item.channel = true;
        }
        int rank = 0;
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (rank == 0)
            {
                Vector2 des = Main.MouseWorld - player.Center;
                des.Normalize();
                if (player.direction == 1)
                    des = new Vector2((float)Math.Cos(des.ToRotation() - 0.6f), (float)Math.Sin(des.ToRotation() - 0.6f));
                else
                    des = new Vector2((float)Math.Cos(des.ToRotation() + 0.6f), (float)Math.Sin(des.ToRotation() + 0.6f));
                Projectile.NewProjectile(source, player.Center, des * 22, ModContent.ProjectileType<Projectiles.Misc.CrystalSentence>(), damage, knockback, player.whoAmI);
            }
            return false;
        }
        public override void AddRecipes()
        {
            base.AddRecipes();
            CreateRecipe()
               .AddIngredient(ItemID.HallowedBar, 12)
               .AddIngredient(ItemID.SoulofLight, 12)
               .AddIngredient(ItemID.CrystalShard, 30)
               .AddTile(TileID.MythrilAnvil)
               .Register();
        }
    }
}