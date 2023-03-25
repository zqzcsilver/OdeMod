using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
namespace OdeMod.Projectiles.Misc
{
    internal class CannonBase : ModProjectile, IMiscProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 34;
            Projectile.height = 34;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.timeLeft = 300;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.alpha = 255;
            Projectile.penetrate = -1;
            Projectile.scale = 1f;
        }
        private bool stop = false;
        private float rad = 0;
        Texture2D texture;
        Texture2D texture2;
        Texture2D texture3;
        Texture2D texture4;
        float factor = 1f;
        float factor2 = 0f;
        float factor3 = 0f;
        int timer = 0;
        public override void AI()
        {

            var own = ModContent.GetInstance<Items.Misc.Weapons.PulseofThunder>();
            Player player = Main.player[Projectile.owner];
            if(player.HeldItem.type!= ModContent.ItemType<Items.Misc.Weapons.PulseofThunder>())
            {
                Projectile.Kill();
            }
           /*if (own.Fire > 0 && factor < -2f)
            {
                own.Fire--;

                var x = Main.MouseWorld - (Projectile.Center - new Vector2(0, 20f));
                x.Normalize();
                x *= 10f;
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center - new Vector2(0, 20f), x, 357, Projectile.damage, 0, player.whoAmI);

            }*/
            rad = (Main.MouseWorld - (Projectile.Center - new Vector2(0, 20f))).ToRotation() + 1.57f;
            if (Projectile.timeLeft == 300)
            {
                Projectile.velocity *= 0;
                Projectile.Center = Main.MouseWorld;
            }
            Projectile.timeLeft = 2;
            Projectile.velocity.X *= 0f;
            if (!stop)
            {
                Dust dust = Dust.NewDustDirect(Projectile.Center - new Vector2(10f, 10f), 20, 20, 226, 0f, 0f, 0, Color.BlueViolet, 0.8f);
                dust.noGravity = true;
                if (Projectile.velocity.Y < 9.8f)
                {
                    Projectile.velocity.Y += 0.3f;
                }
                else
                    Projectile.velocity.Y = 9.8f;
            }

            if (stop)
            {
                if (Projectile.alpha > 0)
                {
                    Projectile.alpha -= 10;
                }
                Projectile.velocity.Y *= 0.55f;
            }
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 20; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.Center - new Vector2(10f, 10f), 20, 20, 226, 0f, 0f, 0, Color.BlueViolet, 0.8f);
                dust.noGravity = true;
                dust.velocity *= 2f;
            }
        }
        public override bool PreDraw(ref Microsoft.Xna.Framework.Color lightColor)
        {
            if (stop) factor -= 0.05f;
            texture = ModContent.Request<Texture2D>("OdeMod/Projectiles/Misc/CannonBase").Value;
            texture2 = ModContent.Request<Texture2D>("OdeMod/Projectiles/Misc/CannonBaseGhost").Value;
            texture3 = ModContent.Request<Texture2D>("OdeMod/Projectiles/Misc/CannonBow").Value;
            texture4 = ModContent.Request<Texture2D>("OdeMod/Projectiles/Misc/CannonBowGhost").Value;
            Vector2 drawPos = Projectile.Center;
            Vector2 drawOrigin = new Vector2(Projectile.width * 0.5f, Projectile.height * 0.5f);
            Vector2 drawPos2 = Projectile.Center - new Vector2(0f, 20f);
            Vector2 drawOrigin2 = new Vector2(19f, 13f);
            if (factor > 0)
            {
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
                Main.spriteBatch.Draw(texture2, drawPos - Main.screenPosition, null, lightColor * factor, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            }
            if (factor < -0.5f && factor >= -1.5f) factor2 = (-factor - 0.5f);
            if (factor < -1.5f && factor >= -2.5f)
            {
                factor2 = (2.5f + factor);
                factor3 = -factor - 1.5f;
            }

            if (factor < -0.5f)
            {

                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
                Main.spriteBatch.Draw(texture4, drawPos2 - Main.screenPosition, null, lightColor * factor2, rad, drawOrigin2, Projectile.scale, SpriteEffects.None, 0f);
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
                Main.spriteBatch.Draw(texture3, drawPos2 - Main.screenPosition, null, lightColor * factor3, rad, drawOrigin2, Projectile.scale, SpriteEffects.None, 0f);
            }
            return true;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            stop = true;
            Projectile.tileCollide = true;
            return false;
        }
    }
}