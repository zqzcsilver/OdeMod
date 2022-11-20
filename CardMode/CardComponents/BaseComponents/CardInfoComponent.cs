using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OdeMod.CardMode.CardComponents.BaseComponents
{
    internal class CardInfoComponent : Component
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
        public Entity Owner;
        public CardInfoComponent() { }
        public override IComponent Clone()
        {
            CardInfoComponent component = new CardInfoComponent();
            component.Scale = Scale;
            component.Texture = Texture;
            component.State = State;
            return component;
        }
    }
}
