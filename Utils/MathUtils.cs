using Microsoft.Xna.Framework;

using System;

namespace OdeMod.Utils
{
    internal static class MathUtils
    {
        public static float GaussValue(Point position, float σ) => (float)(1f / (MathHelper.TwoPi * Math.Pow(σ, 2)) *
            Math.Exp(-((Math.Pow(position.X, 2) + Math.Pow(position.Y, 2)) / (2 * Math.Pow(σ, 2)))));

        public static float[,] GaussValueBlock(Point size, float σ)
        {
            float[,] gauss = new float[size.X, size.Y];
            float a = 0f;
            Point center = new Point((size.X - 1) / 2, (size.Y - 1) / 2);
            for (int i = 0; i < size.X; i++)
            {
                for (int j = 0; j < size.Y; j++)
                {
                    gauss[i, j] = GaussValue(new Point(i - center.X, j - center.Y), σ);
                    a += gauss[i, j];
                }
            }
            for (int i = 0; i < size.X; i++)
            {
                for (int j = 0; j < size.Y; j++)
                {
                    gauss[i, j] /= a;
                }
            }
            return gauss;
        }

        public static float[] GaussValueH(int size, float σ)
        {
            float[] gauss = new float[size];
            float a = 0f;
            for (int i = 0; i < size; i++)
            {
                gauss[i] = GaussValue(new Point(0, i - (size - 1) / 2), σ);
                a += gauss[i];
            }
            for (int i = 0; i < size; i++)
            {
                gauss[i] /= a;
            }
            return gauss;
        }

        public static float[] GaussValueV(int size, float σ)
        {
            float[] gauss = new float[size];
            float a = 0f;
            for (int i = 0; i < size; i++)
            {
                gauss[i] = GaussValue(new Point(i - (size - 1) / 2, 0), σ);
                a += gauss[i];
            }
            for (int i = 0; i < size; i++)
            {
                gauss[i] /= a;
            }
            return gauss;
        }
    }
}