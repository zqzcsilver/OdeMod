using Terraria.DataStructures;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Buffs
{
    internal class NaturalPower : ModBuff
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            //DisplayName.SetDefault("自然力量");
            //Description.SetDefault("被自然所敌视");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }
        public int Inc_Life = 0;
        public override void Update(NPC npc, ref int buffIndex)
        {
            //这块数值之后需要平衡
            if (NPC.downedSlimeKing)
            {
                Inc_Life = 100;
            }
            if (NPC.downedMoonlord)
            {
                Inc_Life = 100;
            }
            if (!npc.friendly)
            {
                npc.lifeRegen -= Inc_Life;
            }
            base.Update(npc, ref buffIndex);
        }
        public override void Update(Player player, ref int buffIndex)
        {
            base.Update(player, ref buffIndex);
        }
    }
}
