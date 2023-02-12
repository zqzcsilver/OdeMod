using System.Text;

using Microsoft.Xna.Framework;

using OdeMod.UI.OdeUISystem.UIElements;

using Terraria;

namespace OdeMod.CardMode.UI.Map
{
    internal class MapGuiPanel : CardUIContainerElement
    {
        public override bool IsVisible => CardSystem.Instance.CardModeVisible;
        private UIText text;
        private MapElement map;
        private UIContainerPanel containerPanel;

        public override void OnInitialization()
        {
            base.OnInitialization();
            UIPanel panel = new UIPanel();
            panel.Info.Width.SetValue(0f, 0.6f);
            panel.Info.Height.SetValue(0f, 0.86f);
            panel.Info.Left.SetValue(0f, (1f - panel.Info.Width.Percent) / 2f);
            panel.Info.Top.SetValue(0f, (1f - panel.Info.Height.Percent) / 2f);
            Register(panel);

            containerPanel = new UIContainerPanel();
            containerPanel.Info.Width.SetValue(0f, 1f);
            containerPanel.Info.Height.SetValue(0f, 1f);
            panel.Register(containerPanel);

            VerticalScrollbar verticalScrollbar = new VerticalScrollbar();
            verticalScrollbar.Info.Height.SetValue(panel.Info.Height);
            verticalScrollbar.Info.Left.SetValue(panel.Info.Left + panel.Info.Width +
                verticalScrollbar.Info.Width + new PositionStyle(10f, 0f));
            verticalScrollbar.Info.Top.SetValue(panel.Info.Top);
            verticalScrollbar.UseScrollWheel = true;
            Register(verticalScrollbar);
            containerPanel.SetVerticalScrollbar(verticalScrollbar);

            HorizontalScrollbar horizontalScrollbar = new HorizontalScrollbar();
            horizontalScrollbar.Info.Width.SetValue(panel.Info.Width);
            horizontalScrollbar.Info.Left.SetValue(panel.Info.Left);
            horizontalScrollbar.Info.Top.SetValue(panel.Info.Top + panel.Info.Height +
                horizontalScrollbar.Info.Height + new PositionStyle(10f, 0f));
            Register(horizontalScrollbar);
            containerPanel.SetHorizontalScrollbar(horizontalScrollbar);

            map = new MapElement(null, containerPanel.Info.Size);
            containerPanel.AddElement(map);

            text = new UIText("--", OdeMod.DefaultFontSystem.GetFont(20f));
            text.Events.OnLeftClick += Events_OnLeftClick;
            Register(text);

            UIText exitText = new UIText("退出", OdeMod.DefaultFont);
            UIPanel exitButton = new UIPanel();
            exitButton.Info.SetMargin(0f);
            exitButton.Register(exitText);
            exitButton.Info.Width.SetValue(exitText.Info.Width);
            exitButton.Info.Height.SetValue(exitText.Info.Height);
            exitButton.Info.Left.SetValue(-exitButton.Info.Width.Pixel, 1f);
            exitButton.Info.IsSensitive = true;
            exitButton.Events.OnLeftClick += Events_OnLeftClick1;
            Register(exitButton);
        }

        private void Events_OnLeftClick1(BaseElement baseElement)
        {
            CardSystem.Instance.CardModeVisible = false;
        }

        private void Events_OnLeftClick(BaseElement baseElement)
        {
        }

        public override void Update(GameTime gt)
        {
            base.Update(gt);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Ode Mod-Card Mode System Test Gui");
            stringBuilder.AppendLine();

            stringBuilder.AppendLine("---------- Screen Info ----------");
            stringBuilder.AppendLine($"Screen Size:{Main.ScreenSize}");
            stringBuilder.AppendLine();

            stringBuilder.AppendLine("---------- Mouse Info ----------");
            stringBuilder.AppendLine($"Mouse Screen Position: {Main.MouseScreen}");
            stringBuilder.AppendLine($"Mouse Left Down: {Main.mouseLeft}");
            stringBuilder.AppendLine($"Mouse Right Down: {Main.mouseRight}");
            stringBuilder.AppendLine($"Mouse Left Click: {CardSystem.GetMouseInfo.MouseLeftClick}");
            stringBuilder.AppendLine($"Mouse Right Click: {CardSystem.GetMouseInfo.MouseRightClick}");
            stringBuilder.AppendLine($"Mouse Left Double Click: {CardSystem.GetMouseInfo.MouseLeftDoubleClick}");
            stringBuilder.AppendLine($"Mouse Right Double Click: {CardSystem.GetMouseInfo.MouseRightDoubleClick}");
            stringBuilder.AppendLine();

            stringBuilder.AppendLine("---------- Map Info ----------");
            stringBuilder.AppendLine($"Map Size: {CardSystem.Instance.Map.MapSize}");
            stringBuilder.AppendLine($"Map Total Size: {CardSystem.Instance.Map.MaxSize}");
            stringBuilder.AppendLine($"Map Draw Offset: {CardSystem.Instance.Map.DrawOffset}");
            stringBuilder.AppendLine();

            text.Text = stringBuilder.ToString();

            map.UpdateMapDrawSize(containerPanel.Info.Size);
            map.UpdateMapDrawOffset(containerPanel.Info.Location - map.Info.Location);

            map.SetMap(CardSystem.Instance.Map);
            map.Calculation();
        }
    }
}