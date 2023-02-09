using System.Collections.Generic;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OdeMod.CardMode.Rooms;
using OdeMod.Utils;
using OdeMod.Utils.Expends;

using Terraria;

namespace OdeMod.CardMode
{
    internal class Map
    {
        private Point _mapSize;
        private Node beginNode, endNode;
        public bool RefreshDraw = false;
        public RoomBuilder RoomBuilder { get; private set; }

        public Point MapSize
        {
            get { return _mapSize; }
            set
            {
                _mapSize = value;
                _map = new Node[_mapSize.X, _mapSize.Y];
            }
        }

        private Node[,] _map;

        public Map() : this(Point.Zero)
        {
        }

        public Map(int width, int height) : this(new Point(width, height))
        {
        }

        public Map(Point mapSize)
        {
            MapSize = mapSize;
            RoomBuilder = new RoomBuilder();
        }

        ~Map()
        {
            Destroy();
        }

        public void Reset()
        {
            for (int i = 0; i < MapSize.X; i++)
            {
                for (int j = 0; j < MapSize.Y; j++)
                {
                    _map[i, j] = null;
                }
            }
        }

        public void Build()
        {
            if (MapSize == Point.Zero) return;

            Reset();
            RoomBuilder.Load();

            List<Node> nodes = new List<Node>(),
                endPoints = new List<Node>();
            if (Main.rand.NextBool())
            {
                int x = Main.rand.Next(MapSize.X), y = Main.rand.NextBool() ? 0 : MapSize.Y - 1;
                Node node = new Node(RoomBuilder.GetCanBuildRoom(new Point(x, y), true, false, false));
                node.Position = new Point(x, y);
                node.Room.Build();
                nodes.Add(node);
                _map[x, y] = node;
            }
            else
            {
                int x = Main.rand.NextBool() ? 0 : MapSize.X - 1, y = Main.rand.Next(MapSize.Y);
                Node node = new Node(RoomBuilder.GetCanBuildRoom(new Point(x, y), true, false, false));
                node.Position = new Point(x, y);
                node.Room.Build();
                nodes.Add(node);
                _map[x, y] = node;
            }
            beginNode = nodes[0];
            while (nodes.Count > 0)
            {
                Node node = Main.rand.Next(nodes);
                var dir = getNextNode(node.Position);
                if (dir.Equals(Point.Zero))
                {
                    node.Room.Destroy();
                    node.Room = RoomBuilder.GetCanBuildRoom(node.Position, true, false, true);
                    node.Room.Build();
                    nodes.Remove(node);
                    endPoints.Add(node);
                    continue;
                }

                var pos = node.Position + dir.Divide(2);
                Node n = new Node(RoomBuilder.GetCanBuildRoom(pos, false, false, false));
                n.Parent = node;
                n.Position = pos;
                n.Room.Build();
                _map[pos.X, pos.Y] = n;

                pos = node.Position + dir;
                Node n1 = new Node(RoomBuilder.GetCanBuildRoom(pos, false, false, false));
                n1.Parent = n;
                n1.Position = pos;
                n1.Room.Build();
                nodes.Add(n1);
                _map[pos.X, pos.Y] = n1;
            }
            endNode = Main.rand.Next(endPoints);
            endNode.Room = RoomBuilder.GetCanBuildRoom(endNode.Position, false, true, true);
            endNode.Room.Build();

            Node nd;
            for (int i = 0; i < MapSize.X; i++)
            {
                for (int j = 0; j < MapSize.Y; j++)
                {
                    nd = _map[i, j];
                    if (nd != null)
                    {
                        if (nd.Room.InMapCenter == Vector2.Zero)
                        {
                            Vector2 randomOffset = new Vector2(50f * (Main.rand.NextFloat() - 0.5f), 50f * (Main.rand.NextFloat() - 0.5f));
                            nd.Room.InMapCenter = randomOffset + new Vector2(i * 90f, j * 90f);
                        }
                    }
                }
            }

            RefreshDraw = true;
        }

        private Point getNextNode(Point pos) => getNextNode(pos.X, pos.Y);

        private Point getNextNode(int x, int y)
        {
            List<Point> dir = new List<Point>();
            if (x > 2 && _map[x - 2, y] == null)
                dir.Add(new Point(-2, 0));
            if (y > 2 && _map[x, y - 2] == null)
                dir.Add(new Point(0, -2));
            if (x + 2 < MapSize.X && _map[x + 2, y] == null)
                dir.Add(new Point(2, 0));
            if (y + 2 < MapSize.Y && _map[x, y + 2] == null)
                dir.Add(new Point(0, 2));
            if (dir.Count == 0)
                return Point.Zero;
            return Main.rand.Next(dir);
        }

