using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OdeMod.Players;

using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Projectiles.Series.Items.HollowKnight
{
    /*if ((Main.tile[(int)playerLeftInWorld.X, (int)playerLeftInWorld.Y] != null ||
                    Main.tile[(int)playerRightInWorld.X, (int)playerRightInWorld.Y] != null) &&
                    (Main.tile[(int)playerLeftInWorld.X, (int)playerLeftInWorld.Y].TileType != 0 ||
                    Main.tile[(int)playerRightInWorld.X, (int)playerRightInWorld.Y].TileType != 0) &&
                    (Main.tileSolid[Main.tile[(int)playerLeftInWorld.X, (int)playerLeftInWorld.Y].TileType] ||
                    Main.tileSolid[Main.tile[(int)playerRightInWorld.X, (int)playerRightInWorld.Y].TileType]))*/
    internal class DescendingDark : ModProjectile, IHollowKnightProjectile
    {
        public void CreateDust(Texture2D t, Vector2 center, float size)
        {
            Color[] c = new Color[t.Width * t.Height];
            Vector2 position = new Vector2(center.X - t.Width / 2f * size, center.Y - t.Height / 2f * size);
            t.GetData(c);
            Color color;
            for (int i = 0; i < c.Length; i++)
            {
                color = c[i];
                if (color != new Color(0, 0, 0, 0))
                {
                    Player player = Main.player[Projectile.owner];
                    Dust d = Dust.NewDustDirect(new Vector2(position.X + (i % t.Width + 1) * size, position.Y + (i / t.Width + 1) * size), 0, 0, DustID.FireflyHit, 0, 0, 0, Color.Black, 1.5f);
                    d.noGravity = true;
                    d.velocity = new Vector2(0.05f, (d.position.Y - (player.Center.Y + 30f)) * 0.4f);
                }
            }
        }

        public static bool SolidCollision(Vector2 Position, int Width, int Height)
        {
            int value = (int)(Position.X / 16f) - 1;
            int value2 = (int)((Position.X + (float)Width) / 16f) + 2;
            int value3 = (int)(Position.Y / 16f) - 1;
            int value4 = (int)((Position.Y + (float)Height) / 16f) + 2;
            int num = Terraria.Utils.Clamp(value, 0, Main.maxTilesX - 1);
            value2 = Terraria.Utils.Clamp(value2, 0, Main.maxTilesX - 1);
            value3 = Terraria.Utils.Clamp(value3, 0, Main.maxTilesY - 1);
            value4 = Terraria.Utils.Clamp(value4, 0, Main.maxTilesY - 1);
            Vector2 vector = default(Vector2);
            for (int i = num; i < value2; i++)
            {
                for (int j = value3; j < value4; j++)
                {
                    if ((Main.tile[i, j] != null && Main.tile[i, j].HasTile && Main.tileSolid[Main.tile[i, j].TileType] && !Main.tileSolidTop[Main.tile[i, j].TileType])
                        || Main.tile[i, j] != null && Main.tile[i, j].HasTile && Main.tile[i, j].TileType == 19)
                    {
                        vector.X = i * 16;
                        vector.Y = j * 16;
                        int num2 = 16;
                        if (Main.tile[i, j].IsHalfBlock)
                        {
                            vector.Y += 8f;
                            num2 -= 8;
                        }

                        if (Position.X + (float)Width > vector.X && Position.X < vector.X + 16f && Position.Y + (float)Height > vector.Y && Position.Y < vector.Y + (float)num2)
                            return true;
                    }
                }
            }

            return false;
        }
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
            Projectile.timeLeft = 600;
            Projectile.alpha = 240;
            Projectile.penetrate = -1;
            Projectile.scale = 1f;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 20;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        Vector2 pos1 = new Vector2(0, 0);
        Vector2 pos2 = new Vector2(0, 0);
        Vector2 pos3 = new Vector2(0, 0);
        int guang = 0;
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Projectile.velocity *= 0f;

            if (Projectile.timeLeft > 15)
            {
                player.GetModPlayer<OdePlayer>().OnHollowKnightItemUsing = true;
            }
            else
            {
                player.GetModPlayer<OdePlayer>().OnHollowKnightItemUsing = false;
            }





            if (Projectile.timeLeft > 20)
                Projectile.position = player.Center + new Vector2(0f, 15f);

            if (Projectile.timeLeft == 595)
            {
                pos1 = player.position;
            }

            if (Projectile.timeLeft < 580 && Projectile.timeLeft > 570)
            {
                for (float i = 0; i < 3; i++)
                {

                    int num = Dust.NewDust(player.Center, 1, 1, DustID.GemDiamond, 0, 0, 120,
                        Color.White, 0.5f + ((585 - Projectile.timeLeft) / 14f));
                    float rad = new Vector2((float)Math.Cos(i * 6.28 / 3) * 20f, (float)Math.Sin(i * 6.28 / 3) * 20f).ToRotation();

                    Main.dust[num].position = player.Center +
                        new Vector2(

                            (float)(Math.Cos(i * 6.28 / 3) * 20 + Math.Cos(rad + ((float)(585 - Projectile.timeLeft) / 15f * 3.14f)) * 20),

                            (float)(Math.Sin(i * 6.28 / 3) * 20 + Math.Sin(rad + ((float)(585 - Projectile.timeLeft) / 15f * 3.14f)) * 20));

                    Main.dust[num].velocity *= 0.1f;
                    Main.dust[num].noGravity = true;


                }
            }

            if (Projectile.timeLeft < 595 && Projectile.timeLeft > 585)
            {
                player.position.X = pos1.X;
                player.velocity.Y = -0.15f * (Projectile.timeLeft - 575);
                player.velocity.X *= 0f;
            }
            if (Projectile.timeLeft == 585)
            {
                pos2 = player.position;
            }
            if (Projectile.timeLeft < 585 && Projectile.timeLeft > 570)
            {
                player.position.X = pos1.X;
                player.position.Y = pos2.Y;
                player.velocity *= 0f;
            }
            if (Projectile.timeLeft <= 570 && Projectile.timeLeft > 564)
            {
                player.velocity.Y = 5f + 2 * (570 - Projectile.timeLeft);
            }
            if (Projectile.timeLeft <= 564 && Projectile.timeLeft > 25)
            {
                //Static.za = 1;

                int num = Dust.NewDust(player.Center, 1, 1, DustID.FireflyHit, 0, 0, 0, Color.Black, 1.8f);
                int num2 = Dust.NewDust(player.Center, 1, 1, DustID.FireflyHit, 0, 0, 0, Color.Black, 1.8f);
                int num3 = Dust.NewDust(player.Center, 1, 1, DustID.FireflyHit, 0, 0, 0, Color.Black, 1.8f);
                Main.dust[num].velocity.X *= 0.12f;
                Main.dust[num2].velocity.X = Main.rand.Next(15, 40) * 0.12f;
                Main.dust[num3].velocity.X = Main.rand.Next(15, 40) * -0.12f;
                Main.dust[num].noGravity = true;
                Main.dust[num2].noGravity = true;
                Main.dust[num3].noGravity = true;
            }
            if (Projectile.timeLeft <= 570)
            {
                player.position.X = pos1.X;
                player.velocity.X *= 0f;
            }
            if (Projectile.timeLeft <= 570 && Projectile.timeLeft > 25)
            {
                player.GetModPlayer<OdePlayer>().Fall = 1;
                //Vector2 playerLeftInWorld = (player.position + new Vector2(1, player.height)) / 16;
                //Vector2 playerRightInWorld = (player.position + new Vector2(player.width - 1, player.height)) / 16;

                if (SolidCollision(player.position, player.width, player.height + 1))
                {

                    pos3 = player.position;
                    player.GetModPlayer<OdePlayer>().Fall = 0;
                    Projectile.timeLeft = 25;
                    player.immune = true;
                    player.immuneTime = 35;
                    player.AddImmuneTime(Terraria.ID.ImmunityCooldownID.General, 35);
                    player.AddImmuneTime(Terraria.ID.ImmunityCooldownID.Lava, 35);
                    player.AddImmuneTime(Terraria.ID.ImmunityCooldownID.Bosses, 35);
                    player.AddImmuneTime(Terraria.ID.ImmunityCooldownID.WrongBugNet, 35);
                    Texture2D t2 = ModContent.Request<Texture2D>("OdeMod/Projectiles/Series/Items/HollowKnight/DescendingDark_F2", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
                    CreateDust(t2, player.Center + new Vector2(0f, -20f), 0.5f);

                    //Static.za = 0;
                    guang = 25;
                    Projectile.NewProjectileDirect(Projectile.GetSource_FromAI(), player.Center + new Vector2(0f, 10f), new Vector2(0f, 0f), ModContent.ProjectileType<Projectiles.Series.Items.HollowKnight.SmashRange>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
                    for (int j = 1; j <= 60; j++)
                    {
                        Dust d = Dust.NewDustDirect(player.position + new Vector2(0f, 40f), player.width, 3, DustID.FireflyHit, 0, 0, 0, Color.Black, 2.5f);
                        d.noGravity = true;
                        if (d.velocity.Y >= 0) d.velocity.Y *= Main.rand.Next(5, 40) * 0.1f;
                        d.velocity.Y -= 1f + Main.rand.Next(10, 80) * 0.1f;
                        if (Math.Abs(d.velocity.X) <= 1f) d.velocity.X *= 2f;
                        else
                            d.velocity.X *= 4f;
                    }
                    for (int j = 1; j <= 40; j++)
                    {
                        Dust d = Dust.NewDustDirect(player.position + new Vector2(0f, 40f), player.width, 3, DustID.FireflyHit, 0, 0, 0, Color.Black, 2.8f);
                        d.noGravity = true;
                        d.velocity.Y *= 0.1f;
                        d.velocity.X *= 7f;
                    }

                    try
                    {
                        float demo = 1 + Vector2.DistanceSquared(Main.player[Main.myPlayer].Center, Projectile.Center) / 420000;
                        player.GetModPlayer<OdePlayer>().ShakeInt = Math.Max(player.GetModPlayer<OdePlayer>().ShakeInt, (int)(45 / demo));
                    }
                    catch
                    {

                    }


                }
            }
            if (Projectile.timeLeft < 25)
            {
                player.velocity *= 0f;
                player.position = pos3;
            }
            if (Projectile.timeLeft == 10)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), player.Center.X, player.Center.Y - 55f, 0, 0, ModContent.ProjectileType<Projectiles.Series.Items.HollowKnight.DescendingDark_Two>(), 80, 4, player.whoAmI, 1f);
            }
        }
        int ok = 0;
        public override bool PreDraw(ref Color lightColor)
        {
            Player player = Main.player[Projectile.owner];
            if (guang > 0)
            {
                ok++;
                Projectile.width = 128;
                Projectile.height = 21;
                Projectile.position = player.Center + new Vector2(-64f, 0f);
                guang--;
                double range = Projectile.scale * 1;
                double range2 = Projectile.scale * 1.25f;
                Texture2D texture = ModContent.Request<Texture2D>("OdeMod/Projectiles/Series/Items/HollowKnight/DesolateDive_Light", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
                Texture2D texture2 = ModContent.Request<Texture2D>("OdeMod/Projectiles/Series/Items/HollowKnight/DesolateDive_F3", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
                Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, Projectile.height * 0.5f);
                Vector2 drawOrigin2 = new Vector2(texture2.Width * 0.5f, Projectile.height * 0.5f);
                if (ok <= 10)
                {
                    Projectile.alpha -= 20;
                }
                if (ok > 10)
                {
                    Projectile.alpha += 15;
                }
                for (int k = 0; k < Projectile.oldPos.Length; k++)
                {
                    range *= 1.07;
                    range2 *= 1.02;
                    Vector2 drawPos = Projectile.position - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                    Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - k * 1.5) / Projectile.oldPos.Length);
                    Main.spriteBatch.Draw(texture, drawPos + new Vector2(0, -6f), null, color, Projectile.rotation, drawOrigin, (float)range, SpriteEffects.None, 0f);
                    Main.spriteBatch.Draw(texture2, drawPos + new Vector2(0, -12f), null, color, Projectile.rotation, drawOrigin2, (float)range2, SpriteEffects.None, 0f);

                }
            }
            return true;
        }
    }
}