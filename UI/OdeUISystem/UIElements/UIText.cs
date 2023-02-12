using FontStashSharp;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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

        private DynamicSpriteFont font;
        public Color Color;

        public bool CalculateSize = true;
        public PositionStyle? CenterX;
        public PositionStyle? CenterY;

        public UIText(string t, DynamicSpriteFont spriteFont)
        {
            text = t;
            font = spriteFont;
            Color = Color.White;

            Vector2 size = font.MeasureString(text);
            Info.Width.Pixel = size.X;
            Info.Height.Pixel = size.Y;
        }

        public UIText(string t, DynamicSpriteFont spriteFont, Color textColor)
        {
            text = t;
            font = spriteFont;
            Color = textColor;

            Vector2 size = font.MeasureString(text);
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
            if (CalculateSize)
            {
                Vector2 size = font.MeasureString(text);
                Info.Width.Pixel = size.X;
                Info.Height.Pixel = size.Y;
                Info.Width.Percent = 0f;
                Info.Height.Percent = 0f;

                if (CenterX != null && CenterY != null)
                {
                    var x = CenterX.Value;
                    var y = CenterY.Value;
                    Info.Left.Percent = x.Percent;
                    Info.Top.Percent = y.Percent;
                    Info.Left.Pixel = x.Pixel - Info.Width.Pixel / 2f;
                    Info.Top.Pixel = y.Pixel - Info.Height.Pixel / 2f;
                }
            }
            base.Calculation();
        }

        protected override void DrawSelf(SpriteBatch sb)
        {
            base.DrawSelf(sb);
            sb.DrawString(font, text, Info.Location, Color);
        }
    }
}