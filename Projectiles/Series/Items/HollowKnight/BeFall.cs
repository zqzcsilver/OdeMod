using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OdeMod.Players;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Projectiles.Series.Items.HollowKnight
{
    internal class BeFall : ModProjectile, IHollowKnightProjectile
    {
        public override void SetDefaults()
        {
            //统一顺序
            base.SetDefaults();
            Projectile.width = 6;
            Projectile.height = 6;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.aiStyle = -1;
            Projectile.alpha = 235;
            Projectile.penetrate = -1;
        }
        public override bool ShouldUpdatePosition()
        {
            return false;
        }
        bool x = false;
        public override void AI()
        {

            if (Projectile.ai[0] < 600)
                Projectile.ai[0]++;
            if (Main.player[Projectile.owner].channel)
            {
                Player player = Main.player[Projectile.owner];
                //Static.hallow = (int)Projectile.ai[0];

            }
            else
            {
                if (Projectile.ai[0] < 120)
                {
                    Projectile.active = false;
                    //Static.hallow = 0;
                }

                if (Projectile.ai[0] >= 120 && Projectile.ai[0] < 205)
                {
                    //Static.hallow = (int)Projectile.ai[0];
                }
                if (Projectile.ai[0] >= 205)
                {
                    Projectile.active = false;
                    //Static.hallow = 0;
                }
            }
        }
    }
}