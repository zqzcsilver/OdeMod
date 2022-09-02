using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terraria.GameContent;

namespace OdeMod.UI.OdeUISystem.UIElements
{
    internal class UIImage : BaseElement
    {
        private Texture2D _texture;
        private Color _color;
        public UIImage(Texture2D texture,Color color) 
        { 
            _texture = texture;
            _color = color;
            Info.Width.Pixel = texture.Width;
            Info.Height.Pixel = texture.Height;
        }
        protected override void DrawSelf(SpriteBatch sb)
        {
            base.DrawSelf(sb);
            sb.Draw(_texture, Info.TotalHitBox, _color);
        }
        public void ChangeColor(Color color) => _color = color;
        public void ChangeImage(Texture2D texture) => _texture = texture;
        public Texture2D GetImage() => _texture;
        public Color GetColor() => _color;
    }
}
