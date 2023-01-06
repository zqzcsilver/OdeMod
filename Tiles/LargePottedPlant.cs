using Microsoft.Xna.Framework;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace OdeMod.Tiles
{
    internal class LargePottedPlant : ModTile, IOdeTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;//非实体
            Main.tileSolidTop[Type] = true;//无法站
            Main.tileLavaDeath[Type] = true;//岩浆
            Main.tileFrameImportant[Type] = true;//帧
            MineResist = 3f;
            MinPick = 10;

            DustType = DustID.GreenMoss;
            ItemDrop = ModContent.ItemType<Items.Misc.LargePottedPlant>();
            //5x5
            TileObjectData.newTile.CopyFrom(TileObjectData.StyleDye);
            TileObjectData.newTile.Width = 5;
            TileObjectData.newTile.Height = 5;
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16, 16, 16 };
            TileObjectData.newTile.DrawYOffset = 2;
            TileObjectData.newTile.Origin = new Point16(0, 4);
            //TileObjectData.newTile.UsesCustomCanPlace = false;
            TileObjectData.addTile(Type);
            AddMapEntry(new Color(120, 85, 60));
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 32, 32, ModContent.ItemType<Items.Misc.LargePottedPlant>());
        }
    }
}