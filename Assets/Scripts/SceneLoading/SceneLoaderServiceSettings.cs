using MIG.API;
using UnityEngine;

namespace MIG.SceneLoading
{
    [CreateAssetMenu(menuName = AssetConsts.CREATE_ASSET_ROOT_MENU + nameof(SceneLoaderServiceSettings))]
    public sealed class SceneLoaderServiceSettings : ScriptableObject
    {
        [SerializeField] private int _emulatedDelaySeconds;

        public int EmulatedDelaySeconds => _emulatedDelaySeconds;
    }
}