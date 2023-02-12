using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Projectiles.Series.Items.RiftValley
{
    internal class RosewoodCrystal_2 : ModProjectile, IRiftBalleyProjectile
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.width = 6;
            Projectile.height = 6;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.aiStyle = -1;
            Projectile.alpha = 0;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 300;
        }
        public override void AI()
        {
            if (Projectile.timeLeft == 300)
                Main.NewText(1);
            Lighting.AddLight(Projectile.position, 0.5f, 0.0f, 0.0f);

            Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.LifeDrain, 0f, 0f, 100, default(Color), 1f);
            dust.noGravity = true;
            dust.velocity *= 0.2f;

            NPC target = null;
            float dismax = 600;
            foreach (NPC npc in Main.npc)
            {
                if (npc.active && !npc.friendly)
                {
                    float dis = Vector2.Distance(npc.Center, Projectile.Center);
                    if (dis < dismax)
                    {
                        dismax = dis;
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
}
