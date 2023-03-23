using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;

using OdeMod.CardMode.PublicComponents.LogicComponents;
using OdeMod.CardMode.Rooms;
using OdeMod.Utils;
using OdeMod.Utils.Expends;

using Terraria;
using Terraria.ModLoader;

namespace OdeMod.CardMode
{
    internal class Map : ICardMode
    {
        private Point _mapSize;
        private Node beginNode, endNode;

        /// <summary>
        /// 绘制偏移值，通常是<see cref="Draw(SpriteBatch, Rectangle)"/>方法中Rectangle矩形参数的位置加屏幕偏移的位置。
        /// <br>[!]此字段仅为适配UI部件，故计算较为复杂！</br>
        /// </summary>
        public Vector2 DrawOffset = Vector2.Zero;

        private List<RoomBase> _needUpdateRooms;

        /// <summary>
        /// 绘制需要的，用于绘制的移动组件
        /// </summary>
        public MoveComponent BindingMoveComponent
        {
            get;
            set;
        }

        public Vector2 MaxSize
        {
            get;
            private set;
        }

        public RoomBuilder RoomBuilder { get; private set; }

        /// <summary>
        /// 焦点房间（鼠标焦点）
        /// </summary>
        public RoomBase FocusRoom { get; private set; }

        public List<Point> CanUseSpawnPosition { get; private set; }

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
            CanUseSpawnPosition = new List<Point>();
        }

        ~Map()
        {
            Destroy();
        }

