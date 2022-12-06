using Microsoft.Xna.Framework;
using OdeMod.Players;
using System.Collections.Generic;

using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;
namespace OdeMod.Systems
{
    internal class AStarSystem : ModSystem, IOdeSystem
    {
        public override void PreUpdateEntities()
        {
            base.PreUpdateEntities();
            if (!Filters.Scene["TemplateMod2:GBlur"].IsActive()&&false)
            {
                // 开启滤镜
                Filters.Scene.Activate("TemplateMod2:GBlur");
            }
            if(Filters.Scene["TemplateMod2:GBlur"].IsActive() && player.GetModPlayer<OdePlayer>().MiracleRecorderShader == 101)
            {
                Filters.Scene.Deactivate("TemplateMod2:GBlur");
            }
        }
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
