using Microsoft.Xna.Framework;

using OdeMod.CardMode.CardComponents.BaseComponents;
using OdeMod.CardMode.PublicComponents;

using System;
using System.Collections.Generic;

namespace OdeMod.CardMode.CardComponents.AnimationComponents
{
    internal class DragComponent : Component
    {
        public Vector2 TargetPos;
        public Vector2 OriginalPos;

        public Func<Vector2, Vector2, float> Speed =
            (c, t) =>
        {
            return (c - t).Length() / 3f;
        };

        public bool Dragging = false;

        public override List<Type> GetDependComponents()
        {
            return new List<Type>() { typeof(BaseInfoComponent) };
        }

        public override void Update(GameTime gt)
        {
            base.Update(gt);
            var infoComponent = Entity.GetComponent<CardInfoComponent>();
            var t = (Dragging ? TargetPos : OriginalPos);
            var unitVelocity = t - infoComponent.BaseInfoComponent.Center;
            if (unitVelocity != Vector2.Zero)
            {
                float speed = Speed(infoComponent.BaseInfoComponent.Center, t);
                if (unitVelocity.Length() < speed)
                {
                    infoComponent.BaseInfoComponent.Center += unitVelocity;
                }
                else
                {
                    unitVelocity.Normalize();
                    infoComponent.BaseInfoComponent.Center += unitVelocity * speed;
                }
            }
        }

        public override IComponent Clone(Entity cloneEntity)
        {
            var op = new DragComponent();
            op.OriginalPos = OriginalPos;
            op.TargetPos = TargetPos;
            op.Dragging = false;
            op.Speed = Speed;
            cloneEntity.GetComponent<BaseInfoComponent>().Center = op.OriginalPos;
            return op;
        }
    }
}