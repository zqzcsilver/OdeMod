using Microsoft.Xna.Framework;

namespace OdeMod.CardMode.PublicComponents
{
    internal class BaseInfoComponent : Component
    {
        public Vector2 Center;
        public float Scale;
        public float Rotation;
        public Rectangle HitBox;

        public override IComponent Clone(Entity cloneEntity)
        {
            BaseInfoComponent op = new BaseInfoComponent();
            op.Center = Center;
            op.Scale = Scale;
            op.Rotation = Rotation;
            op.HitBox = HitBox;
            return op;
        }
    }
}