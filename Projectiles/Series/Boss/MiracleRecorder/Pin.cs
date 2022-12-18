using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OdeMod.Utils;

using System.Collections.Generic;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Projectiles.Series.Boss.MiracleRecorder
{
    internal class Pin : ModProjectile, IMiracleRecorderProj
    {
        public override void SetDefaults()
        {
            Projectile.width = 46;
            Projectile.height = 46;
            Projectile.aiStyle = -1;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.alpha = 255;
            Projectile.timeLeft = 30;
            Projectile.penetrate = 1;
            Projectile.scale = 1f;
            Projectile.extraUpdates = 4;

            Main.projFrames[Projectile.type] = 4;
            ProjectileID.Sets.TrailCacheLength[base.Projectile.type] = 12;
            ProjectileID.Sets.TrailingMode[base.Projectile.type] = 0;
        }

        private float size = 0f;
        public override void AI()
        {
            if(Projectile.timeLeft>=25f)
            {
                size += 0.25f;
            }
            else
            {
                size -= 0.05f;
            }

            var dust3 = Dust.NewDustPerfect(Projectile.Center, ModContent.DustType<Dusts.Dream>(), Vector2.Zero, 0, default, size);

            dust3.noGravity = true;
        }

        
    }
}