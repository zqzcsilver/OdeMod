using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using OdeMod.CardMode.GameInfos;
using OdeMod.CardMode.Scenes.ChangeSceneStyles;
using OdeMod.CardMode.Scenes.CharacterSelectionScene.UIContainer;
using OdeMod.Utils;
using OdeMod.Utils.Geometry;

using Terraria;
using Terraria.GameContent;

namespace OdeMod.CardMode.Scenes.CharacterSelectionScene
{
    internal class CharacterSelectionScene : SceneBase
    {
        public static readonly string SceneFullName = typeof(CharacterSelectionScene).FullName;
        private float time = 0f, timeMax = 240f;
        private Vector2 baseDrawOffset, drawOffset, waitToDrawOffset;
        private float baseDrawScale = 1f, drawScale = 1f, waitToDrawScale = 1f;
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
                if (CardSystem.GameInfo.CharacterType != CharacterType.Melee)
                {
                    CardSystem.GameInfo.CharacterType = CharacterType.Melee;
                    Vector2 csbSize = csb.MaxSize;
                    waitToDrawScale = Main.screenHeight / csbSize.Y * 0.7f;
                    waitToDrawOffset = Main.ScreenSize.ToVector2() / 2f + baseDrawOffset -
                        csb.Center(baseDrawOffset, baseDrawScale * waitToDrawScale);

                    CardSystem.Instance.CardModeUISystem.Elements[CharacterSelectionContainer.ContainerFullName].Close();
                    CardSystem.Instance.CardModeUISystem.Elements[CharacterSelectionContainer.ContainerFullName].Show(6f);
                }
                else
                {
                    CardSystem.GameInfo.CharacterType = CharacterType.None;
                    waitToDrawScale = 1f;
                    waitToDrawOffset = Vector2.Zero;

                    CardSystem.Instance.CardModeUISystem.Elements[CharacterSelectionContainer.ContainerFullName].Close(6f);
                }
            };

            ranger = new CharacterSelectionBox(new Vector2(672, 294),
                new Vector2(892, 294), new Vector2(674, 510), new Vector2(890, 504));
            ranger.OnLeftClick = csb =>
            {
                if (CardSystem.GameInfo.CharacterType != CharacterType.Ranger)
                {
                    CardSystem.GameInfo.CharacterType = CharacterType.Ranger;
                    Vector2 csbSize = csb.MaxSize;
                    waitToDrawScale = Main.screenHeight / csbSize.Y * 0.7f;
                    waitToDrawOffset = Main.ScreenSize.ToVector2() / 2f + baseDrawOffset -
                        csb.Center(baseDrawOffset, baseDrawScale * waitToDrawScale);

                    CardSystem.Instance.CardModeUISystem.Elements[CharacterSelectionContainer.ContainerFullName].Close();
                    CardSystem.Instance.CardModeUISystem.Elements[CharacterSelectionContainer.ContainerFullName].Show(6f);
                }
                else
                {
                    CardSystem.GameInfo.CharacterType = CharacterType.None;
                    waitToDrawScale = 1f;
                    waitToDrawOffset = Vector2.Zero;

                    CardSystem.Instance.CardModeUISystem.Elements[CharacterSelectionContainer.ContainerFullName].Close(6f);
                }
            };

            mage = new CharacterSelectionBox(new Vector2(1032, 292),
                new Vector2(1242, 296), new Vector2(1038, 510), new Vector2(1244, 510));
            mage.OnLeftClick = csb =>
            {
                if (CardSystem.GameInfo.CharacterType != CharacterType.Mage)
                {
                    CardSystem.GameInfo.CharacterType = CharacterType.Mage;
                    Vector2 csbSize = csb.MaxSize;
                    waitToDrawScale = Main.screenHeight / csbSize.Y * 0.7f;
                    waitToDrawOffset = Main.ScreenSize.ToVector2() / 2f + baseDrawOffset -
                        csb.Center(baseDrawOffset, baseDrawScale * waitToDrawScale);

                    CardSystem.Instance.CardModeUISystem.Elements[CharacterSelectionContainer.ContainerFullName].Close();
                    CardSystem.Instance.CardModeUISystem.Elements[CharacterSelectionContainer.ContainerFullName].Show(6f);
                }
                else
                {
                    CardSystem.GameInfo.CharacterType = CharacterType.None;
                    waitToDrawScale = 1f;
                    waitToDrawOffset = Vector2.Zero;

                    CardSystem.Instance.CardModeUISystem.Elements[CharacterSelectionContainer.ContainerFullName].Close(6f);
                }
            };

