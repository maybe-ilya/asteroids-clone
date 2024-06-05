using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace MIG.Editor
{
    internal static class SceneSwitcher
    {
        private const string MENU_ITEM_BASE = "Scene Switcher/";
        private const string MENU_ITEM_BOOTSTRAP = MENU_ITEM_BASE + "Bootstrap";
        private const string MENU_ITEM_MAIN_MENU = MENU_ITEM_BASE + "Main Menu";
        private const string MENU_ITEM_GAME = MENU_ITEM_BASE + "Game";

        private static SceneSwitcherSettings Settings
            => GameConfigLocator.Get<SceneSwitcherSettings>();

        [MenuItem(MENU_ITEM_BOOTSTRAP, true)]
        [MenuItem(MENU_ITEM_MAIN_MENU, true)]
        [MenuItem(MENU_ITEM_GAME, true)]
        private static bool CanSwitchScene() => !EditorApplication.isPlaying;

        [MenuItem(MENU_ITEM_BOOTSTRAP, false)]
        private static void SwitchToBootstrap()
            => SwitchScene(Settings.BootstrapSceneIndex);

        [MenuItem(MENU_ITEM_MAIN_MENU, false)]
        private static void SwitchToMainMenu()
            => SwitchScene(Settings.MainMenuSceneIndex);

        [MenuItem(MENU_ITEM_GAME, false)]
        private static void SwitchToGame()
            => SwitchScene(Settings.GameSceneIndex);

        private static void SwitchScene(int newSceneIndex)
        {
            var scenePath = SceneUtility.GetScenePathByBuildIndex(newSceneIndex);
            EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Single);
        }
    }
}