using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Projectiles.Series.Items.HollowKnight
{
    internal class KnifeLight2 : ModProjectile, IHollowKnightProjectile
    {
        public override void SetDefaults()
        {
            //统一顺序
            base.SetDefaults();
            Projectile.width = 85;
            Projectile.height = 75;
            Projectile.damage = 12;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.aiStyle = -1;
            Projectile.timeLeft = 12;
            Projectile.alpha = 0;
            Projectile.penetrate = -1;
            Projectile.scale = 1f;
            Main.projFrames[Projectile.type] = 4;
            //ProjectileID.Sets.TrailCacheLength[Projectile.type] = 9;
            //ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override bool ShouldUpdatePosition()
        {
            return false;
        }
        int m = 0;
        Vector2 vec2 = new Vector2(0, 0);
        float vec1 = 0;
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            m++;

            float x = Main.MouseWorld.X - player.position.X;
            float y = Main.MouseWorld.Y - player.position.Y;

            if (Projectile.timeLeft == 12)
            {
                if (x > 0 && x > Math.Abs(y))
                {
                    Projectile.rotation = 0f;
                    Projectile.Center = player.Center + new Vector2(25f, 0f);
                    vec2 = new Vector2(20f, 0f);
                }

                if (Math.Abs(x) < Math.Abs(y) && y > 0)
                {
                    Projectile.rotation = 1.57f;
                    Projectile.Center = player.Center + new Vector2(0f, 25f);
                    vec2 = new Vector2(0f, 20f);

                    Rectangle rectangle = Projectile.getRect();
                    int x1 = rectangle.X / 16,
                     y1 = rectangle.Y / 16,
                     width = rectangle.Width / 16 + (rectangle.Width % 16 == 0 ? 0 : 1),
                     height = rectangle.Height / 16 + (rectangle.Height % 16 == 0 ? 0 : 1),
                     i, j;
                    for (i = 0; i <= width; i++)
                    {
                        for (j = 0; j <= height; j++)
                        {
                            if (Main.tile[x1 + i, y1 + j].HasTile && Main.tile[x1 + i, y1 + j].TileType == 48)
                            {
                                player.velocity.Y = -6f;

                                for (float ra = -50; ra <= 50; ra += 0.5f)
                                {
                                    float size = 1.5f - Math.Abs(ra) * 0.03f;
                                    Dust dust = Dust.NewDustDirect(Projectile.Center + new Vector2(ra - 4f, 12f), 1, 1
                    , DustID.SpectreStaff, 0f, 0f, 0, Color.White, size);
                                    dust.noGravity = true;
                                    dust.velocity.Y *= 0.1f;
                                    dust.velocity.X *= 0.8f;
                                }
                                break;
                            }
                        }
                    }
                }
                if (x < 0 && x < -Math.Abs(y))
                {
                    Projectile.rotation = 3.14f;
                    Projectile.Center = player.Center + new Vector2(-25f, 0f);
                    vec2 = new Vector2(-20f, 0f);
                }
                if (Math.Abs(x) < Math.Abs(y) && y < 0)
                {
                    Projectile.rotation = 4.71f;
                    Projectile.Center = player.Center + new Vector2(0f, -25f);
                    vec2 = new Vector2(0f, -20f);
                }
                vec1 = Projectile.rotation;
            }
            if (Projectile.timeLeft < 12)
            {
                Projectile.Center = player.Center + vec2;
                Projectile.rotation = vec1;
            }

            if (Projectile.ai[0] < 300)
                Projectile.ai[0]++;


            Vector2 unit = Vector2.Normalize(Main.MouseWorld - player.Center);
            float rotaion = unit.ToRotation();
            player.direction = Main.MouseWorld.X < player.Center.X ? -1 : 1;
            player.itemRotation = (float)Math.Atan2(rotaion.ToRotationVector2().Y * player.direction,
                rotaion.ToRotationVector2().X * player.direction);

            Projectile.velocity = unit;

            if (m % 3 == 0)
            {
                Projectile.frameCounter++;
            }
            if (Projectile.frameCounter == 1)
            {
                Projectile.frameCounter = 0;
                Projectile.frame++;
                if (Projectile.frame == 4)
                    Projectile.frame = 0;
            }
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255 - Projectile.alpha, 255 - Projectile.alpha, 255 - Projectile.alpha, 255 - Projectile.alpha);
        }
        /*public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            double range = Projectile.scale * 1;
            double range2 = Projectile.scale * 1;
            Texture2D texture = ModContent.GetTexture("Candlight/Projectiles/刀光4");
            Vector2 drawOrigin = new Vector2(Main.ProjectileTexture[Projectile.type].Width * 0.5f, Projectile.height * 0.5f);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                range *= 1.02;
                range2 *= 0.98;
                Vector2 drawPos = Projectile.position - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - (int)((k + 0) * 4)) / Projectile.oldPos.Length);
                spriteBatch.Draw(texture, drawPos, new Rectangle(0, 75 * Projectile.frame, 85, 75), color, Projectile.rotation, drawOrigin, (float)range, SpriteEffects.None, 0f);
                spriteBatch.Draw(texture, drawPos, new Rectangle(0, 75 * Projectile.frame, 85, 75), color, Projectile.rotation, drawOrigin, (float)range2, SpriteEffects.None, 0f);
            }
            return true;
        }*/
        int mh = 0;
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            mh++;
            Player player = Main.player[Projectile.owner];
            float x = Main.MouseWorld.X - player.position.X;
            float y = Main.MouseWorld.Y - player.position.Y;
            if (mh == 1)
                player.statMana += 6;
            if (Math.Abs(x) < Math.Abs(y) && y > 0)
            {
                if (mh == 1)
                {
                    player.velocity.Y = -7f;
                }
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }
    }
}
