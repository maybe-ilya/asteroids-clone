using MIG.API;
using UnityEngine;

namespace MIG.GameMode
{
    [CreateAssetMenu(menuName = AssetConsts.CREATE_ASSET_ROOT_MENU + nameof(BigAsteroidSpawnSystemSettings))]
    public sealed class BigAsteroidSpawnSystemSettings : ScriptableObject
    {
        [SerializeField] private int _initialCount;
        [SerializeField] private float _minTimerDuration;
        [SerializeField] private float _maxTimerDuration;
        [SerializeField] private int _minSpawnCount;
        [SerializeField] private int _maxSpawnCount;

        public int InitialCount => _initialCount;
        public float MinTimerDuration => _minTimerDuration;
        public float MaxTimerDuration => _maxTimerDuration;
        public int MinSpawnCount => _minSpawnCount;
        public int MaxSpawnCount => _maxSpawnCount;
    }
}