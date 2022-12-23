using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OdeMod.CardMode.CardComponents.BaseComponents;
using OdeMod.CardMode.PublicComponents;
using OdeMod.Utils;

using ReLogic.Graphics;

using System;

using Terraria.UI.Chat;

namespace OdeMod.CardMode.CardComponents.DrawComponents
{
    internal class CardTipComponent : CardDrawComponentBase
    {
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
            float scale = infoComponent.Scale * 0.22f;
            var sw = StringUtil.WordWrap1(info.CardTip, info.Font, size.X - 2 * infoComponent.Scale, scale);
            Vector2[] fontsSize = new Vector2[sw.Count];
            float centerY = 0f, y = 0f;
            for (i = 0; i < sw.Count; fontsSize[i] = info.Font.MeasureString(sw[i]) * scale, centerY += fontsSize[i++].Y / 2f) ;
            for (i = 0; i < fontsSize.Length;
                ChatManager.DrawColorCodedStringWithShadow(sb, info.Font, sw[i],
                new Vector2(drawsize.X / 2f + 2 * infoComponent.Scale - fontsSize[i].X / 2f,
                drawsize.Y - size.Y / 2f - 4f * infoComponent.Scale - centerY + y + 1 * infoComponent.Scale),
                Color.White, Color.Black, 0f, Vector2.Zero, new Vector2(scale)), y += fontsSize[i++].Y) ;
        }
    }
}