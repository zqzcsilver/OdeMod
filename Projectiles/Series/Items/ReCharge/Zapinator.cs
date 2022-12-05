using Terraria;
using Terraria.ModLoader;

namespace OdeMod.Projectiles.Series.Items.ReCharge
{
    internal class Zapinator : ModProjectile, IRechargeProjectile
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.width = 42;
            Projectile.height = 2;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.aiStyle = -1;
            Projectile.alpha = 0;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 120;
        }
    }
}
