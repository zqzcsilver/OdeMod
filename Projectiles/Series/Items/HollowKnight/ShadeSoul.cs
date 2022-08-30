using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Projectiles.Series.Items.HollowKnight
{
    internal class ShadeSoul : ModProjectile, IHollowKnightProjectile
    {
        public override void SetDefaults()
        {
            //统一顺序
            base.SetDefaults();
            Projectile.width = 262;
            Projectile.height = 66;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.aiStyle = -1;
            Projectile.timeLeft = 360;
            Projectile.alpha = 0;
            Projectile.penetrate = -1;
            Projectile.scale = 1f;
            Main.projFrames[Projectile.type] = 4;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 10;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
            Projectile.extraUpdates = 2;
        }
        Vector2 vec1 = new Vector2(0, 0);
        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, 1, 1, 1);
            Player player = Main.player[Projectile.owner];
            Projectile.direction = player.direction;
            if (Projectile.timeLeft == 360)
            {
                vec1 = player.position;
            }
            if (Projectile.timeLeft >= 358)
            {
                if (player.direction == -1)
                {
                    player.velocity = new Vector2(20f, 0);
                    player.position.Y = vec1.Y;
                }
                else
                {
                    player.velocity = new Vector2(-20f, 0);
                    player.position.Y = vec1.Y;
                }
            }
            if (Projectile.timeLeft == 357)
            {
                vec1 = player.position;
            }
            if (Projectile.timeLeft >= 356 && Projectile.timeLeft < 358)
            {
                player.velocity *= 0f;
            }
            if (Projectile.timeLeft == 360)
            {


                try
                {
                    float demo = 1 + Vector2.DistanceSquared(Main.player[Main.myPlayer].Center, Projectile.Center) / 500000;
                    //OdeMod.shakeInt = Math.Max(OdeMod.shakeInt, (int)(8 / demo));
                }
                catch
                {

                }
                if (player.direction == -1)
                {

                    Projectile.spriteDirection = -1;
                    Projectile.velocity.Y = 0;
                    Projectile.velocity.X = -7f;
                    Projectile.position = player.position + new Vector2(-100f, -2f);
                    for (int i = 1; i <= 100; i++)
                    {
                        int num = Dust.NewDust(Projectile.Center + new Vector2(-42f, 0f), 1, 1, DustID.FireflyHit, 0f, 0f, 0, Color.Black, 2f);
                        Main.dust[num].velocity /= Main.dust[num].velocity.Length();
                        Main.dust[num].velocity.X *= 0.4f;
                        Main.dust[num].velocity.Y *= 8f;
                        Main.dust[num].noGravity = true;
                    }
                    for (int i = 1; i <= 50; i++)
                    {
                        int num = Dust.NewDust(Projectile.Center + new Vector2(-92f, -30f), 60, 60, DustID.FireflyHit, 0f, 0f, 0, Color.Black, 2f);
                        Main.dust[num].velocity.X -= 2f + Main.rand.Next(10, 35) * 0.8f;
                        Main.dust[num].velocity.Y *= 6f;
                        Main.dust[num].noGravity = true;
                    }

                    for (int i = 1; i <= 20; i++)
                    {
                        int num = Dust.NewDust(Projectile.Center + new Vector2(-62f, -60f), 60, 120, DustID.FireflyHit, 0f, 0f, 0, Color.Black, 2f);
                        Main.dust[num].velocity.X = 6f;
                        Main.dust[num].velocity.Y *= 0f;
                        Main.dust[num].noGravity = true;
                    }
                }
                else
                {

                    Projectile.spriteDirection = 1;
                    Projectile.velocity.Y = 0;
                    Projectile.velocity.X = 7f;
                    Projectile.position = player.position + new Vector2(-80f, -2f);
                    for (int i = 1; i <= 100; i++)
                    {
                        int num = Dust.NewDust(Projectile.Center + new Vector2(-30f, 0f), 1, 1, DustID.FireflyHit, 0f, 0f, 0, Color.Black, 2f);
                        Main.dust[num].velocity /= Main.dust[num].velocity.Length();
                        Main.dust[num].velocity.X *= 0.4f;
                        Main.dust[num].velocity.Y *= 8f;
                        Main.dust[num].noGravity = true;
                    }
                    for (int i = 1; i <= 50; i++)
                    {
                        int num = Dust.NewDust(Projectile.Center + new Vector2(-36f, -30f), 60, 60, DustID.FireflyHit, 0f, 0f, 0, Color.Black, 2f);
                        Main.dust[num].velocity.X += 2f + Main.rand.Next(10, 35) * 0.8f;
                        Main.dust[num].velocity.Y *= 6f;
                        Main.dust[num].noGravity = true;
                    }
                    for (int i = 1; i <= 20; i++)
                    {
                        int num = Dust.NewDust(Projectile.Center + new Vector2(-76f, -60f), 60, 120, DustID.FireflyHit, 0f, 0f, 0, Color.Black, 2f);
                        Main.dust[num].velocity.X = -6f;
                        Main.dust[num].velocity.Y *= 0f;
                        Main.dust[num].noGravity = true;
                    }
                }

                Projectile.frame = 0;

            }

            if (Projectile.timeLeft >= 300 && Projectile.timeLeft < 357)
            {
                player.velocity = new Vector2(0, 0);
                player.position = vec1;
            }
            if (Projectile.timeLeft == 342)
            {
                Projectile.frame = 1;
            }
            if (Projectile.timeLeft == 330)
            {
                Projectile.frame = 2;
            }
            if (Projectile.timeLeft <= 340)
            {
                Vector2 vector = base.Projectile.position;
                int num = Dust.NewDust(vector, Projectile.width, Projectile.height, DustID.FireflyHit, 0f, 0f, 0, Color.Black, 1.5f);
                Main.dust[num].velocity *= 0.5f;
                Main.dust[num].noGravity = true;
            }
            if (Projectile.timeLeft < 320)
            {
                if (Projectile.timeLeft % 20 == 0)
                {
                    if (Projectile.frame == 3)
                    {
                        Projectile.frame = 2;

                    }
                    else
                    {
                        Projectile.frame = 3;
                    }
                }
            }
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255 - Projectile.alpha, 255 - Projectile.alpha, 255 - Projectile.alpha, 255 - Projectile.alpha);
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {

        }
        int ok = 0;
        Texture2D texture3;
        public override bool PreDraw(ref Color lightColor)
        {
            Player player = Main.player[Projectile.owner];
            if (ok == 0)
            {
                ok++;
                if (player.direction == 1) texture3 = ModContent.Request<Texture2D>("OdeMod/Projectiles/Series/Items/HollowKnight/ShadeSoul").Value;
                else texture3 = ModContent.Request<Texture2D>("OdeMod/Projectiles/Series/Items/HollowKnight/ShadeSoul2").Value;
            }


            Vector2 drawOrigin = new Vector2(Projectile.width * 0.5f, Projectile.height * 0.5f);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                if (player.direction == 1)
                    Main.spriteBatch.Draw(texture3, drawPos, new Rectangle(0, 66 * Projectile.frame, 262, 66), color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
                else
                    Main.spriteBatch.Draw(texture3, drawPos, new Rectangle(0, 66 * Projectile.frame, 262, 66), color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
            }
            for (int c = 0; c < 5; c++)
            {
                float range = Projectile.scale + c * 0.01f;
                Vector2 drawPos = Projectile.position - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - c) / (float)Projectile.oldPos.Length);
                if (player.direction == 1)
                    Main.spriteBatch.Draw(texture3, drawPos, new Rectangle(0, 66 * Projectile.frame, 262, 66), color, Projectile.rotation, drawOrigin, range, SpriteEffects.None, 0f);
                else
                    Main.spriteBatch.Draw(texture3, drawPos, new Rectangle(0, 66 * Projectile.frame, 262, 66), color, Projectile.rotation, drawOrigin, range, SpriteEffects.None, 0f);
            }
            return true;
        }
    }
}