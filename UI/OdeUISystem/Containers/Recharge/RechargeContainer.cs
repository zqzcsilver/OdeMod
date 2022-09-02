using Microsoft.Xna.Framework;

using OdeMod.UI.OdeUISystem.UIElements;
using OdeMod.UI.OriginalUISystem;

namespace OdeMod.UI.OdeUISystem.Containers.Recharge
{
    internal class RechargeContainer : ContainerElement, IOriginalUIState
    {
        public override void OnInitialization()
        {
            base.OnInitialization();
            UIPanel panel = new UIPanel();
            panel.Info.Width = new PositionStyle(0f, 0.6f);
            panel.Info.Height = new PositionStyle(0f, 0.6f);
            panel.Info.Left = new PositionStyle(0f, 0.2f);
            panel.Info.Top = new PositionStyle(0f, 0.2f);
            panel.PanelColor = new Color(1f, 0.7f, 0.6f);
            Register(panel);

            UIContainerPanel containerPanel = new UIContainerPanel();
            containerPanel.Info.Width.Pixel = -20f;
            containerPanel.Info.Height.Pixel = -20f;
            panel.Register(containerPanel);

            VerticalScrollbar verticalScrollbar = new VerticalScrollbar();
            containerPanel.SetVerticalScrollbar(verticalScrollbar);
            panel.Register(verticalScrollbar);

            HorizontalScrollbar horizontalScrollbar = new HorizontalScrollbar();
            containerPanel.SetHorizontalScrollbar(horizontalScrollbar);
            panel.Register(horizontalScrollbar);

            BaseElement element;
            UIContainerPanel p;
            VerticalScrollbar v;
            HorizontalScrollbar h;
            UIImage image;
            //for (int i = 0; i < 10; i++)
            //{
            //    for(int j = 0; j < 10; j++)
            //    {
            //        element = new BaseElement();
            //        element.Info.Width.Percent = 0.3334f;
            //        element.Info.Height.Percent = 0.3334f;
            //        element.Info.Left.Percent = 0.3334f * i;
            //        element.Info.Top.Percent = 0.3334f * j;

            //        p = new UIContainerPanel();
            //        element.Register(p);

            //        v = new VerticalScrollbar();
            //        p.SetVerticalScrollbar(v);
            //        element.Register(v);

            //        h = new HorizontalScrollbar();
            //        p.SetHorizontalScrollbar(h);
            //        element.Register(h);

            //        image = new UIImage(ModContent.Request<Texture2D>("OdeMod/Images/Misc/Niko", AssetRequestMode.ImmediateLoad).Value, Color.White);
            //        p.AddElement(image);

            //        containerPanel.AddElement(element);
            //    }
            //}
            UIInputBox box = new UIInputBox("", Point.Zero, Color.White, new Vector2(2f, 4f));
            box.Info.Width.Percent = 1f;
            box.Info.Height.Percent = 1f;
            containerPanel.AddElement(box);
        }
    }
}