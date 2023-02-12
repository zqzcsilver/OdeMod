using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.Graphics.Shaders;

namespace OdeMod.ShaderDatas.ScreenShaderDatas
{
    internal class OdeScreenShaderData : ScreenShaderData, IScreenShaderData
    {
        public virtual bool Visible
        {
            get => _visible;
            set
            {
                if (_visible == value) return;
                _visible = value;
                if (_visible)
                    OnActivate();
                else
                    OnDeactivate();
            }
        }

        protected bool _visible = false;

        public OdeScreenShaderData(Ref<Effect> shader, string passName) : base(shader, passName)
        {
        }

        public virtual void OnActivate()
        {
        }

        public virtual void OnDeactivate()
        {
        }

        public virtual void OnUpdate()
        {
        }

        public virtual void OnRegister()
        {
        }

        public virtual void OnRemove()
        {
        }
    }
}