using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using OdeMod.Utils;

namespace OdeMod.CardMode.Utils
{
    internal class MouseInfo : ICardMode
    {
        public bool MouseLeftDown = false;
        public bool MouseRightDown = false;
        public bool MouseMiddleDown = false;

        public bool MouseLeftClick = false;
        public bool MouseRightClick = false;
        public bool MouseMiddleClick = false;

        public bool MouseLeftDoubleClick = false;
        public bool MouseRightDoubleClick = false;
        public bool MouseMiddleDoubleClick = false;

        public float ScrollWhellValue = 0f;
        public float ScrollWhellValueDifference = 0f;

        public float MouseX = 0f;
        public float MouseY = 0f;
        public Vector2 MousePosition { get => new Vector2(MouseX, MouseY); }

        private KeyCooldown mouseLeftCooldown, mouseRightCooldown, mouseMiddleCooldown;
        private bool mouseLeftBuffer = false, mouseRightBuffer = false, mouseMiddleBuffer = false;
        private float scrollWhellValueBuffer = 0f;

        public MouseInfo()
        {
            mouseLeftCooldown = new KeyCooldown(() => Mouse.GetState().LeftButton == ButtonState.Pressed);
            mouseRightCooldown = new KeyCooldown(() => Mouse.GetState().RightButton == ButtonState.Pressed);
            mouseMiddleCooldown = new KeyCooldown(() => Mouse.GetState().MiddleButton == ButtonState.Pressed);
        }

        public void Update(GameTime gt)
        {
            var state = Mouse.GetState();
            MouseLeftClick = false;
            MouseRightClick = false;
            MouseMiddleClick = false;

            MouseLeftDoubleClick = false;
            MouseRightDoubleClick = false;
            MouseMiddleDoubleClick = false;

            MouseLeftDown = state.LeftButton == ButtonState.Pressed;
            MouseRightDown = state.RightButton == ButtonState.Pressed;
            MouseMiddleDown = state.MiddleButton == ButtonState.Pressed;

            ScrollWhellValueDifference = 0f;
            ScrollWhellValue = state.ScrollWheelValue;
            if (ScrollWhellValue != scrollWhellValueBuffer)
            {
                ScrollWhellValueDifference = ScrollWhellValue - scrollWhellValueBuffer;
                scrollWhellValueBuffer = ScrollWhellValue;
            }

            MouseX = state.X;
            MouseY = state.Y;

            if (mouseLeftBuffer != MouseLeftDown)
            {
                if (MouseLeftDown)
                {
                    if (mouseLeftCooldown.IsCoolDown())
                    {
                        mouseLeftBuffer = MouseLeftDown;
                        MouseLeftClick = true;
                        mouseLeftCooldown.ResetCoolDown();
                    }
                    else
                    {
                        MouseLeftDoubleClick = true;
                        mouseLeftCooldown.CoolDown();
                    }
                }
                mouseLeftBuffer = MouseLeftDown;
            }
            if (mouseRightBuffer != MouseRightDown)
            {
                if (MouseRightDown)
                {
                    if (mouseRightCooldown.IsCoolDown())
                    {
                        mouseRightBuffer = MouseRightDown;
                        MouseRightClick = true;
                        mouseRightCooldown.ResetCoolDown();
                    }
                    else
                    {
                        MouseRightDoubleClick = true;
                        mouseRightCooldown.CoolDown();
                    }
                }
                mouseRightBuffer = MouseRightDown;
            }
            if (mouseMiddleBuffer != MouseMiddleDown)
            {
                if (MouseMiddleDown)
                {
                    if (mouseMiddleCooldown.IsCoolDown())
                    {
                        mouseMiddleBuffer = MouseMiddleDown;
                        MouseMiddleClick = true;
                        mouseMiddleCooldown.ResetCoolDown();
                    }
                    else
                    {
                        MouseMiddleDoubleClick = true;
                        mouseMiddleCooldown.CoolDown();
                    }
                }
                mouseMiddleBuffer = MouseMiddleDown;
            }

            mouseLeftCooldown.Update();
            mouseRightCooldown.Update();
            mouseMiddleCooldown.Update();
        }
    }
}