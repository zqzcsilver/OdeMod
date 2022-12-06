using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.NPCs.Chaos
{
    //每次处于撕咬状态对玩家造成伤害，会吸取玩家最大生命值5，储存在体内。
    //最大储存20点，当死亡或者因任何因素消失时，会返还玩家最大生命值并且为其回复储存数量*10的血量
    //
    internal class ChaoticBite :ModNPC,IChaos
    {
        private enum NPC_State
        {
            Run,
            Bite
        }
        private NPC_State State
        {
            get { return (NPC_State)(int)NPC.ai[0]; }
            set { NPC.ai[0] = (int)value; }
        }
        private void SwitchTo(NPC_State state)
        {
            State = state;
        }
        //攻击张嘴时 防御减半
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 13;
        }
        public override void SetDefaults()
        {
            NPC.lifeMax = 200;
            NPC.damage = 40;
            NPC.defense = 20;
            NPC.knockBackResist = 0.1f;
            NPC.width = 64;
            NPC.height = 68;
            NPC.aiStyle = 3;
            NPC.boss = false;
            NPC.noGravity = true;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath6;
            NPC.value = Item.buyPrice(0, 0, 10, 0);
            NPC.noTileCollide = false;
            NPCID.Sets.TrailingMode[NPC.type] = 1;
        }
        public override void AI()
        {
            Player player = Main.player[NPC.target];
            switch (State)
            {
                case NPC_State.Run:
                    break;
                case NPC_State.Bite:
                    break;
            }
            base.AI();
        }
    }
}