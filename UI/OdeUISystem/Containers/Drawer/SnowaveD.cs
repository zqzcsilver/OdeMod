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
using OdeMod.Utils;
using System.Reflection;

namespace OdeMod.UI.OdeUISystem.Containers.Drawer
{
    internal class SnowaveD : UIContainerElement, IOriginalUIState
    {
        MasterPiece instance = new MasterPiece();
        private UIImage[,] chart = new UIImage[5, 5];
        private int[,] draw = new int[5, 5];
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
                    chart[i, j].Info.Left.SetValue(80f + j * 64f, 0f);
                    chart[i, j].Info.Top.SetValue(35f + i * 64f, 0f);
                    chart[i, j].Events.OnMouseHover += element =>
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

        private static bool AreArraysEqual<T>(T[,] array1, T[,] array2)
        {
            if (array1 == null || array2 == null)
            {
                return false;
            }

            if (array1.GetLength(0) != array2.GetLength(0) || array1.GetLength(1) != array2.GetLength(1))
            {
                return false;
            }

            for (int i = 0; i < array1.GetLength(0); i++)
            {
                for (int j = 0; j < array1.GetLength(1); j++)
                {
                    if (!EqualityComparer<T>.Default.Equals(array1[i, j], array2[i, j]))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public override void Update(GameTime gt)
        {
            
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    var color = chart[i, j].GetColor();

                    if (color.A == 255) 
                    {
                        draw[i, j] = 1;
                    }
                    else
                    {
                        draw[i, j] = 0;
                    }
                    
                }
               
            }
            
            Type myClassType = typeof(MasterPiece);
            FieldInfo[] fields = myClassType.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (FieldInfo field in fields)
            {
                if (field.FieldType.IsArray && field.FieldType.GetArrayRank() == 2)
                {
                    // 获取字段名称
                    string fieldName = field.Name;

                    // 将字段的值转换为二维数组
                    int[,] array = (int[,])field.GetValue(instance);

                    // 在这里对二维数组进行处理
                    if (AreArraysEqual(draw, array))
                    {
                        Main.NewText(fieldName);
                        break;
                    }
                }
            }
        }
    }
}