using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OdeMod.Utils.Expends;

using System;
using System.Collections.Generic;
using System.Linq;
using Terraria.DataStructures;

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
        public Dictionary<Type, IComponent> Components
        { get { return new Dictionary<Type, IComponent>(components); } }

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
            var l = Components.Values.ToArray();
            for (int i = l.Length - 1; i >= 0; l[i--].Draw(sb)) ;
        }

        /// <summary>
        /// 卸载组件
        /// </summary>
        /// <param name="type">被卸载组件的Type</param>
        /// <returns>卸载成功返回true，否则返回false</returns>
        public bool RemoveComponent(Type type)
        {
            List<Type> op = new List<Type>();
            if (Components.ContainsKey(type))
            {
                getRemoveComponent(type, op);
                Type t;
                for (int i = op.Count - 1; i >= 0; i--)
                {
                    t = op[i];
                    components[t].UnLoad();
                    components.Remove(t);
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// 卸载组件
        /// </summary>
        /// <typeparam name="T">被卸载组件的类型</typeparam>
        /// <returns>卸载成功返回true，否则返回false</returns>
        public bool RemoveComponent<T>() where T : IComponent
        {
            return RemoveComponent(typeof(T));
        }

        /// <summary>
        /// 卸载组件
        /// </summary>
        /// <param name="component">被卸载的组件</param>
        /// <returns>卸载成功返回true，否则返回false</returns>
        public bool RemoveComponent(IComponent component)
        {
            return RemoveComponent(component.GetType());
        }

        private void getRemoveComponent(Type type, List<Type> types)
        {
            if (Components.ContainsKey(type))
            {
                types.Add(type);
                foreach (var c in Components.Values)
                {
                    if (c.GetDependComponents().Contains(type))
                    {
                        getRemoveComponent(c.GetType(), types);
                    }
                }
            }
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
        /// 按照依赖关系装载组件组
        /// </summary>
        /// <param name="components">等待被装载的组件组</param>
        /// <returns>全部装载成功返回true，否则返回false</returns>
        public int AddComponents(IEnumerable<IComponent> components)
        {
            int op = 0;
            int i, j;
            Type type;
            Dictionary<Type, IComponent> ts = new Dictionary<Type, IComponent>(Components);
            List<IComponent> cs = new List<IComponent>(components);
            foreach (var component in components)
            {
                type = component.GetType();
                if (Components.ContainsKey(type))
                {
                    cs.Remove(component);
                }
                else
                {
                    if (AddComponent(component))
                    {
                        cs.Remove(component);
                        op++;
                    }
                    else
                        ts.Add(type, component);
                }
            }
            bool needContinueRemove = true;

            List<Type> componentTypes;
            while (needContinueRemove)
            {
                i = 0;
                needContinueRemove = false;
                componentTypes = ts.Keys.ToList();
                while (i < cs.Count)
                {
                    if (!componentTypes.Contains(cs[i].GetDependComponents()))
                    {
                        needContinueRemove = true;
                        j = 0;
                        while (j < componentTypes.Count)
                        {
                            if (ts[componentTypes[j]].GetDependComponents().Contains(cs[i].GetType()))
                            {
                                ts.Remove(componentTypes[j]);
                                needContinueRemove = true;
                            }
                            j++;
                        }
                        cs.RemoveAt(i);
                    }
                    else
                        i++;
                }
            }
            while (cs.Count > 0)
            {
                i = 0;
                while (i < cs.Count)
                {
                    if (AddComponent(cs[i]))
                    {
                        cs.RemoveAt(i);
                        op++;
                    }
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
            IComponent compon;
            foreach (var component in components)
            {
                compon = component.Value.Clone(card);
                compon.Entity = card;
                components.Add(component.Key, compon);
            }
            return card;
        }

        internal IEntitySource GetSource_OnHit()
        {
            throw new NotImplementedException();
        }
    }
}