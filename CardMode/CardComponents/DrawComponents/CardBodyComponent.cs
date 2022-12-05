using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OdeMod.CardMode.CardComponents.BaseComponents;
using OdeMod.CardMode.PublicComponents;

namespace OdeMod.CardMode.CardComponents.DrawComponents
{
    internal class CardBodyComponent : DrawComponentBase
    {
        protected override void OnCardDraw(Entity entity, BaseInfoComponent infoComponent, SpriteBatch sb)
        {
            base.OnCardDraw(entity, infoComponent, sb);

            var info = entity.GetComponent<CardInfoComponent>();

            var dcs = DrawComponent.DrawSize;
            sb.Draw(info.CardBodyTexture, new Rectangle(0, 0, dcs.X, dcs.Y), Color.White);
        }

        public override IComponent Clone(Entity cloneEntity)
        {
            return new CardBodyComponent();
        }
    }
}