using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;

namespace OdeMod.CardMode.Cards.Components
{
    internal interface ICardComponent : ICardMode
    {
        public Card Card { get; set; }
        public void Load();
        public void UnLoad();
        public void Draw(SpriteBatch sb);
        public void Update(GameTime gt);
        public List<Type> GetDependComponents();
        public void CardComponentsChange(Dictionary<Type, ICardComponent> cardComponents);
        public ICardComponent Clone();
    }
}