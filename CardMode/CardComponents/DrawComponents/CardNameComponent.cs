using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OdeMod.CardMode.CardComponents.BaseComponents;
using OdeMod.CardMode.PublicComponents;

using ReLogic.Graphics;

namespace OdeMod.CardMode.CardComponents.DrawComponents
{
    internal class CardNameComponent : DrawComponentBase
    {
        protected override void OnCardDraw(Entity entity, BaseInfoComponent infoComponent, SpriteBatch sb)
        {
            base.OnCardDraw(entity, infoComponent, sb);

            var info = entity.GetComponent<CardInfoComponent>();

            var size = new Point((int)(info.CardNameTexture.Width * infoComponent.Scale),
                (int)(info.CardNameTexture.Height * infoComponent.Scale));
            var drawsize = DrawComponent.DrawSize;
            sb.Draw(info.CardNameTexture,
                new Rectangle((int)(drawsize.X / 2 - size.X / 2 + infoComponent.Scale), drawsize.Y / 2, size.X, size.Y), Color.White);

            float scale = infoComponent.Scale * 0.2f;
            var fontSize = info.Font.MeasureString(info.CardName) * scale;
            sb.DrawString(info.Font, info.CardName,
                new Vector2(drawsize.X / 2f + infoComponent.Scale - fontSize.X / 2f, (drawsize.Y + size.Y - fontSize.Y) / 2f),
                Color.Black, 0F, Vector2.Zero, scale, SpriteEffects.None, 0f);
        }

        public override IComponent Clone(Entity cloneEntity)
        {
            return new CardNameComponent();
        }
    }
}