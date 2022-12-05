using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using ReLogic.Content;

using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace OdeMod.Tiles.RiftValley.SoulCemetery
{
    internal class BoneOak : ModTree, ISoulCemetery
    {
        public override TreePaintingSettings TreeShaderSettings => new TreePaintingSettings
        {
            UseSpecialGroups = true,
            SpecialGroupMinimalHueValue = 11f / 72f,
            SpecialGroupMaximumHueValue = 0.25f,
            SpecialGroupMinimumSaturationValue = 0.88f,
            SpecialGroupMaximumSaturationValue = 1f
        };

        public override void SetStaticDefaults()
        {
            // Makes Example Tree grow on ExampleBlock
            GrowsOnTileId = new int[1] { ModContent.TileType<SoulCongealingSoil>() };
        }

        // This is the primary texture for the trunk. Branches and foliage use different settings.
        public override Asset<Texture2D> GetTexture()
        {
            return ModContent.Request<Texture2D>("OdeMod/Tiles/RiftValley/SoulCemetery/BoneOakTrunk");
        }
        public override int SaplingGrowthType(ref int style)
        {
            style = 0;
            return ModContent.TileType<Plant>();
        }
        // Branch Textures
        public override Asset<Texture2D> GetBranchTextures()
        {
            return ModContent.Request<Texture2D>("OdeMod/Tiles/RiftValley/SoulCemetery/BoneOakLeave");
        }

        // Top Textures
        public override Asset<Texture2D> GetTopTextures()
        {
            return ModContent.Request<Texture2D>("ExampleMod/Content/Tiles/Plants/BoneOakCrown");
        }

        public override int DropWood()
        {
            return ModContent.ItemType<Items.Series.SoulCemetery.BoneOak>();
        }
        public override bool Shake(int x, int y, ref bool createLeaves)
        {
            Item.NewItem(WorldGen.GetItemSource_FromTreeShake(x, y), new Vector2(x, y) * 16, ModContent.ItemType<Items.Series.SoulCemetery.BoneOak>());
            return false;
        }

        public override void SetTreeFoliageSettings(Tile tile, ref int xoffset, ref int treeFrame, ref int floorY, ref int topTextureFrameWidth, ref int topTextureFrameHeight)
        {
            //throw new System.NotImplementedException();
        }
    }
}
