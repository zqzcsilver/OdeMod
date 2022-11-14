using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OdeMod.Utils.Expends;

using System;
using System.Collections.Generic;
using System.Linq;

namespace OdeMod.CardMode.Cards.Components
{
    internal abstract class CardComponent : ICardComponent
    {
        public virtual Card Card { get; set; }
        public CardComponent()
        {
        }
        /// <summary>
        /// 获取前置组件
        /// </summary>
        /// <returns></returns>
        public virtual List<Type> GetDependComponents()
        {
            return null;
        }
        /// <summary>
        /// 获取冲突组件
        /// </summary>
        /// <returns></returns>
        public virtual List<Type> GetConflictComponents()
        {
            return null;
        }
        public virtual void Draw(SpriteBatch sb)
        {

        }
        public virtual void Load()
        {

        }
        public virtual void UnLoad()
        {

        }
        public virtual void Update(GameTime gt)
        {

        }
        public virtual void CardComponentsChange(Dictionary<Type, ICardComponent> cardComponents)
        {
            if (cardComponents.Keys.ToList().Intersect(GetConflictComponents()))
                throw new System.Exception($"现有组件与组件{GetType().FullName}产生了组件冲突！");
        }
        public abstract ICardComponent Clone();
    }
}