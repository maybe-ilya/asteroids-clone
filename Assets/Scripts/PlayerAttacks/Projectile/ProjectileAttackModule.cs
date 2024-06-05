using MIG.API;
using UnityEngine;

namespace MIG.PlayerAttacks
{
    public sealed class ProjectileAttackModule : IAttackModule
    {
        private readonly IGameActor _ownerActor;
        private readonly Transform _originTransform;
        private readonly ProjectileFactory _projectileFactory;
        private readonly ITimerService _timerService;
        private readonly ProjectileAttackModuleSettings _settings;

        private bool _isAttackActivated;
        private int _runningTimerId;
        private const int DEFAULT_TIMER_ID = -1;

        public ProjectileAttackModule(
            IGameActor ownerActor,
            Transform originTransform,
            ProjectileFactory projectileFactory,
            ITimerService timerService,
            ProjectileAttackModuleSettings settings
        )
        {
            _ownerActor = ownerActor;
            _originTransform = originTransform;
            _projectileFactory = projectileFactory;
            _timerService = timerService;
            _settings = settings;
            ResetTimerId();
        }

        public void StartAttack()
        {
            _isAttackActivated = true;
            TryToLaunchProjectile();
        }

        public void StopAttack()
        {
            _isAttackActivated = false;
        }

        private void TryToLaunchProjectile()
        {
            if (CanLaunchProjectile())
            {
                LaunchProjectile();
                StartTimer();
            }
        }

        private bool CanLaunchProjectile()
            => _isAttackActivated && _runningTimerId == DEFAULT_TIMER_ID;

        private void LaunchProjectile()
        {
            var projectile = _projectileFactory.Create(_originTransform);
            projectile.SetOwner(_ownerActor);
            projectile.Launch();
        }

        private void OnTimerFired()
        {
            ResetTimerId();
            TryToLaunchProjectile();
        }

        private void StartTimer()
        {
            _runningTimerId = _timerService.StartTimer(_settings.FireDelaySeconds, OnTimerFired);
        }

        private void ResetTimerId()
            => _runningTimerId = DEFAULT_TIMER_ID;
    }
}