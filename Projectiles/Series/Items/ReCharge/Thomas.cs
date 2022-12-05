using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Projectiles.Series.Items.ReCharge
{
    internal class Thomas : ModProjectile, IRechargeProjectile
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.width = 52;
            Projectile.height = 24;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.aiStyle = ProjAIStyleID.ScutlixLaser;
            Projectile.alpha = 0;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 120;
        }
    }
}
