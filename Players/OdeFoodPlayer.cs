using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Microsoft.Xna.Framework;
namespace OdeMod.Players
{
    /// <summary>
    /// 只是测试用，之后会合并入ODEPlayer中去。
    /// </summary>
    internal class OdeFoodPlayer : ModPlayer, IOdePlayer
    {
        public int Satiety;
        public int Food_Variety;
        public string[] Food_type = new string[7];
        public override void SaveData(TagCompound tag)
        {
            tag.Add("Satiety", Satiety);
            tag.Add("Food_Variety", Food_Variety);
            for (int i = 0; i < Food_type.Length; i++)
                tag.Add("Food_type"+i, Food_type[i]);
        }
        public override void LoadData(TagCompound tag)
        {
            Satiety = tag.Get<int>("Satiety");
            Food_Variety = tag.Get<int>("Food_Variety");
            for (int i = 0; i < Food_type.Length; i++)
                Food_type[i] = tag.Get<string>("Food_type" + i);
        }
        public bool Food_Variety_Test(Item item)
        {
            for (int i = 0; i < Food_type.Length; i++)
            {
                if (Food_type[i] == null || Food_type[i] == "")
                {
                    Food_type[i] = item.Name;
                    Main.NewText("第" + i + "次吃了" + Food_type[i], Color.Red);
                    break;
                }
            }
            return true;
        }
    }
}
