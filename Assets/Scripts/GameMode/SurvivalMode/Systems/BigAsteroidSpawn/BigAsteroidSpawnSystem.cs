using JetBrains.Annotations;
using MIG.API;
using URandom = UnityEngine.Random;

namespace MIG.GameMode
{
    [UsedImplicitly]
    public sealed class BigAsteroidSpawnSystem : ISurvivalModeSystem
    {
        private readonly IGameActorService _gameActorService;
        private readonly ITimerService _timerService;
        private readonly BigAsteroidSpawnSystemSettings _settings;

        private int _runningTimerId;
        private const int DEFAULT_TIMER_ID = -1;

        public BigAsteroidSpawnSystem(
            IGameActorService gameActorService,
            ITimerService timerService,
            BigAsteroidSpawnSystemSettings settings
        )
        {
            _gameActorService = gameActorService;
            _timerService = timerService;
            _settings = settings;
            _runningTimerId = DEFAULT_TIMER_ID;
        }

        public void Start()
        {
            SpawnAsteroids(_settings.InitialCount);
            StartSpawnTimer();
        }

        public void Stop()
        {
            StopSpawnTimer();
        }

        private void StartSpawnTimer()
        {
            var duration = URandom.Range(_settings.MinTimerDuration, _settings.MaxTimerDuration);
            _runningTimerId = _timerService.StartTimer(duration, OnSpawnTimerFired);
        }

        private void StopSpawnTimer()
        {
            _timerService.StopTimer(_runningTimerId);
            _runningTimerId = DEFAULT_TIMER_ID;
        }

        private void OnSpawnTimerFired()
        {
            var count = URandom.Range(_settings.MinSpawnCount, _settings.MaxSpawnCount + 1);
            SpawnAsteroids(count);
            StartSpawnTimer();
        }

        private void SpawnAsteroids(int count)
        {
            for (var i = 0; i < count; ++i)
            {
                _gameActorService.CreateActor<IBigAsteroidActor>();
            }
        }
    }
}