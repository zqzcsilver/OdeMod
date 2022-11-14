using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OdeMod.CardMode.Cards.Components.BaseComponents
{
    internal class InfoComponent : CardComponent
    {
        /// <summary>
        /// 卡牌状态
        /// </summary>
        public enum CardState
        {
            /// <summary>
            /// 在牌库
            /// </summary>
            CardPile,
            /// <summary>
            /// 在手牌
            /// </summary>
            CardHand,
            /// <summary>
            /// 在墓地
            /// </summary>
            CardGraveyard,
        }
        public Vector2 Center;
        public float Scale = 1f;
        public Texture2D Texture;
        public float Rotation = 0f;
        public CardState State;
        public InfoComponent() { }
        public override ICardComponent Clone()
        {
            InfoComponent component = new InfoComponent();
            component.Scale = Scale;
            component.Texture = Texture;
            component.State = State;
            return component;
        }
    }
}
