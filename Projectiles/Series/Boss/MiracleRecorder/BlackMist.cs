using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Projectiles.Series.Boss.MiracleRecorder
{
    internal class BlackMist : ModProjectile, IMiracleRecorderProj
    {
        private float m;

        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.aiStyle = -1;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.timeLeft = 160;
            Projectile.alpha = 255;
            Projectile.penetrate = 1;
            Projectile.scale = 1f;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 8;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        private float scale = 0f;

        public override void AI()
        {
            if (Projectile.timeLeft > 120)
            {
                scale += 0.025f;
            }
            if (Projectile.timeLeft < 40)
            {
                scale -= 0.025f;
            }
            m += 0.1f;
            Projectile.rotation += 0.1f;
            int num = Dust.NewDust(Projectile.position, 20, 20, ModContent.DustType<Dusts.Dream>(), 0f, 0f, 0, Color.White, scale);
            Main.dust[num].noGravity = true;
            Main.dust[num].velocity.Y -= 4f;
            Main.dust[num].velocity.X *= 0.6f;

            Projectile.rotation = (float)
                   System.Math.Atan2((double)Projectile.velocity.Y,
                   (double)Projectile.velocity.X) - 1.57f;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire, damageDone);
        }

        // public override void Kill(int timeLeft)
        // {
        //     Player player = Main.player[Projectile.owner];

        //     for (int i = 0; i < 40; i++)
        //     {
        //         int num = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<Dusts.Dream>(), 0f, 0f, 0, Color.White, 1.5f);
        //         Main.dust[num].velocity = 5 * Main.rand.NextVector2Unit();
        //         Main.dust[num].noGravity = true;
        //     }
        // }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255 - Projectile.alpha, 255 - Projectile.alpha, 255 - Projectile.alpha, 255 - Projectile.alpha);
        }
    }
}