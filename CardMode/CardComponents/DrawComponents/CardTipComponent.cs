using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OdeMod.CardMode.CardComponents.BaseComponents;
using OdeMod.CardMode.PublicComponents;
using OdeMod.Utils;

using System.Collections.Generic;

using Terraria.UI.Chat;

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
                new Rectangle((int)(drawsize.X / 2 - size.X / 2 + 2 * infoComponent.Scale),
                (int)(drawsize.Y - size.Y - 4 * infoComponent.Scale), size.X, size.Y), Color.White);
            int i;
            float scale = 0f;
            //min:0.18,max:0.25
            if (Scale < 0)
            {
                float maxY;
                Scale = 0.25f;
                while (Scale > 0.17f)
                {
                    maxY = 0f;
                    centerY = 0f;
                    scale = infoComponent.Scale * Scale;
                    tooltips = StringUtil.WordWrap1(info.CardTip, info.Font, size.X - 2 * infoComponent.Scale, scale);
                    if (tooltipLinesSize == null || tooltipLinesSize.Length != tooltips.Count)
                        tooltipLinesSize = new Vector2[tooltips.Count];
                    for (i = 0; i < tooltips.Count; i++)
                    {
                        tooltipLinesSize[i] = info.Font.MeasureString(tooltips[i]) * scale;
                        centerY += tooltipLinesSize[i].Y / 2f;
                        maxY += tooltipLinesSize[i].Y;
                        if (maxY > size.Y)
                            break;
                    }
                    if (maxY > size.Y)
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
                ChatManager.DrawColorCodedStringWithShadow(sb, info.Font, tooltips[i],
                new Vector2(drawsize.X / 2f + 2 * infoComponent.Scale - tooltipLinesSize[i].X / 2f,
                drawsize.Y - size.Y / 2f - 4f * infoComponent.Scale - centerY + y + 1 * infoComponent.Scale),
                Color.White, Color.Black, 0f, Vector2.Zero, new Vector2(scale));
                y += tooltipLinesSize[i].Y;
            }
        }
    }
}