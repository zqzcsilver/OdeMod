using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OdeMod.CardMode.ScreenEffectSystem
{
    internal class ScreenEffectManager
    {
        private Dictionary<string, ScreenEffectBase> _screenEffects;
        private Dictionary<string, ScreenEffectBase> _finallyScreenEffects;
        public List<string> ScreenEffectsCallOrder;
        public List<string> FinallyScreenEffectsCallOrder;

        public ScreenEffectBase this[string name]
        {
            get
            {
                return GetScreenEffect(name);
            }
            set
            {
                RegisterScreenEffect(name, value);
            }
        }

        public ScreenEffectManager()
        {
            _screenEffects = new Dictionary<string, ScreenEffectBase>();
            _finallyScreenEffects = new Dictionary<string, ScreenEffectBase>();

            ScreenEffectsCallOrder = new List<string>();
            FinallyScreenEffectsCallOrder = new List<string>();
        }

        public void RegisterScreenEffect(string name, ScreenEffectBase effect)
        {
            if (_screenEffects.ContainsKey(name))
            {
                _screenEffects[name] = effect;
                ScreenEffectsCallOrder.Remove(name);
                ScreenEffectsCallOrder.Add(name);
            }
            else
            {
                _screenEffects.Add(name, effect);
                ScreenEffectsCallOrder.Add(name);
            }
        }

        public void RegisterFinallyScreenEffect(string name, ScreenEffectBase effect)
        {
            if (_finallyScreenEffects.ContainsKey(name))
            {
                _finallyScreenEffects[name] = effect;
                FinallyScreenEffectsCallOrder.Remove(name);
                FinallyScreenEffectsCallOrder.Add(name);
            }
            else
            {
                _finallyScreenEffects.Add(name, effect);
                FinallyScreenEffectsCallOrder.Add(name);
            }
        }

        public void Update(GameTime gt)
        {
            foreach (var c in ScreenEffectsCallOrder)
            {
                _screenEffects[c].Update(gt);
            }
            foreach (var c in FinallyScreenEffectsCallOrder)
            {
                _finallyScreenEffects[c].Update(gt);
            }
        }

        public ScreenEffectBase GetScreenEffect(string name)
        {
            if (_screenEffects.ContainsKey(name))
                return _screenEffects[name];
            return null;
        }

        public ScreenEffectBase GetFinallyScreenEffect(string name)
        {
            if (_finallyScreenEffects.ContainsKey(name))
                return _finallyScreenEffects[name];
            return null;
        }

        public void ApplyScreenEffect(SpriteBatch sb, RenderTarget2D screenTarget, RenderTarget2D screenTargetSwap)
        {
            foreach (var c in ScreenEffectsCallOrder)
            {
                if (_screenEffects[c].Visible)
                    _screenEffects[c].Draw(sb, screenTarget, screenTargetSwap);
            }
        }

        public void ApplyFinallyScreenEffect(SpriteBatch sb, RenderTarget2D screenTarget, RenderTarget2D screenTargetSwap)
        {
            foreach (var c in FinallyScreenEffectsCallOrder)
            {
                if (_finallyScreenEffects[c].Visible)
                    _finallyScreenEffects[c].Draw(sb, screenTarget, screenTargetSwap);
            }
        }
    }
}