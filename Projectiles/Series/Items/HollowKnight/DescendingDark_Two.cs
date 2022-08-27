using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Projectiles.Series.Items.HollowKnight
{
    internal class DescendingDark_Two : ModProjectile, IHollowKnightProjectile
    {
        public override void SetDefaults()
        {
            //统一顺序
            base.SetDefaults();
            Projectile.width = 200;
            Projectile.height = 170;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.aiStyle = -1;
            Projectile.timeLeft = 31;
            Projectile.alpha = 0;
            Projectile.penetrate = -1;
            Projectile.scale = 1f;
            Main.projFrames[Projectile.type] = 7;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 8;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override bool ShouldUpdatePosition()
        {
            return false;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (Projectile.timeLeft == 31)
            {
                Projectile.frame = 0;
            }
            if (Projectile.timeLeft % 4 == 0)
            {
                Projectile.frame++;
            }
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255 - Projectile.alpha, 255 - Projectile.alpha, 255 - Projectile.alpha, 255 - Projectile.alpha);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            double range = Projectile.scale * 1;
            Texture2D texture = ModContent.Request<Texture2D>("OdeMod/Projectiles/Series/Items/HollowKnight/DescendingDark_Two").Value;
            Vector2 drawOrigin = new Vector2(Projectile.width * 0.5f, Projectile.height * 0.5f);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                range *= 1.01;
                Vector2 drawPos = Projectile.position - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - (int)((k + 4))) / Projectile.oldPos.Length);
                Main.spriteBatch.Draw(texture, drawPos, new Rectangle(0, 170 * Projectile.frame, 200, 170), color, Projectile.rotation, drawOrigin, (float)range, SpriteEffects.None, 0f);
            }
            return true;
        }
    }
}
