using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace OdeMod.Items.Misc.Weapons
{
    /// <summary>
    /// 发射一个🔒，当击中非boss敌人时会尝试附加一个🔒状态，生命值低于50%时几率为1/3，高于50%时几率为1/6
    /// 当附加成功时，会锁住敌人并从天空降落一些较为缓慢的⭐砸向敌人
    /// 针对boss敌人直接降落⭐（速度加快）
    /// </summary>
    internal class StarLock : ModItem,IMiscItem
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 36;
            Item.height = 40;
            Item.DamageType = DamageClass.Magic;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.damage = 15;
            Item.crit = 4;
            Item.shoot = ModContent.ProjectileType<Projectiles.Misc.StarLock>();
            Item.shootSpeed = 10f;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.rare = 3;
            Item.value = Item.sellPrice(0, 0, 50, 0);
        }
        //public int dusttype=71;
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            //dusttype++;
            //Main.NewText(dusttype);
            //for (int i = 0; i < 10; i++)
            //{
            //    Dust dust = Dust.NewDustDirect(player.position, player.width, player.height, dusttype, 0f, 0f, 100, default(Color), 2f);
            //    dust.noGravity = true;
            //    dust.velocity *= 2.5f;

            //}
            Projectile.NewProjectile(source, player.Center, velocity, ModContent.ProjectileType<Projectiles.Misc.StarLock>(), damage, knockback, player.whoAmI,1);
            Projectile.NewProjectile(source, player.Center, velocity, ModContent.ProjectileType<Projectiles.Misc.StarLock>(), damage, knockback, player.whoAmI,2);
            //for (int i = 0; i <= Main.rand.Next(3, 5); i++)
            //{
            //    Vector2 pVEC = new Vector2(Main.MouseWorld.X, Main.screenPosition.Y - Main.rand.Next(50, 100)) +
            //        new Vector2(Main.rand.Next(-60, 60), Main.rand.Next(-60, 60));
            //    Vector2 tVEC = Vector2.Normalize(new Vector2(Main.MouseWorld.X + Main.rand.Next(-40, 40), Main.MouseWorld.Y +
            //        Main.rand.NextFloat(-80f, -50f)) - pVEC) * Item.shootSpeed;
            //    Vector2 tShoot = Vector2.Normalize(Main.MouseWorld - pVEC) * Item.shootSpeed;
            //    Projectile.NewProjectile(source, pVEC, tShoot, ModContent.ProjectileType<Projectiles.Series.Items.Sharpsand.SharpsandFire>(), damage, knockback, player.whoAmI);
            //}
            return false;
        }
    }
}
