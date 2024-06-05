using MIG.API;
using UnityEngine;

namespace MIG.App.States
{
    [CreateAssetMenu(menuName = AssetConsts.CREATE_ASSET_ROOT_MENU + nameof(PlayGameAppStateSettings))]
    public sealed class PlayGameAppStateSettings : ScriptableObject
    {
        [SerializeField] [SceneIndex] private int _gameSceneIndex;

        public int GameSceneIndex => _gameSceneIndex;
    }
}