using JetBrains.Annotations;
using MIG.API;
using UnityEngine;
using URandom = UnityEngine.Random;

namespace MIG.GameMode
{
    [UsedImplicitly]
    public sealed class UFOSpawnSystem : ISurvivalModeSystem
    {
        private readonly IGameActorService _gameActorService;
        private readonly ITimerService _timerService;
        private readonly UFOSpawnSystemSettings _settings;

        private int _runningTimerId;
        private Transform _target;

        public UFOSpawnSystem(
            IGameActorService gameActorService,
            ITimerService timerService,
            UFOSpawnSystemSettings settings
        )
        {
            _gameActorService = gameActorService;
            _timerService = timerService;
            _settings = settings;
            _runningTimerId = DEFAULT_TIMER_ID;
        }

        private const int DEFAULT_TIMER_ID = -1;

        public void Start()
        {
            StartTimer();
            _target = _gameActorService.GetActor<IPlayerShipActor>().Transform;
        }

        public void Stop()
        {
            StopTimer();
        }

        private void StartTimer()
        {
            var duration = URandom.Range(_settings.MinTimerDuration, _settings.MaxTimerDuration);
            _runningTimerId = _timerService.StartTimer(duration, OnSpawnTimerFired);
        }

        private void StopTimer()
        {
            _timerService.StopTimer(_runningTimerId);
            _runningTimerId = DEFAULT_TIMER_ID;
        }

        private void OnSpawnTimerFired()
        {
            _gameActorService.CreateActor<IUFOActor>().SetTarget(_target);
            StartTimer();
        }
    }
}