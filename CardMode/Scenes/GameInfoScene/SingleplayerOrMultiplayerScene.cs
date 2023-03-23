﻿using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using Terraria;

namespace OdeMod.CardMode.Scenes.GameInfoScene
{
    internal class SingleplayerOrMultiplayerScene : SceneBase
    {
        private float time = 0f, timeMax = 240f;

        public override void ChangeBegin()
        {
            base.ChangeBegin();
            CardSystem.Instance.CardModeUISystem.Elements["OdeMod.CardMode.Scenes.GameInfoScene.UIContainer.SingleplayerOrMultiplayerContainer"].Show();
        }

        public override void Changing()
        {
            base.Changing();
            CardSystem.Instance.CardModeUISystem.Elements["OdeMod.CardMode.Scenes.GameInfoScene.UIContainer.SingleplayerOrMultiplayerContainer"].Info.IsVisible = false;
        }

        public override void Update(GameTime gt)
        {
            base.Update(gt);
            time++;
            if (time >= timeMax)
                time = 0f;
        }

        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);
            sb.End();
            sb.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp,
                DepthStencilState.Default, RasterizerState.CullNone, null);

            Texture2D texture = CardSystem.GetCardTexture("Scene/MenuScene");

            Effect effect = CardSystem.AssetManager.Request<Effect>("OdeMod/Effects/PixelShaders/BrightnessGradient");
            effect.Parameters["uAlpha"].SetValue(1f);
            effect.Parameters["uMaxDistance"].SetValue(Math.Abs(timeMax / 2f - time) / timeMax * 2f * 0.2f + 0.8f);
            effect.Parameters["SpriteTexture"].SetValue(texture);
            effect.CurrentTechnique.Passes[0].Apply();

            float scale = MathHelper.Max((float)Main.screenWidth / (float)texture.Width, (float)Main.screenHeight / (float)texture.Height);
            sb.Draw(texture, new Vector2(Main.screenWidth, Main.screenHeight) / 2f - texture.Size() / 2f * scale, null,
                Color.White, 0f, Vector2.Zero, scale, 0, 0);

            sb.End();
            sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp,
                DepthStencilState.Default, RasterizerState.CullNone, null);
        }
    }
}