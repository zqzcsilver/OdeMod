using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Events
{
    internal class ChaosEvent : ModSystem
    {
        public static bool Chaos;//开启混沌之日
        /// <summary>
        /// 混沌碎片 混沌之蚀开启时 玩家头顶（或固定位置）会产生一个裂缝 玩家每击杀一个怪物 会产生一个混沌碎片汇入这个裂缝 裂缝也随之扩大
        /// 当达到一定得大小得时候 裂缝中产生Boss 击败boss后裂缝坍塌
        /// </summary>
        public static int ChaosPatch;
        /// <summary>
        /// 根据玩家所击杀boss和怪物得数量计分
        /// </summary>
        public static int ChaosScore;//混沌计分
        /// <summary>
        /// 混沌之蚀进度 设定是当玩家在此次混沌时 如果没有被中途打断（比如日蚀之类的） 那么将会持续增加进度条 当进度条达到一个数值（随着模式变化）
        /// 并且玩家没有能够达到一定分数，会对世界产生一些影响（可以通过某种道具清除）
        /// </summary>
        public static int ChaosNum;//混沌侵蚀进度
        /// <summary>
        /// 开启混沌之日
        /// </summary>
        public static void StarChaos()
        {
            Chaos = true;
            Main.eclipse = false;
            ChaosScore = 0;
            Main.NewText("混沌正在侵蚀", Color.Red);
        }
        /// <summary>
        /// 停止混沌之日
        /// </summary>
        public static void StopChaos()
        {
            Chaos = false;
            Main.NewText("混沌已经褪去", Color.Red);
            Main.NewText("部分生物已经无法回归现实", Color.DarkRed);
        }
    }
}
