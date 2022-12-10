using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System.Collections.Generic;

using Terraria;

namespace OdeMod.Utils
{
    internal class RenderTarget2DPool
    {
        private Dictionary<Point, RenderTarget2D> _renders, _swapRenders;
        private Dictionary<(Point, string), RenderTarget2D> _otherRenders;

        public RenderTarget2DPool()
        {
            _renders = new Dictionary<Point, RenderTarget2D>();
            _swapRenders = new Dictionary<Point, RenderTarget2D>();
            _otherRenders = new Dictionary<(Point, string), RenderTarget2D>();
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

        public RenderTarget2D PoolOther(Point size, string name)
        {
            if (!_otherRenders.ContainsKey((size, name))) _otherRenders.Add((size, name),
                new RenderTarget2D(Main.graphics.GraphicsDevice, size.X, size.Y, false,
                Main.graphics.GraphicsDevice.PresentationParameters.BackBufferFormat, DepthFormat.None));
            return _otherRenders[(size, name)];
        }

        public RenderTarget2D PoolOther(int width, int height, string name)
        {
            return PoolOther(new Point(width, height), name);
        }

        public void Clear()
        {
            foreach (var item in _renders.Values)
                item.Dispose();
            _renders.Clear();

            foreach (var item in _swapRenders.Values)
                item.Dispose();
            _swapRenders.Clear();

            foreach (var item in _otherRenders.Values)
                item.Dispose();
            _otherRenders.Clear();
        }

        public bool Disport(Point size)
        {
            if (!_renders.ContainsKey(size))
                return false;
            _renders[size].Dispose();
            _renders.Remove(size);
            return true;
        }

        public bool Disport(int width, int height) => Disport(new Point(width, height));

        public bool DisportSwap(Point size)
        {
            if (!_swapRenders.ContainsKey(size))
                return false;
            _swapRenders[size].Dispose();
            _swapRenders.Remove(size);
            return true;
        }

        public bool DisportSwap(int width, int height) => DisportSwap(new Point(width, height));

        public bool DisportOther(Point size, string name)
        {
            if (!_otherRenders.ContainsKey((size, name)))
                return false;
            _otherRenders[(size, name)].Dispose();
            _otherRenders.Remove((size, name));
            return true;
        }

        public bool DisportOther(int width, int height, string name) => DisportOther(new Point(width, height), name);
    }
}