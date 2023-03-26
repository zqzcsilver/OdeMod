using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria;

namespace OdeMod.CardMode.Scenes.CharacterSelectionScene
{
    internal class CharacterSelectionScene : SceneBase
    {
        private float time = 0f, timeMax = 240f;

        public override void ChangeBegin()
        {
            base.ChangeBegin();
        }

        public override void BeSelected()
        {
            base.BeSelected();
        }

        public override void ExitSelected()
        {
            base.ExitSelected();
        }

        public override void Changing()
        {
            base.Changing();
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

            Texture2D texture = CardSystem.GetCardTexture("Scene/CharacterSelectionScene");

            Effect effect = CardSystem.AssetManager.Request<Effect>("OdeMod/Effects/PixelShaders/BrightnessGradient");
            effect.Parameters["uAlpha"].SetValue(1f);
            effect.Parameters["uMaxDistance"].SetValue(Math.Abs(timeMax / 2f - time) / timeMax * 2f * 0.05f + 0.95f);
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