using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OdeMod.CardMode.Rooms
{
    internal abstract class RoomBase
    {
        public virtual bool PreBuild()
        {
            return true;
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

        public virtual void DrawInMap(SpriteBatch sb, Vector2 center)
        {
        }
    }
}