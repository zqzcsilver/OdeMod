using Microsoft.Xna.Framework;
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
                Player.KillMe(PlayerDeathReason.ByCustomReason(Player.name + "被吸干了脑髓"), Player.statLife, 0);
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
        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {

            return base.PreKill(damage, hitDirection, pvp, ref playSound, ref genGore, ref damageSource);
        }
    }
    internal class EggHelp : ModCommand
    {
        public override CommandType Type
        {
            get { return CommandType.Chat; }
        }
        public override string Command
        {
            get { return "EggHelp"; }
        }

        public override string Usage
        {
            get { return "/EggHelp"; }
        }
        public override string Description
        {
            get { return "给予一些彩蛋提示"; }
        }
        public override void Action(CommandCaller caller, string input, string[] args)
        {
            Player player = caller.Player;
            switch (Main.rand.Next(1,3))
            {
                case 1:
                    Main.NewText("混沌法杖隐藏一些彩蛋");
                    break;
            }
        }
    }
}