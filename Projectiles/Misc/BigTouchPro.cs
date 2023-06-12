using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace OdeMod.Projectiles.Misc
{
    internal class BigTouchPro : ModProjectile, IMiscProjectile
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.width = 6;
            Projectile.height = 6;
            Projectile.friendly = true;
            Projectile.ignoreWater = false;
            Projectile.tileCollide = true;
            Projectile.aiStyle = -1;
            Projectile.alpha = 0;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 120;
        }
        public override void AI()
        {
            int num = Dust.NewDust(Projectile.position, 6, 6, DustID.PurpleCrystalShard, 0f, 0f, 0, Color.White, 0.8f);
            Main.dust[num].velocity = Projectile.velocity;
            Main.dust[num].noGravity = true;
            base.AI();
        }
    }
}
