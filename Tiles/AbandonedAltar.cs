using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Microsoft.Xna.Framework;

namespace OdeMod.Tiles
{
    internal class AbandonedAltar : ModTile, IOdeTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileID.Sets.FramesOnKillWall[Type] = false;
            //3x2
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
            TileObjectData.addTile(Type);
            AddMapEntry(new Color(247, 199, 224));
            DustType = 7;
        }
        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            Player pls = Main.LocalPlayer;
            int bosstype = ModContent.NPCType<NPCs.Boss.MiracleRecorder.MiracleRecorder>();
            NPC.SpawnOnPlayer(pls.whoAmI, bosstype);
            base.KillTile(i, j, ref fail, ref effectOnly, ref noItem);
        }
    }
}
