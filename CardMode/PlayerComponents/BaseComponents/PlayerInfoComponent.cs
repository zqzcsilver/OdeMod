using Microsoft.Xna.Framework;

namespace OdeMod.CardMode.PlayerComponents.BaseComponents
{
    internal class PlayerInfoComponent : Component
    {
        public Point InMapPosition = new Point(-1, -1);
        public Vector2 InRoomCenter = Vector2.Zero;

        public override IComponent Clone(Entity cloneEntity)
        {
            var x = new PlayerInfoComponent();
            x.InMapPosition = InMapPosition;
            x.InRoomCenter = InRoomCenter;
            return x;
        }
    }
}