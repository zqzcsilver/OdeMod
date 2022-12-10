using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System.Collections.Generic;

using Terraria;

namespace OdeMod.Utils
{
    internal class RenderTarget2DPool
    {
        private Dictionary<Point, RenderTarget2D> _renders, _swapRenders;

        public RenderTarget2DPool()
        {
            _renders = new Dictionary<Point, RenderTarget2D>();
            _swapRenders = new Dictionary<Point, RenderTarget2D>();
        }

        public RenderTarget2D Pool(Point size)
        {
            if (!_renders.ContainsKey(size)) _renders.Add(size,
                new RenderTarget2D(Main.graphics.GraphicsDevice, size.X, size.Y, false,
                Main.graphics.GraphicsDevice.PresentationParameters.BackBufferFormat, DepthFormat.None));
            return _renders[size];
        }

        public RenderTarget2D Pool(int width, int height)
        {
            return Pool(new Point(width, height));
        }

        public RenderTarget2D PoolSwap(Point size)
        {
            if (!_swapRenders.ContainsKey(size)) _swapRenders.Add(size,
                new RenderTarget2D(Main.graphics.GraphicsDevice, size.X, size.Y, false,
                Main.graphics.GraphicsDevice.PresentationParameters.BackBufferFormat, DepthFormat.None));
            return _swapRenders[size];
        }

        public RenderTarget2D PoolSwap(int width, int height)
        {
            return PoolSwap(new Point(width, height));
        }

        public void Clear()
        {
            foreach (var item in _renders.Values)
                item.Dispose();
            _renders.Clear();

            foreach (var item in _swapRenders.Values)
                item.Dispose();
            _swapRenders.Clear();
        }

        public bool Disport(Point size)
        {
            if (!_renders.ContainsKey(size))
                return false;
            _renders[size].Dispose();
            _renders.Remove(size);
            return true;
        }

        public bool DisportSwap(Point size)
        {
            if (!_swapRenders.ContainsKey(size))
                return false;
            _swapRenders[size].Dispose();
            _swapRenders.Remove(size);
            return true;
        }
    }
}