            summoner = new CharacterSelectionBox(new Vector2(1370, 296),
                new Vector2(1562, 304), new Vector2(1360, 510), new Vector2(1550, 516));
            summoner.OnLeftClick = csb =>
            {
                if (CardSystem.GameInfo.CharacterType != CharacterType.Summoner)
                {
                    CardSystem.GameInfo.CharacterType = CharacterType.Summoner;
                    Vector2 csbSize = csb.MaxSize;
                    waitToDrawScale = Main.screenHeight / csbSize.Y * 0.7f;
                    waitToDrawOffset = Main.ScreenSize.ToVector2() / 2f + baseDrawOffset -
                        csb.Center(baseDrawOffset, baseDrawScale * waitToDrawScale);

                    CardSystem.Instance.CardModeUISystem.Elements[CharacterSelectionContainer.ContainerFullName].Close();
                    CardSystem.Instance.CardModeUISystem.Elements[CharacterSelectionContainer.ContainerFullName].Show(6f);
                }
                else
                {
                    CardSystem.GameInfo.CharacterType = CharacterType.None;
                    waitToDrawScale = 1f;
                    waitToDrawOffset = Vector2.Zero;

                    CardSystem.Instance.CardModeUISystem.Elements[CharacterSelectionContainer.ContainerFullName].Close(6f);
                }
            };

            painter = new CharacterSelectionBox(new Vector2(72, 380),
                new Vector2(156, 348), new Vector2(74, 538), new Vector2(160, 486));
            painter.OnLeftClick = csb =>
            {
                if (CardSystem.GameInfo.CharacterType != CharacterType.Painter)
                {
                    CardSystem.GameInfo.CharacterType = CharacterType.Painter;
                    Vector2 csbSize = csb.MaxSize;
                    waitToDrawScale = Main.screenHeight / csbSize.Y * 0.7f;
                    waitToDrawOffset = Main.ScreenSize.ToVector2() / 2f + baseDrawOffset -
                        csb.Center(baseDrawOffset, baseDrawScale * waitToDrawScale);

                    CardSystem.Instance.CardModeUISystem.Elements[CharacterSelectionContainer.ContainerFullName].Close();
                    CardSystem.Instance.CardModeUISystem.Elements[CharacterSelectionContainer.ContainerFullName].Show(6f);
                }
                else
                {
                    CardSystem.GameInfo.CharacterType = CharacterType.None;
                    waitToDrawScale = 1f;
                    waitToDrawOffset = Vector2.Zero;

                    CardSystem.Instance.CardModeUISystem.Elements[CharacterSelectionContainer.ContainerFullName].Close(6f);
                }
            };
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

            if (CardSystem.KeyBoardInputManager.IsKeyClick(Keys.Escape))
                CardSystem.SceneManager.BackLastScene(new FadeStyle());

            melee.Update(gt, baseDrawOffset + drawOffset, baseDrawScale * drawScale);
            ranger.Update(gt, baseDrawOffset + drawOffset, baseDrawScale * drawScale);
            mage.Update(gt, baseDrawOffset + drawOffset, baseDrawScale * drawScale);
            summoner.Update(gt, baseDrawOffset + drawOffset, baseDrawScale * drawScale);
            painter.Update(gt, baseDrawOffset + drawOffset, baseDrawScale * drawScale);

            if (drawScale != waitToDrawScale)
                drawScale += (waitToDrawScale - drawScale) / 6f;
            if (drawOffset != waitToDrawOffset)
                drawOffset += (waitToDrawOffset - drawOffset) / 6f;

            time++;
            if (time >= timeMax)
                time = 0f;
        }

        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);

            //melee.Draw(sb, baseDrawOffset + drawOffset, baseDrawScale * drawScale);
            //ranger.Draw(sb, baseDrawOffset + drawOffset, baseDrawScale * drawScale);
            //mage.Draw(sb, baseDrawOffset + drawOffset, baseDrawScale * drawScale);
            //summoner.Draw(sb, baseDrawOffset + drawOffset, baseDrawScale * drawScale);
            //painter.Draw(sb, baseDrawOffset + drawOffset, baseDrawScale * drawScale);

            sb.End();
            sb.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp,
                DepthStencilState.Default, RasterizerState.CullNone, null);

            Texture2D texture = CardSystem.GetCardTexture("Scene/CharacterSelectionScene/SceneBackground");

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

            public Vector2 Center(Vector2 offset, float scale)
            {
                return (_pos1 + _pos2 + _pos3 + _pos4) * scale / 4f + offset;
            }

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
                if (triangle.Contain(mousePos))
                    return true;
                triangle = new Triangle(_pos2 * scale, _pos3 * scale, _pos4 * scale);
                triangle += offset;
                if (triangle.Contain(mousePos))
                    return true;
                return false;
            }

            public void Draw(SpriteBatch sb, Vector2 offset, float scale)
            {
                List<Triangle> triangles = new List<Triangle>();
                List<Color> colors = new List<Color>();

                Triangle triangle = new Triangle(_pos1 * scale, _pos2 * scale, _pos3 * scale);
                triangle += offset;
                triangles.Add(triangle);
                colors.Add(Color.White);

                triangle = new Triangle(_pos2 * scale, _pos3 * scale, _pos4 * scale);
                triangle += offset;
                triangles.Add(triangle);
                colors.Add(Color.Green);

                DrawUtils.DrawTriangles(sb, triangles, colors);

                var center = Center(offset, scale).ToPoint();
                sb.Draw(TextureAssets.MagicPixel.Value, new Rectangle(center.X - 2, center.Y - 2, 4, 4), Color.White);
            }
        }
    }
}