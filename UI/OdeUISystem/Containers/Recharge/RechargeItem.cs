using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OdeMod.Items.Series.Recharge;
using OdeMod.UI.OdeUISystem.UIElements;

using ReLogic.Content;

using System;

using Terraria.ModLoader;

namespace OdeMod.UI.OdeUISystem.Containers.Recharge
{
    internal class RechargeItem : BaseElement
    {
        private UIItemSlot[] UIItemSlots;
        private IRechargeableWeapon RechargeableWeapon;

        public RechargeItem()
        {
            UIItemSlots = new UIItemSlot[2];
            Info.Height.Pixel = 46f;
        }

        public override void OnInitialization()
        {
            UIItemSlots[0] = new UIItemSlot(ModContent.Request<Texture2D>("OdeMod/UI/OdeUISystem/Containers/Recharge/Images/ItemSlot",
                AssetRequestMode.ImmediateLoad).Value);
            UIItemSlots[0].Info.Width.Pixel = 32f;
            UIItemSlots[0].Info.Height.Pixel = 32f;
            UIItemSlots[0].Info.Left.Percent = 0.5f;
            UIItemSlots[0].Info.Top.Pixel = 7f;
            Register(UIItemSlots[0]);

            UIItemSlots[1] = new UIItemSlot(ModContent.Request<Texture2D>("OdeMod/UI/OdeUISystem/Containers/Recharge/Images/ItemSlot",
                AssetRequestMode.ImmediateLoad).Value);
            UIItemSlots[1].Info.Width.Pixel = 32f;
            UIItemSlots[1].Info.Height.Pixel = 32f;
            UIItemSlots[1].Info.Left.Percent = 0.75f;
            UIItemSlots[1].Info.Top.Pixel = 7f;
            Register(UIItemSlots[1]);
            base.OnInitialization();
        }

        public override void LoadEvents()
        {
            base.LoadEvents();
            UIItemSlots[0].CanPutInSlot += item => RechargeableWeapon != null &&
                item.ModItem != null && item.ModItem is IRechargeAccessory;
            UIItemSlots[0].OnPutItem += element =>
                RechargeableWeapon.RechargeAccessories[0] = (IRechargeAccessory)UIItemSlots[0].ContainedItem.ModItem;
            UIItemSlots[0].OnPickItem += element =>
                RechargeableWeapon.RechargeAccessories[0] = null;
            UIItemSlots[1].CanPutInSlot += item =>
                RechargeableWeapon != null && item.ModItem != null && item.ModItem is IRechargeAccessory;
            UIItemSlots[1].OnPutItem += element =>
                RechargeableWeapon.RechargeAccessories[1] = (IRechargeAccessory)UIItemSlots[1].ContainedItem.ModItem;
            UIItemSlots[1].OnPickItem += element =>
                RechargeableWeapon.RechargeAccessories[1] = null;
        }

        public override void PreUpdate(GameTime gt)
        {
            base.PreUpdate(gt);
            if (RechargeableWeapon == null)
                return;

            float mult = 1f, add = 0f;
            Array.ForEach(RechargeableWeapon.RechargeAccessories, acc =>
            {
                if (acc == null)
                    return;
                mult *= acc.RechargeSpeedMult;
                add += acc.RechargeSpeedAdd;
            });
            if (RechargeableWeapon.EnergyMax > RechargeableWeapon.Energy)
                RechargeableWeapon.Energy += RechargeableWeapon.RechargeSpeed * mult + add;
            else
                RechargeableWeapon.Energy = RechargeableWeapon.EnergyMax;
        }

        public void SetRechargeWeapon(IRechargeableWeapon rechargeableWeapon)
        {
            if (rechargeableWeapon == null)
            {
                RechargeableWeapon = null;
                UIItemSlots[0].ContainedItem = null;
                UIItemSlots[1].ContainedItem = null;
                return;
            }
            RechargeableWeapon = rechargeableWeapon;
            if (RechargeableWeapon.RechargeAccessories[0] != null)
                UIItemSlots[0].ContainedItem = ((ModItem)RechargeableWeapon.RechargeAccessories[0]).Item;
            if (RechargeableWeapon.RechargeAccessories[1] != null)
                UIItemSlots[1].ContainedItem = ((ModItem)RechargeableWeapon.RechargeAccessories[1]).Item;
        }

        protected override void DrawChildren(SpriteBatch sb)
        {
            base.DrawChildren(sb);
            if (RechargeableWeapon == null)
                return;
            sb.End();
            sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap,
                DepthStencilState.None, null, null, Terraria.Main.UIScaleMatrix);

            var bar = RechargeableWeapon.RechargeBar;
            var pos = Info.Location + new Vector2(Info.Size.X / 2f - bar.Width, Info.Size.Y - bar.Height) / 2f;
            var innerSource = RechargeableWeapon.RechargeBarInnerSource;
            innerSource.Width = (int)(innerSource.Width * RechargeableWeapon.Energy / RechargeableWeapon.EnergyMax);
            sb.Draw(bar, pos, Color.White);
            sb.Draw(RechargeableWeapon.RechargeBarInner, pos + RechargeableWeapon.RechargeBarInnerOffset, innerSource, Color.White);
        }
    }
}