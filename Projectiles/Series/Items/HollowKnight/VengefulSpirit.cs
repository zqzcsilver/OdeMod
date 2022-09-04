using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OdeMod.Players;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace OdeMod.Projectiles.Series.Items.HollowKnight
{
    internal class VengefulSpirit : ModProjectile, IHollowKnightProjectile
    {

        public override void SetDefaults()
        {
            Projectile.width = 122;
            Projectile.height = 44;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 360;
            Projectile.alpha = 0;
            Projectile.penetrate = -1;
            Main.projFrames[Projectile.type] = 4;
            Projectile.scale = 1f;
            ProjectileID.Sets.TrailCacheLength[base.Projectile.type] = 10;
            ProjectileID.Sets.TrailingMode[base.Projectile.type] = 0;
            Projectile.extraUpdates = 2;
        }
        Vector2 vec1 = new Vector2(0, 0);
        public override void AI()
        {

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
            if (Projectile.timeLeft == 350)
            {
                Projectile.tileCollide = true;
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
                    OdePlayer.shakeInt = Math.Max(OdePlayer.shakeInt, (int)(8 / demo));
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
                        int num = Dust.NewDust(Projectile.Center + new Vector2(30f, 0f), 1, 1, DustID.GemDiamond, 0f, 0f, 0, Color.White, 2f);
                        Main.dust[num].velocity /= Main.dust[num].velocity.Length();
                        Main.dust[num].velocity.X *= 0.4f;
                        Main.dust[num].velocity.Y *= 8f;
                        Main.dust[num].noGravity = true;
                    }
                    for (int i = 1; i <= 50; i++)
                    {
                        int num = Dust.NewDust(Projectile.Center + new Vector2(-20f, -30f), 60, 60, DustID.GemDiamond, 0f, 0f, 0, Color.White, 2f);
                        Main.dust[num].velocity.X -= 2f + Main.rand.Next(10, 35) * 0.8f;
                        Main.dust[num].velocity.Y *= 6f;
                        Main.dust[num].noGravity = true;
                    }

                    for (int i = 1; i <= 20; i++)
                    {
                        int num = Dust.NewDust(Projectile.Center + new Vector2(10f, -60f), 60, 120, DustID.GemDiamond, 0f, 0f, 0, Color.White, 2f);
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
                        int num = Dust.NewDust(Projectile.Center + new Vector2(35f, 0f), 1, 1, DustID.GemDiamond, 0f, 0f, 0, Color.White, 2f);
                        Main.dust[num].velocity /= Main.dust[num].velocity.Length();
                        Main.dust[num].velocity.X *= 0.4f;
                        Main.dust[num].velocity.Y *= 8f;
                        Main.dust[num].noGravity = true;
                    }
                    for (int i = 1; i <= 50; i++)
                    {
                        int num = Dust.NewDust(Projectile.Center + new Vector2(29f, -30f), 60, 60, DustID.GemDiamond, 0f, 0f, 0, Color.White, 2f);
                        Main.dust[num].velocity.X += 2f + Main.rand.Next(10, 35) * 0.8f;
                        Main.dust[num].velocity.Y *= 6f;
                        Main.dust[num].noGravity = true;
                    }
                    for (int i = 1; i <= 20; i++)
                    {
                        int num = Dust.NewDust(Projectile.Center + new Vector2(-11f, -60f), 60, 120, DustID.GemDiamond, 0f, 0f, 0, Color.White, 2f);
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
                int num = Dust.NewDust(vector, Projectile.width, Projectile.height, DustID.GemDiamond, 0f, 0f, 0, Color.White, 1.2f);
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

        public override void Kill(int timeLeft)
        {

            for (int i = 0; i < 40; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.GemDiamond, 0f, 0f, 100, default(Color), 2f);
                dust.noGravity = true;
                dust.velocity *= 2f;

            }
            //Static.ms = 1; //????
        }
        int ok = 0;
        Texture2D texture3;
        public override bool PreDraw(ref Color lightColor)
        {
            Player player = Main.player[Projectile.owner];
            if (ok == 0)
            {
                ok++;
                if (player.direction == 1) texture3 = ModContent.Request<Texture2D>("OdeMod/Projectiles/Series/Items/HollowKnight/VengefulSpirit").Value;
                else texture3 = ModContent.Request<Texture2D>("OdeMod/Projectiles/Series/Items/HollowKnight/VengefulSpirit2").Value;
            }


            Vector2 drawOrigin = new Vector2(Projectile.width * 0.5f, Projectile.height * 0.5f);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                if (player.direction == 1)
                    Main.spriteBatch.Draw(texture3, drawPos, new Rectangle(0, 44 * Projectile.frame, 122, 44), color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
                else
                    Main.spriteBatch.Draw(texture3, drawPos, new Rectangle(0, 44 * Projectile.frame, 122, 44), color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
            }
            for (int c = 0; c < 5; c++)
            {
                float range = Projectile.scale + c * 0.01f;
                Vector2 drawPos = Projectile.position - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - c) / (float)Projectile.oldPos.Length);
                if (player.direction == 1)
                    Main.spriteBatch.Draw(texture3, drawPos, new Rectangle(0, 44 * Projectile.frame, 122, 44), color, Projectile.rotation, drawOrigin, range, SpriteEffects.None, 0f);
                else
                    Main.spriteBatch.Draw(texture3, drawPos, new Rectangle(0, 44 * Projectile.frame, 122, 44), color, Projectile.rotation, drawOrigin, range, SpriteEffects.None, 0f);
            }
            return true;
        }
    }
}