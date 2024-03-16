using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OdeMod.Professions.Painter;
using OdeMod.UI.OdeUISystem.UIElements;

using System;

using Terraria;

namespace OdeMod.UI.OdeUISystem.Containers.Drawer.UIElements
{
    internal class UICanvas : BaseElement
    {
        public static Color BackgroundColor
        {
            get => new Color(0f, 0f, 0f, 0.1f);
        }

        public static Color BrushColor
        {
            get => Color.Black;
        }

        public Point Size
        {
            get => size;
            set
            {
                if (value.X <= 0 || value.Y <= 0)
                    return;
                if (size != value)
                {
                    Main.RunOnMainThread(() =>
                    {
                        _texture = new Texture2D(Main.graphics.GraphicsDevice, value.X, value.Y);
                        Color[] colors = new Color[value.X * value.Y];
                        colors.AsSpan().Fill(new Color(0f, 0f, 0f, 0.1f));
                        for (int y = 0; y < size.Y; y++)
                        {
                            for (int x = 0; x < size.X; x++)
                            {
                                if (x >= value.X)
                                    break;

                                colors[x + y * value.X] = _canvasColors[x + y * size.X];
                            }
                            if (y >= value.Y)
                                break;
                        }
                        _canvasColors = colors;
                        _texture.SetData(_canvasColors);
                        size = value;

                        Info.Width.Pixel = size.X * 64f;
                        Info.Height.Pixel = size.Y * 64f;
                    });
                }
            }
        }

        private Point size;
        private Texture2D _texture;
        private Color[] _canvasColors;

        public Color this[int x, int y]
        {
            get
            {
                if (x >= Size.X || y >= Size.Y)
                    throw new IndexOutOfRangeException();
                return _canvasColors[x + y * Size.X];
            }
        }

        public UICanvas(int width, int height) : this(new Point(width, height))
        {
        }

        public UICanvas(Point size)
        {
            Size = size;
        }

        public override void LoadEvents()
        {
            base.LoadEvents();
            Events.OnMouseHover += Events_OnMouseHover;
        }

        private void Events_OnMouseHover(BaseElement baseElement)
        {
            if (_texture == null)
                return;
            Point pos = new Point((int)((Main.mouseX - Info.TotalLocation.X) / Info.TotalSize.X * Size.X),
                (int)((Main.mouseY - Info.TotalLocation.Y) / Info.TotalSize.Y * Size.Y));
            if (Main.mouseLeft)
            {
                ChangeColor(BrushColor, pos.X, pos.Y);
            }
            else if (Main.mouseRight)
            {
                ChangeColor(BackgroundColor, pos.X, pos.Y);
            }
        }

        public void ChangeColor(Color color, int x, int y, bool updateCanvas = true)
        {
            if (x >= Size.X || y >= Size.Y)
                return;
            _canvasColors[x + y * Size.X] = color;
            if (updateCanvas)
            {
                Main.RunOnMainThread(() =>
                {
                    _texture.SetData(_canvasColors);
                });
            }
        }

        public Rectangle GetValidInterval()
        {
            return Masterpiece.GetValidInterval(_canvasColors, Size.X, Size.Y, BackgroundColor);
        }

        protected override void DrawSelf(SpriteBatch sb)
        {
            base.DrawSelf(sb);
            if (_texture != null)
            {
                var overflowHiddenRasterizerState = sb.GraphicsDevice.RasterizerState;
                sb.End();
                sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap,
                    DepthStencilState.None, overflowHiddenRasterizerState, null,
                    Main.UIScaleMatrix);

                sb.Draw(_texture, Info.TotalHitBox, null,
                    Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);

                sb.End();
                sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.AnisotropicWrap,
                    DepthStencilState.None, overflowHiddenRasterizerState, null,
                    Main.UIScaleMatrix);

                var range = GetValidInterval();
                Main.NewText(range);
                //var bytes = MasterPiece.GetUniqueIdentifier(_canvasColors, Size.X, range, BackgroundColor);
                var bytes = Masterpiece.GetUniqueIdentifier(_canvasColors, BackgroundColor);
                string s = string.Empty;
                foreach (var b in bytes)
                {
                    s += b.ToString() + " ";
                }
                Main.NewText(s);
            }
        }

        public Texture2D GetImage() => _texture;
    }
}