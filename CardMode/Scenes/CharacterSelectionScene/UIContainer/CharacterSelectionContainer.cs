using System;

using Microsoft.Xna.Framework;

using OdeMod.CardMode.Scenes.AboutScene.UIContainer;
using OdeMod.CardMode.Scenes.ConfigScene.ConfigSystem.Configs;
using OdeMod.CardMode.UI;
using OdeMod.UI.OdeUISystem.UIElements;

namespace OdeMod.CardMode.Scenes.CharacterSelectionScene.UIContainer
{
    internal class CharacterSelectionContainer : CardUIContainerElement
    {
        public static readonly string ContainerFullName = typeof(CharacterSelectionContainer).FullName;
        private float colorAlpha = 0f, waitToColorAlpha = 0f, speed = 1f;
        private bool waitToClose = false;
        private UIContainerPanel recordingConatiner;

        public override void OnInitialization()
        {
            base.OnInitialization();

            float startColorAlpha = 0.6f, waitToStartColorAlpha = 0.6f;
            UIImage start = new UIImage(CardSystem.GetCardTexture("Scene/CharacterSelectionScene/UI/CharacterSelectionButtom"),
                Color.White);
            start.Info.Left.SetValue(-start.Info.Width.Pixel / 2f, 0.8f);
            start.Info.Top.SetValue(-start.Info.Height.Pixel / 2f, 0.8f);
            start.Events.OnUpdate += (element, gt) =>
            {
                ((UIImage)element).ChangeColor(Color.White * (startColorAlpha * 0.4f + 0.6f) * colorAlpha);
                if (startColorAlpha != waitToStartColorAlpha)
                    startColorAlpha += (waitToStartColorAlpha - startColorAlpha) / 4f;
            };
            start.Events.OnLeftClick += element =>
            {
                startColorAlpha = 0.6f;
            };
            start.Events.OnMouseOver += element =>
            {
                waitToStartColorAlpha = 1f;
            };
            start.Events.OnMouseOut += element =>
            {
                waitToStartColorAlpha = 0.6f;
            };
            Register(start);

            BaseElement recordingMain = new BaseElement();
            recordingMain.Info.Width.SetValue(0f, 0.22f);
            recordingMain.Info.Height.SetValue(0f, 0.7f);
            recordingMain.Info.Left.SetValue(new PositionStyle(0f, 0.16f) - recordingMain.Info.Width / 2f);
            recordingMain.Info.Top.SetValue(new PositionStyle(0f, 0.5f) - recordingMain.Info.Height / 2f);
            Register(recordingMain);

            var fs = CardSystem.ConfigManager.GetConfig<InterfaceConfig>().Font;
            var top = new PositionStyle();
            UIText recordingTitle = new UIText("$Scenes.CharacterSelectionScene.UI.RecordingTitle", fs.GetFont(40f));
            recordingTitle.Info.Left.SetValue(new PositionStyle(0f, 0.5f) - recordingTitle.Info.Width / 2f);
            recordingTitle.Events.OnUpdate += (element, gt) =>
            {
                ((UIText)element).Color = Color.White * colorAlpha;
            };
            recordingMain.Register(recordingTitle);

            recordingConatiner = new UIContainerPanel();
            recordingConatiner.Info.Width.SetValue(0f, 1f);
            recordingConatiner.Info.Height.SetValue(new PositionStyle(-20f, 1f) - recordingTitle.Info.Height);
            recordingConatiner.Info.Top.SetValue(new PositionStyle(0f, 1f) - recordingConatiner.Info.Height);
            recordingMain.Register(recordingConatiner);

            UIVerticalScrollbar verticalScrollbar = new UIVerticalScrollbar();
            verticalScrollbar.Info.Width.SetValue(40f, 0f);
            verticalScrollbar.Info.Left.SetValue(0f, 1f);
            verticalScrollbar.Info.Height.SetValue(recordingConatiner.Info.Height);
            verticalScrollbar.Info.Top.SetValue(new PositionStyle(0f, 1f) - verticalScrollbar.Info.Height);
            verticalScrollbar.Events.OnUpdate += (element, gt) =>
            {
                ((UIVerticalScrollbar)element).Alpha = colorAlpha;
            };
            verticalScrollbar.AlwaysOnLight = true;
            verticalScrollbar.UseScrollWheel = true;
            recordingConatiner.SetVerticalScrollbar(verticalScrollbar);
            recordingMain.Register(verticalScrollbar);
        }

        public override void Show(params object[] args)
        {
            if (args.Length == 1 && args[0] is float v)
            {
                speed = v;
                if (speed <= 0f)
                    throw new ArgumentOutOfRangeException(nameof(args), speed, "值应大于0!");
                Info.IsVisible = true;
                waitToColorAlpha = 1f;
                colorAlpha = 0f;
                foreach (var i in CardSystem.Instance.CardModeUISystem.Elements)
                    if (i.Value == this)
                        CardSystem.Instance.CardModeUISystem.SetContainerTop(i.Key);
                Calculation();
            }
            else
            {
                speed = 1f;
                waitToColorAlpha = 1f;
                colorAlpha = 1f;
                base.Show(args);
            }
        }

        public override void Close(params object[] args)
        {
            if (args.Length == 1 && args[0] is float v)
            {
                speed = v;
                if (speed <= 0f)
                    throw new ArgumentOutOfRangeException(nameof(args), speed, "值应大于0!");
                waitToClose = true;
                waitToColorAlpha = 0f;
            }
            else
                base.Close(args);
        }

        public override void PreUpdate(GameTime gt)
        {
            base.PreUpdate(gt);
            if (colorAlpha != waitToColorAlpha)
                colorAlpha += (waitToColorAlpha - colorAlpha) / speed;
        }

        public override void Update(GameTime gt)
        {
            base.Update(gt);
            if (waitToClose && colorAlpha <= 0.01f)
            {
                waitToClose = false;
                Info.IsVisible = false;
            }
        }
    }
}