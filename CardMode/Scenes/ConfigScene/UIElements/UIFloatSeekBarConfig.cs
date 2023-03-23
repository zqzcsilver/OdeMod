using System.Reflection;

using Microsoft.Xna.Framework;

using OdeMod.CardMode.Scenes.ConfigScene.ConfigSystem.Configs;
using OdeMod.UI.OdeUISystem.UIElements;

namespace OdeMod.CardMode.Scenes.ConfigScene.UIElements
{
    internal class UIFloatSeekBarConfig : UIConfigBase<float>
    {
        public readonly float MinValue, MaxValue;
        private UISeekBar seekBar;
        public UIFloatSeekBarConfig(string tip, string description, object obj, FieldInfo fieldInfo, float maxValue, float minValue) :
            base(tip, description, obj, fieldInfo)
        {
            MinValue = minValue;
            MaxValue = maxValue;

            UIText text = new UIText(tip, CardSystem.ConfigManager.GetConfig<InterfaceConfig>().Font.GetFont(40f));
            text.CalculateSize = false;
            text.CenterX = new PositionStyle(-5f, 0.25f);
            text.CenterY = new PositionStyle(0f, 0.5f);
            text.Events.OnCalculation += (baseElement) =>
            {
                ((UIText)baseElement).Font = CardSystem.ConfigManager.GetConfig<InterfaceConfig>().Font.GetFont(baseElement.ParentElement.Info.Size.Y);
            };
            Register(text);

            float value = (float)fieldInfo.GetValue(obj);
            seekBar = new UISeekBar((value - minValue) / (maxValue - minValue));
            seekBar.Info.Width.SetValue(-10f, 0.5f);
            seekBar.Info.Height.SetValue(0f, 1f);
            seekBar.Info.Left.SetValue(10f, 0.5f);
            seekBar.OnValueChange += (sb, v) =>
            {
                fieldInfo.SetValue(obj, minValue + (maxValue - minValue) * v);
            };
            Register(seekBar);
        }

        public override void Update(GameTime gt)
        {
            base.Update(gt);
            float value = (float)FieldInfo.GetValue(Obj);
            seekBar.Value = (value - MinValue) / (MaxValue - MinValue);
        }
    }
}