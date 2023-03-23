using System.Reflection;

using OdeMod.CardMode.Scenes.ConfigScene.ConfigSystem.Configs;
using OdeMod.UI.OdeUISystem.UIElements;

namespace OdeMod.CardMode.Scenes.ConfigScene.UIElements
{
    internal class UIBoolConfig : UIConfigBase<bool>
    {
        public UIBoolConfig(string tip, string description, object obj, FieldInfo fieldInfo) :
            base(tip, description, obj, fieldInfo)
        {
            bool value = (bool)fieldInfo.GetValue(obj);

            UIText text = new UIText(tip, CardSystem.ConfigManager.GetConfig<InterfaceConfig>().Font.GetFont(40f));
            text.CalculateSize = false;
            text.CenterX = new PositionStyle(-5f, 0.25f);
            text.CenterY = new PositionStyle(0f, 0.5f);
            text.Events.OnCalculation += (baseElement) =>
            {
                ((UIText)baseElement).Font = CardSystem.ConfigManager.GetConfig<InterfaceConfig>().Font.GetFont(baseElement.ParentElement.Info.Size.Y);
            };
            Register(text);

            text = new UIText("▢", CardSystem.ConfigManager.GetConfig<InterfaceConfig>().Font.GetFont(40f));
            text.CalculateSize = false;
            text.CenterX = new PositionStyle(0f, 0.75f);
            text.CenterY = new PositionStyle(0f, 0.5f);
            text.Info.IsSensitive = true;
            text.Events.OnLeftDown += element =>
            {
                FieldInfo.SetValue(Obj, !(bool)FieldInfo.GetValue(obj));
            };
            text.Events.OnCalculation += (baseElement) =>
            {
                ((UIText)baseElement).Font = CardSystem.ConfigManager.GetConfig<InterfaceConfig>().Font.GetFont(baseElement.ParentElement.Info.Size.Y);
            };
            Register(text);

            text = new UIText(value ? "×" : "✓", CardSystem.ConfigManager.GetConfig<InterfaceConfig>().Font.GetFont(40f));
            text.CalculateSize = false;
            text.CenterX = new PositionStyle(0f, 0.75f);
            text.CenterY = new PositionStyle(0f, 0.5f);
            text.Events.OnCalculation += (baseElement) =>
            {
                ((UIText)baseElement).Font = CardSystem.ConfigManager.GetConfig<InterfaceConfig>().Font.GetFont(baseElement.ParentElement.Info.Size.Y);
            };
            text.Events.OnUpdate += (element, gt) =>
            {
                ((UIText)element).Text = (bool)FieldInfo.GetValue(Obj) ? "×" : "✓";
            };
            Register(text);
        }
    }
}