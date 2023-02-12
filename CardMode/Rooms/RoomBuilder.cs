using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ModLoader;

namespace OdeMod.CardMode.Rooms
{
    internal class RoomBuilder
    {
        private List<RoomBase> buildRoomPool;
        public Map Map { get; private set; }

        public RoomBuilder(Map map)
        {
            buildRoomPool = new List<RoomBase>();
            Map = map;
        }

        public void Load()
        {
            buildRoomPool.Clear();
            var types = Array.FindAll(Assembly.GetExecutingAssembly().GetTypes(), x =>
            {
                return typeof(RoomBase).IsAssignableFrom(x) && !x.IsAbstract && CheckRoomCanAutoLoad(x) &&
                Array.Find(x.GetConstructors(), y =>
                {
                    return y.IsPublic && y.GetParameters().Length == 0;
                }) != null;
            });
            foreach (var t in types)
            {
                var x = (RoomBase)Activator.CreateInstance(t);
                x.Map = Map;
                buildRoomPool.Add(x);
            }
        }

        public List<RoomBase> GetCanBuildRooms(Point position, bool isBegin, bool isEnd, bool isSilu)
        {
            List<RoomBase> op = new List<RoomBase>();
            foreach (var t in buildRoomPool)
            {
                t.Position = position;
                t.IsBegin = isBegin;
                t.IsEnd = isEnd;
                t.IsSilu = isSilu;
                if (t.PreBuild())
                    op.Add(t.Clone());
            }
            return op;
        }

        public RoomBase GetCanBuildRoom(Point position, bool isBegin, bool isEnd, bool isSilu)
        {
            var rooms = GetCanBuildRooms(position, isBegin, isEnd, isSilu);

            if (rooms.Count == 0) return null;

            List<(RoomBase, float)> roomWeightMap = new List<(RoomBase, float)>();
            float maxRoomWeight = 0f;
            foreach (var room in rooms)
            {
                maxRoomWeight += room.BuildWeight;
                roomWeightMap.Add((room, maxRoomWeight));
            }

            if (roomWeightMap.Count == 0)
                return null;
            if (roomWeightMap.Count == 1)
                return roomWeightMap[0].Item1;

            float randomRoomWright = Main.rand.NextFloat() * maxRoomWeight;
            int searchScope = roomWeightMap.Count;
            int index = searchScope / 2;
            while (true)
            {
                searchScope /= 2;
                if (roomWeightMap[index].Item2 < randomRoomWright)
                    index += searchScope;
                else
                {
                    if (searchScope == 1)
                        return roomWeightMap[index].Item1;
                    index -= searchScope;
                }
            }
        }

        public void AddRoomInBuildList(RoomBase room)
        {
            if (buildRoomPool.Contains(room))
                return;
            buildRoomPool.Add(room);
        }

        public void RemoveRoomInBuildList(RoomBase room)
        {
            if (buildRoomPool.Contains(room))
                return;
            buildRoomPool.Remove(room);
        }

        public void RemoveRoomsInBuildList<T>()
        {
            int i = 0;
            while (i < buildRoomPool.Count)
            {
                if (buildRoomPool[i] is T)
                    buildRoomPool.RemoveAt(i);
                else
                    i++;
            }
        }

        private static bool CheckRoomCanAutoLoad(Type roomType)
        {
            var x = (AutoloadAttribute)roomType.GetCustomAttributes(typeof(AutoloadAttribute)).First();
            return !(x == null || x.Value == false);
        }
    }
}