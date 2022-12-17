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
            ItemDrop = ModContent.ItemType<Items.Series.RiftValley.FantasyStone>();
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("FantasyStone");
            AddMapEntry(new Color(255, 130, 189),name);
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
    }
}
