using JetBrains.Annotations;
using MIG.API;

namespace MIG.GameMode
{
    [UsedImplicitly]
    public sealed class DamageScoreSystem : ISurvivalModeSystem
    {
        private readonly IDamageEventNotifier _damageEventNotifier;
        private readonly IScoreService _scoreService;
        private readonly DamageScoreSystemSettings _settings;

        public DamageScoreSystem(
            IDamageEventNotifier damageEventNotifier,
            IScoreService scoreService,
            DamageScoreSystemSettings settings
        )
        {
            _damageEventNotifier = damageEventNotifier;
            _scoreService = scoreService;
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
            if (damageEventData.Invoker is not IPlayerShipActor)
            {
                return;
            }

            var scoreToAppend = GetScoreToAppend(damageEventData.Target);
            _scoreService.AppendScore(scoreToAppend);
        }

        private int GetScoreToAppend(IGameActor damagedActor)
        {
            return damagedActor switch
            {
                IBigAsteroidActor => _settings.BigAsteroidScore,
                ISmallAsteroidActor => _settings.SmallAsteroidScore,
                IUFOActor => _settings.UfoScore,
                _ => _settings.DefaultDamageScore
            };
        }
    }
}