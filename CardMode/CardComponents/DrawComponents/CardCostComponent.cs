using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OdeMod.CardMode.CardComponents.BaseComponents;
using OdeMod.CardMode.PublicComponents;
using OdeMod.CardMode.PublicComponents.DrawComponents;

using Terraria;
using Terraria.UI.Chat;

namespace OdeMod.CardMode.CardComponents.DrawComponents
{
    internal class CardCostComponent : CardDrawComponentBase
    {
        public CardCostComponent(Texture2D texture) : base(texture)
        {
        }

        public override void OnCardDraw(Entity entity, BaseInfoComponent infoComponent, SpriteBatch sb, HookInfo hookInfo)
        {
            base.OnCardDraw(entity, infoComponent, sb, hookInfo);
            var info = entity.GetComponent<CardInfoComponent>();

            float scale = infoComponent.Scale * 0.6f;
            var dcs = new Point((int)(Texture.Width * infoComponent.Scale),
                (int)(Texture.Height * infoComponent.Scale));
            sb.Draw(Texture, new Rectangle(0, 0, dcs.X, dcs.Y), Color.White);

            string cost = info.CardCost.ToString();
            ChatManager.DrawColorCodedStringWithShadow(sb, info.Font, cost,
                dcs.ToVector2() / 2f - info.Font.MeasureString(cost) / 2f * scale + new Vector2(0f, infoComponent.Scale),
                Color.White, Color.Black, 0f, Vector2.Zero, new Vector2(scale));
        }
    }
}