using Terraria.DataStructures;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using OdeMod.Players;
using System.Collections.Generic;

namespace OdeMod.Buffs.Foods
{
    internal class Inappetence : ModBuff
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            //DisplayName.SetDefault("厌食");
            //Description.SetDefault("你厌恶");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {

            base.Update(player, ref buffIndex);
        }
    }
}
