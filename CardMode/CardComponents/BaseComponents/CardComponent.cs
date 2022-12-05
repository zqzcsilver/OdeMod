namespace OdeMod.CardMode.CardComponents.BaseComponents
{
    internal class CardComponent : Component
    {
        public override IComponent Clone(Entity cloneEntity)
        {
            CardComponent component = new CardComponent();
            return component;
        }
    }
}