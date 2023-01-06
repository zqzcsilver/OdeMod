using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OdeMod.CardMode.CardComponents.BaseComponents;
using OdeMod.CardMode.PublicComponents;

using System;

using Terraria.UI.Chat;

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
                new Rectangle((int)(drawsize.X / 2 - size.X / 2 + 2 * infoComponent.Scale),
                (int)(drawsize.Y / 2 - 4 * infoComponent.Scale), size.X, size.Y), Color.White);

            //min:0.24,max:0.35
            float scale = infoComponent.Scale * Scale;
            if (Scale < 0)
            {
                Scale = 0.35f;
                float maxX = 40f * infoComponent.Scale;
                while (Scale > 0.24f)
                {
                    scale = infoComponent.Scale * Scale;

                    if (info.Font.MeasureString(info.CardName).X * scale > maxX)
                        Scale -= 0.001f;
                    else
                        break;
                }
            }
            else
                scale = infoComponent.Scale * Scale;
            var fontSize = info.Font.MeasureString(info.CardName) * scale;

            float startX = drawsize.X / 2f + 2 * infoComponent.Scale - fontSize.X / 2f, jX = 0f;
            Vector2 charSize;
            float x;
            foreach (var c in info.CardName)
            {
                var s = c.ToString();
                charSize = info.Font.MeasureString(s) * scale;
                x = jX + charSize.X / 2f - fontSize.X / 2f - 2 * infoComponent.Scale;
                ChatManager.DrawColorCodedStringWithShadow(sb, info.Font, s,
                new Vector2(startX + jX,
                    (drawsize.Y + size.Y - fontSize.Y) / 2f - 3 * infoComponent.Scale +
                    (float)(Math.Pow(x, 2f) / 600f)),
                Color.White, Color.Black, (float)Math.Atan(x / 300f), Vector2.Zero, new Vector2(scale));
                jX += charSize.X;
            }
        }
    }
}