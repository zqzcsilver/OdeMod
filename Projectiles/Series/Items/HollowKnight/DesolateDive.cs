﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OdeMod.Players;
using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Projectiles.Series.Items.HollowKnight
{
    internal class DesolateDive : ModProjectile, IHollowKnightProjectile
    {
        public void CreateDust(Texture2D t, Vector2 center, float size)
        {

            Color[] c = new Color[t.Width * t.Height];
            Vector2 position = new Vector2(center.X - t.Width / 2f * size, center.Y - t.Height / 2f * size);
            t.GetData(c);
            Color color;
            for (int i = 0; i < c.Length; i += 2)
            {
                color = c[i];
                if (color != new Color(0, 0, 0, 0))
                {
                    Player player = Main.player[Projectile.owner];
                    Dust d = Dust.NewDustDirect(new Vector2(position.X + (i % t.Width + 1) * size + Main.rand.Next(-100, 100) * 0.02f, position.Y + (i / t.Width + 1) * size + Main.rand.Next(-100, 100) * 0.02f), 0, 0, DustID.GemDiamond, 0, 0, 120, Color.White, 1.2f);
                    d.noGravity = true;

                    d.velocity = new Vector2(0.05f, (d.position.Y - (player.Center.Y + 30f)) * 0.2f);
                }
            }
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

            if(Projectile.timeLeft>5)
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
                    float rad = (player.Center + new Vector2((float)Math.Cos(i * 6.28 / 3) * 20f, (float)Math.Sin(i * 6.28 / 3) * 20f) - player.Center).ToRotation();

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

                int num = Dust.NewDust(player.Center, 1, 1, DustID.GemDiamond, 0, 0, 120, Color.White, 1.2f);
                int num2 = Dust.NewDust(player.Center, 1, 1, DustID.GemDiamond, 0, 0, 120, Color.White, 1.2f);
                int num3 = Dust.NewDust(player.Center, 1, 1, DustID.GemDiamond, 0, 0, 120, Color.White, 1.2f);
                Main.dust[num].velocity.X *= 0.1f;
                Main.dust[num2].velocity.X = Main.rand.Next(15, 40) * 0.1f;
                Main.dust[num3].velocity.X = Main.rand.Next(15, 40) * -0.1f;
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
                player.GetModPlayer<OdePlayer>().fall = 1;
                Vector2 playerw = player.position / 16;
                for (int i = 0; i < (player.position.X % 16 == 0 ? 2 : 3); i++)
                {


                    if (Main.tile[(int)playerw.X + i, (int)playerw.Y + 3] != null &&
                        Main.tile[(int)playerw.X + i, (int)playerw.Y + 3].TileType != 0 &&
                         Main.tileSolid[Main.tile[(int)playerw.X + i, (int)playerw.Y + 3].TileType])
                    {

                        pos3 = player.position;
                        player.GetModPlayer<OdePlayer>().fall = 0;
                        Projectile.timeLeft = 25;
                        player.immune = true;
                        player.immuneTime = 80;
                        player.AddImmuneTime(Terraria.ID.ImmunityCooldownID.General, 60);
                        player.AddImmuneTime(Terraria.ID.ImmunityCooldownID.Lava, 60);
                        player.AddImmuneTime(Terraria.ID.ImmunityCooldownID.Bosses, 60);
                        player.AddImmuneTime(Terraria.ID.ImmunityCooldownID.WrongBugNet, 60);

                        Texture2D t2 = ModContent.Request<Texture2D>("OdeMod/Projectiles/Series/Items/HollowKnight/DesolateDive_F1", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
                        CreateDust(t2, player.Center + new Vector2(-10f, -20f), 1f);
                        guang = 25;
                        Projectile.NewProjectileDirect(Projectile.GetSource_FromAI(), player.Center + new Vector2(0f, 10f), new Vector2(0f, 0f), ModContent.ProjectileType<Projectiles.Series.Items.HollowKnight.SmashRange>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
                        for (int j = 1; j <= 60; j++)
                        {
                            Dust d = Dust.NewDustDirect(player.position + new Vector2(0f, 40f), player.width, 3, DustID.GemDiamond, 0f, 0f, 255, Color.White, 2f);
                            d.noGravity = true;
                            if (d.velocity.Y >= 0) d.velocity.Y *= Main.rand.Next(5, 40) * 0.1f;
                            d.velocity.Y -= 1f + Main.rand.Next(10, 80) * 0.1f;
                            if (Math.Abs(d.velocity.X) <= 1f) d.velocity.X *= 2f;
                            else
                                d.velocity.X *= 4f;
                        }
                        for (int j = 1; j <= 40; j++)
                        {
                            Dust d = Dust.NewDustDirect(player.position + new Vector2(0f, 40f), player.width, 3, DustID.GemDiamond, 0f, 0f, 255, Color.White, 2.2f);
                            d.noGravity = true;
                            d.velocity.Y *= 0.1f;
                            d.velocity.X *= 7f;
                        }

                        float demo = 1 + Vector2.DistanceSquared(Main.player[Main.myPlayer].Center, Projectile.Center) / 420000;
                        player.GetModPlayer<OdePlayer>().shakeInt = Math.Max(player.GetModPlayer<OdePlayer>().shakeInt, (int)(45 / demo));

                    }
                }
            }
            if (Projectile.timeLeft < 25)
            {
                player.velocity *= 0f;
                player.position = pos3;
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
                double range2 = Projectile.scale * 1;
                Texture2D texture = ModContent.Request<Texture2D>("OdeMod/Projectiles/Series/Items/HollowKnight/DesolateDive_Light").Value;
                Texture2D texture2 = ModContent.Request<Texture2D>("OdeMod/Projectiles/Series/Items/HollowKnight/DesolateDive_F2").Value;
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
                    range2 *= 1.01;
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