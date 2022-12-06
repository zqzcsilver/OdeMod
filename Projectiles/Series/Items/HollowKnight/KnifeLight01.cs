using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Projectiles.Series.Items.HollowKnight
{
    internal class KnifeLight01 : ModProjectile, IHollowKnightProjectile
    {
        public override void SetDefaults()
        {
            //统一顺序
            base.SetDefaults();
            Projectile.width = 0;
            Projectile.height = 60;
            Projectile.damage = 12;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.aiStyle = -1;
            Projectile.timeLeft = 15;
            Projectile.alpha = 0;
            Projectile.penetrate = -1;
            Projectile.scale = 1f;
            Main.projFrames[Projectile.type] = 5;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 3;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override bool ShouldUpdatePosition()
        {
            return true;
        }
        int m = 0;
        Vector2 vec2 = new Vector2(0, 0);
        float vec1 = 0;
        Vector2 unit;
        public override void AI()
        {

            Player player = Main.player[Projectile.owner];
            if (Projectile.timeLeft == 15)
            {
                unit = Vector2.Normalize(Main.MouseWorld - player.Center);
                float rotaion = unit.ToRotation();
                player.direction = Main.MouseWorld.X < player.Center.X ? -1 : 1;
                player.itemRotation = (float)Math.Atan2(rotaion.ToRotationVector2().Y * player.direction,
                    rotaion.ToRotationVector2().X * player.direction);
                player.velocity = new Vector2(Main.MouseWorld.X - player.Center.X, Main.MouseWorld.Y - player.Center.Y) / Vector2.Distance(Main.MouseWorld, player.Center) * 12f;
            }
            if (Projectile.timeLeft < 10)
            {
                player.velocity.X *= 0.9f;
                if (player.velocity.Y < -4f) player.velocity.Y *= 0.9f;
            }

            Projectile.Center = player.Center + 10 * unit;
            m++;

            if (m % 3 == 0)
            {
                Projectile.frameCounter++;
            }
            if (Projectile.frameCounter == 1)
            {
                Projectile.frameCounter = 0;
                Projectile.frame++;
                if (Projectile.frame == 5)
                    Projectile.frame = 0;
            }
            if (player.direction == -1)
            {
                Projectile.rotation = (float)
          System.Math.Atan2(-(double)Projectile.velocity.Y,
          -(double)Projectile.velocity.X);
                Projectile.spriteDirection = -1;
            }
            else
            {
                Projectile.rotation = (float)
          System.Math.Atan2((double)Projectile.velocity.Y,
          (double)Projectile.velocity.X);
                Projectile.spriteDirection = 1;
            }
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255 - Projectile.alpha, 255 - Projectile.alpha, 255 - Projectile.alpha, 255 - Projectile.alpha);
        }

        public override bool PreDraw(ref Color lightColor)
        {

            Player player = Main.player[Projectile.owner];
            Vector2 drawOrigin = new Vector2(Projectile.width * 0.5f, Projectile.height * 0.5f);
            if (player.direction == 1)
                for (int k = 0; k < Projectile.oldPos.Length; k++)
                {
                    Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                    Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                    Main.spriteBatch.Draw(ModContent.Request<Texture2D>(Texture).Value, drawPos, new Rectangle(0, 60 * Projectile.frame, 170, 60), color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
                }
            else
            {
                for (int k = 0; k < Projectile.oldPos.Length; k++)
                {
                    Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                    Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                    Main.spriteBatch.Draw(ModContent.Request<Texture2D>("OdeMod/Projectiles/Series/Items/HollowKnight/KnifeLight3").Value, drawPos, new Rectangle(0, 60 * Projectile.frame, 170, 60), color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
                }
            }

            return true;
        }

    }
}
