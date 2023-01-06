using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace OdeMod.Projectiles.Misc
{
    internal class PurpleSteel : ModProjectile
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.width = 32;
            Projectile.height = 38;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.alpha = 0;
            Projectile.penetrate = 1;
            Projectile.scale = 0.8f;
        }
        public bool ShootToMouse = true;
        public bool ShootToTarget = true;
        public bool Locked = true;
        public NPC target = null;
        public override void AI()
        {
            for (int i = 0; i < 10; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.VampireHeal, 0f, 0f, 100, default(Color), 1f);
                dust.noGravity = true;
                dust.velocity *= 0.2f;
            }
            Projectile.ai[0]++;
            //效率太低 之后简化
            if (Projectile.ai[0] >= 30)
            {
                if (Locked)
                {
                    float dismax = 160;
                    foreach (NPC npc in Main.npc)
                    {
                        if (npc.active && !npc.friendly)
                        {
                            float dis = Vector2.Distance(npc.Center, Main.MouseWorld);
                            if (dis < dismax)
                            {
                                dismax = dis;
                                target = npc;
                                Locked = false;
                                break;
                            }
                        }
                    }
                }
                if (target != null && target.active)
                {
                    Vector2 targetVec = target.Center - Projectile.Center;
                    targetVec.Normalize();
                    targetVec *= 20f;
                    Projectile.velocity = (Projectile.velocity * 20f + targetVec) / 21f;
                    ShootToMouse = false;
                }
                else if (ShootToMouse)
                {
                    Vector2 targetVec = Main.MouseWorld - Projectile.Center;
                    targetVec.Normalize();
                    targetVec *= 20f;
                    Projectile.velocity = targetVec;
                    ShootToMouse = false;
                    Locked = false;
                }
            }
            base.AI();
        }
    }
}
