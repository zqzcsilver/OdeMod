using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

using OdeMod.CardMode.Rooms;

namespace OdeMod.CardMode.PublicComponents.LogicComponents
{
    /*
     * 这是一个移动模块，它应该被装载在拥有在地图中的移动能力的实体之上。
     * 例如玩家、AI玩家，或者一些特别的东西。这个模块不会自己行走，需要
     * 一个控制模块来控制其行走目的地。你可以使用抽象的方式来使用此模块，
     * 因为房间与地图需要绑定的并非装载了此模块的实体，而是此模块的实例。
     */

    internal class MoveComponent : Component
    {
        /// <summary>
        /// 绑定的地图
        /// </summary>
        public Map Map;

        private Point _position;

        /// <summary>
        /// 当前位置
        /// </summary>
        public Point Position
        {
            get => _position;
            set
            {
                if (_position != value)
                    _position = Map.RectifyPosition(value);
            }
        }

        /// <summary>
        /// 可以进入的房间
        /// </summary>
        public List<RoomBase> CanTakeInRooms;

        /// <summary>
        /// 无价值房间（显示为灰的房间）
        /// </summary>
        public List<RoomBase> NoValueRooms;

        /// <summary>
        /// 现在所在的房间
        /// </summary>
        public RoomBase NowRoom;

        /// <summary>
        /// 目前是否在房间内
        /// </summary>
        public bool IsInRoom => NowRoom != null;

        /// <summary>
        /// 出生位置
        /// </summary>
        public Point SpawnPosition;

        /// <summary>
        /// 出生房间
        /// </summary>
        public RoomBase SpawnRoom => Map.GetRoom(SpawnPosition);

        /// <summary>
        /// 目前是否能移动
        /// </summary>
        public bool CanMove = true;

        public MoveComponent()
        {
            CanTakeInRooms = new List<RoomBase>();
            NoValueRooms = new List<RoomBase>();
        }

        public virtual bool CanTakeInRoom(RoomBase room)
        {
            return CanMove && CanTakeInRooms.Contains(room) && room.CanTakeIn(this);
        }

        public virtual bool PreTakeInRoom(RoomBase room)
        {
            return room.PreTakeIn(this);
        }

        public virtual void TakeInRoom(RoomBase room)
        {
            NowRoom = room;
            Position = room.Position;
            room.TakeIn(this);
        }

        public virtual bool PreTakeOutRoom(RoomBase room)
        {
            return room.PreTakeOut(this);
        }

        public virtual void TakeOutRoom(RoomBase room)
        {
            room.TakeOut(this);
            NowRoom = null;
            var caar = Map.GetCAARooms(room);
            caar.Remove(room);
            CanTakeInRooms.AddRange(caar);
        }

        public override IComponent Clone(Entity cloneEntity)
        {
            throw new NotImplementedException();
        }
    }
}