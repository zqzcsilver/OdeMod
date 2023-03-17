using Microsoft.CodeAnalysis.Differencing;
using Microsoft.Xna.Framework;

using OdeMod.CardMode.Scenes.ChangeSceneStyles;
using OdeMod.CardMode.Scenes.ConfigScene.ConfigSystem.Configs;
using OdeMod.CardMode.UI;
using OdeMod.UI.OdeUISystem.UIElements;

namespace OdeMod.CardMode.Scenes.ConfigScene.UIContainers
{
    internal class ConfigContainer : CardUIContainerElement
    {
        private UIContainerPanel configConatiner;

        public override void OnInitialization()
        {
            base.OnInitialization();

            UIText title = new UIText("设置", CardSystem.ConfigManager.GetConfig<InterfaceConfig>().Font.GetFont(180f));
            title.Info.Left.SetValue(-title.Info.Width.Pixel / 2f, 0.5f);
            Register(title);

            BaseElement configPanel = new BaseElement();
            configPanel.Info.Width.SetValue(-40f, 0.7f);
            configPanel.Info.Height.SetValue(-40f, 1f);
            configPanel.Info.Height -= title.Info.Height;
            configPanel.Info.Left.SetValue(0f, 0.15f);
            configPanel.Info.Top = new PositionStyle(0f, 1f) - configPanel.Info.Height;
            Register(configPanel);

            configConatiner = new UIContainerPanel();
            configConatiner.Info.Width.SetValue(-40f, 1f);
            configConatiner.Info.Height.SetValue(-40f, 1f);
            configPanel.Register(configConatiner);

            UIVerticalScrollbar verticalScrollbar = new UIVerticalScrollbar();
            verticalScrollbar.Info.Width.SetValue(40f, 0f);
            verticalScrollbar.Info.Left.SetValue(-40f, 0.94f);
            verticalScrollbar.Info.Height = configPanel.Info.Height;
            verticalScrollbar.Info.Top = configPanel.Info.Top;
            verticalScrollbar.AlwaysOnLight = true;
            configConatiner.SetVerticalScrollbar(verticalScrollbar);
            Register(verticalScrollbar);

            UIText back = new UIText("<-返回", CardSystem.ConfigManager.GetConfig<InterfaceConfig>().Font.GetFont(60f));
            back.Info.Left.SetValue(0f, 0.01f);
            back.Info.Top.SetValue(0f, 0.01f);
            back.Events.OnLeftClick += element =>
            {
                CardSystem.SceneManager.ChangeScene(CardSystem.SceneManager.LastScene, new FadeStyle());
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
            configConatiner.ClearAllElements();
            PositionStyle top = PositionStyle.Empty;
            var configs = CardSystem.ConfigManager.GetAllConfigs();
            BaseElement baseElement;
            foreach (var config in configs)
            {
                baseElement = config.GetPage();
                baseElement.Info.Top = top;
                configConatiner.AddElement(baseElement);
                top += baseElement.Info.Height + new PositionStyle(100f, 0f);
            }

            Calculation();
        }
    }
}