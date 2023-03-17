using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OdeMod.UI.OdeUISystem.UIElements;

using Terraria;

namespace OdeMod.UI.OdeUISystem.Containers.QuickBar
{
    internal delegate void HandleTigger(QuickElement quickElement);

    internal abstract class QuickElement : UIImage
    {
        public bool IsMoveing => waitTime >= WaitTime;
        private int waitTime = 0;
        public int WaitTime = 30;
        private Vector2 waitToMouse = Vector2.Zero;
        private bool mouseDown = false;
        public Vector2 Center = Vector2.Zero;

        public event HandleTigger OnTigger;

        public QuickElement(Texture2D texture, Color color) : base(texture, color)
        {
            Style = CalculationStyle.LockAspectRatioMainWidth;
            Info.Width.SetValue(0f, 1f);
        }

        public override void LoadEvents()
        {
            base.LoadEvents();
            Events.OnLeftDown += Events_OnLeftDown;
            Events.OnLeftUp += Events_OnLeftUp;
        }

        private void Events_OnLeftUp(BaseElement baseElement)
        {
            mouseDown = false;
            if (!IsMoveing)
            {
                OnTigger?.Invoke(this);
            }
        }

        private void Events_OnLeftDown(BaseElement baseElement)
        {
            mouseDown = true;
        }

        public override void Update(GameTime gt)
        {
            base.Update(gt);
            if (mouseDown)
                waitTime++;
            else
                waitTime = 0;

            if (IsMoveing)
                MoveTo(Main.MouseScreen);
            else
                MoveTo(Center);
        }

        public bool MoveTo(Vector2 center)
        {
            if (waitToMouse != center)
            {
                waitToMouse += (center - waitToMouse) / 4f;
                Info.Left.SetValue(waitToMouse.X - (ParentElement == null ? 0 : ParentElement.Info.Location.X) - Info.Size.X / 2f, 0f);
                Info.Top.SetValue(waitToMouse.Y - (ParentElement == null ? 0 : ParentElement.Info.Location.Y) - Info.Size.Y / 2f, 0f);
                Calculation();
                return false;
            }
            else
                return true;
        }
    }
}