using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Projectiles.Series.Items.HollowKnight
{
    internal class HowlingWraiths : ModProjectile, IHollowKnightProjectile
    {
        public override void SetDefaults()
        {
            //统一顺序
            base.SetDefaults();
            Projectile.width = 368;
            Projectile.height = 328;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.aiStyle = -1;
            Projectile.timeLeft = 372;
            Projectile.alpha = 255;
            Projectile.penetrate = -1;
            Projectile.scale = 1f;
            Main.projFrames[Projectile.type] = 9;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 10;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        Vector2 vec1 = new Vector2(0, 0);
        public override void AI()
        {
            Projectile.velocity = new Vector2(0, 0);
            Player player = Main.player[Projectile.owner];
            Projectile.direction = player.direction;
            if (Projectile.timeLeft == 372)
            {
                Projectile.width = 1;
                Projectile.height = 1;
                vec1 = player.position;
                Projectile.position += new Vector2(-3f, -130f);
            }


            if (Projectile.timeLeft == 352)
            {
                Projectile.width = 380;
                Projectile.height = 380;
                Projectile.alpha = 0;

                try
                {
                    float demo = 1 + Vector2.DistanceSquared(Main.player[Main.myPlayer].Center, Projectile.Center) / 500000;
                    //Candlight.shakeInt2 = Math.Max(Candlight.shakeInt, (int)(30 / demo));
                }
                catch
                {

                }
                Projectile.frame = 0;


                for (int i = 1; i <= 100; i++)
                {
                    int num = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.GemDiamond, 0f, 0f, 0, Color.White, 2f);
                    Main.dust[num].velocity *= 4f;
                    Main.dust[num].noGravity = true;

                }
            }

            if (Projectile.timeLeft >= 304 && Projectile.timeLeft < 372)
            {
                player.velocity = new Vector2(0, 0);
                player.position = vec1;

            }
            if (Projectile.timeLeft >= 0 && Projectile.timeLeft < 352)
            {


                int num = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.GemDiamond, 0f, 0f, 0, Color.White, 2f);
                Main.dust[num].velocity *= 0.2f;
                Main.dust[num].noGravity = true;



                if (Projectile.timeLeft == 348) Projectile.frame++;
                if (Projectile.timeLeft == 344) Projectile.frame++;
                if (Projectile.timeLeft == 340) Projectile.frame++;
                if (Projectile.timeLeft == 335) Projectile.frame++;
                if (Projectile.timeLeft == 330) Projectile.frame++;
                if (Projectile.timeLeft == 326) Projectile.frame++;
                if (Projectile.timeLeft == 321) Projectile.frame++;
                if (Projectile.timeLeft == 316) Projectile.frame++;
            }
            if (Projectile.timeLeft == 312)
            {
                for (int i = 1; i <= 80; i++)
                {
                    int num = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.GemDiamond, 0f, 0f, 0, Color.White, 2f);
                    Main.dust[num].velocity.Y -= 3f;
                    Main.dust[num].velocity.X *= 0.5f;
                    Main.dust[num].noGravity = true;
                }
                Projectile.active = false;
            }
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255 - Projectile.alpha, 255 - Projectile.alpha, 255 - Projectile.alpha, 255 - Projectile.alpha);
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {

        }
        float mnm = 1;
        public override bool PreDraw(ref Color lightColor)
        {

            double range = Projectile.scale * 1;
            double range3 = Projectile.scale * mnm;
            Texture2D texture = ModContent.Request<Texture2D>("OdeMod/Projectiles/Series/Items/HollowKnight/HowlingWraiths2").Value;
            Texture2D texture2 = ModContent.Request<Texture2D>("OdeMod/Projectiles/Series/Items/HollowKnight/Shock").Value;
            Vector2 drawOrigin = new Vector2(Projectile.width * 0.5f, Projectile.height * 0.5f);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                range *= 1.01;
                Vector2 drawPos = Projectile.position - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - (int)((k + 2))) / Projectile.oldPos.Length);
                Main.spriteBatch.Draw(texture, drawPos, new Rectangle(0, 328 * Projectile.frame, 368, 328), color, Projectile.rotation, drawOrigin, (float)range, SpriteEffects.None, 0f);

            }

            if (Projectile.timeLeft > 320 && Projectile.timeLeft <= 352)
            {
                Vector2 drawOrigin2 = new Vector2(texture2.Width * 0.5f, texture2.Height * 0.5f);
                if (mnm <= 10f)
                {
                    mnm += 0.15f;
                }
                else
                {
                    mnm = 1f;
                }
                Vector2 drawPos2 = Projectile.position - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color2 = Projectile.GetAlpha(lightColor) * (1f - 0.3f * mnm);
                Main.spriteBatch.Draw(texture2, drawPos2, null, color2, Projectile.rotation + mnm * 0.1f, drawOrigin2, (float)range3, SpriteEffects.None, 0f);
            }
            return true;
        }
    }
}