﻿using Microsoft.Xna.Framework;

namespace OdeMod.UI.OdeUISystem.UIElements
{
    internal class UIContainerPanel : BaseElement
    {
        private class InnerPanel : BaseElement
        {
            public override Rectangle HiddenOverflowRectangle => ParentElement.HiddenOverflowRectangle;

            public override Rectangle GetCanHitBox() => Rectangle.Intersect(ParentElement.GetCanHitBox(), ParentElement.Info.TotalHitBox);

            public InnerPanel()
            {
                Info.Width.Percent = 1f;
                Info.Height.Percent = 1f;
            }
        }

        private InnerPanel _innerPanel;
        public UIVerticalScrollbar UIVerticalScrollbar => _verticalScrollbar;
        private UIVerticalScrollbar _verticalScrollbar;
        public UIHorizontalScrollbar UIHorizontalScrollbar => _horizontalScrollbar;
        private UIHorizontalScrollbar _horizontalScrollbar;
        private float verticalWhellValue;
        private float horizontalWhellValue;
        private Vector2 innerPanelMinLocation;
        private Vector2 innerPanelMaxLocation;

        public UIContainerPanel()
        {
            Info.HiddenOverflow = true;
            Info.Width.Percent = 1f;
            Info.Height.Percent = 1f;
            Info.SetMargin(4f);
        }

        public void SetVerticalScrollbar(UIVerticalScrollbar scrollbar) => _verticalScrollbar = scrollbar;

        public void SetHorizontalScrollbar(UIHorizontalScrollbar scrollbar) => _horizontalScrollbar = scrollbar;

        public override void OnInitialization()
        {
            base.OnInitialization();
            _innerPanel = new InnerPanel();
            Register(_innerPanel);
        }

        public override void Update(GameTime gt)
        {
            base.Update(gt);
            if (_verticalScrollbar != null && verticalWhellValue != _verticalScrollbar.WheelValue)
            {
                verticalWhellValue = _verticalScrollbar.WheelValue;
                float maxY = innerPanelMaxLocation.Y - _innerPanel.Info.TotalSize.Y;
                if (maxY < innerPanelMinLocation.Y)
                    maxY = innerPanelMinLocation.Y;
                _innerPanel.Info.Top.Pixel = -MathHelper.Lerp(innerPanelMinLocation.Y, maxY, verticalWhellValue);
                Calculation();
            }

            if (_horizontalScrollbar != null && horizontalWhellValue != _horizontalScrollbar.WheelValue)
            {
                horizontalWhellValue = _horizontalScrollbar.WheelValue;
                float maxX = innerPanelMaxLocation.X - _innerPanel.Info.TotalSize.X;
                if (maxX < innerPanelMinLocation.X)
                    maxX = innerPanelMinLocation.X;
                _innerPanel.Info.Left.Pixel = -MathHelper.Lerp(innerPanelMinLocation.X, maxX, horizontalWhellValue);
                Calculation();
            }
        }

        public bool AddElement(BaseElement element)
        {
            bool flag = _innerPanel.Register(element);
            if (flag)
                Calculation();
            return flag;
        }

        public bool RemoveElement(BaseElement element)
        {
            bool flag = _innerPanel.Remove(element);
            if (flag)
                Calculation();
            return flag;
        }

        public void ClearAllElements()
        {
            _innerPanel.RemoveAll();
            Calculation();
        }

        private void CalculationInnerPanelSize()
        {
            innerPanelMinLocation = Vector2.Zero;
            innerPanelMaxLocation = Vector2.Zero;
            Vector2 v = Vector2.Zero;
            _innerPanel.ForEach(element =>
            {
                v.X = element.Info.TotalLocation.X - _innerPanel.Info.Location.X;
                v.Y = element.Info.TotalLocation.Y - _innerPanel.Info.Location.Y;
                if (innerPanelMinLocation.X > v.X)
                    innerPanelMinLocation.X = v.X;
                if (innerPanelMinLocation.Y > v.Y)
                    innerPanelMinLocation.Y = v.Y;

                v.X = element.Info.TotalLocation.X + element.Info.TotalSize.X - _innerPanel.Info.Location.X;
                v.Y = element.Info.TotalLocation.Y + element.Info.TotalSize.Y - _innerPanel.Info.Location.Y;

                if (innerPanelMaxLocation.X < v.X)
                    innerPanelMaxLocation.X = v.X;
                if (innerPanelMaxLocation.Y < v.Y)
                    innerPanelMaxLocation.Y = v.Y;
            });
            if (_verticalScrollbar != null)
                _verticalScrollbar.WhellValueMult = MathHelper.Max(0f, _innerPanel.Info.TotalSize.Y / (innerPanelMaxLocation.Y - innerPanelMinLocation.Y) * 5f);
        }

        public override void Calculation()
        {
            base.Calculation();
            CalculationInnerPanelSize();
            _innerPanel.Calculation();
        }
    }
}