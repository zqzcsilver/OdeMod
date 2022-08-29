using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria.UI;

namespace OdeMod.UI.OriginalUISystem.UIElements
{
    internal class MyUIImage : UIElement, IOriginalUIElement
    {
        private Texture2D _texture;
        /// <summary>
        /// 最大帧
        /// </summary>
        public readonly int MaxFrame;
        private int frame;
        private int textureHeight;
        /// <summary>
        /// 从0开始计算的拥有溢出保险的当前帧
        /// </summary>
        public int Frame
        {
            get { return frame; }
            set
            {
                if (value < MaxFrame)
                    frame = value;
            }
        }
        public MyUIImage(Texture2D texture)
        {
            _texture = texture;
            MaxFrame = 1;
            Frame = 0;
            Width.Set(_texture.Width, 0f);
            Height.Set(_texture.Height, 0f);
            textureHeight = _texture.Height;
        }
        public MyUIImage(Texture2D texture, int maxFrame)
        {
            _texture = texture;
            MaxFrame = maxFrame;
            Frame = 0;
            Width.Set(_texture.Width, 0f);
            Height.Set(_texture.Height / maxFrame, 0f);
            textureHeight = _texture.Height / maxFrame;
        }
        public void SetImage(Texture2D texture) => _texture = texture;
        public Texture2D GetImage() => _texture;
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);
            CalculatedStyle style = GetDimensions();
            spriteBatch.Draw(_texture, new Rectangle((int)style.X, (int)style.Y, (int)style.Width, (int)style.Height), new Rectangle(0, textureHeight * Frame, _texture.Width, textureHeight), Color.White);
        }
    }
}
