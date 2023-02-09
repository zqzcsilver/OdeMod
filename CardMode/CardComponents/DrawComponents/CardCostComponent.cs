using FontStashSharp;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OdeMod.CardMode.CardComponents.BaseComponents;
using OdeMod.CardMode.PublicComponents;

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

            float scale = infoComponent.Scale * 1.6f;
            var dcs = new Point((int)(Texture.Width * infoComponent.Scale),
                (int)(Texture.Height * infoComponent.Scale));
            sb.Draw(Texture, new Rectangle(0, 0, dcs.X, dcs.Y), Color.White);

            string cost = info.CardCost.ToString();
            sb.DrawString(info.FontSystem.GetFont(info.FontSize * scale), cost,
                dcs.ToVector2() / 2f - info.Font.MeasureString(cost) + new Vector2(0f, -1f) * infoComponent.Scale,
                Color.White, null, 0f, default, 0f, 0f, 0f, 
                TextStyle.None, FontSystemEffect.Stroked,4);
        }
    }
}