using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using MIG.API;
using UnityEngine;

namespace MIG.GameMode
{
    [CreateAssetMenu(menuName = AssetConsts.CREATE_ASSET_ROOT_MENU + nameof(SmallAsteroidSpawnSystemSettings))]
    public sealed class SmallAsteroidSpawnSystemSettings : ScriptableObject
    {
        [SerializeField] private int _smallAsteroidMin;
        [SerializeField] private int _smallAsteroidMax;
        [SerializeField] private Vector2 _spawnSpreadSize;

        public int SmallAsteroidMin => _smallAsteroidMin;
        public int SmallAsteroidMax => _smallAsteroidMax;
        public Vector2 SpawnSpreadSize => _spawnSpreadSize;
    }
}