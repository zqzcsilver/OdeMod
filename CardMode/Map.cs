using System;
using System.Collections.Generic;
using System.Diagnostics;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OdeMod.CardMode.Rooms;
using OdeMod.Utils.Expends;

using Terraria;
using Terraria.GameContent;

namespace OdeMod.CardMode
{
    internal class Map
    {
        private Point _mapSize;
        private Node beginNode, endNode;

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

        public Map()
        {
            MapSize = Point.Zero;
        }

        public Map(int width, int height)
        {
            MapSize = new Point(width, height);
        }

        public Map(Point mapSize)
        {
            MapSize = mapSize;
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
            List<Node> nodes = new List<Node>(),
                endPoints = new List<Node>();
            if (Main.rand.NextBool())
            {
                int x = Main.rand.Next(MapSize.X), y = Main.rand.NextBool() ? 0 : MapSize.Y - 1;
                Node node = new Node(null);
                node.Position = new Point(x, y);
                nodes.Add(node);
                _map[x, y] = node;
            }
            else
            {
                int x = Main.rand.NextBool() ? 0 : MapSize.X - 1, y = Main.rand.Next(MapSize.Y);
                Node node = new Node(null);
                node.Position = new Point(x, y);
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
                    nodes.Remove(node);
                    endPoints.Add(node);
                    continue;
                }

                var pos = node.Position + dir.Divide(2);
                Node n = new Node(null);
                n.Parent = node;
                n.Position = pos;
                _map[pos.X, pos.Y] = n;

                pos = node.Position + dir;
                Node n1 = new Node(null);
                n1.Parent = n;
                n1.Position = pos;
                nodes.Add(n1);
                _map[pos.X, pos.Y] = n1;
            }
            endNode = Main.rand.Next(endPoints);
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
            Vector2 drawStartPos = new Vector2(100f, 200f), drawSize = MapSize.ToVector2();
            Rectangle drawRect = new Rectangle(0, 0, (int)drawSize.X, (int)drawSize.Y);
            for (int i = 0; i < MapSize.X; i++)
            {
                for (int j = 0; j < MapSize.Y; j++)
                {
                    if (_map[i, j] != null)
                    {
                        sb.Draw(TextureAssets.MagicPixel.Value,
                            new Rectangle((int)drawStartPos.X + i * 4, (int)drawStartPos.Y + j * 4, 2, 2),
                            Color.Red);
                        //if (_map[i, j].Parent != null)
                        //{
                        //    Utils.DrawUtils.DrawLine(sb, drawStartPos + _map[i, j].Position.ToVector2() * 4f,
                        //        drawStartPos + _map[i, j].Parent.Position.ToVector2() * 4f, new Vector2(2), Color.Blue);
                        //}
                    }
                }
            }
            if (beginNode != null)
                sb.Draw(TextureAssets.MagicPixel.Value,
                                new Rectangle((int)drawStartPos.X + beginNode.Position.X * 4,
                                (int)drawStartPos.Y + beginNode.Position.Y * 4, 2, 2),
                                Color.Blue);
            if (endNode != null)
                sb.Draw(TextureAssets.MagicPixel.Value,
                                new Rectangle((int)drawStartPos.X + endNode.Position.X * 4,
                                (int)drawStartPos.Y + endNode.Position.Y * 4, 2, 2),
                                Color.Green);

            Node b1 = endNode, b2 = b1.Parent;
            while (b1 != null && b2 != null)
            {
                Utils.DrawUtils.DrawLine(sb, drawStartPos + b1.Position.ToVector2() * 4f,
                    drawStartPos + b2.Position.ToVector2() * 4f, new Vector2(2), Color.Blue);

                b1 = b1.Parent;
                b2 = b1.Parent;
            }
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