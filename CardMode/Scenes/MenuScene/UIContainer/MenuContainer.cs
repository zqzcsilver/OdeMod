﻿using Microsoft.Xna.Framework;

using OdeMod.CardMode.Scenes.ChangeSceneStyles;
using OdeMod.CardMode.Scenes.ConfigScene.ConfigSystem.Configs;
using OdeMod.CardMode.UI;
using OdeMod.UI.OdeUISystem.UIElements;

namespace OdeMod.CardMode.Scenes.MenuScene.UIContainer
{
    internal class MenuContainer : CardUIContainerElement
    {
        public override void OnInitialization()
        {
            base.OnInitialization();
            var fs = CardSystem.ConfigManager.GetConfig<InterfaceConfig>().Font;
            var font = fs.GetFont(50f);
            float spacing = 10f, top = 0f;

            UIText play = new UIText("开始", font, Color.White * 0.8f);
            play.Info.Left.SetValue(0f, 0.1f);
            play.Info.Top.SetValue(top, 0.6f);
            play.Events.OnLeftClick += element =>
            {
                CardSystem.SceneManager.ChangeScene("OdeMod.CardMode.Scenes.GameInfoScene.SingleplayerOrMultiplayerScene", new FadeStyle());
            };
            play.Events.OnMouseOver += element =>
            {
                ((UIText)element).Color = Color.White;
            };
            play.Events.OnMouseOut += element =>
            {
                ((UIText)element).Color = Color.White * 0.8f;
            };
            Register(play);
            top += play.Info.Height.Pixel + spacing;

            UIText collect = new UIText("收集", font, Color.White * 0.8f);
            collect.Info.Left.SetValue(0f, 0.1f);
            collect.Info.Top.SetValue(top, 0.6f);
            collect.Events.OnMouseOver += element =>
            {
                ((UIText)element).Color = Color.White;
            };
            collect.Events.OnMouseOut += element =>
            {
                ((UIText)element).Color = Color.White * 0.8f;
            };
            Register(collect);
            top += collect.Info.Height.Pixel + spacing;

            UIText achievement = new UIText("成就", font, Color.White * 0.8f);
            achievement.Info.Left.SetValue(0f, 0.1f);
            achievement.Info.Top.SetValue(top, 0.6f);
            achievement.Events.OnMouseOver += element =>
            {
                ((UIText)element).Color = Color.White;
            };
            achievement.Events.OnMouseOut += element =>
            {
                ((UIText)element).Color = Color.White * 0.8f;
            };
            Register(achievement);
            top += achievement.Info.Height.Pixel + spacing;

            UIText config = new UIText("设置", font, Color.White * 0.8f);
            config.Info.Left.SetValue(0f, 0.1f);
            config.Info.Top.SetValue(top, 0.6f);
            config.Events.OnLeftClick += element =>
            {
                CardSystem.SceneManager.ChangeScene("OdeMod.CardMode.Scenes.ConfigScene.ConfigScene", new FadeStyle());
            };
            config.Events.OnMouseOver += element =>
            {
                ((UIText)element).Color = Color.White;
            };
            config.Events.OnMouseOut += element =>
            {
                ((UIText)element).Color = Color.White * 0.8f;
            };
            Register(config);
            top += config.Info.Height.Pixel + spacing;

            UIText about = new UIText("关于", font, Color.White * 0.8f);
            about.Info.Left.SetValue(0f, 0.1f);
            about.Info.Top.SetValue(top, 0.6f);
            about.Events.OnLeftClick += element =>
            {
                CardSystem.SceneManager.ChangeScene("OdeMod.CardMode.Scenes.AboutScene.AboutScene", new FadeStyle());
            };
            about.Events.OnMouseOver += element =>
            {
                ((UIText)element).Color = Color.White;
            };
            about.Events.OnMouseOut += element =>
            {
                ((UIText)element).Color = Color.White * 0.8f;
            };
            Register(about);
            top += about.Info.Height.Pixel + spacing;

            UIText exit = new UIText("退出", font, Color.White * 0.8f);
            exit.Info.Left.SetValue(0f, 0.1f);
            exit.Info.Top.SetValue(top, 0.6f);
            exit.Events.OnLeftClick += element =>
            {
                CardSystem.Instance.CardModeVisible = false;
            };
            exit.Events.OnMouseOver += element =>
            {
                ((UIText)element).Color = Color.White;
            };
            exit.Events.OnMouseOut += element =>
            {
                ((UIText)element).Color = Color.White * 0.8f;
            };
            Register(exit);

            UIText logo = new UIText("卡牌模式", fs.GetFont(140f));
            logo.Info.Left.SetValue(-logo.Info.Width.Pixel / 2f, 0.5f);
            logo.Info.Top.SetValue(-logo.Info.Height.Pixel / 2f, 0.3f);
            Register(logo);

            UIText logo1 = new UIText("颂歌", fs.GetFont(60f));
            logo1.Info.Left.SetValue(-logo1.Info.Width.Pixel / 2f, 0.5f);
            logo1.Info.Top.SetValue(-logo.Info.Height.Pixel / 2f - logo1.Info.Height.Pixel, 0.3f);
            Register(logo1);
        }
    }
}