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
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 1;
        }
        public override void SetDefaults()
        {
            NPC.lifeMax = Life();
            NPC.damage = Damage();
            NPC.defense = 10;
            NPC.knockBackResist = 0f;
            NPC.width = 24;
            NPC.height = 24;
            NPC.aiStyle = 14;
            NPC.boss = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPCID.Sets.TrailingMode[NPC.type] = 0;
            NPCID.Sets.TrailCacheLength[NPC.type] = 8;
        }
       public int Life()
        {
            if (Main.masterMode) return 2000;
            else if (Main.expertMode) return 2500;
            else return 3500;
        }
        public int Damage()
        {
            if (Main.masterMode) return 12;
            else if (Main.expertMode) return 15;
            else return 20;
        }
        public override void FindFrame(int frameHeight)
        {
            base.FindFrame(frameHeight);
        }
        public override void AI()
        {
            Player player = Main.player[NPC.target];
            NPC.ai[0]++;
            if (NPC.ai[0] % 60 == 0)
            {
                TeleportationAttack(player);
            }
            base.AI();
        }
        private int Tpnum = 0;
        Vector2[] TP = new Vector2[5];
        /// <summary>
        /// 传送点设置 清除
        /// </summary>
        private void TeleportantionPostion()
        {
            if (Tpnum >= 5)
            {
                NPC.position = TP[Main.rand.Next(5)];
                Tpnum = 0;
            }
            else
            {
                TP[Tpnum] = NPC.position;
                Main.NewText(Tpnum);
                Tpnum++;
            }
        }
        public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
        {
            base.ModifyHitByProjectile(projectile, ref modifiers);
        }
        int telnum =-3;
        private void TeleportationAttack(/*DemoState state, AddStatus addstate,*/ Player player)
        {
            Vector2 hittarget = player.position;
            if (telnum % 2 == 0)
            {
                NPC.position = hittarget + new Vector2(telnum + 1000, 0);
                NPC.velocity = new Vector2(-20, 0);
            }
            else
            {
                NPC.position = hittarget + new Vector2(telnum - 1000, 0);
                NPC.velocity = new Vector2(20, 0);
            }
            telnum++;
            if (telnum >= 4)
                telnum = 0;
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
            //5.5我本来想所有月相都设计一套 是不是很大胆（x 认清现实 满月新月单独做一个 剩下的正常AI就好
            switch (type)
            {
                case 0: //满月
                    multchance -= 0.3f;
                    break;
                case 4://新月
                    break;
                default:
                    break;
            }
            return true;
        }
        //public override string Texture => "OdeMod/Items/Misc/Wan";
        public bool TempItemChange(Player target, int BaseChance)
        {
            
            return BaseChance >= 0.5f;
        }
        
    }
}
