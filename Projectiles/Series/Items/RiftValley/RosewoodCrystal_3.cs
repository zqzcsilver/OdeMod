using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Projectiles.Series.Items.RiftValley
{
    internal class RosewoodCrystal_3 : ModProjectile, IRiftBalleyProjectile
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.width = 6;
            Projectile.height = 6;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.aiStyle = -1;
            Projectile.alpha = 0;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 300;
        }
        public override void AI()
        {
            if (Projectile.timeLeft == 300)
                Main.NewText(2);
            Lighting.AddLight(Projectile.position, 0.0f, 0.5f, 0.0f);

            Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.GemEmerald, 0f, 0f, 100, default(Color), 1f);
            dust.noGravity = true;
            dust.velocity *= 0.2f;

            if (Projectile.velocity.Y <= 12f)
                Projectile.velocity.Y += 0.4f;
        }
        private int hitnum = 0;//撞击次数
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (++hitnum >= 3)
            {
                Projectile.Kill();
            }

            if (Projectile.velocity.X != oldVelocity.X)
            {
                Projectile.velocity.X = -oldVelocity.X * 0.6f;
            }
            if (Projectile.velocity.Y != oldVelocity.Y)
            {
                Projectile.velocity.Y = -oldVelocity.Y * 0.6f;
            }
            return false;
        }
    }
}
