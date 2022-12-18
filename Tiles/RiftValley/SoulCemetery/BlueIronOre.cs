using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Tiles.RiftValley.SoulCemetery
{
    public class BlueIronOre : ModTile
    {
        public override void SetStaticDefaults()
        {
            TileID.Sets.Ore[Type] = true;
            Main.tileSpelunker[Type] = true; // The tile will be affected by spelunker highlighting
            Main.tileOreFinderPriority[Type] = 410; // Metal Detector value, see https://terraria.gamepedia.com/Metal_Detector
            Main.tileShine2[Type] = true; // Modifies the draw color slightly.
            Main.tileShine[Type] = 975; // How often tiny dust appear off this tile. Larger is less frequently
            Main.tileMergeDirt[Type] = true;
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;
            AddMapEntry(new Color(152, 171, 198));

            DustType = 84;
            ItemDrop = ModContent.ItemType<Items.Series.SoulCemetery.BlueIronOre>();
            HitSound = SoundID.Tink;
            MineResist = 10f;
            MinPick = 114514;
        }
    }
}
