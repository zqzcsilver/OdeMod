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
            OdeMod.ScreenShaderDataManager.Update();
        }
    }
}