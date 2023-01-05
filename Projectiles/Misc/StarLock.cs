using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace OdeMod.Projectiles.Misc
{
    internal class StarLock : ModProjectile, IMiscProjectile
    {
        //我直接抄银烛码
        public override string Texture => "OdeMod/Projectiles/Misc/Yao";
        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.width = 6;
            Projectile.height = 6;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.aiStyle = -1;
            Projectile.alpha = 0;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 120;
        }
        private float m = 0f;
        public Vector2 TVEC = Vector2.Zero;
        public override void AI()
        {
            if (TVEC == Vector2.Zero)
            {
                TVEC = Projectile.velocity;
            }

            if (m <= (float)(Math.PI * 5 / 11))
                m += (float)(Math.PI / 100);
            Vector2 shoot;
            int type = DustID.BlueCrystalShard;
            if (Projectile.ai[0] == 1)
            {
                shoot = new Vector2(25 / (float)Math.Sqrt(Math.Cos(m)), 3 * (float)Math.Cos(3 * m)).RotatedBy(TVEC.ToRotation());
                type = DustID.BlueCrystalShard;
            }
            else if (Projectile.ai[0] == 2)
            {
                shoot = new Vector2(25 / (float)Math.Sqrt(Math.Cos(m)), -3 * (float)Math.Cos(3 * m)).RotatedBy(TVEC.ToRotation());
                type = 71;
            }
            else
            {
                shoot = TVEC;//图啥
            }
            Projectile.velocity = Vector2.Normalize(shoot) * 10f;
            int num = Dust.NewDust(Projectile.Center, 1, 1, type, 0f, 0f, 0, Color.White, 1.1f);
            Main.dust[num].noGravity = true;
            Main.dust[num].velocity *= 0.1f;
            Projectile.rotation = (float)
                   System.Math.Atan2((double)Projectile.velocity.Y,
                   (double)Projectile.velocity.X) - 1.57f;
            if (Projectile.timeLeft % 4 == 0)
            {
                if (Projectile.frame == 0 || Projectile.frame == 1)
                    Projectile.frame++;
                else Projectile.frame = 0;

            }
        }
        public override void Kill(int timeLeft)
        {

            base.Kill(timeLeft);
        }
        //让我理一理，首先1/2几率施加debuff，并召唤5个星，然后1/3几率召唤3个，最后召唤1个
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (!target.boss)
            {
                if (target.life <= target.lifeMax && Main.rand.NextBool(2))
                {
                    target.AddBuff(ModContent.BuffType<Buffs.Locked>(), 120);
                    Chance(target, damage, knockback, 5, 20f);
                }
                else if (Main.rand.NextBool(3))
                {
                    Chance(target, damage, knockback, 3, 15f);
                }
                else
                {
                    Chance(target, damage, knockback, 1, 15f);
                }
            }
            else
            {
                Chance(target, damage, knockback, 4, 25f);
            }
            base.OnHitNPC(target, damage, knockback, crit);
        }
        public void Chance(NPC target, int damage, float knockback, int number, float speed)
        {
            for (int i = 0; i < number; i++)
            {
                Vector2 pVEC = new Vector2(Main.MouseWorld.X, Main.screenPosition.Y - Main.rand.Next(50, 100)) +
                    new Vector2(Main.rand.Next(-200, 200), Main.rand.Next(-50, 50));
                //Vector2 tVEC = Vector2.Normalize(new Vector2(Main.MouseWorld.X + Main.rand.Next(-40, 40), Main.MouseWorld.Y +
                //    Main.rand.NextFloat(-80f, -50f)) - pVEC) * 20;
                Vector2 tShoot = Vector2.Normalize(target.Center - pVEC) * speed;
                Projectile.NewProjectile(Projectile.GetSource_OnHit(target), pVEC, tShoot, 9, damage, knockback, Projectile.whoAmI);
            }
        }
    }
}
