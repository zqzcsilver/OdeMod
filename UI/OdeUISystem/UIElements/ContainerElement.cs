using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdeMod.UI.OdeUISystem.UIElements
{
    internal abstract class ContainerElement : BaseElement
    {
        public virtual string Name { get => GetType().FullName; }
        public virtual bool AutoLoad { get => true; }
        public ContainerElement() 
        {
            Info.IsVisible = false;
        }
        public override void OnInitialization()
        {
            base.OnInitialization();
            Info.Width = new PositionStyle(0f, 1f);
            Info.Height = new PositionStyle(0f, 1f);
        }
    }
}
