using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OdeMod.Players;

using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using static Terraria.Utils;

namespace OdeMod.Projectiles.Misc
{
    public class Accumulation : ModProjectile, IMiscProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 6;
            Projectile.height = 6;
            Projectile.aiStyle = -1;
            Projectile.friendly = false;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.alpha = 0;
            Projectile.penetrate = -1;
            ProjectileID.Sets.TrailCacheLength[base.Projectile.type] = 1;
            ProjectileID.Sets.TrailingMode[base.Projectile.type] = 0;
        }
        public override bool ShouldUpdatePosition()
        {
            return false;
        }
        Vector2 unit;
        bool ok = false;
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Projectile.rotation = (float)
               System.Math.Atan2((double)Projectile.velocity.Y,
               (double)Projectile.velocity.X) + 1.57f;

            if (Projectile.ai[0] < 181)
                Projectile.ai[0]++;


            if (Projectile.ai[0] == 180 && ok == false)
            {
                ok = true;
                for (int i = 0; i < 60; i++)
                {
                    var dust2 = Dust.NewDustDirect(player.Center + new Vector2(-3, 0), 1, 1, 235, 0, 0, 0, Color.White, 2f);
                    dust2.velocity = 6 * Main.rand.NextVector2Unit();
                    dust2.noGravity = true;

                }
            }
            if (Main.player[Projectile.owner].channel)
            {



                unit = Vector2.Normalize(Main.MouseWorld - player.Center);
                float rotaion = unit.ToRotation();
                player.direction = Main.MouseWorld.X < player.Center.X ? -1 : 1;
                player.itemRotation = (float)Math.Atan2(rotaion.ToRotationVector2().Y * player.direction,
                    rotaion.ToRotationVector2().X * player.direction);
                player.itemTime = 2;
                player.itemAnimation = 2;
                Projectile.timeLeft = 2;
                Vector2 unit2 = Vector2.Normalize(Main.MouseWorld - Projectile.Center);
                Projectile.velocity = unit2;
                Projectile.Center = player.Center + unit * 32f;


                if (Projectile.timeLeft % 2 == 0)
                {
                    float rad = Main.rand.Next(0, 629) * 0.01f;

                    var dust = Dust.NewDustDirect(player.Center + unit2 * Main.rand.Next(200, 400) * 0.2f + new Vector2(-4, -4) + Main.rand.Next(1, 20) * new Vector2((float)Math.Cos(rad) - 0.05f, (float)Math.Sin(rad)), 1, 1, DustID.RedTorch, 0, 0, 0, Color.White, 2.5f);
                    dust.velocity = Vector2.Normalize(player.Center + unit2 * Main.rand.Next(200, 400) + new Vector2(-3, 0) - dust.position) * -5f;
                    dust.noGravity = true;
                }
                for (int i = 0; i < 5; i++)
                {
                    var dust2 = Dust.NewDustDirect(player.Center + unit2 * Main.rand.Next(200, 700) * 0.1f + new Vector2(-4, -4), 1, 1, 235, 0, 0, 0, Color.White, Projectile.ai[0] * 0.006f);
                    dust2.velocity *= 0f;
                    dust2.noGravity = true;
                }

            }
            else
            {
                if (Projectile.ai[0] >= 180)
                {
                    try
                    {
                        float demo = 1 + Vector2.DistanceSquared(Main.player[Main.myPlayer].Center, player.Center) / 300000;
                        OdePlayer.shakeInt = Math.Max(OdePlayer.shakeInt, (int)(40 / demo));
                    }
                    catch
                    {

                    }
                    Vector2 speed = Main.MouseWorld - player.Center;
                    speed.Normalize();
                    Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, 12 * speed, ModContent.ProjectileType<Projectiles.Misc.Redarrow>(), Projectile.damage, Projectile.knockBack, player.whoAmI);
                    for (int i = 1; i <= 40; i++)
                    {
                        Vector2 plrToMouse = Main.MouseWorld - player.Center;
                        float r = (float)Math.Atan2(plrToMouse.Y, plrToMouse.X);
                        float r2 = r + (Main.rand.Next(-10, 11) * 0.03f);
                        Vector2 shootVel = r2.ToRotationVector2() * Main.rand.Next(40, 200) * 0.15f;
                        int num = Dust.NewDust(player.position, player.width, player.height, 235, 0, 0, 235, default, 2f);

                        Main.dust[num].velocity = shootVel;

                        Main.dust[num].noGravity = true;
                        Main.dust[num].scale *= 1.02f;
                    }


                }
                return;
            }

        }

        Texture2D texture;
        int c = 60;
        int c2 = 60;
        float timer = 0;
        public override bool PreDraw(ref Color lightColor)
        {

            if (timer < 45) timer++;
            else timer = 0;


            Color color1 = new Color(255, 0, 0);
            Color color2 = new Color(255, 255, 255);
            Color color3 = Color.Lerp(color1, color2, Projectile.ai[0] / 180f);
            Color color4 = Color.Lerp(color2, color1, (float)Math.Sin(c * 0.08f) * 0.4f + 0.4f);
            Color color5 = Color.Lerp(color2, color1, (float)Math.Sin(c2 * 0.08f) * 0.2f + 0.2f);
            Player player = Main.player[Projectile.owner];
            Vector2 plrToMouse = Main.MouseWorld - player.Center;
            float r = (float)Math.Atan2(plrToMouse.Y, plrToMouse.X);
            float r1, r2;

            /*texture = ModContent.Request<Texture2D>("OdeMod/Projectiles/Misc/Redarrow").Value;
            Vector2 drawOrigin = new Vector2(Projectile.width * 0.5f, Projectile.height * 0.5f);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Main.NewText(1);
                Vector2 drawPos = Projectile.Center;
                Main.spriteBatch.Draw(texture, drawPos, null, color2, Projectile.rotation, drawOrigin, 1f, SpriteEffects.None, 0f);
            }*/

            texture = ModContent.Request<Texture2D>("OdeMod/Projectiles/Misc/Redarrow2").Value;
            Vector2 drawOrigin = new Vector2(23, 96);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = player.Center - Main.screenPosition + 11.5f * new Vector2(-(float)Math.Sin(unit.ToRotation()), (float)Math.Cos(unit.ToRotation()));
                Color color6 = color5 * (Projectile.ai[0] / 180);
                Main.spriteBatch.Draw(texture, drawPos, null, color6, Projectile.rotation, drawOrigin, 1f, SpriteEffects.None, 0f);
                float sizex = (45 - timer) / 135 + 1.3f;
                Vector2 drawPos2 = player.Center - unit * 40 - Main.screenPosition + 11.5f * sizex * new Vector2(-(float)Math.Sin(unit.ToRotation()), (float)Math.Cos(unit.ToRotation()));
                Main.spriteBatch.Draw(texture, drawPos2, null, color6 * (0.75f - 0.017f * timer), Projectile.rotation, drawOrigin, sizex, SpriteEffects.None, 0f);
            }

            c2++;
            if (Projectile.ai[0] <= 180)
            {
                r1 = r + (180 - Projectile.ai[0]) * (0.0008f + Projectile.ai[0] * 0.000003f);
                r2 = r - (180 - Projectile.ai[0]) * (0.0008f + Projectile.ai[0] * 0.000003f);
                DrawLine(Main.spriteBatch, player.Center, r1.ToRotationVector2() * 1200 + player.Center, color3, color3, 1.5f);
                DrawLine(Main.spriteBatch, player.Center, r2.ToRotationVector2() * 1200 + player.Center, color3, color3, 1.5f);
            }
            else if (Projectile.ai[0] > 180)
            {
                c++;
                r1 = r;
                r2 = r;
                DrawLine(Main.spriteBatch, player.Center, r1.ToRotationVector2() * 1200 + player.Center, color4, color4, 1.5f);
                DrawLine(Main.spriteBatch, player.Center, r2.ToRotationVector2() * 1200 + player.Center, color4, color4, 1.5f);
            }


            return true;
        }
    }
}