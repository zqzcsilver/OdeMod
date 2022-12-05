using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OdeMod.Players;

using System.Collections.Generic;

using Terraria;
using Terraria.ModLoader;

namespace OdeMod.Projectiles.Series.Items.HollowKnight
{
    internal class BeFall : ModProjectile, IHollowKnightProjectile
    {
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
            Projectile.alpha = 235;
            Projectile.penetrate = -1;
            Projectile.hide = true;
        }
        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {

            overPlayers.Add(index);
        }
        public override bool ShouldUpdatePosition()
        {
            return false;
        }
        bool x = false;
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];


            if (Projectile.ai[0] < 600)
                Projectile.ai[0]++;

            if (Projectile.ai[1] > 205)
            {
                Projectile.active = false;
            }

            if (Main.player[Projectile.owner].channel)
            {

                //Static.hallow = (int)Projectile.ai[0];

            }
            else
            {
                if (Projectile.ai[0] < 120)
                {
                    Projectile.active = false;
                }

                if (Projectile.ai[0] >= 120 && Projectile.ai[0] < 205)
                {
                }
                if (Projectile.ai[0] >= 205)
                {
                    Projectile.active = false;
                }
            }

            if ((int)Projectile.ai[0] == 203)
            {

                if (player.GetModPlayer<OdePlayer>().HallowMode)
                {
                    player.GetModPlayer<OdePlayer>().HallowMode = false;
                    Main.NewText("圣巢模式已关闭");
                    return;
                }
                else
                {
                    player.GetModPlayer<OdePlayer>().HallowMode = true;
                    Main.NewText("圣巢模式已开启");
                    return;

                }
            }

        }
        public override bool PreDraw(ref Color lightColor)
        {
            Player player = Main.player[Projectile.owner];
            Texture2D texture = ModContent.Request<Texture2D>("OdeMod/Projectiles/Series/Items/HollowKnight/Hallownest").Value;
            Texture2D texture2 = ModContent.Request<Texture2D>("OdeMod/Projectiles/Series/Items/HollowKnight/WhiteScreen").Value;
            Vector2 drawPos = player.position - Main.screenPosition + new Vector2(6f, 0f);
            Color color = new Color(5 + (int)Projectile.ai[0] * 5, 5 + (int)Projectile.ai[0] * 5, 5 + (int)Projectile.ai[0] * 5, 5 + (int)Projectile.ai[0] * 5);
            Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, texture.Height * 0.5f);
            Vector2 drawOrigin2 = new Vector2(texture2.Width * 0.5f, texture2.Height * 0.5f);
            float range = 1;
            float range2 = 1;
            if ((int)Projectile.ai[0] <= 50)
            {
                Main.spriteBatch.Draw(texture, drawPos, null, color, Projectile.rotation, drawOrigin, range, SpriteEffects.None, 0f);
                for (int i = 1; i <= 10; i++)
                {
                    color *= 0.9f;
                    range += 0.01f;
                    Main.spriteBatch.Draw(texture, drawPos, null, color, Projectile.rotation, drawOrigin, range, SpriteEffects.None, 0f);
                }
            }
            if ((int)Projectile.ai[0] > 50 && (int)Projectile.ai[0] <= 140)
            {
                for (int i = 1; i <= ((int)Projectile.ai[0] - 50) * 0.8f; i++)
                {
                    range *= 1.04f;
                    range2 *= 0.94f;
                    color *= 0.9f;
                    Main.spriteBatch.Draw(texture, drawPos, null, color, Projectile.rotation, drawOrigin, range, SpriteEffects.None, 0f);
                    Main.spriteBatch.Draw(texture, drawPos, null, new Color(255, 255, 255, 255), Projectile.rotation, drawOrigin, range2, SpriteEffects.None, 0f);
                    range2 *= 0.93f;
                    Main.spriteBatch.Draw(texture, drawPos, null, new Color(255, 255, 255, 255), Projectile.rotation, drawOrigin, range2, SpriteEffects.None, 0f);
                }
            }
            if ((int)Projectile.ai[0] > 80 && (int)Projectile.ai[0] <= 140)
            {
                Main.spriteBatch.Draw(texture2, drawPos, null, new Color(15 + ((int)Projectile.ai[0] - 80) * 4, 15 + ((int)Projectile.ai[0] - 80) * 4, 15 + ((int)Projectile.ai[0] - 80) * 4, 15 + ((int)Projectile.ai[0] - 80) * 4), Projectile.rotation, drawOrigin2, 2, SpriteEffects.None, 0f);

            }
            if ((int)Projectile.ai[0] > 140 && (int)Projectile.ai[0] <= 160)
            {
                Main.spriteBatch.Draw(texture2, drawPos, null, new Color(255, 255, 255, 255), Projectile.rotation, drawOrigin2, 2, SpriteEffects.None, 0f);
            }
            if ((int)Projectile.ai[0] > 160 && (int)Projectile.ai[0] <= 200)
            {
                Main.spriteBatch.Draw(texture2, drawPos, null, new Color(255 - ((int)Projectile.ai[0] - 160) * 6, 255 - ((int)Projectile.ai[0] - 160) * 6, 255 - ((int)Projectile.ai[0] - 160) * 6, 255 - ((int)Projectile.ai[0] - 160) * 6), Projectile.rotation, drawOrigin2, 2, SpriteEffects.None, 0f);

            }

            return false;
        }
    }
}