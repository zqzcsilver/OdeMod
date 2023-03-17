using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OdeMod.UI.OdeUISystem.UIElements;
using OdeMod.Utils;
using OdeMod.Utils.Geometry;

using Terraria;

namespace OdeMod.CardMode.Scenes.ConfigScene.UIElements
{
    internal class UISeekBar : BaseElement
    {
        internal delegate void ValueChange(UISeekBar seekBar, float value);

        public float Value
        {
            get => _value;
            set
            {
                if (IsChanging)
                    return;
                _value = value;
                if (_value < 0f)
                    _value = 0f;
                if (_value > 1f)
                    _value = 1f;
                _waitToValue = _value;
            }
        }

        private float _value;
        private float _waitToValue;
        public Vector2 RightTriangleSize, CursorOffset, LeftLineSize;
        public float BarHeight, CursorHeight;

        public Color LeftLineColor = new Color(97, 205, 228), RightTriangleColor = new Color(97, 205, 228),
            CursorColor = new Color(98, 157, 254), BarColor = new Color(100, 193, 218);

        public bool IsChanging { get; private set; }

        public event ValueChange OnValueChange;

        private bool dragging = false;

        public UISeekBar(float initialValue)
        {
            Value = initialValue;
        }

        public override void Calculation()
        {
            base.Calculation();
            LeftLineSize = new Vector2(Info.Size.Y * 0.1f, Info.Size.Y * 0.8f);
            RightTriangleSize = new Vector2(Info.Size.Y * 0.46f, Info.Size.Y * 0.7f);
            BarHeight = Info.Size.Y * 0.1f;
            CursorHeight = Info.Size.Y * 0.6f;
            CursorOffset = new Vector2(CursorHeight * 0.46f, /*-CursorHeight * 0.24f*/0f);
        }

        public override void LoadEvents()
        {
            base.LoadEvents();
            Events.OnLeftDown += (element) =>
            {
                dragging = true;
                IsChanging = true;
            };
            Events.OnLeftUp += (element) =>
            {
                dragging = false;
                IsChanging = false;
            };
        }

        public override void Update(GameTime gt)
        {
            base.Update(gt);
            if (dragging)
            {
                float v = (Main.mouseX - Info.Location.X - LeftLineSize.X - CursorOffset.X) /
                    (Info.Size.X - LeftLineSize.X - RightTriangleSize.X - CursorOffset.X * 2f);
                if (_waitToValue != v)
                {
                    OnValueChange?.Invoke(this, v);
                    _waitToValue = v;
                    if (_waitToValue < 0f)
                        _waitToValue = 0f;
                    if (_waitToValue > 1f)
                        _waitToValue = 1f;
                }
            }
            if (_value != _waitToValue)
                _value += (_waitToValue - _value) / 4f;
        }

        protected override void DrawSelf(SpriteBatch sb)
        {
            base.DrawSelf(sb);

            var x = DrawUtils.GetDrawRenderTarget(sb, (sb) =>
            {
                float usefulRange = Info.Size.X - LeftLineSize.X - RightTriangleSize.X - CursorOffset.X * 2f;
                DrawUtils.DrawTriangles(sb, new List<Triangle>()
                {
                    //左竖线绘制
                    new Triangle(new Vector2(0f, Info.Size.Y / 2f - LeftLineSize.Y / 2f),
                    new Vector2(LeftLineSize.X, Info.Size.Y / 2f + LeftLineSize.Y / 2f),
                    new Vector2(0, Info.Size.Y / 2f + LeftLineSize.Y / 2f)),

                    new Triangle(new Vector2(0f, Info.Size.Y / 2f - LeftLineSize.Y / 2f),
                    new Vector2(LeftLineSize.X, Info.Size.Y / 2f - LeftLineSize.Y / 2f),
                    new Vector2(LeftLineSize.X, Info.Size.Y / 2f + LeftLineSize.Y / 2f)),

                    //右三角绘制
                    new Triangle(new Vector2(Info.Size.X - RightTriangleSize.X, 0f),
                    new Vector2(Info.Size.X - RightTriangleSize.X, Info.Size.Y),
                    new Vector2(Info.Size.X, Info.Size.Y / 2f)),
                    //Bar绘制
                    new Triangle(new Vector2(LeftLineSize.X, Info.Size.Y / 2f + BarHeight / 2f),
                    new Vector2(Info.Size.X - RightTriangleSize.X, Info.Size.Y / 2f + BarHeight / 2f),
                    new Vector2(Info.Size.X - RightTriangleSize.X, Info.Size.Y / 2f - BarHeight / 2f)),

                    new Triangle(new Vector2(LeftLineSize.X, Info.Size.Y / 2f + BarHeight / 2f),
                    new Vector2(LeftLineSize.X, Info.Size.Y / 2f - BarHeight / 2f),
                    new Vector2(Info.Size.X - RightTriangleSize.X, Info.Size.Y / 2f - BarHeight / 2f)),

                    //指针绘制
                    new Triangle(new Vector2(LeftLineSize.X + usefulRange * Value + CursorOffset.X, Info.Size.Y / 2f - CursorHeight / 2f),
                    new Vector2(LeftLineSize.X + usefulRange * Value + CursorOffset.X, Info.Size.Y / 2f + CursorHeight / 2f),
                    new Vector2(LeftLineSize.X + usefulRange * Value + CursorOffset.X, Info.Size.Y / 2f) + CursorOffset),

                    new Triangle(new Vector2(LeftLineSize.X + usefulRange * Value + CursorOffset.X, Info.Size.Y / 2f - CursorHeight / 2f),
                    new Vector2(LeftLineSize.X + usefulRange * Value + CursorOffset.X, Info.Size.Y / 2f + CursorHeight / 2f),
                    new Vector2(LeftLineSize.X + usefulRange * Value - CursorOffset.X + CursorOffset.X, Info.Size.Y / 2f + CursorOffset.Y)),
                },
                new List<Color>()
                {
                    LeftLineColor,LeftLineColor,RightTriangleColor,BarColor,BarColor,CursorColor,CursorColor
                },
                new List<float>()
                {
                    0f,0f,0f,0f,0f,0f,0f
                }, true);
            }, Main.screenWidth, Main.screenHeight);

            sb.Draw(x, Info.Location, Color.White);
        }
    }
}