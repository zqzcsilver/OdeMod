using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OdeMod.CardMode.Scenes.ConfigScene.ConfigSystem.Configs;
using OdeMod.CardMode.ScreenEffectSystem.ScreenEffects;

namespace OdeMod.CardMode.Scenes.ChangeSceneStyles
{
    internal class FadeStyle : ChangeSceneStyleBase
    {
        private float lastTimerMax, nextTimerMax, lastTimer, nextTimer, lastLight, nextLight;
        private bool isFinish = false;
        public override bool Finish => isFinish;

        public FadeStyle(float lastSceneChange = 120f, float nextSceneChange = 120f, float lastSceneLight = 2f, float nextSceneLight = 2f)
        {
            var ic = CardSystem.ConfigManager.GetConfig<InterfaceConfig>();
            lastTimerMax = lastSceneChange * ic.AnimationSpeed;
            nextTimerMax = nextSceneChange * ic.AnimationSpeed;
            lastLight = lastSceneLight;
            nextLight = nextSceneLight;
        }

        public override void OnBegin()
        {
            base.OnBegin();
            lastTimer = 0f;
            nextTimer = 0f;
            CardSystem.ScreenEffectManager.GetFinallyScreenEffect("FadeScreenEffect").Activation();
        }

        public override bool Draw(SpriteBatch sb)
        {
            bool isNext = lastTimer >= lastTimerMax;

            if (isNext)
                NextScene?.Draw(sb);
            else
                LastScene?.Draw(sb);

            float distance = isNext ? nextTimer / nextTimerMax * nextLight :
               (lastTimerMax - lastTimer) / lastTimerMax * lastLight;
            var fse = (FadeScreenEffect)CardSystem.ScreenEffectManager.GetFinallyScreenEffect("FadeScreenEffect");
            fse.Alpha = distance;
            fse.Distance = MathHelper.Lerp(0f, (float)Math.Sqrt(0.5), distance);

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
                {
                    LastScene?.Changing();
                    NextScene?.ChangeBegin();
                }
            }
            else if (nextTimer < nextTimerMax)
            {
                nextTimer += 1f;
                NextScene?.Update(gt);
            }
            else
            {
                NextScene?.ChangeEnd();
                CardSystem.ScreenEffectManager.GetFinallyScreenEffect("FadeScreenEffect").Deactivation();
                isFinish = true;
            }
        }
    }
}