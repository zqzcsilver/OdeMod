using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace OdeMod.Tiles.RiftValley
{
    internal class RottenMeatcs : ModTile, IRiftValley
	{
		public override void SetStaticDefaults()
		{
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileBlockLight[Type] = true;
			ItemDrop = ModContent.ItemType<Items.Series.RiftValley.RottenMeatcs>();
			AddMapEntry(new Color(0, 0, 0));
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}
	}
}
