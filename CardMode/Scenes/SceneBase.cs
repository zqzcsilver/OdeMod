using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;

namespace OdeMod.CardMode.Scenes
{
    internal abstract class SceneBase
    {
        public bool ChangeFinish { get; protected set; }

        public virtual void OnInit()
        {
            LoadContent();
            ChangeFinish = false;
        }

        public virtual void BeSelected()
        {
        }

        public virtual void ExitSelected()
        {
        }

        public virtual void LoadContent()
        {
        }

        public virtual void ChangeBegin()
        {
            ChangeFinish = false;
        }

        public virtual void ChangeEnd()
        {
            ChangeFinish = true;
        }

        public virtual void UnLoad()
        {
        }

        public virtual void Update(GameTime gt)
        {
        }

        public virtual void Draw(SpriteBatch sb)
        {
        }
    }
}