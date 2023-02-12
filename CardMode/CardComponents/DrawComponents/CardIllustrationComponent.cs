using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OdeMod.CardMode.CardComponents.BaseComponents;
using OdeMod.CardMode.PublicComponents;
using OdeMod.CardMode.PublicComponents.DrawComponents;
using OdeMod.Utils;

using Terraria;
using Terraria.ModLoader;

namespace OdeMod.CardMode.CardComponents.DrawComponents
{
    internal delegate void DrawIllustration(SpriteBatch sb, Entity entity, CardIllustrationComponent cardIllustrationComponent
        , RenderTarget2D render, Point renderSize);

    internal class CardIllustrationComponent : CardDrawComponentBase
    {
        public DrawIllustration OnDrawIllustration;
        public Texture2D Illustration;

        public CardIllustrationComponent(Texture2D texture, Texture2D illustration, bool useDefaultDrawStyle = true) : base(texture)
        {
            Illustration = illustration;
            if (useDefaultDrawStyle)
            {
                OnDrawIllustration += (sb, entity, cardIllustrationComponent, render, renderSize) =>
                {
                    CardInfoComponent infoComponent = entity.GetComponent<CardInfoComponent>();
                    Vector2 texSize = new Vector2(Illustration.Width, Illustration.Height);
                    float scale = MathHelper.Max((float)renderSize.X / texSize.X, (float)renderSize.Y / texSize.Y) * 0.7f;
                    sb.Draw(Illustration, renderSize.ToVector2() / 2f - texSize / 2f * scale + new Vector2(0f, 2f) * scale, null,
                        Color.White, 0f, Vector2.Zero, scale, 0, 0);
                };
            }
        }

        public override void OnCardDraw(Entity entity, BaseInfoComponent infoComponent, SpriteBatch sb, HookInfo hookInfo)
        {
            base.OnCardDraw(entity, infoComponent, sb, hookInfo);
            var info = entity.GetComponent<CardInfoComponent>();

            var size = new Point((int)(Texture.Width * infoComponent.Scale),
                (int)(Texture.Height * infoComponent.Scale));
            var drawComponent = entity.GetComponent<DrawComponent>();
            var drawsize = drawComponent.DrawSize;

            DrawUtils.SetDrawRenderTarget(sb, (spriteBatch) =>
            {
                OnDrawIllustration?.Invoke(spriteBatch, entity, this, OdeMod.RenderTarget2DPool.PoolOther(size, "Card Illustration"), size);
            }, OdeMod.RenderTarget2DPool.PoolOther(size, "Card Illustration"), drawComponent.Render, drawComponent.RenderSwap);

            sb.End();
            var effect = ModContent.Request<Effect>("OdeMod/Effects/PixelShaders/Mapping", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
            sb.GraphicsDevice.Textures[0] = Texture;
            sb.GraphicsDevice.Textures[1] = OdeMod.RenderTarget2DPool.PoolOther(size, "Card Illustration");
            sb.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointWrap,
                DepthStencilState.Default, RasterizerState.CullNone, effect);

            sb.Draw(Texture,
                new Rectangle((int)(drawsize.X / 2 - size.X / 2 + 4f * infoComponent.Scale),
                (int)(5 * 4 * infoComponent.Scale), size.X, size.Y), Color.White);

            sb.End();
            sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp,
                DepthStencilState.Default, RasterizerState.CullNone, null, Matrix.Invert(Main.GameViewMatrix.EffectMatrix));
        }

        public override DrawComponentBase PrimitiveClone(DrawComponent drawComponent)
        {
            var op = new CardIllustrationComponent(Texture, Illustration, false);
            op.OnDrawIllustration = OnDrawIllustration;
            return op;
        }
    }
}