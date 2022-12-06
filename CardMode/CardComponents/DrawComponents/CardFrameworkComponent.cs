using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OdeMod.CardMode.CardComponents.BaseComponents;
using OdeMod.CardMode.PublicComponents;

namespace OdeMod.CardMode.CardComponents.DrawComponents
{
    internal class CardFrameworkComponent : DrawComponentBase
    {
        protected override void OnCardDraw(Entity entity, BaseInfoComponent infoComponent, SpriteBatch sb)
        {
            base.OnCardDraw(entity, infoComponent, sb);

            var info = entity.GetComponent<CardInfoComponent>();

            var size = new Point((int)(info.CardFrameworkTexture.Width * infoComponent.Scale),
                (int)(info.CardFrameworkTexture.Height * infoComponent.Scale));
            var drawsize = DrawComponent.DrawSize;
            sb.Draw(info.CardFrameworkTexture,
                new Rectangle((int)(drawsize.X / 2 - size.X / 2 + infoComponent.Scale),
                (int)(drawsize.Y - size.Y - infoComponent.Scale), size.X, size.Y), Color.White);
        }

        public override IComponent Clone(Entity cloneEntity)
        {
            return new CardFrameworkComponent();
        }
    }
}