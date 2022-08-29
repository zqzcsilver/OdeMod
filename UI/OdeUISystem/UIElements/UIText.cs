using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using ReLogic.Graphics;

using Terraria;
using Terraria.GameContent;

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
        /// <summary>
        /// 绘制大小，不改变部件碰撞箱，不改变绘制中心
        /// </summary>
        public float Scale;
        public bool CalculateSize = true;
        public UIText(string t, DynamicSpriteFont spriteFont, float scale = 1f)
        {
            text = t;
            font = spriteFont;
            Scale = scale;
            Color = Color.White;
        }
        public UIText(string t, DynamicSpriteFont spriteFont, Color textColor, float scale = 1f)
        {
            text = t;
            font = spriteFont;
            Scale = scale;
            Color = textColor;
        }
        public override void LoadEvents()
        {
            base.LoadEvents();
            Events.OnLeftClick += element =>
            {
                Main.NewText("你抬起了鼠标左键！");
            };
            Events.OnLeftDown += element =>
            {
                Main.NewText("你按下了鼠标左键！");
            };
            Events.OnLeftDoubleClick += element =>
            {
                Main.NewText("你双击了鼠标左键！");
            };

            Events.OnRightClick += element =>
            {
                Main.NewText("你抬起了鼠标右键！");
            };
            Events.OnRightDown += element =>
            {
                Main.NewText("你按下了鼠标右键！");
            };
            Events.OnRightDoubleClick += element =>
            {
                Main.NewText("你双击了鼠标右键！");
            };

            Events.OnMouseOver += element =>
            {
                Main.NewText("鼠标进入了UI！");
            };
            Events.OnMouseOut += element =>
            {
                Main.NewText("鼠标离开了UI！");
            };
        }
        public override void Calculation()
        {
            if (CalculateSize)
            {
                Vector2 size = font.MeasureString(text) * Scale;
                Info.Width.Pixel = size.X;
                Info.Height.Pixel = size.Y;
                Info.Left.Pixel -= size.X / 2;
                Info.Top.Pixel -= size.Y / 2;
            }
            base.Calculation();
        }
        protected override void DrawSelf(SpriteBatch sb)
        {
            base.DrawSelf(sb);
            Vector2 center = Info.Location;
            sb.DrawString(font, text, center, Color, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);

            sb.Draw(TextureAssets.MagicPixel.Value, Info.TotalHitBox, Color.Red);
        }
    }
}
