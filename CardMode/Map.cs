using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OdeMod.CardMode.Rooms;
using OdeMod.Utils;
using OdeMod.Utils.Expends;

using Terraria;

namespace OdeMod.CardMode
{
    internal class Map : ICardMode
    {
        private Point _mapSize;
        private Node beginNode, endNode;
        public Vector2 DrawOffset = Vector2.Zero;
        private List<RoomBase> _needUpdateRooms;

        public Vector2 MaxSize
        {
            get;
            private set;
        }

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

        public RoomBase this[Point point]
        {
            get
            {
                if (point.X < 0 || point.Y < 0 || point.X >= MapSize.X || point.Y >= MapSize.Y || _map[point.X, point.Y] == null)
                    return null;
                return _map[point.X, point.Y].Room;
            }
        }

        public Map() : this(Point.Zero)
        {
        }

        public Map(int width, int height) : this(new Point(width, height))
        {
        }

        public Map(Point mapSize)
        {
            MapSize = mapSize;
            RoomBuilder = new RoomBuilder(this);
            _needUpdateRooms = new List<RoomBase>();
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

            Vector2 os = Vector2.Zero;

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

            os = beginNode.Room.Icon.Size();

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

                    os.X = Math.Max(os.X, node.Room.Icon.Size().X);
                    os.Y = Math.Max(os.X, node.Room.Icon.Size().Y);

                    continue;
                }

                var pos = node.Position + dir.Divide(2);
                Node n = new Node(RoomBuilder.GetCanBuildRoom(pos, false, false, false));
                n.Parent = node;
                n.Position = pos;
                n.Room.Build();
                _map[pos.X, pos.Y] = n;

                os.X = Math.Max(os.X, n.Room.Icon.Size().X);
                os.Y = Math.Max(os.X, n.Room.Icon.Size().Y);

                pos = node.Position + dir;
                Node n1 = new Node(RoomBuilder.GetCanBuildRoom(pos, false, false, false));
                n1.Parent = n;
                n1.Position = pos;
                n1.Room.Build();
                nodes.Add(n1);
                _map[pos.X, pos.Y] = n1;

                os.X = Math.Max(os.X, n1.Room.Icon.Size().X);
                os.Y = Math.Max(os.X, n1.Room.Icon.Size().Y);
            }
            endNode = Main.rand.Next(endPoints);
            endNode.Room = RoomBuilder.GetCanBuildRoom(endNode.Position, false, true, true);
            endNode.Room.Build();

            Node nd;
            Vector2 min = Vector2.Zero, max = Vector2.Zero;
            bool initMinAndMax = false;
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

