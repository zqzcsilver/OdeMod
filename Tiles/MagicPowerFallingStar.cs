using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace OdeMod.Tiles
{
    /// <summary>
    /// 因为太好看就提前做出来铺路了
    /// </summary>
    internal class MagicPowerFallingStar : ModTile,IOdeTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            ItemDrop = ModContent.ItemType<Items.Misc.MagicPowerFallingStar>();
            Main.tileShine2[Type] = true;
            Main.tileShine[Type] = 975;
            AddMapEntry(new Color(255, 168, 183));
        }
        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
    }
}
