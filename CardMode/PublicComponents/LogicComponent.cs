using OdeMod.CardMode.PublicComponents.LogicComponents;

using System;
using System.Collections.Generic;
using System.Linq;

namespace OdeMod.CardMode.PublicComponents
{
    internal class HookInfo
    {
        /// <summary>
        /// 是否允许执行同一委托池内下一委托
        /// </summary>
        public bool CanRunNextLogic = true;

        /// <summary>
        /// 是否允许执行下一函数或委托池
        /// </summary>
        public bool CanRunNextFunction = true;
    }

    internal class LogicComponent : Component
    {
        internal enum HookType
        {
            PrePlay, OnPlayer, PostPlayer,
            PreTurnBegin, OnTurnBegin, PostTurnBegin, PreTurnEnd, OnTurnEnd, PostTurnEnd,
            PreFightBgein, OnFightBegin, PostFightBegin, PreFightEnd, OnFightEnd, PostFightEnd
        }

        public List<LogicComponentBase> CallOrder { get; private set; }
        private Dictionary<(HookType, LogicComponentBase), Action<Entity, bool, HookInfo>> Hooks;

        public LogicComponent()
        {
            CallOrder = new List<LogicComponentBase>();

            Hooks = new Dictionary<(HookType, LogicComponentBase), Action<Entity, bool, HookInfo>>();
        }

        public void Play(Entity entity)
        {
            bool isMe = entity == Entity;
            (HookType, LogicComponentBase) key;
            HookInfo info = new HookInfo();
            foreach (var c in CallOrder)
            {
                key = (HookType.PrePlay, c);
                if (Hooks.ContainsKey(key))
                    Hooks[key](entity, isMe, info);
                if (!info.CanRunNextLogic)
                    break;
            }

            if (!info.CanRunNextFunction)
                return;
            info = new HookInfo();
            foreach (var c in CallOrder)
            {
                key = (HookType.OnPlayer, c);
                if (Hooks.ContainsKey(key))
                    Hooks[key](entity, isMe, info);
                if (!info.CanRunNextLogic)
                    break;
            }

            if (!info.CanRunNextFunction)
                return;
            info = new HookInfo();
            foreach (var c in CallOrder)
            {
                key = (HookType.PostPlayer, c);
                if (Hooks.ContainsKey(key))
                    Hooks[key](entity, isMe, info);
                if (!info.CanRunNextLogic)
                    break;
            }
        }

        public void TurnBegin(Entity entity)
        {
            bool isMe = entity == Entity;
            (HookType, LogicComponentBase) key;
            HookInfo info = new HookInfo();
            foreach (var c in CallOrder)
            {
                key = (HookType.PreTurnBegin, c);
                if (Hooks.ContainsKey(key))
                    Hooks[key](entity, isMe, info);
                if (!info.CanRunNextLogic)
                    break;
            }

            if (!info.CanRunNextFunction)
                return;
            info = new HookInfo();
            foreach (var c in CallOrder)
            {
                key = (HookType.OnTurnBegin, c);
                if (Hooks.ContainsKey(key))
                    Hooks[key](entity, isMe, info);
                if (!info.CanRunNextLogic)
                    break;
            }

            if (!info.CanRunNextFunction)
                return;
            info = new HookInfo();
            foreach (var c in CallOrder)
            {
                key = (HookType.PostTurnBegin, c);
                if (Hooks.ContainsKey(key))
                    Hooks[key](entity, isMe, info);
                if (!info.CanRunNextLogic)
                    break;
            }
        }

        public void TurnEnd(Entity entity)
        {
            bool isMe = entity == Entity;
            (HookType, LogicComponentBase) key;
            HookInfo info = new HookInfo();
            foreach (var c in CallOrder)
            {
                key = (HookType.PreTurnEnd, c);
                if (Hooks.ContainsKey(key))
                    Hooks[key](entity, isMe, info);
                if (!info.CanRunNextLogic)
                    break;
            }

            if (!info.CanRunNextFunction)
                return;
            info = new HookInfo();
            foreach (var c in CallOrder)
            {
                key = (HookType.OnTurnEnd, c);
                if (Hooks.ContainsKey(key))
                    Hooks[key](entity, isMe, info);
                if (!info.CanRunNextLogic)
                    break;
            }

            if (!info.CanRunNextFunction)
                return;
            info = new HookInfo();
            foreach (var c in CallOrder)
            {
                key = (HookType.PostTurnEnd, c);
                if (Hooks.ContainsKey(key))
                    Hooks[key](entity, isMe, info);
                if (!info.CanRunNextLogic)
                    break;
            }
        }

