using MIG.API;
using UnityEngine;

namespace MIG.Editor
{
    [GameConfig("ProjectSettings/SceneSwitcherSettings.asset",
        "Project/Scene Switcher Settings")]
    internal sealed class SceneSwitcherSettings : GameConfig
    {
        [SerializeField] [SceneIndex] private int _bootstrapSceneIndex, _mainMenuSceneIndex, _gameSceneIndex;

        public int BootstrapSceneIndex => _bootstrapSceneIndex;

        public int MainMenuSceneIndex => _mainMenuSceneIndex;

        public int GameSceneIndex => _gameSceneIndex;
    }
}