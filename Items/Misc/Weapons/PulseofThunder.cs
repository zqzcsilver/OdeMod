using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Items.Misc.Weapons
{
    internal class PulseofThunder : ModItem, IMiscItem
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 32;
            Item.height = 44;
            Item.DamageType = DamageClass.Ranged;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.damage = 64;
            Item.crit = 4;
            Item.shoot = ModContent.ProjectileType<Projectiles.Misc.CannonBase>();
            Item.shootSpeed = 10f;
            Item.useAnimation = 15;
            Item.channel = true;
            Item.useTime = 15;
            Item.value = Item.sellPrice(0, 7, 50, 0);
        }
        private int cannonCount = 0;
        public int Fire = 0;
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 0)
            {
                foreach (Projectile proj in Main.projectile)
                {
                    if (proj.type == ModContent.ProjectileType<Projectiles.Misc.CannonBase>())
                    {
                        var x = Main.MouseWorld - (proj.Center - new Vector2(0, 20f));
                        x.Normalize();
                        x *= 10f;
                        Projectile.NewProjectile(source, proj.Center - new Vector2(0, 20f), x, 357, proj.damage, 0, player.whoAmI);
                    }
                }
            }
            else
            {
                if(cannonCount<4)
                {
                    cannonCount++;
                    Projectile.NewProjectile(source, Main.MouseWorld, Vector2.Zero, ModContent.ProjectileType<Projectiles.Misc.CannonBase>(), damage, 0, player.whoAmI, cannonCount);
                }
                else
                {
                    foreach(Projectile proj in Main.projectile)
                    {
                        if(proj.type== ModContent.ProjectileType<Projectiles.Misc.CannonBase>())
                        {
                            if (proj.ai[0] == 1) proj.Kill();
                            else proj.ai[0]--;
                        }
                    }
                    Projectile.NewProjectile(source, Main.MouseWorld, Vector2.Zero, ModContent.ProjectileType<Projectiles.Misc.CannonBase>(), damage, 0, player.whoAmI, 4);
                }
            }
            return false;
        }
        public override bool CanUseItem(Player player)
        {
            //
            if (player.altFunctionUse == 0)
            {
                Item.autoReuse = true;
            }
            else
            {
                Item.autoReuse = false;
               
            }
            return true;
        }
    }
}
