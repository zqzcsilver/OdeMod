using Microsoft.Xna.Framework;

namespace OdeMod.Utils.Expands
{
    internal static class RectangleExpand
    {
        public static Point GetIntPosition(this Rectangle rectangle) => new Point(rectangle.X, rectangle.Y);

        public static Point GetIntSize(this Rectangle rectangle) => new Point(rectangle.Width, rectangle.Height);

        public static Vector2 GetPosition(this Rectangle rectangle) => new Vector2(rectangle.X, rectangle.Y);

        public static Vector2 GetSize(this Rectangle rectangle) => new Vector2(rectangle.Width, rectangle.Height);
    }
}