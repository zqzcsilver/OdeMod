using Microsoft.Xna.Framework;

using OdeMod.Players;

using System.Collections.Generic;

using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace OdeMod.Systems
{
    internal class ScreenShaderSystem : ModSystem, IOdeSystem
    {
        public override void PreUpdateEntities()
        {
            base.PreUpdateEntities();
            if (!Filters.Scene["OdeMod:MiracleRecorder"].IsActive() && Main.LocalPlayer.GetModPlayer<OdePlayer>().MiracleRecorderShader == 1)
            {
                // 开启滤镜
                Filters.Scene.Activate("OdeMod:MiracleRecorder");
            }
            else if (Filters.Scene["OdeMod:MiracleRecorder"].IsActive() && Main.LocalPlayer.GetModPlayer<OdePlayer>().MiracleRecorderShader == 0)
            {
                Filters.Scene.Deactivate("OdeMod:MiracleRecorder");
            }
        }
    }
}