using Microsoft.Xna.Framework;

using System.Collections.Generic;

using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.UI;

namespace OdeMod.Systems
{
    internal class UISystem : ModSystem, IOdeSystem
    {
        private Point ScreenSize;
        public override void Load()
        {
            base.Load();
            OdeMod.OdeUISystem.Load();
        }
        public override void UpdateUI(GameTime gameTime)
        {
            base.UpdateUI(gameTime);
            if (ScreenSize != Main.ScreenSize)
            {
                ScreenSize = Main.ScreenSize;
                OdeMod.OdeUISystem.Calculation();
            }
            OdeMod.OdeUISystem.Update(gameTime);
        }
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            base.ModifyInterfaceLayers(layers);
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "OdeMod: Ode UI System",
                    delegate
                    {
                        OdeMod.OdeUISystem.Draw(Main.spriteBatch);
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }
}
