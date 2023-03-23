using Microsoft.Xna.Framework;

using OdeMod.CardMode.Scenes.ConfigScene.ConfigSystem.Configs;
using OdeMod.CardMode.UI;
using OdeMod.UI.OdeUISystem.UIElements;

namespace OdeMod.CardMode.Scenes.GameInfoScene.UIContainer
{
    internal class SingleplayerOrMultiplayerContainer : CardUIContainerElement
    {
        public override void OnInitialization()
        {
            base.OnInitialization();
            var fs = CardSystem.ConfigManager.GetConfig<InterfaceConfig>().Font;

            UIPanel singlePlay = new UIPanel();
            singlePlay.Info.IsSensitive = true;
            singlePlay.Info.Width.SetValue(0f, 0.2f);
            singlePlay.Info.Height.SetValue(0f, 0.6f);
            singlePlay.Info.Left.SetValue(new PositionStyle(0f, 0.34f) - singlePlay.Info.Width / 2f);
            singlePlay.Info.Top.SetValue(new PositionStyle(0f, 0.5f) - singlePlay.Info.Height / 2f);
            Register(singlePlay);

            UIImage singlePlayIcon = new UIImage(CardSystem.GetCardTexture("Room/EyeballIcon"), Color.White * 0.8f);
            singlePlayIcon.Info.Left.SetValue(new PositionStyle(0f, 0.5f) - singlePlayIcon.Info.Width / 2f);
            singlePlayIcon.Info.Top.SetValue(new PositionStyle(0f, 0.4f) - singlePlayIcon.Info.Height / 2f);
            singlePlay.Register(singlePlayIcon);

            UIText singlePlayTitle = new UIText("单人模式", fs.GetFont(60f), Color.White * 0.8f);
            singlePlayTitle.Info.Left.SetValue(new PositionStyle(0f, 0.5f) - singlePlayTitle.Info.Width / 2f);
            singlePlayTitle.Info.Top.SetValue(new PositionStyle(0f, 0.6f) - singlePlayTitle.Info.Height / 2f);
            singlePlay.Register(singlePlayTitle);

            singlePlay.Events.OnMouseOver += element =>
            {
                singlePlayIcon.ChangeColor(Color.White);
                singlePlayTitle.Color = Color.White;
            };
            singlePlay.Events.OnMouseOut += element =>
            {
                singlePlayIcon.ChangeColor(Color.White * 0.8f);
                singlePlayTitle.Color = Color.White * 0.8f;
            };

            UIPanel multiplay = new UIPanel();
            multiplay.Info.IsSensitive = true;
            multiplay.Info.Width.SetValue(0f, 0.2f);
            multiplay.Info.Height.SetValue(0f, 0.6f);
            multiplay.Info.Left.SetValue(new PositionStyle(0f, 0.66f) - multiplay.Info.Width / 2f);
            multiplay.Info.Top.SetValue(new PositionStyle(0f, 0.5f) - multiplay.Info.Height / 2f);
            Register(multiplay);

            UIImage multiplayIcon = new UIImage(CardSystem.GetCardTexture("Room/EyeballIcon"), Color.White * 0.8f);
            multiplayIcon.Info.Left.SetValue(new PositionStyle(-10f, 0.5f) - multiplayIcon.Info.Width / 2f);
            multiplayIcon.Info.Top.SetValue(new PositionStyle(0f, 0.4f) - multiplayIcon.Info.Height / 2f);
            multiplay.Register(multiplayIcon);

            UIImage multiplayIcon1 = new UIImage(CardSystem.GetCardTexture("Room/EyeballIcon"), Color.White * 0.8f);
            multiplayIcon1.Info.Left.SetValue(new PositionStyle(10f, 0.5f) - multiplayIcon1.Info.Width / 2f);
            multiplayIcon1.Info.Top.SetValue(new PositionStyle(0f, 0.4f) - multiplayIcon1.Info.Height / 2f);
            multiplay.Register(multiplayIcon1);

            UIText multiplayTitle = new UIText("多人模式", fs.GetFont(60f), Color.White * 0.8f);
            multiplayTitle.Info.Left.SetValue(new PositionStyle(0f, 0.5f) - multiplayTitle.Info.Width / 2f);
            multiplayTitle.Info.Top.SetValue(new PositionStyle(0f, 0.6f) - multiplayTitle.Info.Height / 2f);
            multiplay.Register(multiplayTitle);

            multiplay.Events.OnMouseOver += element =>
            {
                multiplayIcon.ChangeColor(Color.White);
                multiplayIcon1.ChangeColor(Color.White);
                multiplayTitle.Color = Color.White;
            };
            multiplay.Events.OnMouseOut += element =>
            {
                multiplayIcon.ChangeColor(Color.White * 0.8f);
                multiplayIcon1.ChangeColor(Color.White * 0.8f);
                multiplayTitle.Color = Color.White * 0.8f;
            };
        }
    }
}