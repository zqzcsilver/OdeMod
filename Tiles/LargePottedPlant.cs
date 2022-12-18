﻿using Microsoft.Xna.Framework;

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
            Main.tileFrameImportant[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileID.Sets.FramesOnKillWall[Type] = false;
            //5x5
            TileObjectData.newTile.CopyFrom(TileObjectData.Style5x4);
            TileObjectData.addTile(Type);
            AddMapEntry(new Color(120, 85, 60));
            DustType = 7;
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 32, 32, ModContent.ItemType<Items.Misc.LargePottedPlant>());
        }
    }
}