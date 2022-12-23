using Microsoft.Xna.Framework;

namespace OdeMod.CardMode.PublicComponents
{
    internal class BaseInfoComponent : Component
    {
        public Vector2 Center;
        public float Scale;
        public float Rotation;
        public Rectangle HitBox;
        public int UUID;
        private static int UUIDSwap = 0;

        public BaseInfoComponent()
        {
            UUID = UUIDSwap;
            UUIDSwap++;
        }

        public override IComponent Clone(Entity cloneEntity)
        {
            BaseInfoComponent op = new BaseInfoComponent();
            op.Center = Center;
            op.Scale = Scale;
            op.Rotation = Rotation;
            op.HitBox = HitBox;
            return op;
        }

        public override IComponent TotallyClone(Entity cloneEntity)
        {
            var c = (BaseInfoComponent)Clone(cloneEntity);
            c.UUID = UUID;
            return c;
        }
    }
}