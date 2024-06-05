using System;
using MIG.API;
using Unity.Mathematics;
using UnityEngine;

namespace MIG.Asteroids
{
    [CreateAssetMenu(menuName = AssetConsts.CREATE_ASSET_ROOT_MENU + nameof(AsteroidActorFactorySettings))]
    public sealed class AsteroidActorFactorySettings : ScriptableObject
    {
        [SerializeField] [CheckObject] private AbstractAsteroidActor _asteroidPrefab;
        [SerializeField] [CheckObject] private Sprite[] _asteroidSprites = Array.Empty<Sprite>();
        [SerializeField] private float _minSpeedModifier;
        [SerializeField] private float _maxSpeedModifier;
        [SerializeField] private int _initialPoolSize;

        public AbstractAsteroidActor AsteroidPrefab => _asteroidPrefab;

        public Sprite[] AsteroidSprites => _asteroidSprites;

        public float MinSpeedModifier => _minSpeedModifier;

        public float MaxSpeedModifier => _maxSpeedModifier;

        public int InitialPoolSize => _initialPoolSize;

#if UNITY_EDITOR
        private void OnValidate()
        {
            _maxSpeedModifier = math.max(_maxSpeedModifier, 0.0f);
            _minSpeedModifier = math.min(math.max(_minSpeedModifier, 0.0f), _maxSpeedModifier);
        }
#endif
    }
}