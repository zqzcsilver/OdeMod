using Microsoft.Xna.Framework;

using OdeMod.Utils.Expands;

using System;
using System.Collections.Generic;

namespace OdeMod.Professions.Painter
{
    internal class Masterpiece
    {
        public List<byte[]> UniqueIdentifier = [];
        public string Name = string.Empty;

        public bool Adaptation(Color[] colors, int textureWidth, int textureHeight, Color borderColor = default)
        {
            return UniqueIdentifier.FindIndex(x => x.EqualWithArray(
                GetUniqueIdentifier(colors, textureWidth,
                GetValidInterval(colors, textureWidth, textureHeight, borderColor)))) != -1;
        }

        public void InitByColors(Color[] colors, int textureWidth, int textureHeight, Color borderColor = default)
        {
            var uuid = GetUniqueIdentifier(colors, textureWidth, GetValidInterval(colors, textureWidth, textureHeight, borderColor));
            if (UniqueIdentifier.FindIndex(x => x.EqualWithArray(uuid)) == -1)
                UniqueIdentifier.Add(uuid);
        }

        public static byte[] GetUniqueIdentifier(Color[] colors, Color backgroundColor = default)
        {
            List<byte> result = new List<byte>();
            byte uuid = 0;
            byte count = 0;
            foreach (Color color in colors)
            {
                if (color != backgroundColor)
                {
                    uuid += (byte)Math.Pow(2, count);
                }
                count++;
                if (count == 8)
                {
                    count = 0;
                    result.Add(uuid);
                    uuid = 0;
                }
            }
            if (uuid != 0)
            {
                result.Add(uuid);
            }
            return result.ToArray();
        }

        public static byte[] GetUniqueIdentifier(Color[] colors, int textureWidth, Rectangle range, Color backgroundColor = default)
        {
            List<byte> result = new List<byte>();
            byte uuid = 0;
            byte count = 0;
            for (int i = 0; i < range.Width; i++)
            {
                for (int j = 0; j < range.Height; j++)
                {
                    var color = colors[i + range.X + (j + range.Y) * textureWidth];
                    if (color != backgroundColor)
                    {
                        uuid += (byte)Math.Pow(2, count);
                    }
                    count++;
                    if (count == 8)
                    {
                        count = 0;
                        result.Add(uuid);
                        uuid = 0;
                    }
                }
            }
            if (uuid != 0)
            {
                result.Add(uuid);
            }
            return result.ToArray();
        }

        public static Rectangle GetValidInterval(Color[] colors, int textureWidth, int textureHeight, Color backgroundColor = default)
        {
            Rectangle output = new Rectangle(textureWidth - 1, textureHeight - 1, 0, 0);
            for (int i = 0; i < textureWidth; i++)
            {
                for (int j = 0; j < textureHeight; j++)
                {
                    if (colors[i + j * textureWidth] != backgroundColor)
                    {
                        if (i < output.X || j < output.Y)
                        {
                            output.X = Math.Min(output.X, i);
                            output.Y = Math.Min(output.Y, j);
                            output.Width = 0;
                            output.Height = 0;
                        }
                        output.Width = Math.Max(output.Width, i + 1 - output.X);
                        output.Height = Math.Max(output.Height, j + 1 - output.Y);
                    }
                }
            }
            return output;
        }
    }
}