                            if (!initMinAndMax)
                            {
                                min = nd.Room.InMapCenter - os / 2f;
                                max = nd.Room.InMapCenter + os / 2f;

                                initMinAndMax = true;
                            }
                            else
                            {
                                min.X = Math.Min(nd.Room.InMapCenter.X - os.X / 2f, min.X);
                                min.Y = Math.Min(nd.Room.InMapCenter.Y - os.Y / 2f, min.Y);

                                max.X = Math.Max(nd.Room.InMapCenter.X + os.X / 2f, max.X);
                                max.Y = Math.Max(nd.Room.InMapCenter.Y + os.Y / 2f, max.Y);
                            }
                        }
                    }
                }
            }
            //if (min.X == os.X / 2f) os.X = 0;
            //if (min.Y == os.Y / 2f) os.Y = 0;
            for (int i = 0; i < MapSize.X; i++)
            {
                for (int j = 0; j < MapSize.Y; j++)
                {
                    nd = _map[i, j];
                    if (nd != null)
                    {
                        nd.Room.InMapCenter -= min;
                    }
                }
            }

            MaxSize = new Vector2(max.X - min.X, max.Y - min.Y);
            beginNode.Room.CanTakeIn = true;
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

        public void TakeOut(RoomBase roomBase)
        {
            var r = getNode(new Point(roomBase.Position.X - 1, roomBase.Position.Y));
            if (r != null && r.Parent != null && r.Parent.Position == roomBase.Position)
                r.Room.CanTakeIn = true;
            r = getNode(new Point(roomBase.Position.X + 1, roomBase.Position.Y));
            if (r != null && r.Parent != null && r.Parent.Position == roomBase.Position)
                r.Room.CanTakeIn = true;
            r = getNode(new Point(roomBase.Position.X, roomBase.Position.Y - 1));
            if (r != null && r.Parent != null && r.Parent.Position == roomBase.Position)
                r.Room.CanTakeIn = true;
            r = getNode(new Point(roomBase.Position.X, roomBase.Position.Y + 1));
            if (r != null && r.Parent != null && r.Parent.Position == roomBase.Position)
                r.Room.CanTakeIn = true;
        }

        public void TakeIn(RoomBase roomBase)
        {
        }

        private Node getNode(Point point)
        {
            if (point.X < 0 || point.Y < 0 || point.X >= MapSize.X || point.Y >= MapSize.Y)
                return null;
            return _map[point.X, point.Y];
        }

        public void Update(GameTime gt)
        {
            _needUpdateRooms.ForEach(x => x.Update(gt));
        }

        /// <summary>
        /// 绘制地图
        /// </summary>
        /// <param name="sb">画笔</param>
        /// <param name="rectangle">决定绘制内容的矩形，从0,0开始</param>
        public void Draw(SpriteBatch sb, Rectangle rectangle)
        {
            var originPos = rectangle.GetPosition() + DrawOffset;
            var totalRectangle = new Rectangle((int)originPos.X, (int)originPos.Y, rectangle.Width, rectangle.Height);

            Node n;
            List<Vector2> points = new List<Vector2>();
            List<float> thicknesses = new List<float>();
            List<Color> colors = new List<Color>();
            _needUpdateRooms.Clear();
            for (int i = 0; i < MapSize.X; i++)
            {
                for (int j = 0; j < MapSize.Y; j++)
                {
                    n = _map[i, j];
                    if (n != null && n.Room != null)
                    {
                        if (n.Room.NeedAlwaysUpdate)
                            _needUpdateRooms.Add(n.Room);

                        if (rectangle.Intersects(n.Room.InMapHitBox))
                        {
                            Rectangle hb = n.Room.InMapHitBox;
                            hb.X += (int)(DrawOffset.X);
                            hb.Y += (int)(DrawOffset.Y);
                            hb = Rectangle.Intersect(totalRectangle, hb);
                            if (hb.Contains(Main.MouseScreen.ToPoint()))
                            {
                                n.Room.InMapScale += (1.4f - n.Room.InMapScale) / 4f;
                            }
                            else
                            {
                                n.Room.InMapScale += (1f - n.Room.InMapScale) / 4f;
                            }

                            n.Room.DrawInMap(sb, DrawOffset);

                            if (!n.Room.NeedAlwaysUpdate)
                                _needUpdateRooms.Add(n.Room);

                            if (n.Parent != null && n.Parent.Room != null)
                            {
                                var r = (n.Parent.Room.InMapCenter - n.Room.InMapCenter).SafeNormalize(Vector2.Zero);
                                points.Add(n.Room.InMapCenter + DrawOffset - originPos + r * n.Room.InMapHitBox.Size().X / 2f);
                                points.Add(n.Parent.Room.InMapCenter + DrawOffset - originPos - r * n.Parent.Room.InMapHitBox.Size().X / 2f);
                                thicknesses.Add(2f);
                                colors.Add(Color.White);

                                if (!n.Parent.Room.NeedAlwaysUpdate)
                                    _needUpdateRooms.Add(n.Parent.Room);
                            }
                        }
                    }
                }
            }

            DrawUtils.SetDrawRenderTarget(sb, spriteBatch =>
            {
                DrawUtils.DrawLines(sb, points, thicknesses, colors, 0, 1, rectangle.GetSize());
            }, OdeMod.RenderTarget2DPool.Pool(rectangle.GetIntSize()), Main.screenTarget, Main.screenTargetSwap);
            sb.Draw(OdeMod.RenderTarget2DPool.Pool(rectangle.GetIntSize()), originPos, Color.White);
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