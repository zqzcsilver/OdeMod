using Microsoft.Xna.Framework.Graphics;

using OdeMod.CardMode.CardComponents.BaseComponents;
using OdeMod.CardMode.PublicComponents;
using OdeMod.CardMode.PublicComponents.LogicComponents;

using System;
using System.Collections.Generic;

namespace OdeMod.CardMode.PublicComponents.DrawComponents
{
    internal abstract class DrawComponentBase
    {
        public abstract void Load(DrawComponent drawComponent);

        public virtual void Unload(DrawComponent drawComponent)
        {
        }

        /// <summary>
        /// 当LogicComponent被克隆时组件一起跟着被克隆。默认调用 <see cref="PrimitiveClone(DrawComponent)"/> 获取结果
        /// </summary>
        /// <returns>克隆体</returns>
        public virtual DrawComponentBase Clone(DrawComponent drawComponent)
        {
            return PrimitiveClone(drawComponent);
        }

        /// <summary>
        /// 简单克隆该组件。默认会使用反射调用 <see cref="DrawComponentBase()"/> 获取结果，如果构造函数有参数请务必重写该方法
        /// </summary>
        /// <returns>克隆体</returns>
        public virtual DrawComponentBase PrimitiveClone(DrawComponent drawComponent)
        {
            return (DrawComponentBase)Activator.CreateInstance(GetType());
        }

        /// <summary>
        /// 完全克隆该组件，包括标识符。默认调用 <see cref="Clone(DrawComponent)"/> 获取结果
        /// </summary>
        /// <returns>克隆体</returns>
        public virtual DrawComponentBase TotallyClone(DrawComponent drawComponent)
        {
            return Clone(drawComponent);
        }
    }
}