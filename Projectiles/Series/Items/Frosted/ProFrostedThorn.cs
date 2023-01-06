using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ModLoader;

namespace OdeMod.Projectiles.Series.Items.Frosted
{
    internal class ProFrostedThorn : ModProjectile, IFrostedProjectile
    {
        protected int LastThorn = -1;
        internal float Longer = 0;
        internal Vector2 startCenter;
        public override void SetDefaults()
        {
            Projectile.width = 22;
            Projectile.height = 16;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.timeLeft = 70;
            Projectile.alpha = 0;
            Projectile.penetrate = 1;
            Projectile.scale = 1f;
            Projectile.extraUpdates = 1;
        }
        public override void AI()
        {
            base.AI();
            Projectile.Center = startCenter;

            Vector2 unit = Vector2.Normalize(Projectile.velocity);
            Terraria.Utils.PlotTileLine(Projectile.Center, Projectile.Center + unit * Longer, (Projectile.width + 16) * Projectile.scale, DelegateMethods.CutTiles);
        }
        internal void PutNextPro()
        {
            if (Longer == 0)
            {
                Kill(Projectile.timeLeft);
                return;
            }
            Vector2 unit = Vector2.Normalize(Projectile.velocity);
            float l = Longer, ml = Longer;
            float width = (Projectile.width + 16) * Projectile.scale;
            Vector2 start = startCenter - Projectile.Size / 2f * Projectile.scale,
                end = start + unit * l;
            if (!Collision.CanHit(start, (int)(width), 32, end, (int)(width), 32))
            {
                Longer = 0;
                l /= 2f;
                end = start + unit * l;

                do
                {
                    if (!Collision.CanHit(start, (int)(width), 32, end, (int)(width), 32))
                    {
                        l /= 2f;
                        end = start + unit * l;
                    }
                    else
                    {
                        Longer += l;
                        start = end;
                        l /= 2f;
                        end = start + unit * l;
                    }
                }
                while (l > 1);
            }
            end = startCenter + unit * (Longer - 1);
            var proj = (ProFrostedThorn)Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), end,
                Projectile.oldVelocity, Projectile.type, Projectile.damage, Projectile.knockBack).ModProjectile;
            proj.startCenter = proj.Projectile.Center;
            proj.Longer = ml - Longer;
            proj.PutNextPro();
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;
            Vector2 start = startCenter - Main.screenPosition;
            float l = Longer;
            Vector2 unit = Vector2.Normalize(Projectile.velocity), end = start + unit * l, center;
            float rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            Main.spriteBatch.Draw(texture, start, new Rectangle(0, 0, 22, 16),
                Color.White, rotation, new Vector2(11, 8),
                1f, SpriteEffects.FlipVertically, 0f);
            l -= 9;
            Main.spriteBatch.Draw(texture, end, new Rectangle(0, 28, 22, 18),
                Color.White, rotation, new Vector2(11, 9),
                1f, SpriteEffects.FlipVertically, 0f);
            for (int i = 15; i <= l; i += 10)
            {
                center = start + unit * i;
                Main.spriteBatch.Draw(texture, center, new Rectangle(0, 28, 22, 10),
                    Color.White, rotation, new Vector2(11, 5),
                    1f, SpriteEffects.None, 0f);
            }
            return false;
        }
    }
}
