using ReLogic.Graphics;

using System;
using System.Collections.Generic;

namespace OdeMod.Utils
{
    internal class StringUtil
    {
        /// <summary>
        /// 自动换行
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="font">字体</param>
        /// <param name="width">容器宽度</param>
        /// <param name="scale">文字大小</param>
        /// <returns></returns>
        public static List<string> WordWrap(string text, DynamicSpriteFont font, float width, float scale = 1f)
        {
            int nowIndex = 0;
            List<string> output = new List<string>();
            for (int i = 0; i < text.Length; i++)
            {
                if (font.MeasureString(text[nowIndex..i]).X * scale >= width || i == text.Length - 1)
                {
                    if (i == text.Length - 1)
                        output.Add(text.Substring(nowIndex, i - nowIndex + 1));
                    else
                        output.Add(text.Substring(nowIndex, i - nowIndex - 1));
                    nowIndex = i - 1;
                }
            }
            return output;
        }

        /// <summary>
        /// 不会截断单词的自动换行,by RZIN
        /// </summary>
        /// <param name="text"></param>
        /// <param name="font"></param>
        /// <param name="width"></param>
        /// <param name="scale"></param>
        /// <returns></returns>
        public static List<string> WordWrap1(string text, DynamicSpriteFont font, float width, float scale = 1.0f)
        {
            if (string.IsNullOrEmpty(text))
                return null;
            List<string> output = new List<string>();
            if (text.Contains('\n'))
            {
                var x = text.Split('\n');
                Array.ForEach(x, x => output.AddRange(WordWrap1(x, font, width, scale)));
                return output;
            }

            int last = 0, l, r, m;
            while (last < text.Length)
            {
                l = 0;
                r = text.Length - last;
                while (l <= r)
                {
                    m = l + (r - l) / 2;
                    if (font.MeasureString(text.Substring(last, m)).X * scale < width)
                        l = m + 1;
                    else
                        r = m - 1;
                }
                if (last + r < text.Length && isValid(text[last + r]) && isValid(text[last + r - 1]))
                {
                    int t = r;
                    r--;
                    while (r > 0 && isValid(text[last + r - 1]))
                        r--;
                    if (r == 0)
                        r = t;
                }
                output.Add(text.Substring(last, r));
                last += r;
            }
            return output;
        }

        /// <summary>
        /// 判断输入字符是否为字母
        /// </summary>
        /// <param name="c">输入的字符</param>
        /// <returns></returns>
        private static bool isValid(char c) => (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z');
    }
}