using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OdeMod.Players;

using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Items.Series.HollowKnight
{
    internal class ShadeCloak : ModItem, IHollowKnight
    {
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("ShadeCloak");
            //DisplayName.AddTranslation(LanguageType.Chinese, "暗影披风");
            //Tooltip.SetDefault("由深渊的物质形成的披风。允许穿戴者冲刺时穿过敌人和他们的攻击而不受到伤害。");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 16;
            Item.height = 16;
            Item.rare = ItemRarityID.Blue;
            Item.scale = 1f;
        }
        //粒子
        int timer = 15;
        public void CreateDust(Texture2D t, Vector2 center, float size)
        {

            Color[] c = new Color[t.Width * t.Height];
            Vector2 position = new Vector2(center.X - t.Width / 2f * size, center.Y - t.Height / 2f * size);
            t.GetData(c);
            Color color;
            for (int i = 0; i < c.Length; i += 1)
            {
                color = c[i];
                if (color != new Color(0, 0, 0, 0))
                {
                    Player player = Main.player[Item.whoAmI];
                    Dust d = Dust.NewDustDirect(new Vector2(position.X + (i % t.Width + 1) * size + Main.rand.Next(-100, 100) * 0.01f, position.Y + (i / t.Width + 1) * size + Main.rand.Next(-100, 100) * 0.01f), 0, 0, DustID.FireflyHit, 0, 0, 0, Color.Black, 1.2f);
                    d.noGravity = true;
                    if (player.direction == -1)
                    {
                        d.velocity = new Vector2((d.position.X - player.Center.X + 40f) * 0.2f, 0f);
                    }
                    else
                    {
                        d.velocity = new Vector2((d.position.X - player.Center.X - 40f) * 0.2f, 0f);
                    }
                }
            }
        }
        //冲刺
        int dash1 = 0;
        int dash2 = 0;
        int dash3 = 0;
        int dash4 = 0;
        float yy = 0;
        int lacktime = 120;
        int timeall = 30;
        int direction = 0;
        public override void UpdateInventory(Player player)
        {
            if (timer > 0)
            {
                timer--;
            }
            if (lacktime > 0)
            {
                lacktime--;
            }
            if (timeall > 0)
            {
                timeall--;
            }
            if (Item.favorited)
            {


                if (lacktime == 3)
                {
                    for (float i = 1; i <= 40; i++)
                    {
                        float rad2 = Main.rand.Next(0, 360) * 0.01f;
                        double radX1 = Math.Cos(i * (Math.PI / rad2));
                        double radY1 = Math.Sin(i * (Math.PI / rad2));
                        Vector2 cent1;
                        cent1.X = player.Center.X + (float)radX1 * 50f;
                        cent1.Y = player.Center.Y + (float)radY1 * 50f;
                        int num = Dust.NewDust(cent1, 1, 1, DustID.BatScepter, 0, 0, 100, Color.Black, 1.5f);
                        Main.dust[num].noGravity = true;
                        Main.dust[num].velocity = new Vector2(cent1.X - player.Center.X, cent1.Y - player.Center.Y) / -8f;
                    }
                }
                if (player.controlRight && player.releaseRight && timeall == 0 && player.GetModPlayer<OdePlayer>().OnHollowKnightItemUsing == false)
                {
                    if (lacktime != 0)
                    {
                        if (timer > 0 && direction == 1)
                        {
                            direction = 0;
                            timer = 0;
                            dash1 = 18;
                            yy = player.position.Y;
                        }
                        else
                        {
                            direction = 1;
                            timer = 15;
                            return;
                        }
                        timeall = 30;
                    }
                    else
                    {
                        if (timer > 0 && direction == 1)
                        {
                            direction = 0;
                            timer = 0;
                            dash3 = 18;
                            yy = player.position.Y;
                        }
                        else
                        {
                            direction = 1;
                            timer = 15;
                            return;
                        }
                        timeall = 30;
                        lacktime = 120;
                    }
                }
                else if (player.controlLeft && player.releaseLeft && timeall == 0 && player.GetModPlayer<OdePlayer>().OnHollowKnightItemUsing == false)
                {
                    if (lacktime != 0)
                    {
                        if (timer > 0 && direction == 2)
                        {
                            direction = 0;
                            timer = 0;
                            dash2 = 18;
                            yy = player.position.Y;
                        }
                        else
                        {
                            direction = 2;
                            timer = 15;
                            return;
                        }
                        timeall = 30;
                    }
                    else
                    {
                        if (timer > 0 && direction == 2)
                        {
                            direction = 0;
                            timer = 0;
                            dash4 = 18;
                            yy = player.position.Y;
                        }
                        else
                        {
                            direction = 2;
                            timer = 15;
                            return;
                        }
                        lacktime = 120;
                        timeall = 30;
                    }
                }
            }
            if (dash1 > 0)
            {
                if (dash1 == 18)
                {
                    for (int i = 1; i <= 40; i++)
                    {
                        int num = Dust.NewDust(player.Center + new Vector2(-10f, -60f), 60, 120, DustID.GemDiamond, 0f, 0f, 0, Color.White, 1.8f);
                        Main.dust[num].velocity.X -= 5f + Main.rand.Next(10, 30) * 0.5f;
                        Main.dust[num].velocity.Y *= 0.5f;
                        Main.dust[num].noGravity = true;
                    }

                }
                player.velocity *= 0;
                dash1--;
                if (dash1 > 3)
                {
                    player.velocity.X = 12f;
                    player.GetModPlayer<OdePlayer>().OnHollowKnightItemUsing = true;
                    player.gravity = 0.001f;

                    for (int i = 1; i <= 3; i++)
                    {
                        int num = Dust.NewDust(player.position + new Vector2(14f, 5f), player.width, 30, DustID.GemDiamond, 0f, 0f, 0, Color.White, 1.5f);
                        Main.dust[num].noGravity = true;
                        Main.dust[num].velocity.Y *= 0.5f;
                        Main.dust[num].velocity.X -= 4f;
                    }

                }
                if (dash1 <= 3)
                {
                    player.GetModPlayer<OdePlayer>().OnHollowKnightItemUsing = false;
                    player.velocity.X = 3f;
                    player.gravity = 0.001f;
                }
            }
            if (dash2 > 0)
            {
                if (dash2 == 18)
                    for (int i = 1; i <= 40; i++)
                    {
                        int num = Dust.NewDust(player.Center + new Vector2(-10f, -60f), 60, 120, DustID.GemDiamond, 0f, 0f, 0, Color.White, 1.8f);
                        Main.dust[num].velocity.X += 5f + Main.rand.Next(10, 30) * 0.5f;
                        Main.dust[num].velocity.Y *= 0.5f;
                        Main.dust[num].noGravity = true;
                    }
                player.velocity *= 0;
                dash2--;
                if (dash2 > 3)
                {
                    player.GetModPlayer<OdePlayer>().OnHollowKnightItemUsing = true;
                    player.velocity.X = -12f;
                    player.gravity = 0.001f;
                    for (int i = 1; i <= 3; i++)
                    {
                        int num = Dust.NewDust(player.position + new Vector2(-14f, 5f), player.width, 30, DustID.GemDiamond, 0f, 0f, 0, Color.White, 1.3f);
                        Main.dust[num].noGravity = true;
                        Main.dust[num].velocity.Y *= 0.5f;
                        Main.dust[num].velocity.X += 4f;
                    }
                }
                if (dash2 <= 3)
                {
                    player.GetModPlayer<OdePlayer>().OnHollowKnightItemUsing = false;
                    player.velocity.X = -player.maxRunSpeed;
                    player.gravity = 0.001f;
                }
            }
            if (dash3 > 0)
            {
                if (dash3 == 18)
                {
                    for (float i = 1; i <= 90; i++)
                    {
                        player.immuneTime = 40;
                        player.immune = true;
                        player.lavaImmune = true;
                        double radX1 = Math.Cos(i * (Math.PI / 45));
                        double radY1 = Math.Sin(i * (Math.PI / 45));
                        Vector2 cent1;
                        cent1.X = player.Center.X + (float)radX1 * 60f;
                        cent1.Y = player.Center.Y + (float)radY1 * 60f;
                        int num = Dust.NewDust(cent1, 10, 10, DustID.BatScepter, 0, 0, 100, Color.Black, 1.5f);
                        Main.dust[num].noGravity = true;
                        Main.dust[num].velocity = new Vector2(cent1.X - player.Center.X, cent1.Y - player.Center.Y) / -8f;
                    }
                    Texture2D t2 = ModContent.Request<Texture2D>("OdeMod/Items/Series/HollowKnight/JetTrace", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
                    CreateDust(t2, player.Center + new Vector2(0f, -5f), 1f);
                }
                player.velocity.Y *= 0f;
                dash3--;
                if (dash3 > 3)
                {
                    player.velocity.X = 12f;
                    player.GetModPlayer<OdePlayer>().OnHollowKnightItemUsing = true;
                    for (int i = 1; i <= 3; i++)
                    {
                        int num = Dust.NewDust(player.position + new Vector2(14f, 5f), player.width, 30, DustID.BatScepter, 0f, 0f, 0, Color.Black, 2f);
                        Main.dust[num].noGravity = true;
                        Main.dust[num].velocity.Y *= 0.5f;
                        Main.dust[num].velocity.X -= 4f;
                    }
                    player.gravity = 0.001f;
                }
                if (dash3 <= 3)
                {
                    player.GetModPlayer<OdePlayer>().OnHollowKnightItemUsing = false;
                    player.velocity.X = player.maxRunSpeed;
                    player.gravity = 0.001f;
                }
            }
            if (dash4 > 0)
            {
                if (dash4 == 18)
                {
                    Texture2D t2 = ModContent.Request<Texture2D>("OdeMod/Items/Series/HollowKnight/JetTrace", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
                    CreateDust(t2, player.Center + new Vector2(0f, -5f), 1f);
                    for (float i = 1; i <= 90; i++)
                    {
                        player.immuneTime = 40;
                        player.immune = true;
                        player.lavaImmune = true;
                        double radX1 = Math.Cos(i * (Math.PI / 45));
                        double radY1 = Math.Sin(i * (Math.PI / 45));
                        Vector2 cent1;
                        cent1.X = player.Center.X + (float)radX1 * 60f;
                        cent1.Y = player.Center.Y + (float)radY1 * 60f;
                        int num = Dust.NewDust(cent1, 10, 10, DustID.BatScepter, 0, 0, 100, Color.Black, 1.5f);
                        Main.dust[num].noGravity = true;
                        Main.dust[num].velocity = new Vector2(cent1.X - player.Center.X, cent1.Y - player.Center.Y) / -8f;
                    }
                }
                player.velocity.Y *= 0f;
                dash4--;
                if (dash4 > 3)
                {
                    player.GetModPlayer<OdePlayer>().OnHollowKnightItemUsing = true;
                    player.velocity.X = -12f;
                    player.gravity = 0.001f;
                    for (int i = 1; i <= 3; i++)
                    {
                        int num = Dust.NewDust(player.position + new Vector2(14f, 5f), player.width, 30, DustID.BatScepter, 0f, 0f, 0, Color.Black, 2f);
                        Main.dust[num].noGravity = true;
                        Main.dust[num].velocity.Y *= 0.5f;
                        Main.dust[num].velocity.X += 4f;
                    }
                }
                if (dash4 <= 3)
                {
                    player.GetModPlayer<OdePlayer>().OnHollowKnightItemUsing = false;
                    player.velocity.X = -3f;
                    player.gravity = 0.001f;
                }
            }
        }
    }
}
