namespace OdeMod.CardMode.Scenes.AboutScene
{
    internal class AboutScene : SceneBase
    {
        public override void ChangeBegin()
        {
            base.ChangeBegin();
            CardSystem.Instance.CardModeUISystem.Elements["OdeMod.CardMode.Scenes.AboutScene.UIContainer.AboutContainer"].Show();
        }

        public override void Changing()
        {
            base.Changing();
            CardSystem.Instance.CardModeUISystem.Elements["OdeMod.CardMode.Scenes.AboutScene.UIContainer.AboutContainer"].Info.IsVisible = false;
        }
    }
}