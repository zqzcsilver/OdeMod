namespace OdeMod.UI.OdeUISystem.UIElements
{
    internal abstract class UIContainerElement : BaseElement
    {
        public virtual string Name { get => GetType().FullName; }
        public virtual bool AutoLoad { get => true; }

        public UIContainerElement()
        {
            Info.IsVisible = false;
        }

        public override void OnInitialization()
        {
            base.OnInitialization();
            Info.Width = new PositionStyle(0f, 1f);
            Info.Height = new PositionStyle(0f, 1f);
            Info.CanBeInteract = false;
        }

        public virtual void Show(params object[] args)
        {
            Info.IsVisible = true;
            foreach (var i in OdeMod.OdeUISystem.Elements)
                if (i.Value == this)
                    OdeMod.OdeUISystem.SetContainerTop(i.Key);
            Calculation();
        }

        public virtual void Close(params object[] args)
        {
            Info.IsVisible = false;
        }
    }
}