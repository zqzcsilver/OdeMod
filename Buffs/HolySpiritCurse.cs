using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Buffs
{
    internal class HolySpiritCurse : ModBuff, IBuffs
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = false;
        }
        public override void Update(NPC npc, ref int buffIndex)
        {

            base.Update(npc, ref buffIndex);
        }
    }
}