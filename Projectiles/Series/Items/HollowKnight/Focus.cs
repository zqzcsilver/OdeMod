using Microsoft.Xna.Framework;
using OdeMod.Players;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Projectiles.Series.Items.HollowKnight
{
    internal class Focus : ModProjectile, IHollowKnightProjectile
    {
        public override void SetDefaults()
        {
            //统一顺序
            base.SetDefaults();
            Projectile.width = 6;
            Projectile.height = 6;
            Projectile.friendly = false;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.aiStyle = -1;
            Projectile.alpha = 255;
            Projectile.penetrate = -1;
        }

        //要求：聚集时玩家无法移动，只有在玩家不在空中时才能进行聚集 聚集时缩放屏幕，聚集结束后还原
        int timer = 0;
        float screenSize = 0;
        Vector2 Still = Vector2.Zero;
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            if (Collision.SolidCollision(player.position, player.width, player.height + 1))
            {
                if (Main.player[Projectile.owner].channel)
                {

                    timer++;
                    if (timer == 1)
                    {
                        player.GetModPlayer<OdePlayer>().OnHollowKnightItemUsing = true;
                        Still = player.position;
                        screenSize = Main.GameZoomTarget;
                    }

                    if (timer >= 1)
                    {
                        player.position = Still;
                        player.velocity *= 0;
                        player.controlJump = false;
                        player.GetModPlayer<OdePlayer>().HollowKnightMovement = true;
                    }
                    if (timer >= 10 && timer < 110)
                    {
                        Dust d = Dust.NewDustDirect(player.position + new Vector2(-10f, 40f), player.width + 12, 3, ModContent.DustType<Dusts.Focus>(), Projectile.velocity.X / 2f, Projectile.velocity.Y / 2f, 0, Color.White, 1.5f);
                        d.noGravity = true;
                        d.velocity.X = 0;
                        d.velocity.Y = -1.2f;
                        Main.GameZoomTarget += 0.001f;
                        if (timer % 2 == 0)
                        {
                            player.statMana--;
                        }
                    }
                    if (timer == 110)
                    {
                        for (int i = 1; i <= 30; i++)
                        {
                            Dust d = Dust.NewDustDirect(player.position + new Vector2(0f, 40f), player.width, 3, DustID.GemDiamond, 0f, 0f, 255, Color.White, 1.5f);
                            d.noGravity = true;
                            if (d.velocity.Y >= 0) d.velocity.Y *= -1;
                            d.velocity.Y -= 1.5f + Main.rand.Next(15, 40) * 0.1f;
                            if (Math.Abs(d.velocity.X) <= 1f) d.velocity.X = 2f * (d.velocity.X / Math.Abs(d.velocity.X));
                            else
                                d.velocity.X *= 2f;
                        }
                        player.statLife += 50;
                        player.HealEffect(50);
                    }
                    if (timer > 110 && timer <= 120)
                    {
                        for (int i = 1; i <= 10; i++)
                        {
                            if (Main.GameZoomTarget > screenSize)
                            {
                                Main.GameZoomTarget -= 0.001f;
                            }
                        }

                    }
                    if (timer > 120)
                    {
                        player.controlJump = true;
                        player.GetModPlayer<OdePlayer>().OnHollowKnightItemUsing = false;
                        Projectile.active = false;
                        player.GetModPlayer<OdePlayer>().HollowKnightMovement = false;
                    }
                }
                else
                {
                    if (Main.GameZoomTarget <= screenSize)
                    {
                        player.GetModPlayer<OdePlayer>().OnHollowKnightItemUsing = false;
                        Projectile.active = false;
                    }
                    
                    for (int i = 1; i <= 10; i++)
                    {
                        if (Main.GameZoomTarget > screenSize)
                        {
                            Main.GameZoomTarget -= 0.001f;
                        }
                    }
                    player.controlJump = true;
                    player.GetModPlayer<OdePlayer>().HollowKnightMovement = false;
                }
            }
        }
    }
}