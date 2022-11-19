using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdeMod.CardMode.CardComponents.BaseComponents
{
    internal class CardComponent : Component
    {
        public override IComponent Clone()
        {
            CardComponent component = new CardComponent();
            return component;
        }
    }
}
