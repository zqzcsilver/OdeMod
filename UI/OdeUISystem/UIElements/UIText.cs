﻿using FontStashSharp;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OdeMod.Utils;

namespace OdeMod.UI.OdeUISystem.UIElements
{
    internal class UIText : BaseElement
    {
        private string text;

        public string Text
        {
            get { return text; }
            set
            {
                text = value;
                Calculation();
            }
        }

        public string DisplayText => LanguageHelper.GetPrefixTextValue(text);

        private DynamicSpriteFont font;
        public DynamicSpriteFont Font { get => font; set => font = value; }
        public Color Color;

        public bool CalculateSize = true;
        public PositionStyle? CenterX;
        public PositionStyle? CenterY;
        public bool CalculationCenter = true;

        public UIText(string t, DynamicSpriteFont spriteFont)
        {
            text = t;
            font = spriteFont;
            Color = Color.White;

            Vector2 size = font.MeasureString(DisplayText);
            Info.Width.Pixel = size.X;
            Info.Height.Pixel = size.Y;
        }

        public UIText(string t, DynamicSpriteFont spriteFont, Color textColor)
        {
            text = t;
            font = spriteFont;
            Color = textColor;

            Vector2 size = font.MeasureString(DisplayText);
            Info.Width.Pixel = size.X;
            Info.Height.Pixel = size.Y;
        }

        public void ChangeFont(DynamicSpriteFont spriteFont)
        {
            font = spriteFont;
            Calculation();
        }

        public override void Calculation()
        {
            base.Calculation();
            if (CalculateSize)
            {
                Vector2 size = font.MeasureString(DisplayText);
                Info.Width.Pixel = size.X;
                Info.Height.Pixel = size.Y;
                Info.Width.Percent = 0f;
                Info.Height.Percent = 0f;
            }
            if (CalculationCenter && CenterX != null && CenterY != null)
            {
                var x = CenterX.Value;
                var y = CenterY.Value;
                Info.Left.Percent = x.Percent;
                Info.Top.Percent = y.Percent;
                Info.Left.Pixel = x.Pixel - Info.Width.Pixel / 2f;
                Info.Top.Pixel = y.Pixel - Info.Height.Pixel / 2f;
            }
            base.Calculation();
        }

        protected override void DrawSelf(SpriteBatch sb)
        {
            base.DrawSelf(sb);
            sb.DrawString(font, DisplayText,
                Info.Location, Color);
        }
    }
}