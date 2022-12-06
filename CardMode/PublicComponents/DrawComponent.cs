using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OdeMod.Utils;

using System;
using System.Collections.Generic;

using Terraria;

namespace OdeMod.CardMode.PublicComponents
{
    internal delegate void CardDrawHandle(Entity card, BaseInfoComponent infoComponent, SpriteBatch sb);

    internal class DrawComponent : Component
    {
        public CardDrawHandle OnCardDraw;
        public BaseInfoComponent Info => Entity.GetComponent<BaseInfoComponent>();

        public Point DrawSize;

        private RenderTarget2D render, renderSwap;
        public RenderTarget2D Render => render;
        public RenderTarget2D RenderSwap => renderSwap;

        public DrawComponent()
        {
        }

        public override void UnLoad()
        {
            base.UnLoad();
            render?.Dispose();
            render = null;
            renderSwap?.Dispose();
            renderSwap = null;
        }

        public override List<Type> GetDependComponents()
        {
            return new List<Type> { typeof(BaseInfoComponent) };
        }

        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);
            var info = Info;
            var size = DrawSize;
            if (render == null)
            {
                render = new RenderTarget2D(Main.graphics.GraphicsDevice, size.X, size.Y, false,
                    Main.graphics.GraphicsDevice.PresentationParameters.BackBufferFormat, DepthFormat.None);
            }
            if (renderSwap == null)
            {
                renderSwap = new RenderTarget2D(Main.graphics.GraphicsDevice, size.X, size.Y, false,
                    Main.graphics.GraphicsDevice.PresentationParameters.BackBufferFormat, DepthFormat.None);
            }
            DrawUtils.SetDrawRenderTarget(sb, (spriteBatch) =>
            {
                OnCardDraw?.Invoke(Entity, info, spriteBatch);
            }, render, Main.screenTarget, Main.screenTargetSwap);
            sb.Draw(render, info.Center, null, Color.White, info.Rotation,
                    new Vector2(render.Width, render.Height) / 2f, 1f, SpriteEffects.None, 0f);
        }

        public override IComponent Clone(Entity cloneEntity)
        {
            DrawComponent component = new DrawComponent();
            component.OnCardDraw = OnCardDraw;
            return component;
        }
    }
}