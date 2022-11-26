using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;

namespace OdeMod.CardMode.CardComponents.BaseComponents
{
    internal delegate void CardDrawHandle(Entity card, CardInfoComponent infoComponent, SpriteBatch sb);
    internal class DrawComponent : Component
    {
        public CardDrawHandle OnCardDraw;
        public DrawComponent()
        {
        }
        public override List<Type> GetDependComponents()
        {
            return new List<Type> { typeof(CardInfoComponent) };
        }
        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);
            var info = Entity.GetComponent<CardInfoComponent>();
            sb.Draw(info.Texture, info.Center, null, Color.White, info.Rotation,
                new Vector2(info.Texture.Width, info.Texture.Height) * info.Scale / 2f, info.Scale, SpriteEffects.None, 0f);
            OnCardDraw?.Invoke(Entity, info, sb);
        }
        public override IComponent Clone()
        {
            DrawComponent component = new DrawComponent();
            component.OnCardDraw = OnCardDraw;
            return component;
        }
    }
}
