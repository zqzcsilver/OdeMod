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

        /// <summary>
        /// 克隆该组件
        /// </summary>
        /// <param name="cloneEntity"></param>
        /// <returns></returns>
        public abstract IComponent Clone(Entity cloneEntity);

        /// <summary>
        /// 简单克隆该组件。默认会使用反射调用 <see cref="Component()"/> 获取结果。
        /// <br>[!]如果构造函数有参数请务必重写该方法。</br>
        /// </summary>
        /// <param name="cloneEntity">被克隆的实体</param>
        /// <returns>克隆体</returns>
        public virtual IComponent PrimitiveClone(Entity cloneEntity)
        {
            return (IComponent)Activator.CreateInstance(GetType());
        }

        /// <summary>
        /// 完全克隆该组件，包括标识符。默认调用 <see cref="Clone(Entity)"/> 获取结果
        /// </summary>
        /// <param name="cloneEntity">被克隆的实体</param>
        /// <returns>克隆体</returns>
        public virtual IComponent TotallyClone(Entity cloneEntity)
        {
            return Clone(cloneEntity);
        }
    }
}