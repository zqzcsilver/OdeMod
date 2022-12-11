using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Projectiles.Series.Boss.MiracleRecorder
{
    internal class Space : ModProjectile, IMiracleRecorderProj
    {
        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.aiStyle = -1;
            Projectile.friendly = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.timeLeft = 30;
            Projectile.alpha = 0;
            Projectile.penetrate = 1;
            Projectile.scale = 1f;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 1;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        public override void AI()
        {
            float size = (float)(30 - Projectile.timeLeft) / 15f;
            int num = Dust.NewDust(Projectile.Center - new Vector2(4f, 4f), 8, 8, ModContent.DustType<Dusts.Dream>(), 0f, 0f, 0, Color.White, size);
            Main.dust[num].velocity *= 0.5f;
            Main.dust[num].noGravity = true;

            if (Projectile.timeLeft == 30)
                Projectile.velocity = new Vector2(Projectile.ai[0], Projectile.ai[1]) - Projectile.Center;
            Projectile.velocity.Normalize();
            Projectile.velocity *= 12f;
        }

        private Texture2D texture;
        private float sizex = 1.5f;

        public override bool PreDraw(ref Color lightColor)
        {
            return false;
        }
    }
}