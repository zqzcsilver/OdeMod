using OdeMod.CardMode.Scenes.AboutScene.UIContainer;

namespace OdeMod.CardMode.Scenes.AboutScene
{
    internal class AboutScene : SceneBase
    {
        public static readonly string SceneFullName = typeof(AboutScene).FullName;

        public override void ChangeBegin()
        {
            base.ChangeBegin();
            CardSystem.Instance.CardModeUISystem.Elements[AboutContainer.ContainerFullName].Show();
        }

        public override void Changing()
        {
            base.Changing();
            CardSystem.Instance.CardModeUISystem.Elements[AboutContainer.ContainerFullName].Close();
        }
    }
}