using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Projectiles.Misc
{
    public class Light : ModProjectile, IMiscProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 18;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.alpha = 255;
            Projectile.timeLeft = 30;
            Projectile.penetrate = -1;
            ProjectileID.Sets.TrailCacheLength[base.Projectile.type] = 10;
            ProjectileID.Sets.TrailingMode[base.Projectile.type] = 0;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255 - Projectile.alpha, 255 - Projectile.alpha, 255 - Projectile.alpha, 255 - Projectile.alpha);
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (Projectile.timeLeft > 23)
            {
                Projectile.alpha -= 36;
            }
            if (Projectile.timeLeft < 7)
            {
                Projectile.alpha += 36;
            }
            if (Projectile.timeLeft == 29)
            {
                for (int i = 0; i < 20; i++)
                {
                    var dust2 = Dust.NewDustDirect(Projectile.Center - new Vector2(5, 5), 10, 10, 226, 0, 0, 0, Color.White, 1.2f);
                    dust2.velocity = 5 * Main.rand.NextVector2Unit();
                    dust2.noGravity = true;
                    var dust1 = Dust.NewDustDirect(Projectile.Center - new Vector2(5, 5), 10, 10, 254, 0, 0, 0, Color.White, 2f);
                    dust1.velocity = 4 * Main.rand.NextVector2Unit();
                    dust1.noGravity = true;

                }
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            float range = 1;
            float range2 = 1;
            var texture = ModContent.Request<Texture2D>("OdeMod/Projectiles/Misc/Draw textures/light1").Value;
            var texture2 = ModContent.Request<Texture2D>("OdeMod/Projectiles/Misc/Draw textures/shade0").Value;
            Vector2 drawOrigin = new Vector2(Projectile.width * 0.5f, Projectile.height * 0.5f);
            Vector2 drawOrigin2 = new Vector2(31, 21);

            for (int k2 = 0; k2 < Projectile.oldPos.Length - 6; k2++)
            {
                range2 *= 1.2f;
                Vector2 drawPos = Projectile.oldPos[k2] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color2 = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - k2) / (float)Projectile.oldPos.Length) * 0.2f;
                Main.spriteBatch.Draw(texture2, drawPos, null, color2, Projectile.rotation, drawOrigin2, (float)range2, SpriteEffects.None, 0f);
            }
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                range *= 1.1f;

                Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);


                Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - (k * 1.5)) / (float)Projectile.oldPos.Length);
                Main.spriteBatch.Draw(texture, drawPos, null, color, Projectile.rotation, drawOrigin, (float)range, SpriteEffects.None, 0f);
            }
            return true;
        }
    }
}