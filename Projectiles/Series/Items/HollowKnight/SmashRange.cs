using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Projectiles.Series.Items.HollowKnight
{
    internal class SmashRange : ModProjectile, IHollowKnightProjectile
    {
        public override void SetDefaults()
        {
            //统一顺序
            base.SetDefaults();
            Projectile.width = 200;
            Projectile.height = 40;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.aiStyle = 27;
            Projectile.timeLeft = 10;
            Projectile.alpha = 10;
            Projectile.penetrate = -1;
            Projectile.scale = 1.3f;
        }
        public override void AI()
        {
            Projectile.velocity *= 0;
        }
    }
}
