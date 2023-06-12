using OdeMod.Buffs;
using OdeMod.UI.OdeUISystem.UIElements;

namespace OdeMod.CardMode.UI
{
    internal abstract class CardUIContainerElement : UIContainerElement
    {
        public override sealed bool AutoLoad => false;
        public virtual bool AutoLoadInCardMode => true;

        public override void Show(params object[] args)
        {
            Info.IsVisible = true;
            foreach (var i in CardSystem.Instance.CardModeUISystem.Elements)
                if (i.Value == this)
                    CardSystem.Instance.CardModeUISystem.SetContainerTop(i.Key);
            Calculation();
        }
    }
}