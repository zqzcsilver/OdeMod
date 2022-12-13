using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.Graphics.Shaders;

namespace OdeMod.ScreenShaders
{
    public class BossSSD : ScreenShaderData, IOde
    {
        public BossSSD(string passName) : base(passName)
        {
        }

        public BossSSD(Ref<Effect> shader, string passName) : base(shader, passName)
        {
        }

        public override void Apply()
        {
            //Shader.Parameters["uImage4"].SetValue(ModContent.Request<Texture2D>("OdeMod/Images/Effects/Decrate", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value);

            Shader.GraphicsDevice.Textures[4] = OdeMod.RenderTarget2DPool.PoolOther(Main.ScreenSize, "MiracleRecorder:Decrate");
            base.Apply();
        }
    }
}