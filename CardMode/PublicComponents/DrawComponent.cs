using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OdeMod.CardMode.PublicComponents.DrawComponents;
using OdeMod.Utils;

using System;
using System.Collections.Generic;
using System.Linq;

using Terraria;

namespace OdeMod.CardMode.PublicComponents
{
    internal class DrawComponent : Component
    {
        internal enum HookType
        {
            PreDraw,
            OnDraw,
            PostDraw
        }

        public List<DrawComponentBase> CallOrder { get; private set; }
        private Dictionary<(HookType, DrawComponentBase), Action<Entity, BaseInfoComponent, SpriteBatch, HookInfo>> Hooks;
        public BaseInfoComponent Info => Entity.GetComponent<BaseInfoComponent>();

        public Point DrawSize;

        public RenderTarget2D Render => OdeMod.RenderTarget2DPool.Pool(DrawSize);
        public RenderTarget2D RenderSwap => OdeMod.RenderTarget2DPool.PoolSwap(DrawSize);

        public DrawComponent()
        {
            CallOrder = new List<DrawComponentBase>();
            Hooks = new Dictionary<(HookType, DrawComponentBase), Action<Entity, BaseInfoComponent, SpriteBatch, HookInfo>>();
        }

        public override List<Type> GetDependComponents()
        {
            return new List<Type> { typeof(BaseInfoComponent) };
        }

        public override void Draw(SpriteBatch sb)
        {
            if (Hooks.Count == 0)
                return;

            var info = Info;
            var size = DrawSize;

            var hookInfo = new HookInfo();
            DrawUtils.SetDrawRenderTarget(sb, (spriteBatch) =>
            {
                foreach (var c in CallOrder)
                {
                    var key = (HookType.OnDraw, c);
                    if (Hooks.ContainsKey(key))
                        Hooks[key](Entity, info, sb, hookInfo);
                    if (!hookInfo.CanRunNextLogic)
                        break;
                }
            }, Render, Main.screenTarget, Main.screenTargetSwap);

            if (hookInfo.CanRunNextFunction)
            {
                hookInfo = new HookInfo();
                foreach (var c in CallOrder)
                {
                    var key = (HookType.PreDraw, c);
                    if (Hooks.ContainsKey(key))
                        Hooks[key](Entity, info, sb, hookInfo);
                    if (!hookInfo.CanRunNextLogic)
                        break;
                }
            }
            sb.Draw(Render, info.Center, null, Color.White, info.Rotation,
                    new Vector2(Render.Width, Render.Height) / 2f, 1f, SpriteEffects.None, 0f);
            if (hookInfo.CanRunNextFunction)
            {
                hookInfo = new HookInfo();
                foreach (var c in CallOrder)
                {
                    var key = (HookType.PostDraw, c);
                    if (Hooks.ContainsKey(key))
                        Hooks[key](Entity, info, sb, hookInfo);
                    if (!hookInfo.CanRunNextLogic)
                        break;
                }
            }
        }

        public void AddComponent(DrawComponentBase source)
        {
            source.Load(this);
            CallOrder.Add(source);
        }

        public void AddComponent(int index, DrawComponentBase source)
        {
            source.Load(this);
            CallOrder.Insert(index, source);
        }

        public void RemoveComponent(DrawComponentBase source)
        {
            source.Unload(this);
            RemoveHooks(source);
            CallOrder.Remove(source);
        }

        public void RegisterHook(HookType hookType, DrawComponentBase source, Action<Entity, BaseInfoComponent, SpriteBatch, HookInfo> hook)
        {
            var s = (hookType, source);
            if (Hooks.ContainsKey(s))
                Hooks[s] = hook;
            else
                Hooks.Add(s, hook);
        }

        public void RemoveHooks(HookType hookType)
        {
            var array = Hooks.Keys.ToArray();
            foreach (var h in array)
            {
                if (h.Item1 == hookType)
                    Hooks.Remove(h);
            }
        }

        public void RemoveHooks(DrawComponentBase source)
        {
            var array = Hooks.Keys.ToArray();
            foreach (var h in array)
            {
                if (h.Item2 == source)
                    Hooks.Remove(h);
            }
        }

        public bool RemoveHook(HookType hookType, DrawComponentBase source)
        {
            var k = (hookType, source);
            if (Hooks.ContainsKey(k))
            {
                Hooks.Remove(k);
                return true;
            }
            return false;
        }

        public override IComponent Clone(Entity cloneEntity)
        {
            var op = new DrawComponent();
            op.CallOrder = new List<DrawComponentBase>();
            CallOrder.ForEach(x => op.CallOrder.Add(x.Clone(this)));
            foreach (var c in op.CallOrder)
                op.AddComponent(c);
            return op;
        }

        public override IComponent PrimitiveClone(Entity cloneEntity)
        {
            var op = new DrawComponent();
            op.CallOrder = new List<DrawComponentBase>();
            CallOrder.ForEach(x => op.CallOrder.Add(x.PrimitiveClone(this)));
            foreach (var c in op.CallOrder)
                op.AddComponent(c);
            return op;
        }

        public override IComponent TotallyClone(Entity cloneEntity)
        {
            var op = new DrawComponent();

            op.CallOrder = new List<DrawComponentBase>();
            CallOrder.ForEach(x => op.CallOrder.Add(x.TotallyClone(this)));
            foreach (var c in op.CallOrder)
                op.AddComponent(c);
            return op;
        }
    }
}