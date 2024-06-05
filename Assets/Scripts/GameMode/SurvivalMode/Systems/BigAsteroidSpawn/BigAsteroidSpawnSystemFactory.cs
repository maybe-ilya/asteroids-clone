using JetBrains.Annotations;
using MIG.API;

namespace MIG.GameMode
{
    [UsedImplicitly]
    public sealed class BigAsteroidSpawnSystemFactory : ISurvivalModeSystemFactory
    {
        private readonly IGameActorService _gameActorService;
        private readonly ITimerService _timerService;
        private readonly BigAsteroidSpawnSystemSettings _settings;

        public BigAsteroidSpawnSystemFactory(
            IGameActorService gameActorService,
            ITimerService timerService,
            BigAsteroidSpawnSystemSettings settings
        )
        {
            _gameActorService = gameActorService;
            _timerService = timerService;
            _settings = settings;
        }

        public ISurvivalModeSystem Create()
            => new BigAsteroidSpawnSystem(_gameActorService, _timerService, _settings);
    }
}