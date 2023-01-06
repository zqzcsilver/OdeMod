using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Microsoft.Xna.Framework;
using OdeMod.Buffs.Foods;
using Terraria.ID;
using Terraria.Audio;

namespace OdeMod.Players
{
    /// <summary>
    /// 只是测试用，防止出现之前的意外，之后会合并入ODEPlayer中去。
    /// </summary>
    internal class OdeAddPlayer : ModPlayer, IOdePlayer
    {
        //诡灵系列
        //麻了 抛弃中式恐怖，那玩意一个是现实中都不敢整那么多活，别玩个游戏给自己整不吉利了（x
        //另外在开启时选择jumpscare的出现程度，虽说不怎么吓人（
        //我为什么每次都要在晚上写这些东西 捏马 人物理上已经麻了
        /// <summary>
        /// 诡灵模式的开关
        /// </summary>
        public bool SpookyMode = false;
        /// <summary>
        /// 彩蛋系列 黑山羊幼崽
        /// </summary>
        public bool DarkYoung = false;
        /// <summary>
        /// 诡灵的等级，随着游戏的进展会提升诡异程度，触发更高等级/更多诡异的事件。
        /// </summary>
        public int SpookyLevel = 0;

        //食物系列
        /// <summary>
        /// 饱腹
        /// </summary>
        public int Satiety;
        /// <summary>
        /// 吃坏食物的时候，会增加这个数值
        /// </summary>
        public int FoodPutrefaction;
        /// <summary>
        /// 食物多样性
        /// </summary>
        public int FoodVariety;
        /// <summary>
        /// 过去7餐吃了什么
        /// </summary>
        public string[] Foodtype = new string[7];
        public override void SaveData(TagCompound tag)
        {
            tag.Add("Satiety", Satiety);
            tag.Add("FoodVariety", FoodVariety);
            for (int i = 0; i < Foodtype.Length; i++)
                tag.Add("Food_type"+i, Foodtype[i]);
            tag.Add("FoodPutrefaction", FoodPutrefaction);
            tag.Add("SpookyMode", SpookyMode);
        }
        public override void LoadData(TagCompound tag)
        {
            //食物
            Satiety = tag.Get<int>("Satiety");
            FoodVariety = tag.Get<int>("FoodVariety");
            for (int i = 0; i < Foodtype.Length; i++)
                Foodtype[i] = tag.Get<string>("Food_type" + i);
            FoodPutrefaction = tag.Get<int>("FoodPutrefaction");
            //诡灵
            SpookyMode = tag.Get<bool>("SpookyMode");
        }
        public bool Food_Variety_Test(Item item)
        {
            for (int i = 0; i < Foodtype.Length; i++)
            {
                if (Foodtype[i] == null || Foodtype[i] == "")
                {
                    Foodtype[i] = item.Name;
                    break;
                }
            }
            return true;
        }
        public override void UpdateLifeRegen()
        {
            if (Satiety >= 100 || !Player.HasBuff(BuffID.WellFed))
            {
                Player.AddBuff(BuffID.WellFed, 1800);
            }
            base.UpdateLifeRegen();
            if (Main.dayTime)
            {

            }
        }
        public override void UpdateBadLifeRegen()
        {
            if (Player.HasBuff(ModContent.BuffType<Addiction>()))
            {
                if (Main.rand.NextBool(200))
                {
                    Player.AddBuff(BuffID.Confused, 120);
                }
                Player.GetDamage<GenericDamageClass>() += 0.3f;
                Player.statDefense /= 2;
            }
            base.UpdateBadLifeRegen();
        }
        //实验区（测试区 之后会不断改）
        /// <summary>
        /// 触发一些灵异事件
        /// </summary>
        /// <param name="level">灵异事件的等级</param>
        /// <param name="strangeStyle">指定具体的灵异事件</param>
        public void StrangeEvent(int level, int strangeStyle,Player player)
        {
            switch (strangeStyle)
            {
                case 0:
                    Main.NewText("Error");
                    break;
                case 1:
                    SoundEngine.PlaySound(SoundID.Zombie101, player.Center);
                    Main.NewText("Test");
                    break;
                default:
                    break;
            }
        }
        public void StrangeEvent(int level)
        {

        }
        public void SaveSound(Player player)
        {
            SoundEngine.PlaySound(SoundID.Zombie101, player.Center);
        }
        
    }
}