        public void Destroy()
        {
            Reset();
        }

        public void Update()
        {
        }

        public void Draw(SpriteBatch sb)
        {
            //Vector2 drawStartPos = new Vector2(100f, 200f), drawSize = MapSize.ToVector2();
            //Rectangle drawRect = new Rectangle(0, 0, (int)drawSize.X, (int)drawSize.Y);
            //for (int i = 0; i < MapSize.X; i++)
            //{
            //    for (int j = 0; j < MapSize.Y; j++)
            //    {
            //        if (_map[i, j] != null)
            //        {
            //            sb.Draw(TextureAssets.MagicPixel.Value,
            //                new Rectangle((int)drawStartPos.X + i * 4, (int)drawStartPos.Y + j * 4, 2, 2),
            //                Color.Red);
            //            //if (_map[i, j].Parent != null)
            //            //{
            //            //    Utils.DrawUtils.DrawLine(sb, drawStartPos + _map[i, j].Position.ToVector2() * 4f,
            //            //        drawStartPos + _map[i, j].Parent.Position.ToVector2() * 4f, new Vector2(2), Color.Blue);
            //            //}
            //        }
            //    }
            //}
            //if (beginNode != null)
            //    sb.Draw(TextureAssets.MagicPixel.Value,
            //                    new Rectangle((int)drawStartPos.X + beginNode.Position.X * 4,
            //                    (int)drawStartPos.Y + beginNode.Position.Y * 4, 2, 2),
            //                    Color.Blue);
            //if (endNode != null)
            //    sb.Draw(TextureAssets.MagicPixel.Value,
            //                    new Rectangle((int)drawStartPos.X + endNode.Position.X * 4,
            //                    (int)drawStartPos.Y + endNode.Position.Y * 4, 2, 2),
            //                    Color.Green);

            //Node b1 = endNode, b2 = b1.Parent;
            //while (b1 != null && b2 != null)
            //{
            //    Utils.DrawUtils.DrawLine(sb, drawStartPos + b1.Position.ToVector2() * 4f,
            //        drawStartPos + b2.Position.ToVector2() * 4f, new Vector2(2), Color.Blue);

            //    b1 = b1.Parent;
            //    b2 = b1.Parent;
            //}

            if (RefreshDraw)
            {
                Vector2 offset = new Vector2(50f);
                Node n;
                List<Vector2> points = new List<Vector2>();
                List<float> thicknesses = new List<float>();
                List<Color> colors = new List<Color>();

                DrawUtils.SetDrawRenderTarget(sb, spriteBatch =>
                {
                    Node n;
                    List<Vector2> points = new List<Vector2>();
                    List<float> thicknesses = new List<float>();
                    List<Color> colors = new List<Color>();
                    for (int i = 0; i < MapSize.X; i++)
                    {
                        for (int j = 0; j < MapSize.Y; j++)
                        {
                            n = _map[i, j];
                            if (n != null)
                            {
                                n.Room.DrawInMap(sb);
                                if (n.Parent != null && n.Parent.Room != null)
                                {
                                    var r = (n.Parent.Room.InMapCenter - n.Room.InMapCenter).SafeNormalize(Vector2.Zero);
                                    points.Add(n.Room.InMapCenter + r * n.Room.Icon.Size().X / 2);
                                    points.Add(n.Parent.Room.InMapCenter - r * n.Room.Icon.Size().X / 2);
                                    thicknesses.Add(2f);
                                    colors.Add(Color.White);
                                }
                            }
                        }
                    }
                    DrawUtils.DrawLines(sb, points, thicknesses, colors, 0, 1, new Vector2(MapSize.X * 90 + 100, MapSize.Y * 90 + 100));
                }, OdeMod.RenderTarget2DPool.PoolOther(MapSize.X * 90 + 100, MapSize.Y * 90 + 100, "Card System Room Map"),
                Main.screenTarget, Main.screenTargetSwap);

                RefreshDraw = false;
            }
            if (!File.Exists("./CardSystemMap.png"))
                using (var s = new FileStream("./CardSystemMap.png", FileMode.Create))
                    OdeMod.RenderTarget2DPool.PoolOther(MapSize.X * 90 + 100, MapSize.Y * 90 + 100, "Card System Room Map").
                        SaveAsPng(s, MapSize.X * 90 + 100, MapSize.Y * 90 + 100);

            //sb.Draw(OdeMod.RenderTarget2DPool.PoolOther(MapSize.X * 90 + 100, MapSize.Y * 90 + 100, "Card System Room Map"),
            //    new Vector2(100f, 50f), Color.White);
        }

        private class Node
        {
            public Point Position;
            public RoomBase Room;
            public Node Parent;

            public Node(RoomBase room)
            {
                Room = room;
            }
        }
    }
}