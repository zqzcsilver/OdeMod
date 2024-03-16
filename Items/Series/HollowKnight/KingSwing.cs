using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Items.Series.HollowKnight
{
    internal class KingSwing : ModItem, IHollowKnight
    {
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
            Item.width = 58;
            Item.height = 48;
            Item.rare = ItemRarityID.Blue;
            Item.scale = 1f;
        }
        bool CanSwing = true;
        int timer = 0;
        int jumpTick = 0;
        public override void UpdateInventory(Player player)
        {
            if (Item.favorited)
            {
                if (SolidCollision(player.position, player.width, player.height + 1))
                {
                    CanSwing = true;
                }

                if (player.velocity.Y != 0 && CanSwing && player.controlJump && player.releaseJump)
                {
                    jumpTick = 1;
                    Projectile.NewProjectileDirect(Item.GetSource_FromAI(), player.Center, new Vector2(0f, 0f), ModContent.ProjectileType<Projectiles.Series.Items.HollowKnight.Swing>(), 0, 0, player.whoAmI);
                }
                if (jumpTick == 1)
                {
                    timer++;
                    if (timer <= 9)
                    {
                        CanSwing = false;
                        player.velocity.Y = (4.5f - timer) * 1.5f;
                    }
                    if (timer > 9 && timer <= 16)
                    {
                        player.velocity.Y = -8f;
                    }
                    if (timer > 16)
                    {
                        timer = 0;
                        jumpTick = 0;
                    }
                }
            }
        }
    }
}