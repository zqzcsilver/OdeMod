using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Microsoft.Xna.Framework;
using OdeMod.Buffs.Foods;
using Terraria.ID;

namespace OdeMod.Players
{
    /// <summary>
    /// 只是测试用，之后会合并入ODEPlayer中去。
    /// </summary>
    internal class OdeFoodPlayer : ModPlayer, IOdePlayer
    {
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
        }
        public override void LoadData(TagCompound tag)
        {
            Satiety = tag.Get<int>("Satiety");
            FoodVariety = tag.Get<int>("FoodVariety");
            for (int i = 0; i < Foodtype.Length; i++)
                Foodtype[i] = tag.Get<string>("Food_type" + i);
            FoodPutrefaction = tag.Get<int>("FoodPutrefaction");
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
    }
}
