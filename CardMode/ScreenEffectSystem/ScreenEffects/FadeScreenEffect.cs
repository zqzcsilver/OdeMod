using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;

using Microsoft.Xna.Framework.Graphics;

using OdeMod.Utils;

using Terraria.ModLoader;

namespace OdeMod.CardMode.ScreenEffectSystem.ScreenEffects
{
    internal class FadeScreenEffect : ScreenEffectBase
    {
        public float Alpha, Distance;

        public FadeScreenEffect() : base(CardSystem.AssetManager.Request<Effect>("OdeMod/Effects/PixelShaders/BrightnessGradient"))
        {
        }

        public override void Draw(SpriteBatch sb, RenderTarget2D screenTarget, RenderTarget2D screenTargetSwap)
        {
            base.Draw(sb, screenTarget, screenTargetSwap);

            Effect effect = CardSystem.AssetManager.Request<Effect>("OdeMod/Effects/PixelShaders/BrightnessGradient");
            effect.Parameters["uAlpha"].SetValue(Alpha);
            effect.Parameters["uMaxDistance"].SetValue(Distance);
            DrawUtils.SetEffectToScreen(sb, effect);
        }
    }
}