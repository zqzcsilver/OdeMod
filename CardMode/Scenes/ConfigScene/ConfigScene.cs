namespace OdeMod.CardMode.Scenes.ConfigScene
{
    internal class ConfigScene : SceneBase
    {
        public override void BeSelected()
        {
            base.BeSelected();
            CardSystem.Instance.CardModeUISystem.Elements["OdeMod.CardMode.Scenes.ConfigScene.UIContainers.ConfigContainer"].Show();
        }

        public override void ExitSelected()
        {
            base.ExitSelected();
            CardSystem.Instance.CardModeUISystem.Elements["OdeMod.CardMode.Scenes.ConfigScene.UIContainers.ConfigContainer"].Info.IsVisible = false;
        }
    }
}