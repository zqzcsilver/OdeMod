using Microsoft.Xna.Framework.Graphics;

using OdeMod.CardMode.CardComponents.BaseComponents;
using OdeMod.CardMode.PublicComponents;

using System;
using System.Collections.Generic;

namespace OdeMod.CardMode.CardComponents.DrawComponents
{
    internal abstract class DrawComponentBase : Component
    {
        public DrawComponent DrawComponent => Entity.GetComponent<DrawComponent>();

        public override void Load()
        {
            base.Load();
            DrawComponent.OnCardDraw += OnCardDraw;
        }

        public override void UnLoad()
        {
            base.UnLoad();
            DrawComponent.OnCardDraw -= OnCardDraw;
        }

        public override List<Type> GetDependComponents()
        {
            return new List<Type> { typeof(CardComponent), typeof(DrawComponent), typeof(CardInfoComponent) };
        }

        protected virtual void OnCardDraw(Entity entity, BaseInfoComponent infoComponent, SpriteBatch sb)
        {
        }
    }
}