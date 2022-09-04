using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using ReLogic.Graphics;
using ReLogic.Text;

using System;
using System.Collections.Generic;
using System.Reflection;

using Terraria;

namespace OdeMod.Utils.FontInfos
{
    /// <summary>
    /// 用于储存字体数据
    /// </summary>
    public struct SpriteCharacterData
    {
        public readonly Texture2D Texture;
        public readonly Rectangle Glyph;
        public readonly Rectangle Padding;
        public readonly Vector3 Kerning;
        public SpriteCharacterData(Texture2D texture, Rectangle glyph, Rectangle padding, Vector3 kerning)
        {
            Texture = texture;
            Glyph = glyph;
            Padding = padding;
            Kerning = kerning;
        }
        public GlyphMetrics ToGlyphMetric()
        {
            return GlyphMetrics.FromKerningData(Kerning.X, Kerning.Y, Kerning.Z);
        }
    }
    /// <summary>
    /// 用于获取和管理字体数据
    /// </summary>
    internal sealed class DynamicSpriteFontInfoManager
    {
        /// <summary>
        /// 获取字体数据
        /// </summary>
        /// <param name="font">输入的字体</param>
        /// <returns>获取到的数据</returns>
        public Dictionary<char, SpriteCharacterData> this[DynamicSpriteFont font]
        {
            get
            {
                if (!_spriteFontCharacters.ContainsKey(font))
                    LoadDynamicSpriteFontInfo(font);
                return _spriteFontCharacters[font];
            }
        }
        /// <summary>
        /// 用于储存字体数据的字典
        /// </summary>
        private Dictionary<DynamicSpriteFont, Dictionary<char, SpriteCharacterData>> _spriteFontCharacters;
        public DynamicSpriteFontInfoManager()
        {
            _spriteFontCharacters = new Dictionary<DynamicSpriteFont, Dictionary<char, SpriteCharacterData>>();
        }
        /// <summary>
        /// 加载字体数据
        /// </summary>
        /// <param name="font">等待加载的字体</param>
        /// <returns>如果加载成功返回true，否则返回false</returns>
        public bool LoadDynamicSpriteFontInfo(DynamicSpriteFont font)
        {
            if (_spriteFontCharacters.ContainsKey(font))
                return false;

            object _spriteCharacters = typeof(DynamicSpriteFont).GetField("_spriteCharacters",
                BindingFlags.Instance | BindingFlags.NonPublic).GetValue(font);
            var type = _spriteCharacters.GetType();
            Array entries = (Array)type.GetField("_entries", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(_spriteCharacters);
            if (entries.Length == 0)
                return false;

            Type entrieType = entries.GetValue(0).GetType();
            FieldInfo keyField = entrieType.GetField("key", BindingFlags.Instance | BindingFlags.Public);
            FieldInfo valueField = entrieType.GetField("value", BindingFlags.Instance | BindingFlags.Public);

            Dictionary<char, SpriteCharacterData> fontInfo = new Dictionary<char, SpriteCharacterData>();
            foreach (var x in entries)
            {
                var c = (char)keyField.GetValue(x);
                if (!fontInfo.ContainsKey(c))
                    fontInfo.Add((char)keyField.GetValue(x), ObjectToSpriteCharacterData(valueField.GetValue(x)));
            }

            _spriteFontCharacters.Add(font, fontInfo);
            return true;
        }
        /// <summary>
        /// 获取字体数据
        /// </summary>
        /// <param name="font">字体</param>
        /// <param name="c">字符</param>
        /// <returns>字符的数据</returns>
        public SpriteCharacterData GetSpriteCharacterData(DynamicSpriteFont font, char c) => this[font][c];
        /// <summary>
        /// 获取字体字形
        /// </summary>
        /// <param name="font">字体</param>
        /// <param name="c">字符</param>
        /// <returns>字符的字形</returns>
        public Rectangle GetCharacterGlyph(DynamicSpriteFont font, char c) => GetSpriteCharacterData(font, c).Glyph;
        /// <summary>
        /// 获取字符大小
        /// </summary>
        /// <param name="font">字体</param>
        /// <param name="c">字符</param>
        /// <returns>字符的大小</returns>
        public Vector2 GetCharacterSize(DynamicSpriteFont font, char c) => GetCharacterGlyph(font, c).Size();
        /// <summary>
        /// 获取输入的单行字符串大小
        /// </summary>
        /// <param name="font">字体</param>
        /// <param name="s">字符串</param>
        /// <returns>单行字符串大小</returns>
        public Vector2 GetALineStringSize(DynamicSpriteFont font, string s)
        {
            Vector2 size = Vector2.Zero, charSize;
            foreach (var c in s)
            {
                charSize = GetCharacterSize(font, c);
                size.X += charSize.X;
                if (size.Y < charSize.Y)
                {
                    size.Y = charSize.Y;
                }
            }
            return size;
        }
        /// <summary>
        /// 获取输入的字符串大小
        /// </summary>
        /// <param name="font">字体</param>
        /// <param name="s">字符串</param>
        /// <returns>字符串大小</returns>
        public Vector2 GetStringSize(DynamicSpriteFont font, string s)
        {
            string[] s1 = s.Split('\n');
            Vector2 size = Vector2.Zero, sSize;

            foreach (var c in s1)
            {
                sSize = GetALineStringSize(font, c);
                size.Y += sSize.Y;
                if (size.X < sSize.X)
                {
                    size.X = sSize.X;
                }
            }
            return size;
        }
        private static SpriteCharacterData ObjectToSpriteCharacterData(object obj)
        {
            var type = obj.GetType();
            Texture2D texture = (Texture2D)type.GetField("Texture", BindingFlags.Public | BindingFlags.Instance).GetValue(obj);
            Rectangle glyph = (Rectangle)type.GetField("Glyph", BindingFlags.Public | BindingFlags.Instance).GetValue(obj);
            Rectangle padding = (Rectangle)type.GetField("Padding", BindingFlags.Public | BindingFlags.Instance).GetValue(obj);
            Vector3 kerning = (Vector3)type.GetField("Kerning", BindingFlags.Public | BindingFlags.Instance).GetValue(obj);
            return new SpriteCharacterData(texture, glyph, padding, kerning);
        }
    }
}