using Microsoft.Xna.Framework;

using OdeMod.Projectiles.Series.Items.Frosted;

using System;
using System.Collections.Generic;

using Terraria;
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
            if(HollowKnightMovement)
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

        public override void ResetEffects()
        {
            if(fall==1)
            {
                fallTimer = 3;
                Player.maxFallSpeed = 25f;
                Player.velocity.Y += 25f;
                Player.immune = true;
                Player.immuneTime = 10;
            }
            if(fall==0)
            {
                if(fallTimer>0)
                {

                    Player.maxFallSpeed = 10.01f;
                    Player.velocity.Y *= 0.01f;
                    fallTimer--;
                }
            }
        }
    }
}