        public void Reset()
        {
            CanUseSpawnPosition.Clear();
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

            Vector2 os;

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

                if (node.Parent != null && (node.Position - node.Parent.Position) != dir.Divide(2) &&
                    !CanUseSpawnPosition.Contains(node.Position))
                {
                    CanUseSpawnPosition.Add(node.Position);
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
            //beginNode.Room.CanTakeIn = true;
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

        public MoveComponent CreateMoveComponent()
        {
            MoveComponent op = new MoveComponent();
            op.Map = this;
            op.SpawnPosition = CanUseSpawnPosition[Main.rand.Next(CanUseSpawnPosition.Count)];
            CanUseSpawnPosition.Remove(op.SpawnPosition);
            op.CanTakeInRooms.Add(op.SpawnRoom);
            return op;
        }

        public void TakeOut(RoomBase roomBase)
        {
            //var r = getNode(new Point(roomBase.Position.X - 1, roomBase.Position.Y));
            //if (r != null && r.Parent != null && r.Parent.Position == roomBase.Position)
            //    r.Room.CanTakeIn = true;
            //r = getNode(new Point(roomBase.Position.X + 1, roomBase.Position.Y));
            //if (r != null && r.Parent != null && r.Parent.Position == roomBase.Position)
            //    r.Room.CanTakeIn = true;
            //r = getNode(new Point(roomBase.Position.X, roomBase.Position.Y - 1));
            //if (r != null && r.Parent != null && r.Parent.Position == roomBase.Position)
            //    r.Room.CanTakeIn = true;
            //r = getNode(new Point(roomBase.Position.X, roomBase.Position.Y + 1));
            //if (r != null && r.Parent != null && r.Parent.Position == roomBase.Position)
            //    r.Room.CanTakeIn = true;
        }

        public void TakeIn(RoomBase roomBase)
        {
        }

        public bool IsInMap(Point point)
        {
            return point.X < 0 || point.Y < 0 || point.X >= MapSize.X || point.Y >= MapSize.Y;
        }

        /// <summary>
        /// 如果输入坐标范围超出地图边界，纠正坐标为地图内
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public Point RectifyPosition(Point point)
        {
            if (point.X < 0)
                point.X = 0;
            if (point.Y < 0)
                point.Y = 0;
            if (point.X >= MapSize.X)
                point.X = MapSize.X;
            if (point.Y >= MapSize.Y)
                point.Y = MapSize.Y;
            return point;
        }

        private Node getNode(Point point)
        {
            if (IsInMap(point))
                return null;
            return _map[point.X, point.Y];
        }

        private List<Node> getCAANodes(Point pos) => getCAANodes(getNode(pos));

        private List<Node> getCAANodes(Node node)
        {
            if (node == null) return null;

            var op = new List<Node>();
            var r = getNode(new Point(node.Position.X - 1, node.Position.Y));
            if (r != null && r.Parent != null && (r.Parent == node || node.Parent == r) && r.Parent.Position == node.Position)
                op.Add(r);
            r = getNode(new Point(node.Position.X + 1, node.Position.Y));
            if (r != null && r.Parent != null && (r.Parent == node || node.Parent == r) && r.Parent.Position == node.Position)
                op.Add(r);
            r = getNode(new Point(node.Position.X, node.Position.Y - 1));
            if (r != null && r.Parent != null && (r.Parent == node || node.Parent == r) && r.Parent.Position == node.Position)
                op.Add(r);
            r = getNode(new Point(node.Position.X, node.Position.Y + 1));
            if (r != null && r.Parent != null && (r.Parent == node || node.Parent == r) && r.Parent.Position == node.Position)
                op.Add(r);
            return op;
        }

        public RoomBase GetRoom(Point point) => getNode(point).Room;

        /// <summary>
        /// 获取与输入房间相邻且相连的房间
        /// <br>[!]当输入为null或房间位置超出地图范围时返回null</br>
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        public List<RoomBase> GetCAARooms(RoomBase room) => GetCAARooms(room);

        /// <summary>
        /// 获取与输入位置相邻且相连的房间
        /// <br>[!]当输入位置超出地图范围时返回null</br></summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public List<RoomBase> GetCAARooms(Point pos)
        {
            var x = getCAANodes(pos);
            if (x == null) return null;

            var op = new List<RoomBase>();
            x.ForEach(r => op.Add(r.Room));
            return op;
        }

        public void Update(GameTime gt)
        {
            _needUpdateRooms.ForEach(x => x.Update(gt));
        }

        /// <summary>
        /// 绘制地图
        /// </summary>
        /// <param name="sb">画笔</param>
        /// <param name="rectangle">决定绘制内容的矩形。
        /// <br>位置范围为(0,0)-<see cref="MapSize"/></br>
        /// <br>大小范围为(0,0)-(<see cref="int.MaxValue"/>,<see cref="int.MaxValue"/>)</br>
        /// </param>
        public void Draw(SpriteBatch sb, Rectangle rectangle)
        {
            var originPos = rectangle.GetPosition() + DrawOffset;
            var totalRectangle = new Rectangle((int)originPos.X, (int)originPos.Y, rectangle.Width, rectangle.Height);

            Node n;
            List<Vector2> points = new List<Vector2>();
            List<float> thicknesses = new List<float>();
            List<Color> colors = new List<Color>();
            _needUpdateRooms.Clear();
            var mousePos = Main.MouseScreen.ToPoint();
            FocusRoom = null;
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
                            //FocusRoom = null;
                            if (hb.Contains(mousePos))
                            {
                                n.Room.InMapScale += (1.4f - n.Room.InMapScale) / 4f;
                                FocusRoom = n.Room;
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

        public void DrawBackground(SpriteBatch sb)
        {
            Texture2D texture = CardSystem.AssetManager.Request<Texture2D>("OdeMod/Images/Effects/Night");
            float scale = MathHelper.Max((float)Main.screenWidth / (float)texture.Width, (float)Main.screenHeight / (float)texture.Height);
            sb.Draw(texture, new Vector2(Main.screenWidth, Main.screenHeight) / 2f - texture.Size() / 2f * scale, null,
                Color.White, 0f, Vector2.Zero, scale, 0, 0);

            BindingMoveComponent?.NowRoom?.Draw(sb);
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