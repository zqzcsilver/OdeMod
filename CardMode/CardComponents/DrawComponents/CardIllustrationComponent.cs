﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OdeMod.CardMode.CardComponents.BaseComponents;
using OdeMod.CardMode.PublicComponents;

using Terraria;
using Terraria.ModLoader;

namespace OdeMod.CardMode.CardComponents.DrawComponents
{
    internal delegate void DrawIllustration(SpriteBatch sb, RenderTarget2D render, Point renderSize);

    internal class CardIllustrationComponent : DrawComponentBase
    {
        public DrawIllustration OnDrawIllustration;

        public RenderTarget2D Render
        {
            get
            {
                var info = Entity.GetComponent<CardInfoComponent>();
                var infoComponent = Entity.GetComponent<BaseInfoComponent>();
                var size = new Point((int)(info.CardIllustrationTexture.Width * infoComponent.Scale),
                    (int)(info.CardIllustrationTexture.Height * infoComponent.Scale));
                return OdeMod.RenderTarget2DPool.Pool(size);
            }
        }

        public CardIllustrationComponent(bool useDefaultDrawStyle = true)
        {
            if (useDefaultDrawStyle)
            {
                OnDrawIllustration += (sb, render, renderSize) =>
                {
                    CardInfoComponent infoComponent = Entity.GetComponent<CardInfoComponent>();
                    Vector2 texSize = new Vector2(infoComponent.CardIllustration.Width, infoComponent.CardIllustration.Height);
                    float scale = MathHelper.Max((float)renderSize.X / texSize.X, (float)renderSize.Y / texSize.Y) * 0.7f;
                    sb.Draw(infoComponent.CardIllustration, renderSize.ToVector2() / 2f - texSize / 2f * scale, null,
                        Color.White, 0f, Vector2.Zero, scale, 0, 0);
                };
            }
        }

        protected override void OnCardDraw(Entity entity, BaseInfoComponent infoComponent, SpriteBatch sb)
        {
            base.OnCardDraw(entity, infoComponent, sb);

            var info = entity.GetComponent<CardInfoComponent>();

            var size = new Point((int)(info.CardIllustrationTexture.Width * infoComponent.Scale),
                (int)(info.CardIllustrationTexture.Height * infoComponent.Scale));
            var drawsize = DrawComponent.DrawSize;

            Utils.DrawUtils.SetDrawRenderTarget(sb, (spriteBatch) =>
            {
                OnDrawIllustration?.Invoke(spriteBatch, OdeMod.RenderTarget2DPool.Pool(size), size);
            }, OdeMod.RenderTarget2DPool.Pool(size), DrawComponent.Render, DrawComponent.RenderSwap);

            sb.End();
            var effect = ModContent.Request<Effect>("OdeMod/Effects/PixelShaders/Mapping", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
            effect.Parameters["SpriteTexture"].SetValue(info.CardIllustrationTexture);
            effect.Parameters["MappingSpriteTexture"].SetValue(OdeMod.RenderTarget2DPool.Pool(size));
            sb.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointWrap,
                DepthStencilState.Default, RasterizerState.CullNone, effect);

            sb.Draw(info.CardIllustrationTexture,
                new Rectangle((int)(drawsize.X / 2 - size.X / 2 + infoComponent.Scale),
                (int)(5 * infoComponent.Scale), size.X, size.Y), Color.White);

            sb.End();
            sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp,
                DepthStencilState.Default, RasterizerState.CullNone, null, Matrix.Invert(Main.GameViewMatrix.EffectMatrix));
        }

        public override IComponent Clone(Entity cloneEntity)
        {
            return new CardIllustrationComponent();
        }
    }
}