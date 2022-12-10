using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Projectiles.Misc
{
    internal class Yao : ModProjectile, IMiscProjectile
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
            Projectile.penetrate = 1;
            Projectile.timeLeft = 120;
        }
        public override void AI()
        {
            Projectile.ai[1]++;
            if (Projectile.ai[1] <= 2f)
            {
            }
            else if (Projectile.ai[1] <= 10f)
            {
                for (int i = 0; i < 20; i++)
                {
                    Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.PinkFairy, 0f, 0f, 100, default(Color), 1f);
                    dust.noGravity = true;
                    dust.velocity *= 0.1f;
                }
            }
            else
            {
                for (int i = 0; i < 30; i++)
                {
                    Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.WitherLightning, 0f, 0f, 100, default(Color), 1f);
                    dust.noGravity = true;
                    dust.velocity *= 0.5f;
                }
            }
            
            base.AI();
        }
    }
}
