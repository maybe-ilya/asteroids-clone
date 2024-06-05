using MIG.API;
using UnityEngine;

namespace MIG.UI.Windows
{
    [CreateAssetMenu(menuName = AssetConsts.CREATE_ASSET_ROOT_MENU + nameof(PlayerHUDFactorySettings))]
    public sealed class PlayerHUDFactorySettings : ScriptableObject
    {
        [SerializeField] [CheckObject] private PlayerHUD _playerHUDPrefab;

        public PlayerHUD PlayerHUDPrefab => _playerHUDPrefab;
    }
}