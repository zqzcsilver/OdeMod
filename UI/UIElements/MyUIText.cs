using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using ReLogic.Content;
using ReLogic.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.UI;
using Terraria.UI.Chat;

namespace OdeMod.UI.UIElements
{
    public class MyUIText : UIElement
    {
        public string Text;
        public float Scale;
        private DynamicSpriteFont Font;
        private int drawMode;
        public Color Color, BorderColor;
        public MyUIText(string text, DynamicSpriteFont font, Color color, float scale = 1f)
        {
            Text = text;
            Scale = scale;
            Font = font;
            Color = color;
            drawMode = 0;
            Width.Pixels = Font.MeasureString(Text).X;
            Height.Pixels = Font.MeasureString(Text).Y;
            drawMode = 0;
        }
        public MyUIText(string text, DynamicSpriteFont font, Color color,Color borderColor, float scale = 1f)
        {
            Text = text;
            Scale = scale;
            Font = font;
            Color = color;
            BorderColor = borderColor;
            drawMode = 1;
            Width.Pixels = Font.MeasureString(Text).X;
            Height.Pixels = Font.MeasureString(Text).Y;
        }
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);
            CalculatedStyle dimensions = GetDimensions();
            if (drawMode == 0)
            {
                ChatManager.DrawColorCodedStringWithShadow(spriteBatch, Font, Text, new Vector2(dimensions.X, dimensions.Y), Color, 0, Vector2.Zero, new Vector2(Scale));
            }
            else if (drawMode == 1)
            {
                Terraria.Utils.DrawBorderStringFourWay(spriteBatch, Font, Text, dimensions.X, dimensions.Y, Color, BorderColor, Vector2.Zero, Scale);
            }
            Width.Pixels = Font.MeasureString(Text).X;
            Height.Pixels = Font.MeasureString(Text).Y;
        }
    }
}
