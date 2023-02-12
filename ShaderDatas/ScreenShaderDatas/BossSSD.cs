using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ModLoader;

namespace OdeMod.ShaderDatas.ScreenShaderDatas
{
    internal class BossSSD : OdeScreenShaderData
    {
        public float BlurFactor = 0f;
        public Vector2 LightRange = new Vector2(0.8f, 1f);

        public float Alpha
        {
            get => _alpha;
            set
            {
                _alpha = value;
                WaitToAlpha = value;
            }
        }
        public float WaitToAlpha
        {
            get => _waitToAlpha;
            set => _waitToAlpha = value;
        }
        private float _alpha = 0f;
        private float _waitToAlpha = 2f;

        public float MaxDistance
        {
            get => _maxDistance;
            set
            {
                _maxDistance = value;
                _waitToMaxDistance = value;
            }
        }
        public float WaitToMaxDistance
        {
            get => _waitToMaxDistance;
            set => _waitToMaxDistance = value;
        }
        private float _maxDistance = 2f;
        private float _waitToMaxDistance = 1.2f;
        private bool _waitToVisible = false;

        public override bool Visible
        {
            get => base.Visible;
            set
            {
                _waitToVisible = value;
                if (_waitToVisible)
                {
                    _visible = true;
                    _waitToMaxDistance = 1.2f;
                    _maxDistance = 2f;
                    Alpha = 2f;
                }
            }
        }

        public BossSSD(Ref<Effect> shader, string passName) : base(shader, passName)
        {
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (!_waitToVisible)
            {
                _waitToMaxDistance += 0.01f;
                Alpha = 0f;
                if (_maxDistance >= 2f && _maxDistance == _waitToMaxDistance)
                    _visible = false;
            }

            if (_waitToMaxDistance != _maxDistance)
            {
                _maxDistance += (_waitToMaxDistance - _maxDistance) / 10f;
            }
            if (_waitToAlpha != _alpha)
            {
                _alpha += (_waitToAlpha - _alpha) / 10f;
            }
        }

        public override void Apply()
        {
            Shader.GraphicsDevice.Textures[4] = ModContent.Request<Texture2D>("OdeMod/Images/Effects/Night", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
            Shader.GraphicsDevice.Textures[5] = OdeMod.RenderTarget2DPool.PoolOther(Main.ScreenSize, "MiracleRecorder:Night");

            Shader.Parameters["uAlpha"].SetValue(Alpha);
            Shader.Parameters["uMaxDistance"].SetValue(MaxDistance);
            base.Apply();
        }
    }
}