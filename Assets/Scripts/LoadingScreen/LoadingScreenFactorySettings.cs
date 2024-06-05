using MIG.API;
using UnityEngine;

namespace MIG.LoadingScreen
{
    [CreateAssetMenu(menuName = AssetConsts.CREATE_ASSET_ROOT_MENU + nameof(LoadingScreenFactorySettings))]
    public sealed class LoadingScreenFactorySettings : ScriptableObject
    {
        [SerializeField] [CheckObject] private LoadingScreen _loadingScreenPrefab;

        public LoadingScreen LoadingScreenPrefab => _loadingScreenPrefab;
    }
}