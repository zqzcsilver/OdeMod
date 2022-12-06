using Microsoft.Xna.Framework;

using OdeMod.Players;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Items.Series.HollowKnight
{
    internal class MothwingCloak : ModItem, IHollowKnight
    {
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("MothwingCloak");
            //DisplayName.AddTranslation(LanguageType.Chinese, "蛾翼披风");
            //Tooltip.SetDefault("蛾翅线编制的披风。让穿着者可在地面或空中向前冲刺。");
        }
        public override void SetDefaults()
        {
            Item.width = 64;
            Item.height = 58;
            Item.rare = ItemRarityID.Blue;
            Item.scale = 1f;
        }
        int timer = 15;
        int dash1 = 0;
        int dash2 = 0;
        float yy = 0;
        int timeall = 30;
        int direction = 0;
        public override void UpdateInventory(Player player)
        {
            if (timer > 0)
            {
                timer--;
            }
            if (timeall > 0)
            {
                timeall--;
            }
            if (Item.favorited)
            {
                if (player.controlRight && player.releaseRight && timeall == 0 && player.GetModPlayer<OdePlayer>().OnHollowKnightItemUsing == false)
                {
                    if (timer > 0 && direction == 2)
                    {
                        direction = 0;
                        timer = 0;
                        dash1 = 18;
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
                else if (player.controlLeft && player.releaseLeft && timeall == 0 && player.GetModPlayer<OdePlayer>().OnHollowKnightItemUsing == false)
                {
                    if (timer > 0 && direction == 1)
                    {
                        direction = 0;
                        timer = 0;
                        dash2 = 18;
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
            }
            if (dash1 > 0)
            {
                if (dash1 == 18)
                    for (int i = 1; i <= 40; i++)
                    {
                        int num = Dust.NewDust(player.Center + new Vector2(-10f, -60f), 60, 120, DustID.GemDiamond, 0f, 0f, 0, Color.White, 1.8f);
                        Main.dust[num].velocity.X -= 5f + Main.rand.Next(10, 30) * 0.5f;
                        Main.dust[num].velocity.Y *= 0.5f;
                        Main.dust[num].noGravity = true;

                    }
                player.velocity *= 0;
                dash1--;
                if (dash1 > 3)
                {
                    player.GetModPlayer<OdePlayer>().OnHollowKnightItemUsing = true;
                    player.velocity.X = 12f;
                    /*player.position.Y = yy;
                    player.velocity.Y *= 0;*/
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
                    player.velocity.X = player.maxRunSpeed;
                    player.gravity = 0.001f;
                    //player.velocity.X *= 0.9f;
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
                    player.gravity = 0.001f;
                    player.velocity.X = -12f;
                    /*player.position.Y = yy;
                    player.velocity.Y = 0;*/
                    for (int i = 1; i <= 3; i++)
                    {
                        int num = Dust.NewDust(player.position + new Vector2(-14f, 5f), player.width, 30, DustID.GemDiamond, 0f, 0f, 0, Color.White, 1.5f);
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
                    //player.velocity.X *= 0.9f;
                }
            }
        }

    }
}