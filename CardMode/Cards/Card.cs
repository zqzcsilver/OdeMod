using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OdeMod.CardMode.Cards.Components;
using OdeMod.Utils.Expends;

using System;
using System.Collections.Generic;
using System.Linq;

namespace OdeMod.CardMode.Cards
{
    internal class Card : ICard
    {
        public readonly object Source;
        private Dictionary<Type, ICardComponent> components;
        public Dictionary<Type, ICardComponent> Components { get { return components; } }
        public Card(object source)
        {
            Source = source;
            components = new Dictionary<Type, ICardComponent>();
        }
        public void Update(GameTime gt)
        {
            foreach (var c in Components)
                c.Value.Update(gt);
        }
        public void Draw(SpriteBatch sb)
        {
            foreach (var c in Components)
                c.Value.Draw(sb);
        }
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
        public bool RemoveComponent<T>() where T : ICardComponent
        {
            return RemoveComponent(typeof(T));
        }
        public bool RemoveComponent(ICardComponent component)
        {
            return RemoveComponent(component.GetType());
        }
        public bool AddComponent(ICardComponent component)
        {
            var op = addComponent(component);
            foreach (var c in Components)
                c.Value.CardComponentsChange(Components);
            return op;
        }
        private bool addComponent(ICardComponent component)
        {
            var k = component.GetType();
            var depends = component.GetDependComponents();
            if (!Components.ContainsKey(k) && (depends == null || depends.Count == 0 || Components.ContainsKeys(depends)))
            {
                components.Add(k, component);
                component.Card = this;
                component.Load();
                return true;
            }
            return false;
        }
        public bool AddComponent<T>() where T : ICardComponent
        {
            T t = (T)Activator.CreateInstance(typeof(T));
            return AddComponent(t);
        }
        public bool AddComponents(IEnumerable<ICardComponent> components)
        {
            bool op = true;
            int i = 0;
            Type type;
            List<Type> ts = new List<Type>(Components.Keys);
            List<ICardComponent> cs = new List<ICardComponent>(components);
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
                c.Value.CardComponentsChange(Components);
            return op;
        }
        public T GetComponent<T>() where T : ICardComponent
        {
            return (T)components[typeof(T)];
        }
        public ICardComponent GetComponent(Type type)
        {
            return components[type];
        }
        public Card Clone()
        {
            Card card = new Card(this);
            card.components = new Dictionary<Type, ICardComponent>();
            foreach (var component in components)
            {
                components.Add(component.Key, component.Value.Clone());
            }
            return card;
        }
    }
}
