using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OdeMod.Utils.Geometry;

using Terraria;

namespace OdeMod.CardMode.Scenes.CharacterSelectionScene
{
    internal class CharacterSelectionScene : SceneBase
    {
        private float time = 0f, timeMax = 240f;
        private Vector2 baseDrawOffset, drawOffset;
        private float baseDrawScale, drawScale = 1f, waitToDrawScale = 1f;
        private CharacterSelectionBox melee, ranger, mage, summoner, painter;

        public override void ChangeBegin()
        {
            base.ChangeBegin();
        }

        public override void BeSelected()
        {
            base.BeSelected();
            melee = new CharacterSelectionBox(new Vector2(330, 320),
                new Vector2(534, 308), new Vector2(330, 512), new Vector2(528, 510));
            melee.OnLeftClick = csb =>
            {
                Vector2 csbSize = csb.MaxSize, csbCenter = csb.Center * baseDrawScale * drawScale;
                waitToDrawScale = Main.screenHeight / csbSize.Y;
                if (csbCenter.X - Main.screenWidth / 2f < (baseDrawOffset + drawOffset).X)
                {
                    drawOffset.X = csbCenter.X - Main.screenWidth / 2f - baseDrawOffset.X;
                }
                if (csbCenter.Y - Main.screenWidth / 2f < (baseDrawOffset + drawOffset).Y)
                {
                    drawOffset.Y = csbCenter.Y - Main.screenWidth / 2f - baseDrawOffset.Y;
                }
            };

            ranger = new CharacterSelectionBox(new Vector2(672, 294),
                new Vector2(892, 294), new Vector2(674, 510), new Vector2(890, 504));
            mage = new CharacterSelectionBox(new Vector2(1032, 292),
                new Vector2(1242, 296), new Vector2(1038, 510), new Vector2(1244, 510));
            summoner = new CharacterSelectionBox(new Vector2(1370, 296),
                new Vector2(1562, 304), new Vector2(1360, 510), new Vector2(1550, 516));
            painter = new CharacterSelectionBox(new Vector2(72, 380),
                new Vector2(156, 348), new Vector2(74, 538), new Vector2(160, 486));
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

            melee.Update(gt, baseDrawOffset + drawOffset, drawScale);
            ranger.Update(gt, baseDrawOffset + drawOffset, drawScale);
            mage.Update(gt, baseDrawOffset + drawOffset, drawScale);
            summoner.Update(gt, baseDrawOffset + drawOffset, drawScale);
            painter.Update(gt, baseDrawOffset + drawOffset, drawScale);

            if (drawScale != waitToDrawScale)
                drawScale += (waitToDrawScale - drawScale) / 6f;

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

            baseDrawScale = MathHelper.Max((float)Main.screenWidth / (float)texture.Width, (float)Main.screenHeight / (float)texture.Height);
            baseDrawOffset = new Vector2(Main.screenWidth, Main.screenHeight) / 2f - texture.Size() / 2f * baseDrawScale;
            sb.Draw(texture, baseDrawOffset + drawOffset, null, Color.White, 0f, Vector2.Zero,
                baseDrawScale * drawScale, 0, 0);

            sb.End();
            sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp,
                DepthStencilState.Default, RasterizerState.CullNone, null);
        }

        private class CharacterSelectionBox
        {
            public Action<CharacterSelectionBox> OnLeftClick;
            private Vector2 _pos1, _pos2, _pos3, _pos4;

            public Vector2 MaxSize
            {
                get
                {
                    Vector2 op = new Vector2(Math.Max(Math.Max(Math.Max(_pos1.X, _pos2.X), _pos3.X), _pos4.X) -
                        Math.Min(Math.Min(Math.Min(_pos1.X, _pos2.X), _pos3.X), _pos4.X),
                        Math.Max(Math.Max(Math.Max(_pos1.Y, _pos2.Y), _pos3.Y), _pos4.Y) -
                        Math.Min(Math.Min(Math.Min(_pos1.Y, _pos2.Y), _pos3.Y), _pos4.Y));
                    return op;
                }
            }

            public Vector2 Center => (_pos1 + _pos2 + _pos3 + _pos4) / 4f;

            public CharacterSelectionBox(Vector2 pos1, Vector2 pos2, Vector2 pos3, Vector2 pos4)
            {
                _pos1 = pos1;
                _pos2 = pos2;
                _pos3 = pos3;
                _pos4 = pos4;
            }

            public void Update(GameTime gt, Vector2 offset, float scale)
            {
                if (CardSystem.GetMouseInfo.MouseLeftClick && Collision(CardSystem.GetMouseInfo.MousePosition, offset, scale))
                {
                    OnLeftClick?.Invoke(this);
                }
            }

            public bool Collision(Vector2 mousePos, Vector2 offset, float scale)
            {
                Triangle triangle = new Triangle(_pos1 * scale, _pos2 * scale, _pos3 * scale);
                triangle += offset;
                if (!triangle.Contain(mousePos))
                    return false;
                triangle = new Triangle(_pos2 * scale, _pos3 * scale, _pos4 * scale);
                triangle += offset;
                if (!triangle.Contain(mousePos))
                    return false;
                return true;
            }
        }
    }
}