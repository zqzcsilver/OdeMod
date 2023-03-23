using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using OdeMod.Utils;

namespace OdeMod.CardMode.Utils
{
    internal class KeyBoardInputManager
    {
        private List<Keys> _keyPressedCache;
        private List<Keys> _keyClickCache;
        private List<Keys> _keyUpCache;
        private List<Keys> _keyDoubleClickCache;
        private Dictionary<Keys, KeyCooldown> _keyCooldownCache;
        public List<Keys> KeysPressed => _keyPressedCache;
        public List<Keys> KeysClick => _keyClickCache;
        public List<Keys> KeysUp => _keyUpCache;
        public List<Keys> KeysDoubleClick => _keyDoubleClickCache;
        public KeyBoardInputManager()
        {
            _keyPressedCache = new List<Keys>();
            _keyClickCache = new List<Keys>();
            _keyUpCache = new List<Keys>();
            _keyDoubleClickCache = new List<Keys>();
            _keyCooldownCache = new Dictionary<Keys, KeyCooldown>();
        }
        public bool IsKeyPressed(Keys key) => _keyPressedCache.Contains(key);

        public bool IsKeyRelease(Keys key) => !IsKeyPressed(key);

        public bool IsKeyDown(Keys key) => IsKeyClick(key) || IsKeyDoubleClick(key);

        public bool IsKeyClick(Keys key) => _keyClickCache.Contains(key);

        public bool IsKeyUp(Keys key) => _keyUpCache.Contains(key);

        public bool IsKeyDoubleClick(Keys key) => _keyDoubleClickCache.Contains(key);

        public List<Keys> GetPressedKeys() => _keyPressedCache;

        public bool HasPressedKey => _keyPressedCache.Count > 0;

        public int GetKeyCoolDownTime(Keys key)
        {
            if (!_keyCooldownCache.ContainsKey(key))
                _keyCooldownCache.Add(key, new KeyCooldown(() => Keyboard.GetState().IsKeyDown(key)));
            return _keyCooldownCache[key].CoolDownTime;
        }

        public void SetKeyCoolDownTime(Keys key, int time)
        {
            if (_keyCooldownCache.ContainsKey(key))
            {
                _keyCooldownCache[key].CoolDownTime = time;
            }
            else
                _keyCooldownCache.Add(key, new KeyCooldown(() => Keyboard.GetState().IsKeyDown(key), time));
        }

        public void Update(GameTime gt)
        {
            _keyPressedCache.Clear();
            _keyClickCache.Clear();
            _keyUpCache.Clear();
            _keyDoubleClickCache.Clear();

            var keyBoard = Keyboard.GetState();
            var keysPressed = keyBoard.GetPressedKeys();

            foreach (var key in keysPressed)
            {
                if (IsKeyRelease(key))
                {
                    if (!_keyCooldownCache.ContainsKey(key))
                        _keyCooldownCache.Add(key, new KeyCooldown(() => keyBoard.IsKeyDown(key)));
                    if (_keyCooldownCache[key].IsCoolDown())
                        _keyClickCache.Add(key);
                    else
                    {
                        _keyDoubleClickCache.Add(key);
                        _keyCooldownCache[key].CoolDown();
                    }
                }
            }
            Array.ForEach(keysPressed, x => _keyPressedCache.Remove(x));
            foreach (var key in _keyPressedCache)
            {
                _keyUpCache.Add(key);
            }

            _keyPressedCache.AddRange(keysPressed);

            foreach (var kc in _keyCooldownCache.Values)
                kc.Update();
        }
    }
}