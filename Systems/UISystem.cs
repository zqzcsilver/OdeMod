using Microsoft.Xna.Framework;

using OdeMod.UI.OdeUISystem;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace OdeMod.Systems
{
    internal class UISystem : ModSystem, IOdeSystem
    {
        public OdeUISystem OdeUISystem { get; private set; }
        public UISystem()
        {
            OdeUISystem = new OdeUISystem();
            Main.OnResolutionChanged += size =>
            {
                OdeUISystem.Calculation();
            };
        }
        public override void Load()
        {
            base.Load();
            OdeUISystem.Load();
        }
        public override void UpdateUI(GameTime gameTime)
        {
            base.UpdateUI(gameTime);
            OdeUISystem.Elements["OdeMod.UI.OdeUISystem.Containers.Recharge.RechargeContainer"].Info.IsVisible = true;
            OdeUISystem.Update(gameTime);
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
                        OdeUISystem.Draw(Main.spriteBatch);
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }
}
