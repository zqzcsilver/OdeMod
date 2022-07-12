using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace OdeMod.Projectiles.Series.Items.Frosted
{
    internal class Icelight_ : ModProjectile, IFrostedProjectile
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
            Projectile.alpha = 255;
            Projectile.penetrate = -1;
            Projectile.scale = 1f;
            ProjectileID.Sets.TrailCacheLength[base.Projectile.type] = 6;
            ProjectileID.Sets.TrailingMode[base.Projectile.type] = 0;
            Projectile.extraUpdates = 1;
        }
        public override void AI()
        {
            int num = Dust.NewDust(Projectile.position, 26, 26, DustID.IceTorch, 0f, 0f, 0, Color.White, 1f);
            Main.dust[num].velocity *= 0.1f;
            Main.dust[num].noGravity = true;
            Projectile.rotation += 0.2f;

            if (Projectile.timeLeft >= 290)
            {
                Projectile.alpha -= 26;
            }

            if (Projectile.timeLeft >= 190)
            {
                Projectile.penetrate = -1;
                Projectile.velocity *= 0.95f;
            }
            else
            {
                NPC target = null;
                Projectile.penetrate = 1;
                float distanceMax = 600f;
                foreach (NPC npc in Main.npc)
                {
                    if (npc.active && !npc.friendly)
                    {

                        float currentDistance = Vector2.Distance(npc.Center, Projectile.Center);

                        if (currentDistance < distanceMax)
                        {


                            distanceMax = currentDistance;
                            target = npc;
                        }
                    }
                }
                if (target != null)
                {

                    Vector2 targetVec = target.Center - Projectile.Center;
                    targetVec.Normalize();

                    targetVec *= 20f;

                    Projectile.velocity = (Projectile.velocity * 30f + targetVec) / 31f;
                }
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(44, damage);
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 40; i++)
            {
                int num = Dust.NewDust(Projectile.position, 26, 26, DustID.IceTorch, 0f, 0f, 0, Color.White, 1.5f);
                Main.dust[num].velocity = Projectile.velocity * 0.6f;
                Main.dust[num].noGravity = true;
            }
        }
        Texture2D texture;
        public override bool PreDraw(ref Color lightColor)
        {
            texture = ModContent.Request<Texture2D>(Texture).Value;
            Vector2 drawOrigin = new Vector2(Projectile.width * 0.5f, Projectile.height * 0.5f);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                Main.spriteBatch.Draw(texture, drawPos, null, color, Projectile.rotation - (k * 0.3f), drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
            }
            return true;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255 - Projectile.alpha, 255 - Projectile.alpha, 255 - Projectile.alpha, 255 - Projectile.alpha);
        }
    }
}