using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OdeMod.CardMode.CardComponents.BaseComponents;
using OdeMod.CardMode.PublicComponents;

namespace OdeMod.CardMode.CardComponents.DrawComponents
{
    internal class CardBodyComponent : CardDrawComponentBase
    {
        public CardBodyComponent(Texture2D texture) : base(texture)
        {
        }

        public override void OnCardDraw(Entity entity, BaseInfoComponent infoComponent, SpriteBatch sb, HookInfo hookInfo)
        {
            base.OnCardDraw(entity, infoComponent, sb, hookInfo);
            var dcs = entity.GetComponent<DrawComponent>().DrawSize;
            var info = entity.GetComponent<CardInfoComponent>();
            Point s = new Point((int)(Texture.Width * infoComponent.Scale), (int)(Texture.Height * infoComponent.Scale));
            sb.Draw(Texture, new Rectangle(dcs.X - s.X, dcs.Y - s.Y, s.X, s.Y), Color.White);
        }
    }
}