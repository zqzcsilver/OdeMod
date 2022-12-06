namespace OdeMod.CardMode.PublicComponents.LogicComponents
{
    internal abstract class LogicComponentBase
    {
        public abstract void Load(LogicComponent logicComponent);

        public virtual void Unload(LogicComponent logicComponent)
        {
        }
    }
}