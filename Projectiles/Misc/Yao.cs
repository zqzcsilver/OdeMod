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
        public int num = 0;
        Vector2 tVec = Vector2.Zero;
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if(tVec == Vector2.Zero)
            {
                tVec = Vector2.Normalize(Main.MouseWorld - player.Center) * 10;
                Main.NewText(player.name, Color.Red);
            }
            num++;
            if (num <= 10f)
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
                Projectile.velocity = tVec;
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
