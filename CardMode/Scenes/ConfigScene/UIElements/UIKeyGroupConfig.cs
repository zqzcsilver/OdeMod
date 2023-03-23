using System.Reflection;

using OdeMod.CardMode.KeyBindSystem;
using OdeMod.CardMode.Scenes.ConfigScene.ConfigSystem.Configs;
using OdeMod.UI.OdeUISystem.UIElements;

namespace OdeMod.CardMode.Scenes.ConfigScene.UIElements
{
    internal class UIKeyGroupConfig : UIConfigBase<KeyGroup>
    {
        public UIKeyGroupConfig(string tip, string description, object obj, FieldInfo fieldInfo) :
            base(tip, description, obj, fieldInfo)
        {
            var value = (KeyGroup)fieldInfo.GetValue(obj);

            UIText text = new UIText(tip, CardSystem.ConfigManager.GetConfig<InterfaceConfig>().Font.GetFont(40f));
            text.CalculateSize = false;
            text.CenterX = new PositionStyle(-5f, 0.25f);
            text.CenterY = new PositionStyle(0f, 0.5f);
            text.Events.OnCalculation += (baseElement) =>
            {
                ((UIText)baseElement).Font = CardSystem.ConfigManager.GetConfig<InterfaceConfig>().Font.GetFont(baseElement.ParentElement.Info.Size.Y);
            };
            Register(text);

            text = new UIText(value.ToString(), CardSystem.ConfigManager.GetConfig<InterfaceConfig>().Font.GetFont(40f));
            text.CalculateSize = true;
            text.CenterX = new PositionStyle(0f, 0.75f);
            text.CenterY = new PositionStyle(0f, 0.5f);
            text.Events.OnLeftUp += element =>
            {
                var kg = (KeyGroup)FieldInfo.GetValue(Obj);
                if (!kg.NeedResetKey)
                {
                    kg.NeedResetKey = true;
                }
            };
            text.Events.OnCalculation += (baseElement) =>
            {
                ((UIText)baseElement).Font = CardSystem.ConfigManager.GetConfig<InterfaceConfig>().Font.GetFont(baseElement.ParentElement.Info.Size.Y);
            };
            text.Events.OnUpdate += (element, gt) =>
            {
                var kg = (KeyGroup)FieldInfo.GetValue(Obj);
                if (!kg.NeedResetKey)
                {
                    ((UIText)element).Text = kg.ToString();
                }
                else
                {
                    ((UIText)element).Text = "Pressed Key";
                }
                element.Calculation();
            };
            Register(text);
        }
    }
}