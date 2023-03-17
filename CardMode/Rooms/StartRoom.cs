using Microsoft.Xna.Framework.Graphics;

namespace OdeMod.CardMode.Rooms
{
    internal class StartRoom : RoomBase
    {
        public override bool PreBuild()
        {
            return IsBegin;
        }

        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);
        }
    }
}