namespace OdeMod.Shaders
{
    internal interface IShader : IOde
    {
        void OnActivate();

        void OnDeactivate();
    }
}