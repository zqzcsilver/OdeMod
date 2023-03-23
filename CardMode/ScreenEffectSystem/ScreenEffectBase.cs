using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OdeMod.CardMode.ScreenEffectSystem
{
    internal abstract class ScreenEffectBase
    {
        public virtual Effect Effect { get; private set; }
        public virtual bool Visible { get; private set; }

        public ScreenEffectBase(Effect effect)
        {
            Effect = effect;
        }

        public virtual void Update(GameTime gt)
        {
        }

        public virtual void Draw(SpriteBatch sb, RenderTarget2D screenTarget, RenderTarget2D screenTargetSwap)
        {
        }

        public virtual void Activation()
        {
            Visible = true;
        }

        public virtual void Deactivation()
        {
            Visible = false;
        }
    }
}