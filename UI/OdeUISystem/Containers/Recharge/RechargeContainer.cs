using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OdeMod.Items.Series.Recharge;
using OdeMod.UI.OdeUISystem.UIElements;
using OdeMod.UI.OriginalUISystem;

using ReLogic.Content;

using System;
using System.Collections.Generic;
using System.Linq;

using Terraria;
using Terraria.ModLoader;

namespace OdeMod.UI.OdeUISystem.Containers.Recharge
{
    internal class RechargeContainer : ContainerElement, IOriginalUIState
    {
        public RechargeItem[] RechargeItems;

        public RechargeContainer()
        {
            RechargeItems = new RechargeItem[3];
        }

        public override void OnInitialization()
        {
            base.OnInitialization();
            Info.IsVisible = true;

            UIImagePanel panel = new UIImagePanel(ModContent.Request<Texture2D>("OdeMod/UI/OdeUISystem/Containers/Recharge/Images/Panel",
                AssetRequestMode.ImmediateLoad).Value, Color.White);
            panel.Style = UIImage.CalculationStyle.LockAspectRatioMainWidth;
            panel.Info.Left.SetValue(-Info.Width.Pixel / 2f, 0.5f);
            panel.Info.Top.SetValue(-Info.Height.Pixel / 2f, 0.5f);
            panel.Info.SetMargin(11f);
            panel.CanDrag = true;
            Register(panel);

            UIImage closeButton = new UIImage(ModContent.Request<Texture2D>("OdeMod/UI/OdeUISystem/Containers/Recharge/Images/CloseButtonUp",
                AssetRequestMode.ImmediateLoad).Value, Color.White);
            closeButton.Info.Left.Pixel = 2f;
            closeButton.Info.Top.Pixel = 2f;
            closeButton.Events.OnLeftDown += element =>
                closeButton.ChangeImage(ModContent.Request<Texture2D>("OdeMod/UI/OdeUISystem/Containers/Recharge/Images/CloseButtonDown",
                AssetRequestMode.ImmediateLoad).Value);
            closeButton.Events.OnLeftUp += element =>
                closeButton.ChangeImage(ModContent.Request<Texture2D>("OdeMod/UI/OdeUISystem/Containers/Recharge/Images/CloseButtonUp",
                AssetRequestMode.ImmediateLoad).Value);
            closeButton.Events.OnLeftClick += element => Info.IsVisible = false;
            panel.Register(closeButton);

            RechargeItems[0] = new RechargeItem();
            RechargeItems[0].Info.Width.Pixel = -28f;
            RechargeItems[0].Info.Width.Percent = 1f;
            RechargeItems[0].Info.Left.Pixel = 14f;
            RechargeItems[0].Info.Top.Pixel = 5f;
            panel.Register(RechargeItems[0]);

            RechargeItems[1] = new RechargeItem();
            RechargeItems[1].Info.Width.Pixel = -28f;
            RechargeItems[1].Info.Width.Percent = 1f;
            RechargeItems[1].Info.Left.Pixel = 14f;
            RechargeItems[1].Info.Top.Pixel = 51f;
            panel.Register(RechargeItems[1]);

            RechargeItems[2] = new RechargeItem();
            RechargeItems[2].Info.Width.Pixel = -28f;
            RechargeItems[2].Info.Width.Percent = 1f;
            RechargeItems[2].Info.Left.Pixel = 14f;
            RechargeItems[2].Info.Top.Pixel = 97f;
            panel.Register(RechargeItems[2]);
        }

        public override void PreUpdate(GameTime gt)
        {
            base.PreUpdate(gt);
            var player = Main.LocalPlayer;
            Dictionary<Type, IRechargeableWeapon> weapons = new Dictionary<Type, IRechargeableWeapon>();
            List<Type> noLoadType = new List<Type>();
            IRechargeableWeapon weapon;
            Type type;
            foreach (var item in player.inventory)
            {
                if (item.ModItem is IRechargeableWeapon)
                {
                    weapon = (IRechargeableWeapon)item.ModItem;
                    type = item.ModItem.GetType();
                    if (!weapons.ContainsKey(type))
                    {
                        if (!noLoadType.Contains(type))
                            weapons.Add(type, weapon);
                    }
                    else
                    {
                        weapons.Remove(type);
                        if (!noLoadType.Contains(type))
                            noLoadType.Add(type);
                    }
                }
            }
            var rechargeableWeapons = weapons.Values.ToList();
            if (player.HeldItem != null && player.HeldItem.ModItem != null && player.HeldItem.ModItem is IRechargeableWeapon)
            {
                var x = (IRechargeableWeapon)player.HeldItem.ModItem;
                if (rechargeableWeapons.Contains(x))
                {
                    rechargeableWeapons.Remove(x);
                    rechargeableWeapons.Insert(0, x);
                }
            }

            if (rechargeableWeapons.Count >= 3)
            {
                SetRechargeWeapons(rechargeableWeapons.ToArray());
                return;
            }
            var w = new IRechargeableWeapon[3];
            for (int i = 0; i < rechargeableWeapons.Count; i++)
                w[i] = rechargeableWeapons[i];
            SetRechargeWeapons(w);
        }

        public void SetRechargeWeapons(IRechargeableWeapon[] rechargeItems)
        {
            for (int i = 0; i < RechargeItems.Length; i++)
                RechargeItems[i].SetRechargeWeapon(null);
            for (int i = 0; i < RechargeItems.Length; i++)
                RechargeItems[i].SetRechargeWeapon(rechargeItems[i]);
        }
    }
}