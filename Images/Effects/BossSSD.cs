using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Shaders;

public class BossSSD : ScreenShaderData
{
    public BossSSD(string passName) : base(passName)
    {
    }
    public BossSSD(Ref<Effect> shader, string passName) : base(shader, passName)
    {
    }
    public override void Apply()
    {
        base.Apply();
    }
}