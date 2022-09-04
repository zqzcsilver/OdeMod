using Microsoft.Xna.Framework;

using System.Collections.Generic;

using Terraria;
using Terraria.ModLoader;

namespace OdeMod.Systems
{
    internal class AStarSystem : ModSystem, IOdeSystem
    {
        private struct BlockState
        {
            public float G = 0f, H = 0f, F = 0f;
            public readonly Point Position = Point.Zero;
            public BlockState(Point point)
            {
                Position = point;
            }
        }
        public bool[,] WorldBlockMap;
        public bool GetBlockState(Point p) => WorldBlockMap[p.X, p.Y];
        public void UpdateMap()
        {
            WorldBlockMap = new bool[Main.maxTilesX, Main.maxTilesY];
            int i, j;
            for (i = 0; i < Main.maxTilesX; i++)
            {
                for (j = 0; j < Main.maxTilesY; j++)
                {
                    WorldBlockMap[i, j] = Main.tile[i, j].HasTile;
                }
            }
        }
        public List<Point> FindRoute(Point start, Point end)
        {
            List<Point> route = new List<Point>();
            return route;
        }
    }
}
