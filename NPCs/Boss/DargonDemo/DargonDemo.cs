using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OdeMod.Players;
using OdeMod.ShaderDatas.ScreenShaderDatas;
using OdeMod.Utils;

using System;
using System.Collections.Generic;
using System.Drawing;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.ModLoader;
using System.IO;
using static Terraria.Utils;

namespace OdeMod.NPCs.Boss.DargonDemo
{
    internal class DargonDemo : ModNPC, IBoss
    {
        private enum DemoState
        {
            /// <summary>
            /// 出现
            /// </summary>
            Start,
            /// <summary>
            /// 移位
            /// </summary>
            Displacement,
        }
        private enum AddStatus
        {
            /// <summary>
            /// 沉睡
            /// </summary>
            Sleep,
            /// <summary>
            /// 激怒
            /// </summary>
            Anger,
            /// <summary>
            /// 戏谑
            /// </summary>
            Banter,
            /// <summary>
            /// 润
            /// </summary>
            Run
        }
        private Dictionary<DemoState, Action<Player>> _npcLogic;
        public override void SetDefaults()
        {
            NPC.lifeMax = 100;
            NPC.damage = 35;
            NPC.defense = 10;
            NPC.knockBackResist = 0f;
            NPC.width = 24;
            NPC.height = 24;
            NPC.aiStyle = -1;
            NPC.boss = true;
            NPC.alpha = 255;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPCID.Sets.TrailingMode[NPC.type] = 0;
            NPCID.Sets.TrailCacheLength[NPC.type] = 8;

        }
        public int BaseChange(int basestyle)
        {
            int baselife = 0;
            int basedamage = 0;
            int basedefense = 0;
            switch (Main.moonPhase)
            {
                case 0: return 1;
            }
            return 0;
        }
        public override void FindFrame(int frameHeight)
        {
            base.FindFrame(frameHeight);
        }
        public override void AI()
        {
            base.AI();
        }
        private void WeightCharge(DemoState state)
        {
        }
        //行为 按阶段排序
        private int Tpnum = 0;
        /// <summary>
        /// 传送点设置&&清除
        /// </summary>
        private void TeleportantionPostion()
        {
            if(Tpnum >= 5)
            {

            }
            else
            {
                Tpnum++;
            }
        }
        private void TeleportationAttack(DemoState state, AddStatus addstate)
        {
            float telinterval = (float)(Main.rand.Next(1, 6) * 0.2);
        }
        /// <summary>
        /// 根据月相进行一些攻击的权重调整及额外延展判定
        /// </summary>
        /// <returns>返回权重的调整 true表示进行额外延展判定 反之不进行</returns>
        public bool MoontypeEffect(int basechance)
        {
            int type = Main.moonPhase;
            float multchance = 1f;
            //GB 设定上没说幼龙来自那颗星球 月亮也是星星（ 所以就越接近满月提供的加成越少了
            //我对宇宙相关知识少得可怜淦
            //2.6 我是傻逼 我设计个这干嘛
            switch (type)
            {
                case 0: //满月
                    multchance -= 0.3f;
                    break;
                case 1:
                    multchance -= 0.3f;
                    break;
                case 2:
                    multchance += 0.1f;
                    break;
                case 3:

                    break;
                case 4:

                    break;
                case 5:
                    break;
                case 6:
                    break;
                case 7:
                    break;
            }
            return true;
        }
        public override string Texture => "OdeMod/Items/Misc/Wan";
        public bool TempItemChange(Player target, int BaseChance)
        {
            
            return BaseChance >= 0.5f;
        }
        
    }
}
