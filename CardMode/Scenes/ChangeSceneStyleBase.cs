using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;

namespace OdeMod.CardMode.Scenes
{
    internal abstract class ChangeSceneStyleBase
    {
        protected SceneBase LastScene { get; private set; }
        protected SceneBase NextScene { get; private set; }

        public void SetScene(SceneBase lastScene, SceneBase nextScene)
        {
            LastScene = lastScene;
            NextScene = nextScene;
        }

        public virtual bool Finish { get => true; }

        public virtual void OnBegin()
        {
        }

        public virtual void Update(GameTime gt)
        {
        }

        public virtual bool Draw(SpriteBatch sb)
        {
            return true;
        }
    }
}