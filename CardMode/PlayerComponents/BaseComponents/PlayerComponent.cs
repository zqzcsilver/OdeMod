namespace OdeMod.CardMode.PlayerComponents.BaseComponents
{
    internal class PlayerComponent : Component
    {
        public override IComponent Clone(Entity cloneEntity)
        {
            return new PlayerComponent();
        }
    }
}