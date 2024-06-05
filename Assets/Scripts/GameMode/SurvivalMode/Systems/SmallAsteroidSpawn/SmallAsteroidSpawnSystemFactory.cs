using JetBrains.Annotations;
using MIG.API;

namespace MIG.GameMode
{
    [UsedImplicitly]
    public sealed class SmallAsteroidSpawnSystemFactory : ISurvivalModeSystemFactory
    {
        private readonly IDamageEventNotifier _damageEventNotifier;
        private readonly IGameActorService _gameActorService;
        private readonly SmallAsteroidSpawnSystemSettings _settings;

        public SmallAsteroidSpawnSystemFactory(
            IDamageEventNotifier damageEventNotifier,
            IGameActorService gameActorService,
            SmallAsteroidSpawnSystemSettings settings
        )
        {
            _damageEventNotifier = damageEventNotifier;
            _gameActorService = gameActorService;
            _settings = settings;
        }

        public ISurvivalModeSystem Create()
            => new SmallAsteroidSpawnSystem(_damageEventNotifier, _gameActorService, _settings);
    }
}