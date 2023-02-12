using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using ReLogic.Content;

using Terraria;

namespace OdeMod.UI.OdeUISystem.UIElements
{
    internal class HorizontalScrollbar : BaseElement
    {
        private Texture2D uiScrollbarTexture;
        private UIImage inner;
        private float mouseX;
        private float wheelValue;
        private int whell = 0;
        private bool isMouseDown = false;
        private float alpha = 0f;
        private float waitToWheelValue = 0f;
        public bool UseScrollWheel = false;
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
        public HorizontalScrollbar(float wheelValue = 0f)
        {
            Info.Height = new PositionStyle(20f, 0f);
            Info.Top = new PositionStyle(-20f, 1f);
            Info.Width = new PositionStyle(-20f, 1f);
            Info.Left = new PositionStyle(10f, 0f);
            Info.LeftMargin.Pixel = 5f;
            Info.RightMargin.Pixel = 5f;
            Info.IsSensitive = true;
            uiScrollbarTexture = OdeMod.Instance.Assets.Request<Texture2D>("Images/UI/HorizontalScrollbar", AssetRequestMode.ImmediateLoad).Value;
            WheelValue = wheelValue;
        }
        public override void LoadEvents()
        {
            base.LoadEvents();
            Events.OnLeftDown += element =>
            {
                if (!isMouseDown)
                {
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
            inner = new UIImage(OdeMod.Instance.Assets.Request<Texture2D>("Images/UI/HorizontalScrollbarInner", AssetRequestMode.ImmediateLoad).Value, Color.White);
            inner.Info.Top.Pixel = -(inner.Info.Height.Pixel - Info.Height.Pixel) / 2f;
            Register(inner);
        }
        public override void Update(GameTime gt)
        {
            base.Update(gt);
            if (ParentElement == null)
                return;

            bool isMouseHover = ParentElement.GetCanHitBox().Contains(Main.MouseScreen.ToPoint());
            if ((isMouseHover || isMouseDown) && alpha < 1f)
                alpha += 0.01f;
            if ((!(isMouseHover || isMouseDown)) && alpha > 0f)
                alpha -= 0.01f;

            inner.ChangeColor(Color.White * alpha);

            MouseState state = Mouse.GetState();
            float width = Info.Size.X - 26f;
            if (!isMouseHover)
                whell = state.ScrollWheelValue;

            if (UseScrollWheel && isMouseHover && whell != state.ScrollWheelValue)
            {
                WheelValue -= (float)(state.ScrollWheelValue - whell) / 10f / width;
                whell = state.ScrollWheelValue;
            }
            if (isMouseDown && mouseX != Main.mouseX)
            {
                WheelValue = ((float)Main.mouseX - Info.Location.X - 13f) / width;
                mouseX = Main.mouseX;
            }

            inner.Info.Left.Pixel = width * WheelValue;
            wheelValue += (waitToWheelValue - wheelValue) / 6f;

            if (waitToWheelValue != wheelValue)
                Calculation();
        }
        protected override void DrawSelf(SpriteBatch sb)
        {
            sb.Draw(uiScrollbarTexture, new Rectangle(Info.HitBox.X - 12,
                Info.HitBox.Y + (Info.HitBox.Height - uiScrollbarTexture.Height) / 2, 12, uiScrollbarTexture.Height),
                new Rectangle(0, 0, 12, uiScrollbarTexture.Height), Color.White * alpha);

            sb.Draw(uiScrollbarTexture, new Rectangle(Info.HitBox.X,
                Info.HitBox.Y + (Info.HitBox.Height - uiScrollbarTexture.Height) / 2, Info.HitBox.Width, uiScrollbarTexture.Height),
                new Rectangle(12, 0, uiScrollbarTexture.Width - 24, uiScrollbarTexture.Height), Color.White * alpha);

            sb.Draw(uiScrollbarTexture, new Rectangle(Info.HitBox.X + Info.HitBox.Width,
                Info.HitBox.Y + (Info.HitBox.Height - uiScrollbarTexture.Height) / 2, 12, uiScrollbarTexture.Height),
                new Rectangle(uiScrollbarTexture.Width - 12, 0, 12, uiScrollbarTexture.Height), Color.White * alpha);
        }
    }
}
