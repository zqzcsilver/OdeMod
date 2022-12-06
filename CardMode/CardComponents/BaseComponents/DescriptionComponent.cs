using System;
using System.Collections.Generic;

namespace OdeMod.CardMode.CardComponents.BaseComponents
{
    internal class DescriptionComponent : Component
    {
        public override IComponent Clone(Entity cloneEntity)
        {
            return new DescriptionComponent();
        }

        public override List<Type> GetDependComponents()
        {
            return new List<Type> { typeof(CardComponent) };
        }
    }
}