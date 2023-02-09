using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OdeMod.CardMode.PublicComponents;

namespace OdeMod.CardMode.CardComponents.DrawComponents
{
    internal class CardNameFrameworkComponent : CardDrawComponentBase
    {
        public CardNameFrameworkComponent(Texture2D texture) : base(texture)
        {
        }

        public override void OnCardDraw(Entity entity, BaseInfoComponent infoComponent, SpriteBatch sb, HookInfo hookInfo)
        {
            base.OnCardDraw(entity, infoComponent, sb, hookInfo);

            var size = new Point((int)(Texture.Width * infoComponent.Scale),
                (int)(Texture.Height * infoComponent.Scale));
            var drawsize = entity.GetComponent<DrawComponent>().DrawSize;
            sb.Draw(Texture,
                new Rectangle((int)(drawsize.X / 2 - size.X / 2 + 2 * 4 * infoComponent.Scale),
                (int)(drawsize.Y - size.Y + 8 * 4 * infoComponent.Scale) / 2, size.X, size.Y), Color.White);
        }
    }
}