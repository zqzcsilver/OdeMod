namespace OdeMod.CardMode.PlayerComponents.BaseComponents
{
    internal class PlayerDrawComponent : Component
    {
        public override IComponent Clone(Entity cloneEntity)
        {
            var x = new PlayerDrawComponent();
            return x;
        }
    }
}