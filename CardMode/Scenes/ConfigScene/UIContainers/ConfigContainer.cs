using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

using OdeMod.CardMode.Scenes.AboutScene.UIContainer;
using OdeMod.CardMode.Scenes.ChangeSceneStyles;
using OdeMod.CardMode.Scenes.ConfigScene.ConfigSystem.Configs;
using OdeMod.CardMode.UI;
using OdeMod.UI.OdeUISystem.UIElements;

namespace OdeMod.CardMode.Scenes.ConfigScene.UIContainers
{
    internal class ConfigContainer : CardUIContainerElement
    {
        public static readonly string ContainerFullName = typeof(ConfigContainer).FullName;
        private UIContainerPanel configConatiner;

        public override void OnInitialization()
        {
            base.OnInitialization();

            UIText title = new UIText("$Scenes.ConfigScene.UI.Title", CardSystem.ConfigManager.GetConfig<InterfaceConfig>().Font.GetFont(80f));
            title.Info.Left.SetValue(-title.Info.Width.Pixel / 2f, 0.5f);
            title.Info.Top.SetValue(0f, 0.08f);
            Register(title);

            BaseElement configPanel = new BaseElement();
            configPanel.Info.Width.SetValue(0f, 0.7f);
            configPanel.Info.Height.SetValue(-80f, 1f);
            configPanel.Info.Height -= title.Info.Top + title.Info.Height;
            configPanel.Info.Left.SetValue(0f, 0.15f);
            configPanel.Info.Top = new PositionStyle(0f, 1f) - configPanel.Info.Height;
            Register(configPanel);

            configConatiner = new UIContainerPanel();
            configConatiner.Info.Width.SetValue(0f, 1f);
            configConatiner.Info.Height.SetValue(0f, 1f);
            configPanel.Register(configConatiner);

            UIVerticalScrollbar verticalScrollbar = new UIVerticalScrollbar();
            verticalScrollbar.Info.Width.SetValue(40f, 0f);
            verticalScrollbar.Info.Left.SetValue(-40f, 0.94f);
            verticalScrollbar.Info.Height = configPanel.Info.Height - new PositionStyle(40f, 0f);
            verticalScrollbar.Info.Top = configPanel.Info.Top;
            verticalScrollbar.AlwaysOnLight = true;
            verticalScrollbar.UseScrollWheel = true;
            configConatiner.SetVerticalScrollbar(verticalScrollbar);
            Register(verticalScrollbar);

            UIText back = new UIText("$Scenes.ConfigScene.UI.Back", CardSystem.ConfigManager.GetConfig<InterfaceConfig>().Font.GetFont(40f));
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
            configConatiner.ClearAllElements();
            PositionStyle top = PositionStyle.Empty;
            var configs = CardSystem.ConfigManager.GetAllConfigs();
            var configOrder = new List<string>()
            {
                "InterfaceConfig",
                "MusicConfig",
                "KeyBindConfig"
            };
            if (!(configOrder == null || configOrder.Count == 0))
            {
                configs.Sort((f1, f2) =>
                {
                    int i1 = configOrder.IndexOf(f1.SaveName);
                    int i2 = configOrder.IndexOf(f2.SaveName);
                    if (i1 == -1 && i2 == i1)
                        return 0;
                    if (i1 == -1)
                        return 1;
                    if (i2 == -1)
                        return -1;
                    return i1 < i2 ? -1 : 1;
                });
            }
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