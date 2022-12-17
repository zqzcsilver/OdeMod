using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Projectiles.Misc
{
    internal class BlueLightPro : ModProjectile, IMiscProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.timeLeft = 180;
            Projectile.alpha = 0;
            Projectile.penetrate = -1;
            Projectile.scale = 1f;
            Main.projFrames[Projectile.type] = 2;
        }
        public override void AI()
        {
            if (Projectile.timeLeft % 5 == 0)
            {
                Projectile.frame++;
            }
            if (Projectile.frame > 1)
            {
                Projectile.frame = 0;
            }
            Projectile.rotation += 0.25f;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(ModContent.BuffType<Buffs.NaturalPower>(), 120);
            base.OnHitNPC(target, damage, knockback, crit);
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
              
            if (Projectile.velocity.X != oldVelocity.X)
            {
                Projectile.velocity.X = -oldVelocity.X;
            }
            if (Projectile.velocity.Y != oldVelocity.Y)
            {
                Projectile.velocity.Y = -oldVelocity.Y;
            }
            return false;
        }
    }
}
