﻿using System.Reflection;

using Microsoft.Xna.Framework;

using OdeMod.CardMode.Scenes.ConfigScene.ConfigSystem.Configs;
using OdeMod.UI.OdeUISystem.UIElements;

namespace OdeMod.CardMode.Scenes.ConfigScene.UIElements
{
    internal class UILongSeekBarConfig : UIConfigBase<long>
    {
        public readonly long MinValue, MaxValue;
        private UISeekBar seekBar;
        public UILongSeekBarConfig(string tip, string description, object obj, FieldInfo fieldInfo, long maxValue, long minValue) :
            base(tip, description, obj, fieldInfo)
        {
            MinValue = minValue;
            MaxValue = maxValue;

            long value = (long)fieldInfo.GetValue(obj);
            seekBar = new UISeekBar((float)(((double)value - (double)MinValue) / ((double)MaxValue - (double)MinValue)));
            seekBar.Info.Width.SetValue(-10f, 0.5f);
            seekBar.Info.Height.SetValue(0f, 1f);
            seekBar.Info.Left.SetValue(10f, 0.5f);
            seekBar.OnValueChange += (sb, v) =>
            {
                fieldInfo.SetValue(obj, (long)(minValue + (maxValue - minValue) * v));
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
            long value = (long)FieldInfo.GetValue(Obj);
            seekBar.Value = (float)(((double)value - (double)MinValue) / ((double)MaxValue - (double)MinValue));
        }
    }
}