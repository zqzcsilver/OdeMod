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
        private UIImage[,] chart = new UIImage[5, 5];
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
            panel.CanDrag = false;
            Register(panel); 
            
            UIImage closeButton = new UIImage(ModContent.Request<Texture2D>("OdeMod/UI/OdeUISystem/Containers/Recharge/Images/XButtonUp",
                AssetRequestMode.ImmediateLoad).Value, Color.White);
            closeButton.Info.Left.Percent = 0.9f;
            closeButton.Info.Top.Percent=0.02f;
            closeButton.Info.Width.Pixel = 44f;
            closeButton.Info.Height.Pixel = 44f;
            closeButton.Events.OnLeftDown += element =>
                closeButton.ChangeImage(ModContent.Request<Texture2D>("OdeMod/UI/OdeUISystem/Containers/Recharge/Images/XButtonDown",
                AssetRequestMode.ImmediateLoad).Value);
            closeButton.Events.OnLeftUp += element =>
                closeButton.ChangeImage(ModContent.Request<Texture2D>("OdeMod/UI/OdeUISystem/Containers/Recharge/Images/XButtonUp",
                AssetRequestMode.ImmediateLoad).Value);
            closeButton.Events.OnLeftClick += element => Info.IsVisible = false;
            panel.Register(closeButton);

            for (int i = 0; i < 5; i++) 
            {
                for (int j = 0; j < 5; j++) 
                {
                    chart[i, j] = new UIImage(ModContent.Request<Texture2D>("OdeMod/UI/OdeUISystem/Containers/Recharge/Images/PaintArea",
                AssetRequestMode.ImmediateLoad).Value, new Color(0, 0, 0, 0.1f));
                    chart[i, j].Info.Width.SetValue(64f, 0f);
                    chart[i, j].Info.Height.SetValue(64f, 0f);
                    chart[i, j].Info.Left.SetValue(80f + j * 70f, 0f);
                    chart[i, j].Info.Top.SetValue(35f + i * 70f, 0f);
                    chart[i, j].Events.OnUpdate += element =>
                    {
                        if(Main.mouseLeft)
                        {
                            ((UIImage)element).ChangeColor(new Color(0, 0, 0, 1f));
                        }
                        if(Main.mouseRight)
                        {
                            ((UIImage)element).ChangeColor(new Color(0, 0, 0, 0.1f));
                        }
                    };
                    //chart[i, j].Events.OnLeftClick += element =>
                    //    ((UIImage)element).ChangeColor(new Color(0, 0, 0, 1f));
                    //chart[i, j].Events.OnRightClick += element =>
                    //    ((UIImage)element).ChangeColor(new Color(0, 0, 0, 0.1f));
                    panel.Register(chart[i, j]);
                }
            }
        }

        public override void PreUpdate(GameTime gt)
        {
            base.PreUpdate(gt);
            var player = Main.LocalPlayer;
            

        }
    }
}