using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace OdeMod.Players
{
    internal class EggPlayer : ModPlayer, IOdePlayer
    {
        public int GG = 0;
        public bool WanAngery = false;
        public int Wan = 0;
        public int BrainNum = 0;
        public bool Allotriophagy = false;
        public override void SaveData(TagCompound tag)
        {
            base.SaveData(tag);
            tag.Add("WanAngery", WanAngery);
            tag.Add("Wan", Wan);
            tag.Add("BrainNum", BrainNum);
            tag.Add("Allotriophagy", Allotriophagy);
        }
        public override void LoadData(TagCompound tag)
        {
            base.LoadData(tag);
            WanAngery = tag.Get<bool>("WanAngery");
            Wan = tag.Get<int>("Wan");
            BrainNum = tag.Get<int>("BrainNum");
            Allotriophagy = tag.Get<bool>("Allotriophagy");
        }
        public override void PostUpdate()
        {
            base.PostUpdate();
            if (!WanAngery)
            {
                WanAngery = Wan > 50;
            }
            if (GG >= 5)
            {
                Player.KillMe(PlayerDeathReason.ByCustomReason(Player.name + "被吸干了脑髓"),Player.statLife,0);
            }
            if (!Allotriophagy)
            {
                Allotriophagy = BrainNum > 99;
                
            }
        }
        public override void UpdateDead()
        {
            GG = 0;
            base.UpdateDead();
        }
    }
}
