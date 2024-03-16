using Microsoft.Xna.Framework;

using OdeMod.QuickAssetReference;
using OdeMod.UI.OdeUISystem.Containers.Drawer.UIElements;
using OdeMod.UI.OdeUISystem.UIElements;
using OdeMod.UI.OriginalUISystem;

namespace OdeMod.UI.OdeUISystem.Containers.Drawer
{
    internal class SnowaveD : UIContainerElement, IOriginalUIState
    {
        public static SnowaveD Instance => (SnowaveD)OdeMod.OdeUISystem.Elements[typeof(SnowaveD).FullName];

        public override void OnInitialization()
        {
            base.OnInitialization();

            UIImagePanel panel = new UIImagePanel(ModAssets_Texture2D.UI.OdeUISystem.Containers.Recharge.Images.SnowavePaperImmediateAsset.Value, Color.White);
            panel.Style = UIImage.CalculationStyle.LockAspectRatioMainWidth;
            panel.Info.Width.SetValue(660f, 0f);
            panel.Info.Height.SetValue(504f, 0f);
            panel.Info.Left.SetValue(-panel.Info.Width.Pixel / 2f, 0.5f);
            panel.Info.Top.SetValue(-panel.Info.Height.Pixel / 2f, 0.5f);

            panel.Info.SetMargin(20f);
            panel.CanDrag = false;
            Register(panel);

            UIImage closeButton = new UIImage(ModAssets_Texture2D.UI.OdeUISystem.Containers.Recharge.Images.XButtonUpImmediateAsset.Value, Color.White);
            closeButton.Info.Left.Percent = 0.9f;
            closeButton.Info.Top.Percent = 0.02f;
            closeButton.Info.Width.Pixel = 44f;
            closeButton.Info.Height.Pixel = 44f;
            closeButton.Events.OnLeftDown += element =>
                closeButton.ChangeImage(ModAssets_Texture2D.UI.OdeUISystem.Containers.Recharge.Images.XButtonDownImmediateAsset.Value);
            closeButton.Events.OnLeftUp += element =>
                closeButton.ChangeImage(ModAssets_Texture2D.UI.OdeUISystem.Containers.Recharge.Images.XButtonUpImmediateAsset.Value);
            closeButton.Events.OnLeftClick += element => Close();
            panel.Register(closeButton);

            UICanvas canvas = new UICanvas(5, 5);
            canvas.Info.Left.SetValue(80f, 0f);
            canvas.Info.Top.SetValue(35f, 0f);
            panel.Register(canvas);
        }
    }
}