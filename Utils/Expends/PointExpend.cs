using Microsoft.Xna.Framework;

namespace OdeMod.Utils.Expends
{
    internal static class PointExpend
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