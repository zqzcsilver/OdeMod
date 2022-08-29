
using Terraria.GameContent;

using OdeMod.UI.OdeUISystem.UIElements;
using OdeMod.UI.OriginalUISystem;

using Terraria.UI;
using Microsoft.Xna.Framework.Graphics;

namespace OdeMod.UI.OdeUISystem.Containers.Recharge
{
    internal class RechargeContainer : ContainerElement, IOriginalUIState
    {
        public override void OnInitialization()
        {
            base.OnInitialization();
            UIText text = new UIText("我日", FontAssets.MouseText.Value);
            text.Info.Left.Percent = 0.5f;
            text.Info.Top.Percent = 0.5f;
            Register(text);
        }
    }
}
