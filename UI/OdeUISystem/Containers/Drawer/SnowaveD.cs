using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OdeMod.CardMode.Scenes.AboutScene.UIContainer;
using OdeMod.Items.Series.Recharge;
using OdeMod.UI.OdeUISystem.UIElements;
using OdeMod.UI.OriginalUISystem;

using ReLogic.Content;

using System;
using System.Collections.Generic;
using System.Linq;

using Terraria;
using Terraria.ModLoader;

namespace OdeMod.UI.OdeUISystem.Containers.Drawer
{
    internal class SnowaveD : UIContainerElement, IOriginalUIState
    {

        public override void OnInitialization()
        {
            base.OnInitialization();
            UIImagePanel panel = new UIImagePanel(ModContent.Request<Texture2D>("OdeMod/UI/OdeUISystem/Containers/Recharge/Images/SnowavePaper",
                AssetRequestMode.ImmediateLoad).Value, Color.White);
            panel.Style = UIImage.CalculationStyle.LockAspectRatioMainWidth;
            panel.Info.Width.SetValue(660f, 0f);
            panel.Info.Height.SetValue(504f, 0f);
            panel.Info.Left.SetValue(-panel.Info.Width.Pixel / 2f, 0.5f);
            panel.Info.Top.SetValue(-panel.Info.Height.Pixel / 2f, 0.5f);
 
            panel.Info.SetMargin(20f);
            panel.CanDrag = true;
            Register(panel); 
            
            UIImage closeButton = new UIImage(ModContent.Request<Texture2D>("OdeMod/UI/OdeUISystem/Containers/Recharge/Images/CloseButtonUp",
                AssetRequestMode.ImmediateLoad).Value, Color.White);
            closeButton.Info.Left.Pixel = 2f;
            closeButton.Info.Top.Pixel = 2f;
            closeButton.Info.Width.Pixel = 10f;
            closeButton.Info.Height.Pixel = 10f;
            closeButton.Events.OnLeftDown += element =>
                closeButton.ChangeImage(ModContent.Request<Texture2D>("OdeMod/UI/OdeUISystem/Containers/Recharge/Images/CloseButtonDown",
                AssetRequestMode.ImmediateLoad).Value);
            closeButton.Events.OnLeftUp += element =>
                closeButton.ChangeImage(ModContent.Request<Texture2D>("OdeMod/UI/OdeUISystem/Containers/Recharge/Images/CloseButtonUp",
                AssetRequestMode.ImmediateLoad).Value);
            closeButton.Events.OnLeftClick += element => Info.IsVisible = false;
            panel.Register(closeButton);

        }

        public override void PreUpdate(GameTime gt)
        {
            base.PreUpdate(gt);
            var player = Main.LocalPlayer;
            

        }
    }
}