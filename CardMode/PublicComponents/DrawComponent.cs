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

        public RenderTarget2D Render => OdeMod.RenderTarget2DPool.Pool(DrawSize);
        public RenderTarget2D RenderSwap => OdeMod.RenderTarget2DPool.PoolSwap(DrawSize);

        public DrawComponent()
        {
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

            DrawUtils.SetDrawRenderTarget(sb, (spriteBatch) =>
            {
                OnCardDraw?.Invoke(Entity, info, spriteBatch);
            }, Render, Main.screenTarget, Main.screenTargetSwap);

            sb.Draw(Render, info.Center, null, Color.White, info.Rotation,
                    new Vector2(Render.Width, Render.Height) / 2f, 1f, SpriteEffects.None, 0f);
        }

        public override IComponent Clone(Entity cloneEntity)
        {
            DrawComponent component = new DrawComponent();
            component.OnCardDraw = OnCardDraw;
            return component;
        }
    }
}