using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OdeMod.CardMode.CardComponents.BaseComponents;
using OdeMod.CardMode.PublicComponents;

using Terraria;
using Terraria.UI.Chat;

namespace OdeMod.CardMode.CardComponents.DrawComponents
{
    internal class CardCostComponent : DrawComponentBase
    {
        protected override void OnCardDraw(Entity entity, BaseInfoComponent infoComponent, SpriteBatch sb)
        {
            base.OnCardDraw(entity, infoComponent, sb);

            var info = entity.GetComponent<CardInfoComponent>();

            float scale = infoComponent.Scale * 0.4f;
            var dcs = new Point((int)(info.CardCostTexture.Width * infoComponent.Scale),
                (int)(info.CardCostTexture.Height * infoComponent.Scale));
            sb.Draw(info.CardCostTexture, new Rectangle(0, 0, dcs.X, dcs.Y), Color.White);

            string cost = info.CardCost.ToString();
            ChatManager.DrawColorCodedStringWithShadow(sb, info.Font, cost,
                dcs.ToVector2() / 2f - info.Font.MeasureString(cost) / 2f * scale + new Vector2(0f, infoComponent.Scale),
                Color.White, Color.Black, 0f, Vector2.Zero, new Vector2(scale));
        }

        public override IComponent Clone(Entity cloneEntity)
        {
            return new CardCostComponent();
        }
    }
}