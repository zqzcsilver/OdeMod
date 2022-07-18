using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terraria.GameContent;

namespace OdeMod.Utils
{
    internal static class DrawUtils
    {
        /// <summary>
        /// 绘制直线
        /// </summary>
        /// <param name="sb">画布</param>
        /// <param name="startPoint">起点</param>
        /// <param name="endPoint">终点</param>
        /// <param name="size">线条大小</param>
        /// <param name="color">线条颜色</param>
        public static void DrawLine(SpriteBatch sb, Vector2 startPoint, Vector2 endPoint, Vector2 size, Color color)
        {
            float length = (startPoint - endPoint).Length();
            Vector2 normalVec = endPoint - startPoint;
            normalVec.Normalize();
            Rectangle rectangle = new Rectangle();
            rectangle.Width = (int)size.X;
            rectangle.Height = (int)size.Y;
            for (int i = 0; i < length; i++)
            {
                rectangle.X = (int)(startPoint.X + i * normalVec.X);
                rectangle.Y = (int)(startPoint.Y + i * normalVec.Y);
                sb.Draw(TextureAssets.MagicPixel.Value, rectangle, color);
            }
        }
    }
}
