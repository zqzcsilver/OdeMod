using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OdeMod.Utils.Expends;

using System;
using System.Collections.Generic;
using System.Linq;

namespace OdeMod.CardMode
{
    internal abstract class Component : IComponent
    {
        public virtual Entity Entity { get; set; }
        public Component()
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
        /// <summary>
        /// 当组件改变时会调用
        /// </summary>
        /// <param name="cardComponents"></param>
        /// <exception cref="Exception"></exception>
        public virtual void EntityComponentsChange(Dictionary<Type, IComponent> entityComponents)
        {
            if (entityComponents.Keys.ToList().Intersect(GetConflictComponents()))
                throw new Exception($"现有组件与组件{GetType().FullName}产生了组件冲突！");
        }
        public abstract IComponent Clone();
    }
}