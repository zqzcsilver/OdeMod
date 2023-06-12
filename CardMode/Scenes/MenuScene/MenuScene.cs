using System;

using Microsoft.Xna.Framework;

using Microsoft.Xna.Framework.Graphics;

using OdeMod.CardMode.Scenes.MenuScene.UIContainer;

using Terraria;

namespace OdeMod.CardMode.Scenes.MenuScene
{
    internal class MenuScene : SceneBase
    {
        public static readonly string SceneFullName = typeof(MenuScene).FullName;
        private float time = 0f, timeMax = 240f;

        public override void ChangeBegin()
        {
            base.ChangeBegin();
            CardSystem.Instance.CardModeUISystem.Elements[MenuContainer.ContainerFullName].Show();
        }

        public override void Changing()
        {
            base.Changing();
            CardSystem.Instance.CardModeUISystem.Elements[MenuContainer.ContainerFullName].Close();
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

            Texture2D texture = CardSystem.GetCardTexture("Scene/MenuScene/SceneBackground");

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