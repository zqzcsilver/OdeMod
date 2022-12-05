using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace OdeMod.Players
{
    internal class EggPlayer : ModPlayer, IOdePlayer
    {
        public bool WanAngery = false;
        public int Wan = 0;
        public override void SaveData(TagCompound tag)
        {
            base.SaveData(tag);
            tag.Add("WanAngery", WanAngery);
            tag.Add("Wan", Wan);
        }
        public override void LoadData(TagCompound tag)
        {
            base.LoadData(tag);
            WanAngery = tag.Get<bool>("WanAngery");
            Wan = tag.Get<int>("Wan");
        }
        public override void PostUpdate()
        {
            base.PostUpdate();
            if (!WanAngery)
            {
                WanAngery = Wan > 50;
            }
        }
    }
}
