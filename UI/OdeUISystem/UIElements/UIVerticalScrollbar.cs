using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using ReLogic.Content;

using Terraria;

namespace OdeMod.UI.OdeUISystem.UIElements
{
    internal class UIVerticalScrollbar : BaseElement
    {
        private Texture2D uiScrollbarTexture;
        private UIImage inner;
        private float mouseY;
        private float wheelValue;
        private int whell = 0;
        private bool isMouseDown = false;
        private float alpha = 0f;
        private float waitToWheelValue = 0f;
        public bool UseScrollWheel = false;
        public bool AlwaysOnLight = false;

        public float WheelValue
        {
            get { return wheelValue; }
            set
            {
                if (value > 1f)
                    waitToWheelValue = 1f;
                else if (value < 0f)
                    waitToWheelValue = 0f;
                else
                    waitToWheelValue = value;
            }
        }

        public UIVerticalScrollbar(float wheelValue = 0f)
        {
            Info.Width = new PositionStyle(20f, 0f);
            Info.Left = new PositionStyle(-20f, 1f);
            Info.Height = new PositionStyle(-20f, 1f);
            Info.Top = new PositionStyle(10f, 0f);
            Info.TopMargin.Pixel = 5f;
            Info.ButtomMargin.Pixel = 5f;
            Info.IsSensitive = true;
            uiScrollbarTexture = OdeMod.Instance.Assets.Request<Texture2D>("Images/UI/VerticalScrollbar", AssetRequestMode.ImmediateLoad).Value;
            WheelValue = wheelValue;
        }

        public override void LoadEvents()
        {
            base.LoadEvents();
            Events.OnLeftDown += element =>
            {
                if (!isMouseDown)
                {
                    //float height = Info.Size.Y - 26f;
                    //WheelValue = ((float)Main.mouseY - Info.Location.Y - 13f) / height;
                    //mouseY = Main.mouseY;

                    isMouseDown = true;
                }
            };
            Events.OnLeftUp += element =>
            {
                isMouseDown = false;
            };
        }

        public override void OnInitialization()
        {
            base.OnInitialization();
            inner = new UIImage(OdeMod.Instance.Assets.Request<Texture2D>("Images/UI/VerticalScrollbarInner", AssetRequestMode.ImmediateLoad).Value, Color.White);
            inner.Info.Left.Pixel = -(inner.Info.Width.Pixel - Info.Width.Pixel) / 2f;
            Register(inner);
        }

        public override void Update(GameTime gt)
        {
            base.Update(gt);
            if (ParentElement == null)
                return;

            bool isMouseHover = ParentElement.GetCanHitBox().Contains(Main.MouseScreen.ToPoint());
            if (AlwaysOnLight)
                alpha = 1f;
            else
            {
                if ((isMouseHover || isMouseDown) && alpha < 1f)
                    alpha += 0.01f;
                if ((!(isMouseHover || isMouseDown)) && alpha > 0f)
                    alpha -= 0.01f;
            }

            inner.ChangeColor(Color.White * alpha);

            MouseState state = Mouse.GetState();
            float height = Info.Size.Y - 26f;
            if (!isMouseHover)
                whell = state.ScrollWheelValue;

            if (UseScrollWheel && isMouseHover && whell != state.ScrollWheelValue)
            {
                WheelValue -= (float)(state.ScrollWheelValue - whell) / 6f / height;
                whell = state.ScrollWheelValue;
            }
            if (isMouseDown && mouseY != Main.mouseY)
            {
                WheelValue = ((float)Main.mouseY - Info.Location.Y - 13f) / height;
                mouseY = Main.mouseY;
            }

            inner.Info.Top.Pixel = WheelValue * height;
            wheelValue += (waitToWheelValue - wheelValue) / 6f;

            if (waitToWheelValue != wheelValue)
                Calculation();
        }

        protected override void DrawSelf(SpriteBatch sb)
        {
            sb.Draw(uiScrollbarTexture, new Rectangle(Info.HitBox.X + (Info.HitBox.Width - uiScrollbarTexture.Width) / 2,
                Info.HitBox.Y - 12, uiScrollbarTexture.Width, 12),
                new Rectangle(0, 0, uiScrollbarTexture.Width, 12), Color.White * alpha);

            sb.Draw(uiScrollbarTexture, new Rectangle(Info.HitBox.X + (Info.HitBox.Width - uiScrollbarTexture.Width) / 2,
                Info.HitBox.Y, uiScrollbarTexture.Width, Info.HitBox.Height),
                new Rectangle(0, 12, uiScrollbarTexture.Width, uiScrollbarTexture.Height - 24), Color.White * alpha);

            sb.Draw(uiScrollbarTexture, new Rectangle(Info.HitBox.X + (Info.HitBox.Width - uiScrollbarTexture.Width) / 2,
                Info.HitBox.Y + Info.HitBox.Height, uiScrollbarTexture.Width, 12),
                new Rectangle(0, uiScrollbarTexture.Height - 12, uiScrollbarTexture.Width, 12), Color.White * alpha);
        }
    }
}