namespace OdeMod.CardMode.Scenes.MenuScene
{
    internal class MenuScene : SceneBase
    {
        public override void BeSelected()
        {
            base.BeSelected();
            CardSystem.Instance.CardModeUISystem.Elements["OdeMod.CardMode.Scenes.MenuScene.UIContainer.MenuContainer"].Show();
        }

        public override void ExitSelected()
        {
            base.ExitSelected();
            CardSystem.Instance.CardModeUISystem.Elements["OdeMod.CardMode.Scenes.MenuScene.UIContainer.MenuContainer"].Info.IsVisible = false;
        }
    }
}