using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Effects;
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
        public int shakeInt = 0;//ÕðÆÁ
        private int shaketick = 0;
        public int fall = 0;
        public bool OnHollowKnightItemUsing = false;
        protected int fallTimer = 0;
        public bool HollowKnightMovement = false;
        public int hallow = 0;
        public int rolling = 0;
        public override void SaveData(TagCompound tag)
        {
            tag.Add("Carapace", Carapace);
            tag.Add("Carapace_Open", Carapace_Open);
            tag.Add("HallowMode", HallowMode);
        }
        public class Ssd
        {

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
    }
}