
using OdeMod.UI.OdeUISystem.UIElements;
using OdeMod.UI.OriginalUISystem;

using Terraria.GameContent;

namespace OdeMod.UI.OdeUISystem.Containers.Recharge
{
    internal class RechargeContainer : ContainerElement, IOriginalUIState
    {
        public override void OnInitialization()
        {
            base.OnInitialization();
            UIText text = new UIText("我日", FontAssets.MouseText.Value);
            text.CenterX = new PositionStyle(0f, 0.5f);
            text.CenterY = new PositionStyle(0f, 0.5f);
            Register(text);
        }
    }
}
