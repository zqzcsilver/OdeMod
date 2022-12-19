using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System.Linq;

using Terraria;
using Terraria.ModLoader;

namespace OdeMod.ShaderDatas.ScreenShaderDatas
{
    internal class BossSSD : OdeScreenShaderData
    {
        public float BlurFactor = 0f;
        public Vector2 LightRange = new Vector2(0.8f, 1f);
        public float Alpha = 2f;
        public float MaxDistance = 1.4f;
        public BossSSD(Ref<Effect> shader, string passName) : base(shader, passName)
        {
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