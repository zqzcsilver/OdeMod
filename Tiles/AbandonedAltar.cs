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
            MinPick = 200;//神圣稿
            MineResist = 4f;
        }
        //我是傻逼 一开始写killtile里了 敲一次召唤一次
        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Main.NewText("这里已经足够衰落了......不需要你再来破坏！",Color.Pink);//原本是右键召唤 后来看了下这里说的破坏就先改成挖掘了 后面要改把这段注释下面那段启用就行
            Player pls = Main.LocalPlayer;
            int bosstype = ModContent.NPCType<NPCs.Boss.MiracleRecorder.MiracleRecorder>();
            NPC.SpawnOnPlayer(pls.whoAmI, bosstype);
            base.KillMultiTile(i, j, frameX, frameY);
        }
        //下面是原本的方式
        //public override bool RightClick(int i, int j)
        //{
        //    int bosstype = ModContent.NPCType<NPCs.Boss.MiracleRecorder.MiracleRecorder>();
        //    foreach (NPC npc in Main.npc)
        //    {
        //        if (npc.type == bosstype)
        //        {
        //            Main.NewText("已存在辉煌纪录者。", Color.Pink);
        //            return true;
        //        }
        //    }
        //    Main.NewText("这里已经足够衰落了......不需要你再来破坏！", Color.Pink);
        //    Player pls = Main.LocalPlayer;
        //    NPC.SpawnOnPlayer(pls.whoAmI, bosstype);
        //    return true;
        //}
        public override bool CanKillTile(int i, int j, ref bool blockDamaged)
        {
            return true;
        }
    }
}
