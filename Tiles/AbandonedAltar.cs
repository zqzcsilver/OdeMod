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
        //我是傻逼 一开始写killtile里了 敲一次召唤一次
        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Player pls = Main.LocalPlayer;
            int bosstype = ModContent.NPCType<NPCs.Boss.MiracleRecorder.MiracleRecorder>();
            NPC.SpawnOnPlayer(pls.whoAmI, bosstype);
            base.KillMultiTile(i, j, frameX, frameY);
        }
        public override bool CanKillTile(int i, int j, ref bool blockDamaged)
        {
            return false;
        }
    }
}
