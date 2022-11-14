using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;

namespace OdeMod.CardMode.Cards.Components.BaseComponents
{
    internal delegate void CardDrawHandle(Card card, InfoComponent infoComponent, SpriteBatch sb);
    internal class DrawComponent : CardComponent
    {
        public CardDrawHandle OnCardDraw;
        public DrawComponent()
        {
        }
        public override List<Type> GetDependComponents()
        {
            return new List<Type> { typeof(InfoComponent) };
        }
        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);
            var info = Card.GetComponent<InfoComponent>();
            sb.Draw(info.Texture, info.Center, null, Color.White, info.Rotation, 
                new Vector2(info.Texture.Width, info.Texture.Height) * info.Scale / 2f, info.Scale, SpriteEffects.None, 0f);
            OnCardDraw?.Invoke(Card, info, sb);
        }
        public override ICardComponent Clone()
        {
            DrawComponent component = new DrawComponent();
            component.OnCardDraw = OnCardDraw;
            return component;
        }
    }
}
