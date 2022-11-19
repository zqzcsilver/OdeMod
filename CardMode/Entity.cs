using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OdeMod.Utils.Expends;

using System;
using System.Collections.Generic;
using System.Linq;

namespace OdeMod.CardMode
{
    internal class Entity : ICardMode
    {
        /// <summary>
        /// Entity来源
        /// </summary>
        public readonly object Source;
        /// <summary>
        /// Entity已加载的组件
        /// </summary>
        private Dictionary<Type, IComponent> components;
        /// <summary>
        /// Entity已加载的组件
        /// </summary>
        public Dictionary<Type, IComponent> Components { get { return new Dictionary<Type, IComponent>(components); } }
        /// <summary>
        /// 构造一个Entity
        /// </summary>
        /// <param name="source">Entity来源</param>
        public Entity(object source)
        {
            Source = source;
            components = new Dictionary<Type, IComponent>();
        }
        /// <summary>
        /// 执行组件逻辑
        /// </summary>
        /// <param name="gt"></param>
        public void Update(GameTime gt)
        {
            foreach (var c in Components)
                c.Value.Update(gt);
        }
        /// <summary>
        /// 绘制组件
        /// </summary>
        /// <param name="sb">画笔</param>
        public void Draw(SpriteBatch sb)
        {
            foreach (var c in Components)
                c.Value.Draw(sb);
        }
        /// <summary>
        /// 移除组件
        /// </summary>
        /// <param name="type">被移除组件的Type</param>
        /// <returns>移除成功返回true，否则返回false</returns>
        public bool RemoveComponent(Type type)
        {
            List<Type> op = new List<Type>();
            if (Components.ContainsKey(type))
            {
                op.Add(type);
                foreach (var c in Components.Values)
                {
                    if (c.GetDependComponents().Contains(type))
                    {
                        op.Add(type);
                    }
                }
                op.ForEach(x => components.Remove(x));
                return true;
            }
            return false;
        }
        /// <summary>
        /// 移除组件
        /// </summary>
        /// <typeparam name="T">被移除组件的类型</typeparam>
        /// <returns>移除成功返回true，否则返回false</returns>
        public bool RemoveComponent<T>() where T : IComponent
        {
            return RemoveComponent(typeof(T));
        }
        /// <summary>
        /// 移除组件
        /// </summary>
        /// <param name="component">被移除的组件</param>
        /// <returns>移除成功返回true，否则返回false</returns>
        public bool RemoveComponent(IComponent component)
        {
            return RemoveComponent(component.GetType());
        }
        /// <summary>
        /// 装载组件
        /// </summary>
        /// <param name="component">等待被装载的组件</param>
        /// <returns>装载成功返回true，否则返回false</returns>
        public bool AddComponent(IComponent component)
        {
            var op = addComponent(component);
            foreach (var c in Components)
                c.Value.EntityComponentsChange(Components);
            return op;
        }
        /// <summary>
        /// 装载组件
        /// </summary>
        /// <param name="component">等待被装载的组件</param>
        /// <returns>装载成功返回true，否则返回false</returns>
        private bool addComponent(IComponent component)
        {
            var k = component.GetType();
            var depends = component.GetDependComponents();
            if (!Components.ContainsKey(k) && (depends == null || depends.Count == 0 || Components.ContainsKeys(depends)))
            {
                components.Add(k, component);
                component.Entity = this;
                component.Load();
                return true;
            }
            return false;
        }
        /// <summary>
        /// 装载组件（将自动创建组件实例并装载）
        /// </summary>
        /// <typeparam name="T">被装载组件的类型</typeparam>
        /// <returns></returns>
        public bool AddComponent<T>() where T : IComponent
        {
            T t = (T)Activator.CreateInstance(typeof(T));
            return AddComponent(t);
        }
        /// <summary>
        /// 按照依赖关系装载一堆组件
        /// </summary>
        /// <param name="components">等待被装载的组件们</param>
        /// <returns>全部装载成功返回true，否则返回false</returns>
        public bool AddComponents(IEnumerable<IComponent> components)
        {
            bool op = true;
            int i = 0;
            Type type;
            List<Type> ts = new List<Type>(Components.Keys);
            List<IComponent> cs = new List<IComponent>(components);
            foreach (var component in components)
            {
                type = component.GetType();
                if (Components.ContainsKey(type))
                {
                    cs.Remove(component);
                    op = false;
                }
                else
                {
                    if (AddComponent(component))
                        cs.Remove(component);
                    else
                        ts.Add(type);
                }
            }
            while (i < cs.Count)
            {
                if (!ts.Contains(cs[i].GetDependComponents()))
                {
                    cs.RemoveAt(i);
                    op = false;
                }
                else
                    i++;
            }
            while (cs.Count > 0)
            {
                i = 0;
                while (i < cs.Count)
                {
                    if (AddComponent(cs[i]))
                        cs.RemoveAt(i);
                    else
                        i++;
                }
            }
            foreach (var c in Components)
                c.Value.EntityComponentsChange(Components);
            return op;
        }
        /// <summary>
        /// 是否已装载组件
        /// </summary>
        /// <typeparam name="T">需要判断的组件类型</typeparam>
        /// <returns>如果已装载返回true，否则返回false</returns>
        public bool HasComponent<T>() => components.ContainsKey(typeof(T));
        /// <summary>
        /// 是否已装载组件
        /// </summary>
        /// <param name="type">组件的Type</param>
        /// <returns>如果已装载组件返回true，否则返回false</returns>
        public bool HasComponent(Type type) => components.ContainsKey(type);
        /// <summary>
        /// 获取组件
        /// </summary>
        /// <typeparam name="T">需要获取的组件的类型</typeparam>
        /// <returns>获取到的组件实例</returns>
        public T GetComponent<T>() where T : IComponent
        {
            return (T)components[typeof(T)];
        }
        /// <summary>
        /// 获取组件
        /// </summary>
        /// <param name="type">需要获取的组件的Type</param>
        /// <returns>获取到的组件的实例</returns>
        public IComponent GetComponent(Type type)
        {
            return components[type];
        }
        /// <summary>
        /// 克隆该Entity与其已装载的组件
        /// </summary>
        /// <returns>克隆体</returns>
        public Entity Clone()
        {
            Entity card = new Entity(this);
            card.components = new Dictionary<Type, IComponent>();
            foreach (var component in components)
            {
                components.Add(component.Key, component.Value.Clone());
            }
            return card;
        }
    }
}
