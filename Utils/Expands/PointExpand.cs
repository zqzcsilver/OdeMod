using Microsoft.Xna.Framework;

namespace OdeMod.Utils.Expands
{
    internal static class PointExpand
    {
        public static Point Multiply(this Point p, int num)
        {
            return new Point(p.X * num, p.Y * num);
        }

        public static Point Divide(this Point p, int num)
        {
            return new Point(p.X / num, p.Y / num);
        }
    }
}