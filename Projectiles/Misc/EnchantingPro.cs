using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Projectiles.Misc
{
    internal class EnchantingPro : ModProjectile,IMiscProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 34;
            Projectile.height = 18;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.alpha = 0;
            Projectile.timeLeft = 120;
            Projectile.penetrate = 3;
            Main.projFrames[Projectile.type] = 10;
        }
        public Vector2 vec = Vector2.Zero;
        public override void AI()
        {
            vec = (vec == Vector2.Zero)? Projectile.velocity : vec;
            if (Projectile.timeLeft % 3 == 0)
            {
                Projectile.frame++;
            }
            if(Projectile.frame >= Main.projFrames[Projectile.type])
            {
                Projectile.frame = 0;
                Projectile.velocity *= 1.5f;
            }
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X);
            float r = (float)Math.Atan2(vec.Y, vec.X);
            for (int i = 0; i < 3; i++)
            {
                float r2 = r + (Main.rand.Next(-10, 11) * 0.08f);
                Vector2 shootVel = r2.ToRotationVector2() * Main.rand.Next(40, 200) * 0.1f;

                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width/2, Projectile.height/2, 58, 0f, 0f, 100, default(Color), 1f);
                dust.noGravity = true;
                dust.velocity = -shootVel;
            }
            base.AI();
        }
    }
}
