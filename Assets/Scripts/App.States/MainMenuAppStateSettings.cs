using MIG.API;
using UnityEngine;

namespace MIG.App.States
{
    [CreateAssetMenu(menuName = AssetConsts.CREATE_ASSET_ROOT_MENU + nameof(MainMenuAppStateSettings))]
    public sealed class MainMenuAppStateSettings : ScriptableObject
    {
        [SerializeField] [SceneIndex] private int _mainMenuSceneIndex;

        public int MainMenuSceneIndex => _mainMenuSceneIndex;
    }
}