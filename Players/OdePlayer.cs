using Microsoft.Xna.Framework;

using System;
using System.Collections.Generic;

using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace OdeMod.Players
{
    internal class OdePlayer : ModPlayer, IOdePlayer
    {
        public bool HallowMode = false;
        public int shakeInt = 0;
        private int shaketick = 0;
        public int fall = 0;
        public bool OnHollowKnightItemUsing = false;
        protected int fallTimer = 0;
        public bool HollowKnightMovement = false;
        public int hallow = 0;
        public int wan = 0;
        public bool wanman = false;
        public override void SaveData(TagCompound tag)
        {
            tag.Add("HallowMode", HallowMode);
        }
        public override void LoadData(TagCompound tag)
        {
            HallowMode = tag.Get<bool>("HallowMode");
        }
        public override void PreUpdateMovement()
        {
            if (HollowKnightMovement)
                Player.velocity *= 0f;
        }
        public override void PostUpdate()
        {
            if(wan >= 10)
            {
                wanman = true;
            }
            base.PostUpdate();
        }

        public override void ModifyScreenPosition()
        {
            if (!Main.gameMenu)
            {
                shaketick++;
                if (shakeInt >= 0 && shaketick >= 60)
                    shakeInt--;
                if (shakeInt > 50)
                    shakeInt = 50;
                if (shakeInt < 0)
                    shakeInt = 0;
                if (!Main.gamePaused && Main.hasFocus)
                {
                    Main.screenPosition += new Vector2(
                        shakeInt * Main.rand.NextFloatDirection() / 2f,
                        shakeInt * Main.rand.NextFloatDirection() / 2f);
                }
            }
            else
            {
                shakeInt = 0;
                shaketick = 0;
            }

            ////ΥπΖΑ
            //try
            //{
            //    float demo = 1 + Vector2.DistanceSquared(Main.player[Main.myPlayer].Center, player.Center) / 500000;
            //    OdePlayer.shakeInt = Math.Max(OdePlayer.shakeInt, (int)(15 / demo));
            //}
            //catch
            //{

            //}

        }
        public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if (HallowMode)
            {
                damage = 25 + Player.statDefense / 2;
            }
            return true;
        }
        public override void Hurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit)
        {
            if (HallowMode)
            {
                damage = 25;
                crit = false;
            }
        }
        public override void PostHurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit)
        {
            if (HallowMode)
            {
                Player.immune = true;
                Player.immuneTime = 60;
            }
        }
        public override void ResetEffects()
        {
            if (fall == 1)
            {
                fallTimer = 3;
                Player.maxFallSpeed = 25f;
                Player.velocity.Y += 25f;
                Player.immune = true;
                Player.immuneTime = 10;
            }
            if (fall == 0)
            {
                if (fallTimer > 0)
                {

                    Player.maxFallSpeed = 10.01f;
                    Player.velocity.Y *= 0.01f;
                    fallTimer--;
                }
            }
            if(HallowMode)
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
                Player.runSlowdown = 2f;
                Player.jumpSpeedBoost = 2.4f;
                Player.noFallDmg = true;
            }


        }
    }
}