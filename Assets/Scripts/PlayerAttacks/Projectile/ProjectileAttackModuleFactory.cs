using JetBrains.Annotations;
using MIG.API;
using UnityEngine;

namespace MIG.PlayerAttacks
{
    [UsedImplicitly]
    public sealed class ProjectileAttackModuleFactory : IAttackModuleFactory
    {
        private readonly ProjectileFactory _projectileFactory;
        private readonly ITimerService _timerService;
        private readonly ProjectileAttackModuleSettings _settings;

        public ProjectileAttackModuleFactory(
            ProjectileFactory projectileFactory,
            ITimerService timerService,
            ProjectileAttackModuleSettings settings
        )
        {
            _projectileFactory = projectileFactory;
            _timerService = timerService;
            _settings = settings;
        }

        public IAttackModule Create(IGameActor ownerActor, Transform originTransform)
        {
            return new ProjectileAttackModule(ownerActor, originTransform, _projectileFactory, _timerService,
                _settings);
        }
    }
}