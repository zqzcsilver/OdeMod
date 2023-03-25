using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Color = Microsoft.Xna.Framework.Color;

namespace OdeMod.Projectiles.Series.Items.Miracle
{
    internal class GloryProj : ModProjectile, IMiracleProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 56;
            Projectile.height = 54;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 32;
            Projectile.alpha = 0;
            Projectile.penetrate = -1;
            Projectile.scale = 1f;
            Projectile.hide = true;
            ProjectileID.Sets.TrailCacheLength[base.Projectile.type] = 8;
            ProjectileID.Sets.TrailingMode[base.Projectile.type] = 2;
        }
        private int direc = 0;
        private float rad = 0;
        public override void AI()
        {

            Player player = Main.player[Projectile.owner];
            if (Projectile.timeLeft == 32)
            {
                direc = player.direction;
                Projectile.spriteDirection = direc;
                if (player.direction == -1)
                    rad = 2.356f + 3.1416f;
                else
                    rad = 0.785f + 3.1416f;
            }
            if (Projectile.timeLeft == 22)
            {
                 Projectile.NewProjectile(Projectile.GetSource_FromAI(), player.Center, Vector2.Normalize(Main.MouseWorld - player.Center) * 5f, ModContent.ProjectileType<Projectiles.Series.Items.Miracle.GloryLight>(), Projectile.damage, Projectile.knockBack, player.whoAmI);
            }
            if (Projectile.timeLeft <= 22)
            {
                rad += direc * (float)Math.Sin(Projectile.timeLeft / 22f * 3.1416f) * 0.2f;
                int num2 = Dust.NewDust(Projectile.Center + new Vector2((float)Math.Cos(rad), (float)Math.Sin(rad)) * 72, Projectile.width, Projectile.height, DustID.PinkTorch, 0f, 0f, 0, Color.White, 1.5f);
                Main.dust[num2].noGravity = true;

                Main.dust[num2].velocity = new Vector2(-(float)Math.Sin(rad), (float)Math.Cos(rad)) * 4f * direc;
                int num3 = Dust.NewDust(Projectile.Center + new Vector2((float)Math.Cos(rad), (float)Math.Sin(rad)) * 48, Projectile.width, Projectile.height, DustID.MagicMirror, 0f, 0f, 0, Color.White, 1.5f);
                Main.dust[num3].noGravity = true;

                Main.dust[num3].velocity = new Vector2(-(float)Math.Sin(rad), (float)Math.Cos(rad)) * 3f * direc;

            }
            Projectile.Center = player.Center + new Vector2((float)Math.Cos(rad), (float)Math.Sin(rad)) * 44f;
            if (direc == -1)
                Projectile.rotation = rad + 0.785f + 1.57f;
            else
                Projectile.rotation = rad + 0.785f;
        }
        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {
            overPlayers.Add(index);
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            Player player = Main.player[Projectile.owner];
            float point = 0f;
            return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Projectile.Center,
                Projectile.Center + 72 * new Vector2((float)Math.Cos(rad), (float)Math.Sin(rad)), 40, ref point);
        }
        public override void PostDraw(Color lightColor)
        {
            Player player = Main.player[Projectile.owner];
            Texture2D texture = ModContent.Request<Texture2D>("OdeMod/Projectiles/Series/Items/Miracle/GloryProjGhost").Value;
            Vector2 drawOrigin = new Vector2(Projectile.width * 0.5f, Projectile.height * 0.5f);
            float size = 0;
            if (Projectile.timeLeft > 22)
                size = 1f + (32 - Projectile.timeLeft) * 0.1f;
            else
                size = 2f;
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            if (player.direction == 1)
                Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition, null, Color.White, Projectile.rotation, drawOrigin, Projectile.scale * size, SpriteEffects.None, 0f);
            else
                Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition, null, Color.White, Projectile.rotation, drawOrigin, Projectile.scale * size, SpriteEffects.FlipHorizontally, 0f);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length) * 0.8f;
                if (player.direction == 1)
                    Main.spriteBatch.Draw(texture, drawPos, null, color, Projectile.oldRot[k], drawOrigin, Projectile.scale * size, SpriteEffects.None, 0f);
                else
                    Main.spriteBatch.Draw(texture, drawPos, null, color, Projectile.oldRot[k], drawOrigin, Projectile.scale * size, SpriteEffects.FlipHorizontally, 0f);
            }
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
        }
        /*public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255 - Projectile.alpha, 255 - Projectile.alpha, 255 - Projectile.alpha, 255 - Projectile.alpha);
        }*/
    }
}