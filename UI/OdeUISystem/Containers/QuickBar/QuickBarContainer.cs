using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OdeMod.CardMode;
using OdeMod.UI.OdeUISystem.UIElements;
using OdeMod.Utils;

using Terraria;
using Terraria.ModLoader;

namespace OdeMod.UI.OdeUISystem.Containers.QuickBar
{
    internal class QuickBarContainer : UIContainerElement
    {
        private UIPanel mainPanel;
        private bool open = true;
        private KeyCooldown cardModeVisibleCoolDown = new KeyCooldown(() => true, 14);

        public override void OnInitialization()
        {
            base.OnInitialization();

            mainPanel = new UIPanel();
            mainPanel.Info.Width.SetValue(50f, 0f);
            mainPanel.Events.OnCalculation += element =>
            {
                mainPanel.Info.Top.SetValue(-mainPanel.Info.Size.Y / 2f, 0.5f);
            };
            mainPanel.Info.SetMargin(0f);
            Register(mainPanel);

            QuickBar quickBar = new QuickBar();
            quickBar.Info.Width.SetValue(0f, 0.7f);
            quickBar.Info.Left.SetValue(0f, 0.15f);
            quickBar.Events.OnCalculation += element =>
            {
                mainPanel.Info.Height.SetValue(element.Info.Size.Y + mainPanel.Info.TopMargin.Pixel + mainPanel.Info.ButtomMargin.Pixel,
                    0f + mainPanel.Info.TopMargin.Percent + mainPanel.Info.ButtomMargin.Percent);
            };
            mainPanel.Register(quickBar);

            TriggeredTypeQuickElement quickElement = new TriggeredTypeQuickElement(
                    ModContent.Request<Texture2D>("OdeMod/Items/Series/Recharge/ElectrifiedBlasterCannon",
                    ReLogic.Content.AssetRequestMode.ImmediateLoad).Value, Color.White);
            quickElement.Events.OnUpdate += (element, gt) =>
            {
                var e = OdeMod.OdeUISystem.Elements["OdeMod.UI.OdeUISystem.Containers.Recharge.RechargeContainer"];
                ((UIImage)element).ChangeColor(Color.White * (e.Info.IsVisible ? 1f : 0.6f));
            };
            quickElement.OnTigger += element =>
            {
                var e = OdeMod.OdeUISystem.Elements["OdeMod.UI.OdeUISystem.Containers.Recharge.RechargeContainer"];
                if (!e.Info.IsVisible)
                    e.Show();
                else
                    e.Info.IsVisible = false;
            };
            quickBar.Register(quickElement);

            quickElement = new TriggeredTypeQuickElement(
                    ModContent.Request<Texture2D>("OdeMod/Images/Card/Original/Room/EyeballIcon",
                    ReLogic.Content.AssetRequestMode.ImmediateLoad).Value, Color.White);
            quickElement.OnTigger += element =>
            {
                if (cardModeVisibleCoolDown.IsCoolDown())
                {
                    CardSystem.Instance.CardModeVisible = true;
                    cardModeVisibleCoolDown.ResetCoolDown();
                }
            };
            quickBar.Register(quickElement);

            UIImage image = new UIImage(
                ModContent.Request<Texture2D>("OdeMod/UI/OdeUISystem/Containers/QuickBar/Images/array",
                ReLogic.Content.AssetRequestMode.ImmediateLoad).Value, Color.White);
            image.Info.Left.SetValue(2f, 1f);
            image.Info.Top.SetValue(-image.Info.Height.Pixel / 2f, 0.5f);
            image.Events.OnUpdate += (element, gt) =>
            {
                if (mainPanel.Info.TotalLocation.X - mainPanel.Info.TotalSize.X < 4f)
                    ((UIImage)element).SpriteEffects = SpriteEffects.FlipHorizontally;
                if (mainPanel.Info.TotalLocation.X < 2f && mainPanel.Info.TotalLocation.X > -2f)
                    ((UIImage)element).SpriteEffects = SpriteEffects.None;
            };
            image.Events.OnLeftClick += element =>
            {
                open = !open;
            };
            mainPanel.Register(image);
        }

        public override void PreUpdate(GameTime gt)
        {
            base.PreUpdate(gt);
            Info.IsVisible = !Main.playerInventory;
        }

        public override void Update(GameTime gt)
        {
            base.Update(gt);

            cardModeVisibleCoolDown.Update();

            if (!open && mainPanel.Info.TotalLocation.X != -mainPanel.Info.TotalSize.X)
            {
                mainPanel.Info.Left.SetValue(mainPanel.Info.TotalLocation.X -
                    (mainPanel.Info.TotalLocation.X + mainPanel.Info.TotalSize.X) / 4f, 0f);
            }
            if (open && mainPanel.Info.TotalLocation.X != 0)
            {
                mainPanel.Info.Left.SetValue(mainPanel.Info.TotalLocation.X -
                    mainPanel.Info.TotalLocation.X / 4f, 0f);
            }
            mainPanel.Calculation();
        }
    }
}