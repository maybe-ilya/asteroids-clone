using JetBrains.Annotations;
using MIG.API;
using UnityEngine;
using URandom = UnityEngine.Random;

namespace MIG.GameMode
{
    [UsedImplicitly]
    public sealed class SmallAsteroidSpawnSystem : ISurvivalModeSystem
    {
        private readonly IDamageEventNotifier _damageEventNotifier;
        private readonly IGameActorService _gameActorService;
        private readonly SmallAsteroidSpawnSystemSettings _settings;

        public SmallAsteroidSpawnSystem(
            IDamageEventNotifier damageEventNotifier,
            IGameActorService gameActorService,
            SmallAsteroidSpawnSystemSettings settings
        )
        {
            _damageEventNotifier = damageEventNotifier;
            _gameActorService = gameActorService;
            _settings = settings;
        }

        public void Start()
        {
            _damageEventNotifier.OnActorDamaged += OnActorDamaged;
        }

        public void Stop()
        {
            _damageEventNotifier.OnActorDamaged -= OnActorDamaged;
        }

        private void OnActorDamaged(DamageEventData damageEventData)
        {
            if (damageEventData.Target is not IBigAsteroidActor)
            {
                return;
            }

            var count = URandom.Range(_settings.SmallAsteroidMin, _settings.SmallAsteroidMax + 1);
            var spreadSize = _settings.SpawnSpreadSize;

            for (var i = 0; i < count; ++i)
            {
                var spreadOffset = new Vector3(
                    URandom.Range(-spreadSize.x, spreadSize.x),
                    URandom.Range(-spreadSize.y, spreadSize.y),
                    0);
                var spawnPoint = damageEventData.Point + spreadOffset;
                _gameActorService.CreateActor<ISmallAsteroidActor>(spawnPoint);
            }
        }
    }
}