        public void FightBegin(Entity entity)
        {
            bool isMe = entity == Entity;
            (HookType, LogicComponentBase) key;
            HookInfo info = new HookInfo();
            foreach (var c in CallOrder)
            {
                key = (HookType.PreFightBgein, c);
                if (Hooks.ContainsKey(key))
                    Hooks[key](entity, isMe, info);
                if (!info.CanRunNextLogic)
                    break;
            }

            if (!info.CanRunNextFunction)
                return;
            info = new HookInfo();
            foreach (var c in CallOrder)
            {
                key = (HookType.OnFightBegin, c);
                if (Hooks.ContainsKey(key))
                    Hooks[key](entity, isMe, info);
                if (!info.CanRunNextLogic)
                    break;
            }

            if (!info.CanRunNextFunction)
                return;
            info = new HookInfo();
            foreach (var c in CallOrder)
            {
                key = (HookType.PostFightBegin, c);
                if (Hooks.ContainsKey(key))
                    Hooks[key](entity, isMe, info);
                if (!info.CanRunNextLogic)
                    break;
            }
        }

        public void FightEnd(Entity entity)
        {
            bool isMe = entity == Entity;
            (HookType, LogicComponentBase) key;
            HookInfo info = new HookInfo();
            foreach (var c in CallOrder)
            {
                key = (HookType.PreFightEnd, c);
                if (Hooks.ContainsKey(key))
                    Hooks[key](entity, isMe, info);
                if (!info.CanRunNextLogic)
                    break;
            }

            if (!info.CanRunNextFunction)
                return;
            info = new HookInfo();
            foreach (var c in CallOrder)
            {
                key = (HookType.OnFightEnd, c);
                if (Hooks.ContainsKey(key))
                    Hooks[key](entity, isMe, info);
                if (!info.CanRunNextLogic)
                    break;
            }

            if (!info.CanRunNextFunction)
                return;
            info = new HookInfo();
            foreach (var c in CallOrder)
            {
                key = (HookType.PostFightEnd, c);
                if (Hooks.ContainsKey(key))
                    Hooks[key](entity, isMe, info);
                if (!info.CanRunNextLogic)
                    break;
            }
        }

        public void AddComponent(LogicComponentBase source)
        {
            source.Load(this);
            CallOrder.Add(source);
        }

        public void AddComponent(int index, LogicComponentBase source)
        {
            source.Load(this);
            CallOrder.Insert(index, source);
        }

        public void RemoveComponent(LogicComponentBase source)
        {
            source.Unload(this);
            RemoveHooks(source);
            CallOrder.Remove(source);
        }

        public void RegisterHook(HookType hookType, LogicComponentBase source, Action<Entity, bool, HookInfo> hook)
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

        public void RemoveHooks(LogicComponentBase source)
        {
            var array = Hooks.Keys.ToArray();
            foreach (var h in array)
            {
                if (h.Item2 == source)
                    Hooks.Remove(h);
            }
        }

        public bool RemoveHook(HookType hookType, LogicComponentBase source)
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
            var op = new LogicComponent();
            op.CallOrder = new List<LogicComponentBase>();
            CallOrder.ForEach(x => op.CallOrder.Add(x.Clone(this)));
            foreach (var c in op.CallOrder)
                op.AddComponent(c);
            return op;
        }

        public override IComponent PrimitiveClone(Entity cloneEntity)
        {
            var op = new LogicComponent();
            op.CallOrder = new List<LogicComponentBase>();
            CallOrder.ForEach(x => op.CallOrder.Add(x.PrimitiveClone(this)));
            foreach (var c in op.CallOrder)
                op.AddComponent(c);
            return op;
        }

        public override IComponent TotallyClone(Entity cloneEntity)
        {
            var op = new LogicComponent();

            op.CallOrder = new List<LogicComponentBase>();
            CallOrder.ForEach(x => op.CallOrder.Add(x.TotallyClone(this)));
            foreach (var c in op.CallOrder)
                op.AddComponent(c);
            return op;
        }
    }
}