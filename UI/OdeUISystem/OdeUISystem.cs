using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OdeMod.UI.OdeUISystem.UIElements;
using OdeMod.Utils;

using System;
using System.Collections.Generic;
using System.Linq;

using Terraria;

namespace OdeMod.UI.OdeUISystem
{
    internal class OdeUISystem : IOdeUISystem
    {
        /// <summary>
        /// 存放着所有<see cref="ContainerElement"/>实例的字典
        /// </summary>
        public Dictionary<string, ContainerElement> Elements { get; private set; }
        /// <summary>
        /// 访问顺序
        /// </summary>
        public List<string> CallOrder { get; private set; }
        /// <summary>
        /// 交互部件缓存
        /// </summary>
        private List<BaseElement> interactContainerElementsBuffer;
        /// <summary>
        /// 缓存鼠标左键状态
        /// </summary>
        private bool mouseLeftDown = false;
        /// <summary>
        /// 缓存鼠标右键状态
        /// </summary>
        private bool mouseRightDown = false;
        /// <summary>
        /// 鼠标右键冷却
        /// </summary>
        private KeyCooldown mouseLeftCooldown;
        /// <summary>
        /// 鼠标左键冷却
        /// </summary>
        private KeyCooldown mouseRightCooldown;
        public OdeUISystem()
        {
            Elements = new Dictionary<string, ContainerElement>();
            CallOrder = new List<string>();
            interactContainerElementsBuffer = new List<BaseElement>();
            mouseLeftCooldown = new KeyCooldown(() =>
            {
                return Main.mouseLeft;
            });
            mouseRightCooldown = new KeyCooldown(() =>
            {
                return Main.mouseRight;
            });
        }
        public void Load()
        {
            var containers = from c in GetType().Assembly.GetTypes()
                             where !c.IsAbstract && c.IsSubclassOf(typeof(ContainerElement))
                             select c;
            ContainerElement element;
            foreach (var c in containers)
            {
                element = (ContainerElement)Activator.CreateInstance(c);
                if (element.AutoLoad)
                    Register(element);
            }
        }
        /// <summary>
        /// 执行逻辑
        /// </summary>
        /// <param name="gt"></param>
        public void Update(GameTime gt)
        {
            if (CallOrder.Count == 0 || Elements.Count == 0)
                return;
            ContainerElement child;
            foreach (var key in CallOrder)
            {
                child = Elements[key];
                if (child != null && child.IsVisible) child.Update(gt);
            }

            var interact = Elements[CallOrder[0]].GetElementsContainsPoint(Main.MouseScreen.ToPoint());
            foreach (var ce in interact)
                if (!interactContainerElementsBuffer.Contains(ce))
                    ce.Events.MouseOver(ce);
            foreach (var ce in interactContainerElementsBuffer)
                if (!interact.Contains(ce))
                    ce.Events.MouseOut(ce);
            interactContainerElementsBuffer = interact;

            if (mouseLeftDown != Main.mouseLeft)
            {
                var i = Elements[CallOrder[0]].GetElementsContainsPoint(Main.MouseScreen.ToPoint());
                if (Main.mouseLeft)
                {
                    i.ForEach(x => x.Events.LeftDown(x));
                }
                else
                {
                    if (mouseLeftCooldown.IsCoolDown())
                    {
                        i.ForEach(x => x.Events.LeftClick(x));
                        mouseLeftCooldown.ResetCoolDown();
                    }
                    else
                    {
                        i.ForEach(x => x.Events.LeftDoubleClick(x));
                        mouseLeftCooldown.CoolDown();
                    }
                }

                mouseLeftDown = Main.mouseLeft;
            }

            if (mouseRightDown != Main.mouseRight)
            {
                var i = Elements[CallOrder[0]].GetElementsContainsPoint(Main.MouseScreen.ToPoint());
                if (Main.mouseRight)
                {
                    i.ForEach(x => x.Events.RightDown(x));
                }
                else
                {
                    if (mouseRightCooldown.IsCoolDown())
                    {
                        i.ForEach(x => x.Events.RightClick(x));
                        mouseRightCooldown.ResetCoolDown();
                    }
                    else
                    {
                        i.ForEach(x => x.Events.RightDoubleClick(x));
                        mouseRightCooldown.CoolDown();
                    }
                }
                mouseRightDown = Main.mouseRight;
            }

            mouseLeftCooldown.Update();
            mouseRightCooldown.Update();
        }
        /// <summary>
        /// 绘制
        /// </summary>
        /// <param name="sb">画笔</param>
        public void Draw(SpriteBatch sb)
        {
            if (CallOrder.Count == 0 || Elements.Count == 0)
                return;
            ContainerElement child;
            for (int i = CallOrder.Count - 1; i >= 0; i--)
            {
                child = Elements[CallOrder[i]];
                if (child != null && child.IsVisible) child.Draw(sb);
            }
        }
        /// <summary>
        /// 添加子元素
        /// </summary>
        /// <param name="element">需要添加的子元素</param>
        /// <returns>成功时返回true，否则返回false</returns>
        public bool Register(ContainerElement element)
        {
            if (element == null || Elements.ContainsKey(element.Name) || CallOrder.Contains(element.Name)) return false;
            Elements.Add(element.Name, element);
            CallOrder.Add(element.Name);
            element.OnInitialization();
            element.Calculation();
            return true;
        }
        /// <summary>
        /// 添加子元素
        /// </summary>
        /// <param name="name">需要添加的子元素的Key</param>
        /// <param name="element">需要添加的子元素</param>
        /// <returns>成功时返回true，否则返回false</returns>
        public bool Register(string name, ContainerElement element)
        {
            if (element == null || Elements.ContainsKey(name) || CallOrder.Contains(name)) return false;
            Elements.Add(name, element);
            CallOrder.Add(element.Name);
            element.OnInitialization();
            element.Calculation();
            return true;
        }
        /// <summary>
        /// 移除子元素
        /// </summary>
        /// <param name="name">需要移除的子元素的Key</param>
        /// <returns>成功时返回true，否则返回false</returns>
        public bool Remove(string name)
        {
            if (CallOrder.Count == 0 || Elements.Count == 0 || !(Elements.ContainsKey(name) || CallOrder.Contains(name)))
                return false;
            Elements.Remove(name);
            CallOrder.Remove(name);
            return true;
        }
        /// <summary>
        /// 将所有容器相对坐标计算为具体坐标
        /// </summary>
        public void Calculation()
        {
            foreach (var child in Elements.Values)
                if (child != null)
                    child.Calculation();
        }
    }
}
