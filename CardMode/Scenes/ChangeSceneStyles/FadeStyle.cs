using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OdeMod.Utils;

using Terraria.ModLoader;

namespace OdeMod.CardMode.Scenes.ChangeSceneStyles
{
    internal class FadeStyle : ChangeSceneStyleBase
    {
        private float lastTimerMax, nextTimerMax, lastTimer, nextTimer, lastLight, nextLight;
        private bool isFinish = false;
        public override bool Finish => isFinish;

        public FadeStyle(float lastSceneChange = 120f, float nextSceneChange = 120f, float lastSceneLight = 2f, float nextSceneLight = 2f)
        {
            lastTimerMax = lastSceneChange;
            nextTimerMax = nextSceneChange;
            lastLight = lastSceneLight;
            nextLight = nextSceneLight;
        }

        public override void OnBegin()
        {
            base.OnBegin();
            lastTimer = 0f;
            nextTimer = 0f;
        }

        public override bool Draw(SpriteBatch sb)
        {
            bool isNext = lastTimer >= lastTimerMax;

            if (isNext)
                NextScene?.Draw(sb);
            else
                LastScene?.Draw(sb);

            Effect effect = ModContent.Request<Effect>("OdeMod/Effects/PixelShaders/BrightnessGradient").Value;
            float distance = isNext ? nextTimer / nextTimerMax * nextLight :
               (lastTimerMax - lastTimer) / lastTimerMax * lastLight;
            effect.Parameters["uAlpha"].SetValue(distance);
            effect.Parameters["uMaxDistance"].SetValue(MathHelper.Lerp(0f, (float)Math.Sqrt(0.5), distance));
            DrawUtils.SetEffectToScreen(sb, effect);

            return false;
        }

        public override void Update(GameTime gt)
        {
            base.Update(gt);
            if (lastTimer < lastTimerMax)
            {
                lastTimer += 1f;
                LastScene?.Update(gt);
                if (lastTimer >= lastTimerMax)
                    NextScene?.ChangeBegin();
            }
            else if (nextTimer < nextTimerMax)
            {
                nextTimer += 1f;
                NextScene?.Update(gt);
            }
            else
            {
                NextScene?.ChangeEnd();
                isFinish = true;
            }
        }
    }
}