using Microsoft.Xna.Framework.Graphics;

using OdeMod.Shaders;

using Terraria;
using Terraria.Graphics.Shaders;

namespace OdeMod.ShaderDatas
{
    internal class OdeShaderData : ShaderData, IShader
    {
        public bool Visible
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

        private bool _visible = false;

        public OdeShaderData(Ref<Effect> shader, string passName) : base(shader, passName)
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
    }
}