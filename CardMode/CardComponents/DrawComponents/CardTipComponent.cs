using System.Collections.Generic;

using FontStashSharp;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OdeMod.CardMode.CardComponents.BaseComponents;
using OdeMod.CardMode.PublicComponents;
using OdeMod.Utils;

namespace OdeMod.CardMode.CardComponents.DrawComponents
{
    internal class CardTipComponent : CardDrawComponentBase
    {
        /// <summary>
        /// 将此值设为小于0则会重新计算字体大小
        /// </summary>
        public float Scale = -1f;

        private float centerY;
        private List<string> tooltips;
        private Vector2[] tooltipLinesSize;

        public CardTipComponent(Texture2D texture) : base(texture)
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
                (int)(drawsize.Y - size.Y - 4 * 4 * infoComponent.Scale), size.X, size.Y), Color.White);
            int i;
            float scale = 0f;
            //min:0.18,max:0.25
            if (Scale < 0)
            {
                float maxY;
                Scale = 1f;
                DynamicSpriteFont font;
                while (Scale > 0.09f)
                {
                    maxY = 0f;
                    centerY = 0f;
                    scale = infoComponent.Scale * Scale;
                    font = info.FontSystem.GetFont(scale * info.FontSize);
                    tooltips = StringUtil.WordWrap2(info.CardTip, font,
                        size.X - 2 * 4 * infoComponent.Scale);
                    if (tooltipLinesSize == null || tooltipLinesSize.Length != tooltips.Count)
                        tooltipLinesSize = new Vector2[tooltips.Count];
                    for (i = 0; i < tooltips.Count; i++)
                    {
                        tooltipLinesSize[i] = font.MeasureString(tooltips[i]);
                        centerY += tooltipLinesSize[i].Y / 2f;
                        maxY += tooltipLinesSize[i].Y;
                        if (maxY > size.Y)
                            break;
                    }
                    if (maxY > (size.Y - 4 * 4 * infoComponent.Scale))
                        Scale -= 0.001f;
                    else
                        break;
                }
            }
            else
                scale = infoComponent.Scale * Scale;

            float y = 0f;
            for (i = 0; i < tooltipLinesSize.Length; i++)
            {
                sb.DrawString(info.FontSystem.GetFont(scale * info.FontSize), tooltips[i],
                    new Vector2(drawsize.X / 2f + 2 * 4 * infoComponent.Scale - tooltipLinesSize[i].X / 2f,
                drawsize.Y - size.Y / 2f - 4f * 4f * infoComponent.Scale - centerY + y + 1 * infoComponent.Scale), Color.White,
                null, 0f, default, 0f, 0f, 0f,
                TextStyle.None, FontSystemEffect.Stroked, 1);
                y += tooltipLinesSize[i].Y;
            }
        }
    }
}