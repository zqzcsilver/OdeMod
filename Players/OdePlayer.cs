using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OdeMod.CardMode.CardComponents.AnimationComponents;
using OdeMod.CardMode.CardComponents.BaseComponents;
using OdeMod.CardMode.CardComponents.DrawComponents;
using OdeMod.CardMode.PublicComponents;

using ReLogic.Content;

using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace OdeMod.Players
{
    internal class OdePlayer : ModPlayer, IOdePlayer
    {
        public bool Carapace = false;//ÉúÃü¼×¿Ç
        private bool Carapace_Open = false;
        private int Carapace_Num = 0;
        public bool HallowMode = false;//Ê¥³²Ä£Ê½
        public int ShakeInt = 0;//ÕðÆÁ
        private int shaketick = 0;
        public int Fall = 0;
        public bool OnHollowKnightItemUsing = false;
        protected int FallTimer = 0;
        public bool HollowKnightMovement = false;
        public int Hallow = 0;
        public int Rolling = 0;

        public int MiracleX = 0;

        public bool MagicBoneShield = false;//Ä§·¨¹Ç¶Ü
        public bool HolyFlameCrown = false;

        public override void SaveData(TagCompound tag)
        {
            tag.Add("Carapace", Carapace);
            tag.Add("Carapace_Open", Carapace_Open);
            tag.Add("HallowMode", HallowMode);
        }

        public override void LoadData(TagCompound tag)
        {
            Carapace = tag.Get<bool>("Carapace");
            Carapace_Open = tag.Get<bool>("Carapace_Open");
            HallowMode = tag.Get<bool>("HallowMode");
        }

        public override void PreUpdateMovement()
        {
            if (HollowKnightMovement)
                Player.velocity *= 0f;
        }

        public override void PostUpdate()
        {
            if (Carapace && !Carapace_Open)
            {
                Carapace_Num++;
                if (Carapace_Num > 1200)
                {
                    Carapace_Open = true;
                    Carapace_Num = 0;
                }
            }
            base.PostUpdate();
        }

        public override void ModifyScreenPosition()
        {
            if (!Main.gameMenu)
            {
                shaketick++;
                if (ShakeInt >= 0 && shaketick >= 60)
                    ShakeInt--;
                if (ShakeInt > 50)
                    ShakeInt = 50;
                if (ShakeInt < 0)
                    ShakeInt = 0;
                if (!Main.gamePaused && Main.hasFocus)
                {
                    Main.screenPosition += new Vector2(
                        ShakeInt * Main.rand.NextFloatDirection() / 2f,
                        ShakeInt * Main.rand.NextFloatDirection() / 2f);
                }
            }
            else
            {
                ShakeInt = 0;
                shaketick = 0;
            }

            ////ÕðÆÁ
            //try
            //{
            //    float demo = 1 + Vector2.DistanceSquared(Main.player[Main.myPlayer].Center, player.Center) / 500000;
            //    OdePlayer.shakeInt = Math.Max(OdePlayer.shakeInt, (int)(15 / demo));
            //}
            //catch
            //{
            //}
        }

        public override void OnHurt(Player.HurtInfo info)
        {
            base.OnHurt(info);
            if (HallowMode)
            {
                info.Damage = 25 + Player.statDefense / 2;
            }
        }

        public override void PostHurt(Player.HurtInfo info)
        {
            base.PostHurt(info);
            if (HallowMode)
            {
                Player.immune = true;
                Player.immuneTime = 60;
            }
        }

        public override void ResetEffects()
        {
            MagicBoneShield = false;
            HolyFlameCrown = false;
            if (Fall == 1)
            {
                FallTimer = 3;
                Player.maxFallSpeed = 25f;
                Player.velocity.Y += 25f;
                Player.immune = true;
                Player.immuneTime = 10;
            }
            if (Fall == 0)
            {
                if (FallTimer > 0)
                {
                    Player.maxFallSpeed = 10.01f;
                    Player.velocity.Y *= 0.01f;
                    FallTimer--;
                }
            }
            if (HallowMode)
            {
                if (Player.lifeRegen > 0)
                {
                    Player.lifeRegen = 0;
                }
                Player.lifeRegenTime = 0;
                Player.lifeRegenCount = 0;
                if (Player.manaRegen > 0)
                {
                    Player.manaRegen = 0;
                }
                Player.manaRegenCount = 0;
                Player.manaRegenBuff = false;
                Player.manaRegenBonus = 0;
                Player.manaRegenDelay = 10;
                Player.maxRunSpeed = 4.2f;
                Player.runAcceleration = 1f;
                Player.runSlowdown = 0.6f;
                Player.jumpSpeedBoost = 2f;
                Player.noFallDmg = true;
            }
        }

        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref NPC.HitModifiers modifiers)
        {
            base.ModifyHitNPCWithProj(proj, target, ref modifiers);
            if (HolyFlameCrown && proj.type != ModContent.ProjectileType<Projectiles.Misc.HolyFlame>() && Main.rand.NextBool(10))
            {
                Vector2 vector2 = new(0, 0);
                Projectile.NewProjectile(proj.GetSource_OnHit(target), target.position,
                    vector2, ModContent.ProjectileType<Projectiles.Misc.HolyFlame>(), proj.damage / 2,
                    0, proj.whoAmI);
            }
        }

        public override void ModifyHitNPCWithItem(Item item, NPC target, ref NPC.HitModifiers modifiers)
        {
            base.ModifyHitNPCWithItem(item, target, ref modifiers);

            if (HolyFlameCrown && Main.rand.NextBool(10))
            {
                Vector2 vector2 = new(0, 0);
                Projectile.NewProjectile(item.GetSource_OnHit(target), target.position,
                    vector2, ModContent.ProjectileType<Projectiles.Misc.HolyFlame>(), item.damage / 2, 0,
                    item.whoAmI);
            }
        }

        public override void OnHitByProjectile(Projectile proj, Player.HurtInfo hurtInfo)
        {
            if (MagicBoneShield)
            {
                proj.friendly = true;
                proj.hostile = false;
                proj.damage = hurtInfo.Damage / 2;
                NPC target = null;
                float dismax = 3200;
                foreach (NPC npc in Main.npc)
                {
                    if (npc.active && !npc.friendly)
                    {
                        float dis = Vector2.Distance(npc.Center, proj.Center);
                        if (dis < dismax)
                        {
                            dismax = dis;
                            target = npc;
                        }
                    }
                }
                if (target != null)
                {
                    Vector2 targetVec = target.Center - proj.Center;
                    targetVec.Normalize();
                    targetVec *= 20f;
                    proj.velocity = targetVec;
                }
            }
            base.OnHitByProjectile(proj, hurtInfo);
        }
    }
}