using System.Reflection;

using Microsoft.Xna.Framework;

using OdeMod.CardMode.Scenes.ConfigScene.ConfigSystem.Configs;
using OdeMod.UI.OdeUISystem.UIElements;

namespace OdeMod.CardMode.Scenes.ConfigScene.UIElements
{
    internal class UIStringSelectBarConfig : UIConfigBase<string>
    {
        public readonly string[] Selecteds;
        public Color Color = Color.White;

        public int Index
        {
            get => _index;
            set
            {
                _index = value;
                if (_index < 0)
                    _index += Selecteds.Length;
                if (_index >= Selecteds.Length)
                    _index -= Selecteds.Length;

                FieldInfo.SetValue(Obj, Selecteds[_index]);
            }
        }

        private int _index = 0;

        public UIStringSelectBarConfig(string tip, string description, object obj, FieldInfo fieldInfo, string[] selecteds) : base(tip, description, obj, fieldInfo)
        {
            Selecteds = selecteds;

            UIText text = new UIText(tip, CardSystem.ConfigManager.GetConfig<InterfaceConfig>().Font.GetFont(40f));
            text.CalculateSize = false;
            text.CenterX = new PositionStyle(-5f, 0.25f);
            text.CenterY = new PositionStyle(0f, 0.5f);
            text.Events.OnCalculation += (baseElement) =>
            {
                ((UIText)baseElement).Font = CardSystem.ConfigManager.GetConfig<InterfaceConfig>().Font.GetFont(baseElement.ParentElement.Info.Size.Y);
            };
            Register(text);

            text = new UIText("<", CardSystem.ConfigManager.GetConfig<InterfaceConfig>().Font.GetFont(40f));
            text.CalculateSize = false;
            text.Info.Left.SetValue(Info.Width / 2f);
            text.Info.Top.SetValue(Info.Height / 2f - text.Info.Height / 2f);
            text.Events.OnLeftClick += (element) =>
            {
                Index--;
            };
            text.Events.OnCalculation += (baseElement) =>
            {
                ((UIText)baseElement).Font = CardSystem.ConfigManager.GetConfig<InterfaceConfig>().Font.GetFont(baseElement.ParentElement.Info.Size.Y);
                baseElement.Info.Left.SetValue(Info.Width / 2f);
                baseElement.Info.Top.SetValue(Info.Height / 2f - baseElement.Info.Height / 2f);
            };

            Register(text);

            text = new UIText(string.Empty, CardSystem.ConfigManager.GetConfig<InterfaceConfig>().Font.GetFont(40f));
            text.CalculateSize = true;
            text.CenterX = new PositionStyle(0f, 0.75f);
            text.CenterY = new PositionStyle(0f, 0.5f);
            text.Events.OnCalculation += (baseElement) =>
            {
                ((UIText)baseElement).Font = CardSystem.ConfigManager.GetConfig<InterfaceConfig>().Font.GetFont(Info.Size.Y);
            };
            text.Events.OnUpdate += (element, gt) =>
            {
                ((UIText)element).Text = Selecteds[Index];
            };
            Register(text);

            text = new UIText(">", CardSystem.ConfigManager.GetConfig<InterfaceConfig>().Font.GetFont(40f));
            text.CalculateSize = false;
            text.Info.Left.SetValue(Info.Width - text.Info.Width);
            text.Info.Top.SetValue(Info.Height / 2f - text.Info.Height / 2f);
            text.Events.OnLeftClick += (element) =>
            {
                Index++;
            };
            text.Events.OnCalculation += (baseElement) =>
            {
                ((UIText)baseElement).Font = CardSystem.ConfigManager.GetConfig<InterfaceConfig>().Font.GetFont(baseElement.ParentElement.Info.Size.Y);
                baseElement.Info.Left.SetValue(Info.Width - baseElement.Info.Width);
                baseElement.Info.Top.SetValue(Info.Height / 2f - baseElement.Info.Height / 2f);
            };
            Register(text);
        }
    }
}