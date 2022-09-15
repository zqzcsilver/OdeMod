using Microsoft.Xna.Framework;

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
        int m = 0;
        int h = 0;
        Vector2 vec1;
        float si = 0;
        bool bujump = false;
        //要求：聚集时玩家无法移动，只有在玩家不在空中时才能进行聚集
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (m == 0 && player.statMana < 49)
            {
                Projectile.active = false;
            }

            if (player.velocity.Y == 0 && bujump == false)
            {
                bujump = true;
            }

            Projectile.damage = 0;
            m++;
            if (h == 0 && Main.player[Projectile.owner].velocity.Y == 0 && bujump == true)
            {
                vec1 = player.position;
                h = 1;
                si = Main.GameZoomTarget;
            }

            if (Main.player[Projectile.owner].channel && Main.player[Projectile.owner].velocity.Y == 0 && bujump == true)
            {

                player.itemTime = 30;
                player.itemAnimation = 30;
                Projectile.timeLeft = 30;
                player.velocity = new Vector2(0, 0);
                player.position = vec1;
                if (m < 100 && m >= 10)
                {
                    Dust d = Dust.NewDustDirect(player.position + new Vector2(-10f, 40f), player.width + 12, 3, ModContent.DustType<Dusts.Focus>(), Projectile.velocity.X / 2f, Projectile.velocity.Y / 2f, 0, Color.White, 1.5f);
                    d.noGravity = true;
                    d.velocity.X = 0;
                    d.velocity.Y = -1.2f;
                    Main.GameZoomTarget += 0.002f;

                }
                if (m < 100 && m > 25)
                {
                    if (m % 3 == 0)
                        player.statMana -= 2;
                }
                if (m == 100)
                {
                    for (int i = 1; i <= 30; i++)
                    {
                        Dust d = Dust.NewDustDirect(player.position + new Vector2(0f, 40f), player.width, 3, DustID.GemDiamond, 0f, 0f, 255, Color.White, 2f);
                        d.noGravity = true;
                        if (d.velocity.Y >= 0) d.velocity.Y *= -1;
                        d.velocity.Y -= 1f + Main.rand.Next(15, 40) * 0.1f;
                        if (Math.Abs(d.velocity.X) <= 1f) d.velocity.X = 1.8f * (d.velocity.X / Math.Abs(d.velocity.X));
                        else
                            d.velocity.X *= 1.8f;
                    }
                }
            }

            if (m > 100 && m <= 125)
            {
                for (int i = 1; i <= 10; i++)
                {
                    if (Main.GameZoomTarget > si)
                    {
                        Main.GameZoomTarget -= 0.002f;
                    }
                }
                player.statLife += 2;
            }
            if (m >= 125)
            {
                m = 0;
                Projectile.active = false;
            }


            if (!Main.player[Projectile.owner].channel)
            {
                if (Main.player[Projectile.owner].velocity.Y == 0 && bujump == true && Main.GameZoomTarget > si)
                {
                    for (int i = 1; i <= 10; i++)
                    {
                        if (Main.GameZoomTarget > si)
                        {
                            Main.GameZoomTarget -= 0.002f;
                        }
                    }

                }
            }
        }
    }
}