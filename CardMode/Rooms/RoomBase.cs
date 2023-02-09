using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ModLoader;

namespace OdeMod.CardMode.Rooms
{
    [Autoload(true)]
    internal abstract class RoomBase
    {
        public Point Position;
        public bool IsBegin;
        public bool IsEnd;
        public bool IsSilu;
        public Vector2 InMapCenter;
        public virtual float BuildWeight => 1f;

        public virtual Texture2D Icon => ModContent.Request<Texture2D>("OdeMod/Images/Card/Original/Room/EyeballIcon",
            ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;

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

        public virtual bool PreTakeIn()
        {
            return true;
        }

        public virtual void TakeIn()
        {
        }

        public virtual bool PreTakeOut()
        {
            return true;
        }

        public virtual void TakeOut()
        {
        }

        public virtual void Update(GameTime gt)
        {
        }

        public virtual void Draw(SpriteBatch sb)
        {
        }

        public virtual void DrawInMap(SpriteBatch sb)
        {
            sb.Draw(Icon, InMapCenter, null, Color.White, 0f, Icon.Size() / 2f,
                1f, SpriteEffects.None, 0f);
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
            return op;
        }
    }
}