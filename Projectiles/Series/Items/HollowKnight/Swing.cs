using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Projectiles.Series.Items.HollowKnight
{
    internal class Swing : ModProjectile, IHollowKnightProjectile
    {
        public override void SetDefaults()
        {
            //统一顺序
            base.SetDefaults();
            Projectile.width = 160;
            Projectile.damage = 0;
            Projectile.height = 120;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.aiStyle = -1;
            Projectile.timeLeft = 16;
            Projectile.alpha = 0;
            Projectile.penetrate = -1;
            Projectile.scale = 1f;
            Main.projFrames[Projectile.type] = 4;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 4;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        Vector2 vec1 = new Vector2(0, 0);
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (Projectile.timeLeft == 12 || Projectile.timeLeft == 8 || Projectile.timeLeft == 4)
            {
                Projectile.frame++;
            }

            int num = Dust.NewDust(Projectile.Center + new Vector2(-30f, -20f), 60, 40, DustID.GemDiamond, 0f, 0f, 0, Color.White, 1.5f);
            Main.dust[num].velocity = -player.velocity*0.6f;
            Main.dust[num].noGravity = true;



            Projectile.direction = player.direction;
            if (player.direction == 1)
            {
                Projectile.spriteDirection = -1;
                Projectile.Center = player.Center;
            }
            else
            {
                Projectile.spriteDirection = 1;
                Projectile.Center = player.Center;
            }
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255 - Projectile.alpha, 255 - Projectile.alpha, 255 - Projectile.alpha, 255 - Projectile.alpha);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = ModContent.Request<Texture2D>("OdeMod/Projectiles/Series/Items/HollowKnight/Swing").Value;
            Vector2 drawOrigin = new Vector2(Projectile.width * 0.5f, Projectile.height * 0.5f);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length) * 0.5f;
                Main.spriteBatch.Draw(texture, drawPos, new Rectangle(0, 120 * Projectile.frame, 160, 120), color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
            }
            return true;
        }
    }
}