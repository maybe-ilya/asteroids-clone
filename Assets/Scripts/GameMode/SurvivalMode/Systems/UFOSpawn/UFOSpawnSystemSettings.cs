using MIG.API;
using UnityEngine;

namespace MIG.GameMode
{
    [CreateAssetMenu(menuName = AssetConsts.CREATE_ASSET_ROOT_MENU + nameof(UFOSpawnSystemSettings))]
    public sealed class UFOSpawnSystemSettings : ScriptableObject
    {
        [SerializeField] private float _minTimerDuration;
        [SerializeField] private float _maxTimerDuration;

        public float MinTimerDuration => _minTimerDuration;

        public float MaxTimerDuration => _maxTimerDuration;
    }
}