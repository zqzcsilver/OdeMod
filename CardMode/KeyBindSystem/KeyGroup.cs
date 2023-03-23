using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using OdeMod.Utils;

namespace OdeMod.CardMode.KeyBindSystem
{
    internal class KeyGroup
    {
        public string Name;
        private List<Keys> _keys;
        private bool _bindMouseLeft = false;
        private bool _bindMouseRight = false;
        private bool _bindMouseMiddle = false;
        private bool _canCloseResetKey = false;
        private KeyCooldown _keyCoolDown;
        public bool IsPressed { get; private set; }
        private bool _pressCache = false;
        public bool IsClick = false;
        public bool IsDoubleClick = false;
        public bool IsDown = false;
        public bool IsUp = false;
        public bool NeedResetKey
        {
            get => _needResetKey;
            set
            {
                _needResetKey = value;
                if (_needResetKey)
                {
                    _bindMouseLeft = false;
                    _bindMouseRight = false;
                    _bindMouseMiddle = false;
                    _keys.Clear();
                }
            }
        }
        private bool _needResetKey = false;
        public bool NeedBindMouse = false;
        public bool NeedBindKeys = true;
        public KeyGroup()
        {
            Name = string.Empty;
            _keys = new List<Keys>();
            _keyCoolDown = new KeyCooldown(() => IsPressed);
        }
        public KeyGroup(string name)
        {
            Name = name;
            _keys = new List<Keys>();
            _keyCoolDown = new KeyCooldown(() => IsPressed);
        }

        public KeyGroup(string name, List<Keys> keys)
        {
            Name = name;
            _keys = new List<Keys>(keys);
            _keyCoolDown = new KeyCooldown(() => IsPressed);
        }

        public void Update(GameTime gt)
        {
            IsClick = false;
            IsDoubleClick = false;
            IsDown = false;
            IsUp = false;
            if (NeedResetKey)
            {
                var pks = CardSystem.KeyBoardInputManager.GetPressedKeys();
                if ((NeedBindKeys && pks.Count > 0) || (NeedBindMouse &&
                    (CardSystem.GetMouseInfo.MouseLeftDown || CardSystem.GetMouseInfo.MouseRightDown || CardSystem.GetMouseInfo.MouseMiddleDown)))
                {
                    if (NeedBindMouse)
                    {
                        if (CardSystem.GetMouseInfo.MouseLeftDown)
                            _bindMouseLeft = true;
                        if (CardSystem.GetMouseInfo.MouseRightDown)
                            _bindMouseRight = true;
                        if (CardSystem.GetMouseInfo.MouseMiddleDown)
                            _bindMouseMiddle = true;
                    }
                    foreach (var k in pks)
                    {
                        if (!_keys.Contains(k))
                            _keys.Add(k);
                    }
                    _canCloseResetKey = true;
                }
                else if (_canCloseResetKey)
                {
                    NeedResetKey = false;
                    _canCloseResetKey = false;
                }
            }
            else
            {
                IsPressed = false;
                if (NeedBindMouse)
                {
                    if (_bindMouseLeft)
                    {
                        IsPressed = CardSystem.GetMouseInfo.MouseLeftDown;
                        if (!IsPressed)
                            return;
                    }
                    if (_bindMouseRight)
                    {
                        IsPressed = CardSystem.GetMouseInfo.MouseRightDown;
                        if (!IsPressed)
                            return;
                    }
                    if (_bindMouseMiddle)
                    {
                        IsPressed = CardSystem.GetMouseInfo.MouseMiddleDown;
                        if (!IsPressed)
                            return;
                    }
                }
                if (NeedBindKeys)
                {
                    IsPressed = true;
                    foreach (var k in _keys)
                    {
                        if (!CardSystem.KeyBoardInputManager.IsKeyDown(k))
                            IsPressed = false;
                        if (!IsPressed)
                            break;
                    }
                }
                if (_pressCache != IsPressed)
                {
                    _pressCache = IsPressed;
                    if (IsPressed)
                    {
                        IsDown = true;
                        if (_keyCoolDown.IsCoolDown())
                        {
                            IsClick = true;
                        }
                        else
                        {
                            IsDoubleClick = true;
                            _keyCoolDown.ResetCoolDown();
                        }
                    }
                    else
                    {
                        IsUp = true;
                    }
                }
            }
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (NeedBindMouse)
            {
                if (_bindMouseLeft)
                {
                    stringBuilder.Append("MouseLeft+");
                }
                if (_bindMouseRight)
                {
                    stringBuilder.Append("MouseRight+");
                }
                if (_bindMouseMiddle)
                {
                    stringBuilder.Append("MouseMiddle+");
                }
            }
            if (NeedBindKeys)
            {
                foreach (var k in _keys)
                {
                    stringBuilder.Append($"{k}+");
                }
            }
            if (stringBuilder.Length > 1)
                stringBuilder.Remove(stringBuilder.Length - 1, 1);
            return stringBuilder.ToString();
        }
    }
}