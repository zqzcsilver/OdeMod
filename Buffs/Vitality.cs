using Terraria.DataStructures;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Buffs
{
    internal class Vitality : ModBuff
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults(); 
            //DisplayName.SetDefault("元气");
            //Description.SetDefault("免疫寒冷，大大增加生命回复速度。");
            Main.debuff[Type] = false;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            if (player.HasBuff(BuffID.Frostburn))
            {
                player.ClearBuff(BuffID.Frostburn);
            }
            player.lifeRegen += 10;
            base.Update(player, ref buffIndex);
        }
    }
}
