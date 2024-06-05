using JetBrains.Annotations;
using MIG.API;

namespace MIG.GameMode
{
    [UsedImplicitly]
    public sealed class DamageScoreSystemFactory : ISurvivalModeSystemFactory
    {
        private readonly IDamageEventNotifier _damageEventNotifier;
        private readonly IScoreService _scoreService;
        private readonly DamageScoreSystemSettings _settings;

        public DamageScoreSystemFactory(
            IDamageEventNotifier damageEventNotifier,
            IScoreService scoreService,
            DamageScoreSystemSettings settings
        )
        {
            _damageEventNotifier = damageEventNotifier;
            _scoreService = scoreService;
            _settings = settings;
        }

        public ISurvivalModeSystem Create()
            => new DamageScoreSystem(_damageEventNotifier, _scoreService, _settings);
    }
}