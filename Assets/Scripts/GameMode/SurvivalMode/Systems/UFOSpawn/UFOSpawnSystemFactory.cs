using JetBrains.Annotations;
using MIG.API;

namespace MIG.GameMode
{
    [UsedImplicitly]
    public sealed class UFOSpawnSystemFactory : ISurvivalModeSystemFactory
    {
        private readonly IGameActorService _gameActorService;
        private readonly ITimerService _timerService;
        private readonly UFOSpawnSystemSettings _settings;

        public UFOSpawnSystemFactory(
            IGameActorService gameActorService,
            ITimerService timerService,
            UFOSpawnSystemSettings settings
        )
        {
            _gameActorService = gameActorService;
            _timerService = timerService;
            _settings = settings;
        }

        public ISurvivalModeSystem Create()
            => new UFOSpawnSystem(_gameActorService, _timerService, _settings);
    }
}