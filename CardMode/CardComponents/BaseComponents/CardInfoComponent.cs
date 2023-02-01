using FontStashSharp;

using Microsoft.Xna.Framework.Graphics;

using OdeMod.CardMode.PublicComponents;

using System;
using System.Collections.Generic;

using Terraria.GameContent;

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

        public CardState State;
        public Entity CardOwner;
        public string CardID;
        public string CardTip;
        public string CardName;
        public DynamicSpriteFont Font => FontSystem.GetFont(FontSize);
        public FontSystem FontSystem;
        public float FontSize;
        public int CardCost;
        private static int DefaultIDCount;
        public BaseInfoComponent BaseInfoComponent => Entity.GetComponent<BaseInfoComponent>();

        public CardInfoComponent()
        {
            State = CardState.CardPile;
            CardCost = 0;
            FontSystem = OdeMod.DefaultFontSystem;
            FontSize = OdeMod.DEFAULT_FONT_SIZE;
            CardName = "Default Card";
            CardTip = "This is a default card";

            CardID = $"Default Card {DefaultIDCount}";
            DefaultIDCount++;
        }

        public override List<Type> GetDependComponents()
        {
            return new List<Type> { typeof(CardComponent), typeof(BaseInfoComponent) };
        }

        public override IComponent Clone(Entity cloneEntity)
        {
            CardInfoComponent component = new CardInfoComponent();
            component.State = State;
            return component;
        }
    }
}