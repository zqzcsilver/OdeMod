using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace OdeMod.Tiles.RiftValley
{
    internal class DisasterSilverOre : ModTile
	{
		public override void SetStaticDefaults()
		{
			TileID.Sets.Ore[Type] = true;
			Main.tileSpelunker[Type] = true; 
			Main.tileOreFinderPriority[Type] = 410; 
			Main.tileShine2[Type] = true;
			Main.tileShine[Type] = 975; 
			Main.tileMergeDirt[Type] = true;
			Main.tileSolid[Type] = true;
			Main.tileBlockLight[Type] = true;

			ModTranslation name = CreateMapEntryName();
			name.SetDefault("DisasterSilverOre");
			AddMapEntry(new Color(152, 171, 198), name);

			DustType = 84;
			ItemDrop = ModContent.ItemType<Items.Series.RiftValley.DisasterSilverOre>();
			HitSound = SoundID.Tink;
			MineResist = 10f;
			MinPick = 114514;
		}
	}
}
