using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
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
            Projectile.penetrate = 3;
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
        public int hitnum = 0;//撞击次数
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if(++hitnum >= 3)
            {
                Projectile.Kill();
            }
            else
            {
                SoundEngine.TryGetActiveSound(SoundEngine.PlaySound(SoundID.Item94, Projectile.position), out ActiveSound active);
                active.Volume = 0.4f;
            }
            for (int i = 0; i < 10; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position + oldVelocity, Projectile.width, Projectile.height, DustID.Clentaminator_Cyan, 0f, 0f, 100, default(Color), 1f);
                dust.noGravity = true;
                dust.velocity = Projectile.velocity * 0.5f;
            }
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
        public override void Kill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item96, Projectile.position);
            SoundEngine.TryGetActiveSound(SoundEngine.PlaySound(SoundID.Item96, Projectile.position), out ActiveSound active);
            active.Volume = 0.6f;
            base.Kill(timeLeft);
        }
    }
}
