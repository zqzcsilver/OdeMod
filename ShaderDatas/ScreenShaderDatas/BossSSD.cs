using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OdeMod.Utils;

using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace OdeMod.ShaderDatas.ScreenShaderDatas
{
    internal class BossSSD : OdeScreenShaderData, IOde
    {
        public BossSSD(Ref<Effect> shader, string passName) : base(shader, passName)
        {
        }
        public override void Apply()
        {
            Shader.GraphicsDevice.Textures[4] = OdeMod.RenderTarget2DPool.PoolOther(Main.ScreenSize, "MiracleRecorder:Decrate");
            base.Apply();
        }
    }
}