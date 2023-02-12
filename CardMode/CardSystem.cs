using System.Collections.Generic;
using System.Data;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OdeMod.CardMode.UI;
using OdeMod.CardMode.Utils;
using OdeMod.Systems;
using OdeMod.Utils;
using OdeMod.Utils.Expands;
using OdeMod.Utils.Geometry;

using Terraria;
using Terraria.ModLoader;

namespace OdeMod.CardMode
{
    internal class CardSystem : ModSystem, ICardMode, IOdeSystem
    {
        public bool CardModeVisible
        {
            get => _cardModeVisible;
            set
            {
                if (_cardModeVisible != value)
                {
                    _cardModeVisible = value;
                    if (_cardModeVisible)
                        openCardMode();
                    else
                        closeCardMode();
                }
            }
        }

        private bool _cardModeVisible = false;
        public static CardSystem Instance => ModContent.GetInstance<CardSystem>();
        public static MouseInfo GetMouseInfo => Instance.MouseInfo;
        public Map Map;
        private Point ScreenSize;
        public MouseInfo MouseInfo;
        public CardModeUISystem CardModeUISystem;

        public CardSystem()
        {
            Map = new Map();
            MouseInfo = new MouseInfo();
            CardModeUISystem = new CardModeUISystem();
        }

        public override void Load()
        {
            base.Load();
            Map.MapSize = new Point(200, 200);
            Map.Build();
            CardModeUISystem.Load();
        }

        public void Draw(SpriteBatch sb)
        {
            CardModeUISystem.Draw(sb);

            List<Triangle> triangles = new List<Triangle>()
            {
                new Triangle(MouseInfo.MousePosition,
                Vector2Expand.GetVector2ByRotation(MathHelper.PiOver2 * 8f / 24f,26f,MouseInfo.MousePosition),
                Vector2Expand.GetVector2ByRotation(MathHelper.PiOver2 * 13f / 24f,22f,MouseInfo.MousePosition)),
                new Triangle(MouseInfo.MousePosition,
                Vector2Expand.GetVector2ByRotation(MathHelper.PiOver2 * 13f/ 24f,22f,MouseInfo.MousePosition),
                Vector2Expand.GetVector2ByRotation(MathHelper.PiOver2 * 18f / 24f,26f,MouseInfo.MousePosition))
            };
            List<Color> colors = new List<Color>()
            {
                MouseInfo.MouseLeftDown ? Color.Red : Color.White,
                MouseInfo.MouseRightDown ? Color.Green : Color.White
            };
            List<float> thicknesses = new List<float>()
            {
                0f,0f
            };
            DrawUtils.DrawTriangles(sb, triangles, colors, thicknesses, true);
        }

        public void Update(GameTime gt)
        {
            Main.audioSystem.UpdateAudioEngine();
            Main.audioSystem.Update();
            Main.audioSystem.UpdateMisc();

            //MusicLoader.GetMusic(OdeMod.Instance, "Sounds/Music/Boss/Glorious Carol").Play();
            //(Main.audioSystem as LegacyAudioSystem).AudioTracks[MusicID.Plantera].Play();
            //float i = 1f;
            //Main.audioSystem.UpdateCommonTrack(true, MusicLoader.GetMusicSlot("OdeMod/Sounds/Music/Boss/Glorious Carol"), 1f, ref i);
            //Main.audioSystem.UpdateAmbientCueState(MusicLoader.GetMusicSlot("OdeMod/Sounds/Music/Boss/Glorious Carol"), true, ref i, 1f);
            //Main.newMusic = MusicID.Plantera;

            MouseInfo.Update(gt);
            Main.mouseX = (int)MouseInfo.MouseX;
            Main.mouseY = (int)MouseInfo.MouseY;
            Main.mouseLeft = MouseInfo.MouseLeftDown;
            Main.mouseRight = MouseInfo.MouseRightDown;

            if (ScreenSize != Main.ScreenSize)
            {
                ScreenSize = Main.ScreenSize;
                CardModeUISystem.Calculation();
            }
            CardModeUISystem.Update(gt);
        }

        private void openCardMode()
        {
            Main.audioSystem.PauseAll();
        }

        private void closeCardMode()
        {
        }
    }
}