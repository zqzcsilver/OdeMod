using System.Collections.Generic;

using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ModLoader;

namespace OdeMod.Tiles.RiftValley
{
    internal class FantasyStone : ModTile, IRiftValley
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            AddMapEntry(new Color(255, 130, 189));
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }

        public override IEnumerable<Item> GetItemDrops(int i, int j)
        {
            List<Item> list = new List<Item>();
            Item item = new Item();
            item.SetDefaults(ModContent.ItemType<Items.Series.RiftValley.FantasyStone>());
            list.Add(item);
            return list;
        }
    }
}