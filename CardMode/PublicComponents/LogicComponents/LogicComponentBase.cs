using System;

namespace OdeMod.CardMode.PublicComponents.LogicComponents
{
    internal abstract class LogicComponentBase
    {
        public abstract void Load(LogicComponent logicComponent);

        public virtual void Unload(LogicComponent logicComponent)
        {
        }

        /// <summary>
        /// 当LogicComponent被克隆时组件一起跟着被克隆。默认调用 <see cref="PrimitiveClone(LogicComponent)"/> 获取结果
        /// </summary>
        /// <returns>克隆体</returns>
        public virtual LogicComponentBase Clone(LogicComponent logicComponent)
        {
            return PrimitiveClone(logicComponent);
        }

        /// <summary>
        /// 简单克隆该组件。默认会使用反射调用 <see cref="LogicComponentBase()"/> 获取结果，如果构造函数有参数请务必重写该方法
        /// </summary>
        /// <returns>克隆体</returns>
        public virtual LogicComponentBase PrimitiveClone(LogicComponent logicComponent)
        {
            return (LogicComponentBase)Activator.CreateInstance(GetType());
        }

        /// <summary>
        /// 完全克隆该组件，包括标识符。默认调用 <see cref="Clone(LogicComponent)"/> 获取结果
        /// </summary>
        /// <returns>克隆体</returns>
        public virtual LogicComponentBase TotallyClone(LogicComponent logicComponent)
        {
            return Clone(logicComponent);
        }
    }
}