using Microsoft.Xna.Framework;

namespace OdeMod.CardMode
{
    internal class PlayerInfo : ICardMode
    {
        public Point Position;

        public PlayerInfo()
        {
            Position = new Point();
        }
    }
}