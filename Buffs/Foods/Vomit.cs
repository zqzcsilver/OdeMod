using Terraria.DataStructures;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using OdeMod.Players;
using System.Collections.Generic;

namespace OdeMod.Buffs.Foods
{
    internal class Vomit : ModBuff
    {
        public override string Texture => "OdeMod/Buffs/Foods/Inappetence";

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            base.Update(player, ref buffIndex);
        }
    }
}