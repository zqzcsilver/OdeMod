using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OdeMod.CardMode.CardComponents.BaseComponents;
using OdeMod.CardMode.PublicComponents;
using OdeMod.Utils;

using ReLogic.Graphics;

namespace OdeMod.CardMode.CardComponents.DrawComponents
{
    internal class CardTipComponent : DrawComponentBase
    {
        protected override void OnCardDraw(Entity entity, BaseInfoComponent infoComponent, SpriteBatch sb)
        {
            base.OnCardDraw(entity, infoComponent, sb);

            var info = entity.GetComponent<CardInfoComponent>();

            var size = new Point((int)(info.CardTipTexture.Width * infoComponent.Scale),
                (int)(info.CardTipTexture.Height * infoComponent.Scale));
            var drawsize = DrawComponent.DrawSize;
            sb.Draw(info.CardTipTexture,
                new Rectangle((int)(drawsize.X / 2 - size.X / 2 + infoComponent.Scale),
                (int)(drawsize.Y - size.Y - 2 * infoComponent.Scale), size.X, size.Y), Color.White);

            int i;
            float scale = infoComponent.Scale * 0.18f;
            var sw = StringUtil.WordWrap1(info.CardTip, info.Font, size.X - 2 * infoComponent.Scale, scale);
            Vector2[] fontsSize = new Vector2[sw.Count];
            float centerY = 0f, y = 0f;
            for (i = 0; i < sw.Count; fontsSize[i] = info.Font.MeasureString(sw[i]) * scale, centerY += fontsSize[i++].Y / 2f) ;
            for (i = 0; i < fontsSize.Length;
                sb.DrawString(info.Font, sw[i],
                new Vector2(drawsize.X / 2f + infoComponent.Scale - fontsSize[i].X / 2f,
                drawsize.Y - size.Y / 2f - 2f * infoComponent.Scale - centerY + y + 1 * infoComponent.Scale),
                Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f), y += fontsSize[i++].Y) ;
        }

        public override IComponent Clone(Entity cloneEntity)
        {
            return new CardTipComponent();
        }
    }
}