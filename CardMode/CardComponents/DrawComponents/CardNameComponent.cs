using System;

using FontStashSharp;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OdeMod.CardMode.CardComponents.BaseComponents;
using OdeMod.CardMode.PublicComponents;
using OdeMod.Utils.Expends;

namespace OdeMod.CardMode.CardComponents.DrawComponents
{
    internal class CardNameComponent : CardDrawComponentBase
    {
        /// <summary>
        /// 将此值设为小于或等于0则会重新计算字体大小
        /// </summary>
        public float Scale = -1f;

        public CardNameComponent(Texture2D texture) : base(texture)
        {
        }

        public override void OnCardDraw(Entity entity, BaseInfoComponent infoComponent, SpriteBatch sb, HookInfo hookInfo)
        {
            base.OnCardDraw(entity, infoComponent, sb, hookInfo);

            var info = entity.GetComponent<CardInfoComponent>();
            var size = new Point((int)(Texture.Width * infoComponent.Scale),
                (int)(Texture.Height * infoComponent.Scale));
            var drawsize = entity.GetComponent<DrawComponent>().DrawSize;
            sb.Draw(Texture,
                new Rectangle((int)(drawsize.X / 2 - size.X / 2 + 2 * 4 * infoComponent.Scale),
                (int)(drawsize.Y / 2 - 4 * 4 * infoComponent.Scale), size.X, size.Y), Color.White);

            //min:0.24,max:0.35
            float scale;
            if (Scale < 0)
            {
                Scale = 4f;
                scale = infoComponent.Scale * Scale;
                float maxX = 30f * 4f * infoComponent.Scale;
                while (scale > 0.12f)
                {
                    scale = infoComponent.Scale * Scale;

                    if (info.FontSystem.GetFont(scale * info.FontSize).MeasureString(info.CardName).X > maxX)
                        Scale -= 0.001f;
                    else
                        break;
                }
            }
            else
                scale = infoComponent.Scale * Scale;

            var font = info.FontSystem.GetFont(scale * info.FontSize);
            var fontSize = Vector2.Zero /*font.MeasureString(info.CardName)*/;
            foreach (var c in info.CardName)
            {
                var s = c.ToString();
                var cs = font.GetGlyphs(s, Vector2.Zero, default, null,
                0, 0, FontSystemEffect.Stroked, 2)[0].Bounds.GetSize();
                fontSize.X += cs.X;
                fontSize.Y = Math.Max(fontSize.Y, cs.Y);
            }
            //font.GetGlyphs(info.CardName, Vector2.Zero, default, null,
            //0, 0, FontSystemEffect.Stroked, 2).
            //ForEach(x =>
            //{
            //    fontSize.X += x.Bounds.Width;
            //    fontSize.Y = Math.Max(fontSize.Y, x.Bounds.Height);
            //});

            float startX = drawsize.X / 2f + 5 * 4f * infoComponent.Scale - fontSize.X / 2f, jX = 0f;
            Vector2 charSize;
            float x;
            foreach (var c in info.CardName)
            {
                var s = c.ToString();
                charSize = font.GetGlyphs(s, Vector2.Zero, default, null,
                0, 0, FontSystemEffect.Stroked, 2)[0].Bounds.GetSize();
                x = jX + charSize.X / 2f - fontSize.X / 2f - 2 * 4f * infoComponent.Scale;

                sb.DrawString(font, s,
                    new Vector2(startX + jX,
                    (drawsize.Y + size.Y - fontSize.Y) / 2f - 2 * 4 * infoComponent.Scale +
                    (float)(Math.Pow(x, 2f) / 600f)), Color.White,
                null, (float)Math.Atan(x / 300f), font.MeasureString(s) / 2f, 0f, 0f, 0f,
                TextStyle.None, FontSystemEffect.Stroked, 2);
                jX += charSize.X;
            }
        }
    }
}