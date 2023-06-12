using Microsoft.Xna.Framework;

using OdeMod.CardMode.Scenes.ChangeSceneStyles;
using OdeMod.CardMode.Scenes.ConfigScene.ConfigSystem.Configs;
using OdeMod.CardMode.UI;
using OdeMod.UI.OdeUISystem.UIElements;

namespace OdeMod.CardMode.Scenes.AboutScene.UIContainer
{
    internal class AboutContainer : CardUIContainerElement
    {
        public static readonly string ContainerFullName = typeof(AboutContainer).FullName;
        private UIContainerPanel aboutContainer;
        private bool autoMove = true;

        public override void OnInitialization()
        {
            base.OnInitialization();
            UIText title = new UIText(t: "$Scenes.AboutScene.UI.Title", CardSystem.ConfigManager.GetConfig<InterfaceConfig>().Font.GetFont(80f));
            title.Info.Top.SetValue(0f, 0.08f);
            title.Info.Left.SetValue(-title.Info.Width.Pixel / 2f, 0.5f);
            Register(title);

            BaseElement aboutPanel = new BaseElement();
            aboutPanel.Info.Width.SetValue(0f, 0.7f);
            aboutPanel.Info.Height.SetValue(-80f, 1f);
            aboutPanel.Info.Height -= title.Info.Top + title.Info.Height;
            aboutPanel.Info.Left.SetValue(0f, 0.15f);
            aboutPanel.Info.Top = new PositionStyle(0f, 1f) - aboutPanel.Info.Height;
            Register(aboutPanel);

            aboutContainer = new UIContainerPanel();
            aboutContainer.Info.Width.SetValue(0f, 1f);
            aboutContainer.Info.Height.SetValue(0f, 1f);
            aboutPanel.Register(aboutContainer);

            UIVerticalScrollbar verticalScrollbar = new UIVerticalScrollbar();
            verticalScrollbar.Info.Width.SetValue(40f, 0f);
            verticalScrollbar.Info.Left.SetValue(-40f, 0.94f);
            verticalScrollbar.Info.Height = aboutPanel.Info.Height - new PositionStyle(40f, 0f);
            verticalScrollbar.Info.Top = aboutPanel.Info.Top;
            verticalScrollbar.AlwaysOnLight = true;
            verticalScrollbar.UseScrollWheel = true;
            verticalScrollbar.Events.OnLeftDown += element =>
            {
                autoMove = false;
            };
            aboutContainer.SetVerticalScrollbar(verticalScrollbar);
            Register(verticalScrollbar);

            UIText back = new UIText("$Scenes.AboutScene.UI.Back", CardSystem.ConfigManager.GetConfig<InterfaceConfig>().Font.GetFont(40f));
            back.Info.Left.SetValue(0f, 0.01f);
            back.Info.Top.SetValue(0f, 0.01f);
            back.Events.OnLeftClick += element =>
            {
                CardSystem.SceneManager.BackLastScene(new FadeStyle());
            };
            back.Events.OnMouseOver += element =>
            {
                ((UIText)element).Color = Color.White;
            };
            back.Events.OnMouseOut += element =>
            {
                ((UIText)element).Color = Color.White * 0.8f;
            };
            Register(back);
        }

        public override void Show(params object[] args)
        {
            base.Show(args);
            autoMove = true;
        }

        public override void Update(GameTime gt)
        {
            base.Update(gt);
            if (autoMove && aboutContainer.UIVerticalScrollbar.WheelValue + 0.01f < 1f)
                aboutContainer.UIVerticalScrollbar.WheelValue += aboutContainer.UIVerticalScrollbar.WhellValueMult;
            else
                autoMove = false;
        }
    }
}