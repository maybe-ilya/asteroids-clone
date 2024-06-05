using JetBrains.Annotations;
using MIG.API;
using UnityEngine;

namespace MIG.PlayerAttacks
{
    [UsedImplicitly]
    public sealed class LaserAttackModuleFactory : IAttackModuleFactory
    {
        private readonly IDamageService _damageService;
        private readonly LaserViewFactory _laserViewFactory;
        private readonly LaserAttackModuleSettings _laserAttackModuleSettings;
        private readonly ITimerService _timerService;
        private readonly ILaserAttackDataUpdater _laserAttackDataUpdater;

        public LaserAttackModuleFactory(
            IDamageService damageService,
            LaserViewFactory laserViewFactory,
            LaserAttackModuleSettings laserAttackModuleSettings,
            ITimerService timerService, ILaserAttackDataUpdater laserAttackDataUpdater)
        {
            _damageService = damageService;
            _laserViewFactory = laserViewFactory;
            _laserAttackModuleSettings = laserAttackModuleSettings;
            _timerService = timerService;
            _laserAttackDataUpdater = laserAttackDataUpdater;
        }

        public IAttackModule Create(IGameActor ownerActor, Transform originTransform)
        {
            return new LaserAttackModule(
                ownerActor,
                originTransform,
                _damageService,
                _laserViewFactory,
                _laserAttackModuleSettings,
                _timerService,
                _laserAttackDataUpdater);
        }
    }
}