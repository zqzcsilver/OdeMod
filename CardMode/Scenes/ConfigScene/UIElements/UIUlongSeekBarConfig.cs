using System.Reflection;

using Microsoft.Xna.Framework;

using OdeMod.CardMode.Scenes.ConfigScene.ConfigSystem.Configs;
using OdeMod.UI.OdeUISystem.UIElements;

namespace OdeMod.CardMode.Scenes.ConfigScene.UIElements
{
    internal class UIUlongSeekBarConfig : UIConfigBase<ulong>
    {
        public readonly ulong MinValue, MaxValue;
        private UISeekBar seekBar;
        public UIUlongSeekBarConfig(string tip, string description, object obj, FieldInfo fieldInfo, ulong maxValue, ulong minValue) :
            base(tip, description, obj, fieldInfo)
        {
            MinValue = minValue;
            MaxValue = maxValue;

            ulong value = (ulong)fieldInfo.GetValue(obj);
            seekBar = new UISeekBar((float)(((double)value - (double)minValue) / ((double)maxValue - (double)minValue)));
            seekBar.Info.Width.SetValue(-10f, 0.5f);
            seekBar.Info.Height.SetValue(0f, 1f);
            seekBar.Info.Left.SetValue(10f, 0.5f);
            seekBar.OnValueChange += (sb, v) =>
            {
                fieldInfo.SetValue(obj, (ulong)(minValue + (maxValue - minValue) * v));
            };
            Register(seekBar);

            UIText text = new UIText(tip, CardSystem.ConfigManager.GetConfig<InterfaceConfig>().Font.GetFont(40f));
            text.CalculateSize = false;
            text.CenterX = new PositionStyle(-5f, 0.25f);
            text.CenterY = new PositionStyle(0f, 0.5f);
            text.Events.OnCalculation += (baseElement) =>
            {
                ((UIText)baseElement).Font = CardSystem.ConfigManager.GetConfig<InterfaceConfig>().Font.GetFont(baseElement.ParentElement.Info.Size.Y);
            };
            Register(text);
        }

        public override void Update(GameTime gt)
        {
            base.Update(gt);
            ulong value = (ulong)FieldInfo.GetValue(Obj);
            seekBar.Value = ((float)(((double)value - (double)MinValue) / ((double)MaxValue - (double)MinValue)));
        }
    }
}