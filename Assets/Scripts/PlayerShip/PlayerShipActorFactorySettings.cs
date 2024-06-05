using MIG.API;
using UnityEngine;

namespace MIG.PlayerShip
{
    [CreateAssetMenu(menuName = AssetConsts.CREATE_ASSET_ROOT_MENU + nameof(PlayerShipActorFactorySettings))]
    public sealed class PlayerShipActorFactorySettings : ScriptableObject
    {
        [SerializeField] [CheckObject] private PlayerShipActor _playerShipActorPrefab;

        public PlayerShipActor PlayerShipActorPrefab => _playerShipActorPrefab;
    }
}