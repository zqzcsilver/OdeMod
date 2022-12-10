using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Projectiles.Series.Items.Sharpsand
{
    internal class SharpsandSwordgas : ModProjectile, ISharpsandProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 26;
            Projectile.height = 26;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.timeLeft = 300;
            Projectile.alpha = 0;
            Projectile.penetrate = 1;
            Projectile.scale = 1f;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 1;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
            Projectile.extraUpdates = 1;
        }

        public override void AI()
        {
            int num = Dust.NewDust(Projectile.Center - new Vector2(4f, 4f), 8, 8, DustID.FlameBurst, 0f, 0f, 0, Color.White, 1.2f + (float)Math.Sin((300 - Projectile.timeLeft) * 0.06f) * 0.5f);
            Main.dust[num].velocity *= 0.5f;
            Main.dust[num].noGravity = true;

            Projectile.rotation = (float)
               System.Math.Atan2((double)Projectile.velocity.Y,
               (double)Projectile.velocity.X) + 0.785f;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, damage);
        }

        public override void Kill(int timeLeft)
        {
            Player player = Main.player[Projectile.owner];
            Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<Projectiles.Series.Items.Sharpsand.SharpsandBOOM>(), Projectile.damage, 0, player.whoAmI);
            for (int i = 0; i < 40; i++)
            {
                int num = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.GoldFlame, 0f, 0f, 0, Color.White, 1.5f);
                Main.dust[num].velocity *= 4f;
                Main.dust[num].noGravity = true;
            }
        }

        private Texture2D texture;
        private float sizex = 1.5f;

        public override bool PreDraw(ref Color lightColor)
        {
            /*GraphicsDevice gd = Main.instance.GraphicsDevice;
            SpriteBatch sb = Main.spriteBatch;

            gd.SetRenderTarget(Main.screenTargetSwap);
            sb.End();
            sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            sb.Draw(Main.screenTarget, Vector2.Zero, Color.White);
            sb.End();
            //在screenTargetSwap中保存原图
            if (render == null)
            {
                render = new RenderTarget2D(Main.graphics.GraphicsDevice, Main.screenWidth, Main.screenHeight);
            }
            gd.SetRenderTarget(render);
            gd.Clear(Color.Transparent);
            sb.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            foreach (Dust d in Main.dust)
            {
                if (d.type == ModContent.DustType<Dusts.Dream>() && d.active)
                {
                    Texture2D hallowseal = ModContent.Request<Texture2D>("OdeMod/Images/Effects/ballself", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
                    sb.Draw(hallowseal, d.position - Main.screenPosition, null, Color.White, 0, hallowseal.Size() / 2, d.scale, SpriteEffects.None, 0);
                }
            }
            sb.End();

            //在render中绘制图案

            gd.SetRenderTarget(Main.screenTarget);
            gd.Clear(Color.Transparent);
            sb.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            sb.Draw(Main.screenTargetSwap, Vector2.Zero, Color.White);
            sb.End();
            //在screenTarget上绘制保存过的原图
            sb.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            var shader = ModContent.Request<Effect>("OdeMod/Effects/Content/Starry", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
            gd.Textures[0] = render;
            gd.Textures[1] = ModContent.Request<Texture2D>("OdeMod/Images/Effects/Night", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
            Main.graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
            Main.graphics.GraphicsDevice.SamplerStates[1] = SamplerState.PointWrap;
            shader.CurrentTechnique.Passes[0].Apply();
            sb.Draw(render, Vector2.Zero, Color.White);
            sb.End();
            sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

            //在screenTarget上绘制render
            return false;*/

            sizex = 1.5f + (float)Math.Sin((300 - Projectile.timeLeft) * 0.05f) * 0.32f;
            texture = ModContent.Request<Texture2D>(Texture).Value;
            Vector2 drawOrigin = new Vector2(Projectile.width * 0.5f, Projectile.height * 0.5f);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Color color;
                Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                if (sizex > 1.56f)
                    color = Projectile.GetAlpha(lightColor) * 0.5f * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                else
                    color = Color.Red * 0.5f * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                Main.spriteBatch.Draw(texture, drawPos, null, color, Projectile.rotation, drawOrigin, sizex, SpriteEffects.None, 0f);
            }

            GraphicsDevice gd = Main.instance.GraphicsDevice;
            SpriteBatch sb = Main.spriteBatch;

            return true;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255 - Projectile.alpha, 255 - Projectile.alpha, 255 - Projectile.alpha, 255 - Projectile.alpha);
        }
    }
}