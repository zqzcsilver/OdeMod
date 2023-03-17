using System.Collections.Generic;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OdeMod.CardMode.PublicComponents.LogicComponents;
using OdeMod.CardMode.Scenes;
using OdeMod.CardMode.Scenes.ConfigScene.ConfigSystem;
using OdeMod.CardMode.Scenes.MenuScene;
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
        public const string ENTITY_SOURCE_FROM_SYSTEM = "Ode Mod - Card System Mode";
        public static readonly string SavePath = Path.Combine(Main.SavePath, "OdeMod", "CardMode");

        public bool CardModeVisible
        {
            get => _cardModeVisible;
            set
            {
                if (_cardModeVisible != value)
                {
                    if (value)
                        openCardMode();
                    else
                        closeCardMode();
                    _cardModeVisible = value;
                }
            }
        }

        private bool _cardModeVisible = false;
        public static CardSystem Instance { get; private set; }
        public static MouseInfo GetMouseInfo => Instance.MouseInfo;
        public Map Map;
        private Point ScreenSize;
        public MouseInfo MouseInfo;
        private ConfigManager _configManager;
        public static ConfigManager ConfigManager => Instance._configManager;
        public CardModeUISystem CardModeUISystem { get; private set; }
        public PlayerManager PlayerManager { get; private set; }

        private SceneManager _sceneManager;
        public static SceneManager SceneManager { get => Instance._sceneManager; }

        public CardSystem()
        {
            Instance = this;

            Map = new Map();
            MouseInfo = new MouseInfo();
            _configManager = new ConfigManager();
            CardModeUISystem = new CardModeUISystem();
            PlayerManager = new PlayerManager();
            _sceneManager = new SceneManager();
        }

        public override void Load()
        {
            base.Load();
            Map.MapSize = new Point(200, 200);
            Map.Build();

            _configManager.LoadConfigs();
            CardModeUISystem.Load();
            _sceneManager.Init();

            var p = PlayerManager.CreatePlayer();
            Map.BindingMoveComponent = p.GetComponent<MoveComponent>();
            PlayerManager.AddPlayer(p);
        }

        public void Draw(SpriteBatch sb)
        {
            PlayerManager.Draw(sb);
            _sceneManager.Draw(sb);
            //Map.DrawBackground(sb);
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

            _sceneManager.Update(gt);

            PlayerManager.Update(gt);
        }

        private void openCardMode()
        {
            Main.audioSystem.PauseAll();
            loadAsset();
            SceneManager.ChangeScene("OdeMod.CardMode.Scenes.MenuScene.MenuScene");
        }

        private void loadAsset()
        {
            ModContent.Request<Texture2D>("OdeMod/Images/Effects/Night", ReLogic.Content.AssetRequestMode.ImmediateLoad);
            ModContent.Request<Effect>("OdeMod/Effects/PixelShaders/BrightnessGradient", ReLogic.Content.AssetRequestMode.ImmediateLoad);
        }

        private void closeCardMode()
        {
            SceneManager.ChangeScene((SceneBase)null);
        }
    }
}