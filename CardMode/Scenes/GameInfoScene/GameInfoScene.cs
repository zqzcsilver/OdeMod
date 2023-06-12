using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using OdeMod.CardMode.Scenes.ChangeSceneStyles;

namespace OdeMod.CardMode.Scenes.GameInfoScene
{
    internal class GameInfoScene : SceneBase
    {
        public static readonly string SceneFullName = typeof(GameInfoScene).FullName;

        public override void Update(GameTime gt)
        {
            base.Update(gt);
            if (CardSystem.KeyBoardInputManager.IsKeyClick(Keys.Escape))
                CardSystem.SceneManager.BackLastScene(new FadeStyle());
        }
    }
}