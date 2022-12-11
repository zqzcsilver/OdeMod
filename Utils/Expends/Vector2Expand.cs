using Microsoft.Xna.Framework;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdeMod.Utils.Expands
{
    internal static class Vector2Expand
    {
        /// <summary>
        /// 求向量的斜率
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        public static float ToSlope(this Vector2 vec)
        {
            if (vec.X == 0f)
                return 0f;
            return vec.Y / vec.X;
        }

        /// <summary>
        /// 求向量的弧度
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        public static float ToRotation(this Vector2 vec)
        {
            return (float)Math.Atan2(vec.Y, vec.X);
        }

        /// <summary>
        /// 向量旋转以某点为中心旋转到指定角度
        /// </summary>
        /// <param name="spinningpoint"></param>
        /// <param name="radians"></param>
        /// <param name="center"></param>
        /// <returns></returns>
        public static Vector2 RotatedBy(this Vector2 spinningpoint, double radians, Vector2 center = default(Vector2))
        {
            Vector2 vector = spinningpoint - center;
            Vector2 result = center;
            result.X += vector.Length() * (float)Math.Sin(radians);
            result.Y += vector.Length() * (float)Math.Cos(radians);
            return result;
        }

        /// <summary>
        /// 向量以当前角度绕某点旋转固定角度
        /// </summary>
        /// <param name="spinningpoint"></param>
        /// <param name="radians"></param>
        /// <param name="center"></param>
        /// <returns></returns>
        public static Vector2 Rotated(this Vector2 spinningpoint, double radians, Vector2 center = default(Vector2))
        {
            float cos = (float)Math.Cos(radians);
            float sin = (float)Math.Sin(radians);
            Vector2 vector = spinningpoint - center;
            Vector2 result = center;
            result.X += vector.X * cos + vector.Y * sin;
            result.Y += -vector.X * sin + vector.Y * cos;
            return result;
        }

        /// <summary>
        /// 根据弧度获取长度为输入长度的向量
        /// </summary>
        /// <param name="radians"></param>
        /// <param name="length"></param>
        /// <param name="center"></param>
        /// <returns></returns>
        public static Vector2 GetVector2ByRotation(float radians, float length, Vector2 center)
        {
            return center + new Vector2(length * (float)Math.Sin(radians), length * (float)Math.Cos(radians));
        }
    }
}