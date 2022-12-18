using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace OdeMod.Projectiles.Series.Boss.MiracleRecorder
{
    internal class Servants : ModProjectile, IMiracleRecorderProj
    {
        public override void SetDefaults()
        {
            Projectile.width = 42;
            Projectile.height = 42;
            Projectile.aiStyle = -1;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.alpha = 0;
            Projectile.penetrate = 1;
            Projectile.scale = 1f;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 8;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override void AI()
        {
            Projectile.velocity *= 0f;
            Projectile.rotation += 0.01f;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, damage);
        }
       
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255 - Projectile.alpha, 255 - Projectile.alpha, 255 - Projectile.alpha, 255 - Projectile.alpha);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;
            Vector2 drawOrigin = new Vector2(42 * 0.5f, 56 * 0.7f);
            Vector2 drawPos = Projectile.position - Main.screenPosition + drawOrigin + new Vector2(0f, -16f);
            Main.spriteBatch.Draw(texture, drawPos, new Rectangle(0, Projectile.frame*56, 42, 56), lightColor * ((255f - (float)Projectile.alpha) / 255f), Projectile.rotation, drawOrigin, 1f, SpriteEffects.None, 0f);
            return false;

        }
        public override void PostDraw(Color lightColor)
        {
          
        }
    }
}