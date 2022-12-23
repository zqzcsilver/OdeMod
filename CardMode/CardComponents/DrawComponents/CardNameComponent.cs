using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OdeMod.CardMode.CardComponents.BaseComponents;
using OdeMod.CardMode.PublicComponents;

using ReLogic.Graphics;

using System;
using System.Net;

using Terraria.UI.Chat;

namespace OdeMod.CardMode.CardComponents.DrawComponents
{
    internal class CardNameComponent : CardDrawComponentBase
    {
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

            float scale = infoComponent.Scale * 0.3f;
            var fontSize = info.Font.MeasureString(info.CardName) * scale;
            float startX = drawsize.X / 2f + 2 * infoComponent.Scale - fontSize.X / 2f, jX = 0f;
            Vector2 charSize;
            foreach (var c in info.CardName)
            {
                var s = c.ToString();
                charSize = info.Font.MeasureString(s) * scale;
                ChatManager.DrawColorCodedStringWithShadow(sb, info.Font, s,
                new Vector2(startX + jX,
                    (drawsize.Y + size.Y - fontSize.Y) / 2f - 4 * infoComponent.Scale + (float)(Math.Pow(jX + charSize.X / 2f - fontSize.X / 2f, 2f) / 280f)),
                Color.White, Color.Black, 0f, Vector2.Zero, new Vector2(scale));
                jX += charSize.X;
            }
        }
    }
}