using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OdeMod.CardMode.PublicComponents.LogicComponents;

using Terraria;
using Terraria.ModLoader;

namespace OdeMod.CardMode.Rooms
{
    [Autoload(true)]
    internal abstract class RoomBase
    {
        public Map Map;
        public Point Position;
        public bool IsBegin;
        public bool IsEnd;
        public bool IsSilu;
        public Vector2 InMapCenter;
        public float InMapScale = 1f;
        private Dictionary<MoveComponent, int> _numberOfEntries;
        public virtual float BuildWeight => 1f;
        public virtual bool NeedAlwaysUpdate => false;

        public virtual Color MapColor =>
            (Map.BindingMoveComponent == null || GetNumberOfEntries(Map.BindingMoveComponent) > 0) ? Color.White * 0.6f :
            (CanTakeIn(Map.BindingMoveComponent) ? Color.Yellow : Color.White);

        public virtual Texture2D Icon => ModContent.Request<Texture2D>("OdeMod/Images/Card/Original/Room/EyeballIcon",
            ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;

        public virtual Rectangle InMapHitBox
        {
            get
            {
                Vector2 size = Icon.Size() * InMapScale;
                return new Rectangle((int)(InMapCenter.X - size.X / 2f), (int)(InMapCenter.Y - size.Y / 2f), (int)size.X, (int)size.Y);
            }
        }

        public RoomBase()
        {
            _numberOfEntries = new Dictionary<MoveComponent, int>();
        }

        public int GetNumberOfEntries(MoveComponent moveComponent)
        {
            if (!_numberOfEntries.ContainsKey(moveComponent))
                _numberOfEntries.Add(moveComponent, 0);
            return _numberOfEntries[moveComponent];
        }

        public virtual bool PreBuild()
        {
            return !IsBegin && !IsEnd && !IsSilu;
        }

        public virtual void Build()
        {
        }

        public virtual void Destroy()
        {
        }

        public bool CanTakeIn(MoveComponent moveComponent)
        {
            return moveComponent.CanTakeInRooms.Contains(this);
        }

        public virtual bool PreTakeIn(MoveComponent moveComponent)
        {
            return true;
        }

        public virtual void TakeIn(MoveComponent moveComponent)
        {
            Map.TakeIn(this);
            //CardSystem.Instance.CardModeUISystem.Elements[""]
        }

        public virtual bool PreTakeOut(MoveComponent moveComponent)
        {
            return true;
        }

        public virtual void TakeOut(MoveComponent moveComponent)
        {
            Map.TakeOut(this);
        }

        public virtual void Update(GameTime gt)
        {
        }

        public virtual void Draw(SpriteBatch sb)
        {
        }

        public virtual void DrawInMap(SpriteBatch sb, Vector2 drawOffset)
        {
            sb.Draw(Icon, InMapCenter + drawOffset, null, MapColor, 0f, Icon.Size() / 2f,
                InMapScale, SpriteEffects.None, 0f);
        }

        /// <summary>
        /// 在生成中会用到的实例克隆，默认调用公开的无参构造函数进行实例化。
        /// <br>[!]如果没有公开的无参构造函数须重写。</br>
        /// <br>[!]如果有需要和生成样本保持一致的数据须重写。</br>
        /// </summary>
        /// <returns></returns>
        public virtual RoomBase Clone()
        {
            var op = (RoomBase)Activator.CreateInstance(GetType());
            op.Position = Position;
            op.IsBegin = IsBegin;
            op.IsEnd = IsEnd;
            op.IsSilu = IsSilu;
            op.InMapCenter = InMapCenter;
            op.InMapScale = InMapScale;
            op._numberOfEntries = _numberOfEntries;
            op.Map = Map;
            return op;
        }
    }
}