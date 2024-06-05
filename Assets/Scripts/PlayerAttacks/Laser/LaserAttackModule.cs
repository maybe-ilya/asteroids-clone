using MIG.API;
using Unity.Mathematics;
using UnityEngine;

namespace MIG.PlayerAttacks
{
    public sealed class LaserAttackModule : IAttackModule
    {
        private readonly IGameActor _ownerActor;
        private readonly Transform _originTransform;
        private readonly IDamageService _damageService;
        private readonly LaserViewFactory _laserViewFactory;
        private readonly LaserAttackModuleSettings _settings;
        private readonly ITimerService _timerService;
        private readonly ILaserAttackDataUpdater _laserAttackDataUpdater;
        private readonly RaycastHit2D[] _allocatedHits;

        private int _cooldownTimerId, _rechargeTimerId;
        private int _currentCharges;
        private bool _isAttackActivated;

        private const int DEFAULT_TIMER_ID = -1;

        public LaserAttackModule(
            IGameActor ownerActor,
            Transform originTransform,
            IDamageService damageService,
            LaserViewFactory laserViewFactory,
            LaserAttackModuleSettings settings,
            ITimerService timerService,
            ILaserAttackDataUpdater laserAttackDataUpdater
        )
        {
            _ownerActor = ownerActor;
            _originTransform = originTransform;
            _damageService = damageService;
            _laserViewFactory = laserViewFactory;
            _settings = settings;
            _timerService = timerService;
            _laserAttackDataUpdater = laserAttackDataUpdater;
            _allocatedHits = new RaycastHit2D[_settings.MaxHits];
            ResetCooldownTimerId();
            ResetRechargeTimer();
            SetCharges(_settings.MaxCharges);
        }

        public void StartAttack()
        {
            _isAttackActivated = true;
            TryToAttackWithLaser();
        }

        public void StopAttack()
        {
            _isAttackActivated = false;
        }

        private void TryToAttackWithLaser()
        {
            if (CanAttack())
            {
                DamageActors();
                ShowLaserView();
                DecreaseCharges();
                StartCooldownTimer();
                StartRechargeTimer();
            }
        }

        private bool CanAttack()
            => _isAttackActivated && _currentCharges > 0 && _cooldownTimerId == DEFAULT_TIMER_ID;

        private void DecreaseCharges()
            => SetCharges(_currentCharges - 1);

        private void IncreaseCharges()
            => SetCharges(_currentCharges + 1);

        private void StartCooldownTimer()
            => _cooldownTimerId = _timerService.StartTimer(_settings.CooldownDelaySeconds, OnCooldownTimerFired);

        private void ResetCooldownTimerId()
            => _cooldownTimerId = DEFAULT_TIMER_ID;

        private void OnCooldownTimerFired()
        {
            ResetCooldownTimerId();
            TryToAttackWithLaser();
        }

        private void StartRechargeTimer()
        {
            if (_rechargeTimerId != DEFAULT_TIMER_ID)
            {
                _timerService.StopTimer(_rechargeTimerId);
            }

            _rechargeTimerId = _timerService.StartTimer(_settings.RechargeDelaySeconds, OnRechargeTimerFired);
            _timerService.TimerUpdated += OnTimerUpdated;
        }

        private void ResetRechargeTimer()
            => _rechargeTimerId = DEFAULT_TIMER_ID;

        private void OnTimerUpdated(int timerId, float timerTime)
        {
            if (timerId != _rechargeTimerId) return;

            _laserAttackDataUpdater.UpdateLaserRechargeTime(timerTime);
        }

        private void OnRechargeTimerFired()
        {
            ResetRechargeTimer();
            _timerService.TimerUpdated -= OnTimerUpdated;
            _laserAttackDataUpdater.UpdateLaserRechargeTime(0.0f);
            IncreaseCharges();
            if (_currentCharges < _settings.MaxCharges)
            {
                StartRechargeTimer();
            }
        }

        private void SetCharges(int newValue)
        {
            _currentCharges = math.clamp(newValue, 0, _settings.MaxCharges);
            _laserAttackDataUpdater.UpdateLaserCharges(_currentCharges);
        }

        private void DamageActors()
        {
            var point = _originTransform.position;
            var direction = _originTransform.up;

            var hitCount = Physics2D.BoxCastNonAlloc(point,
                _settings.BoxSize,
                _originTransform.rotation.eulerAngles.z,
                direction,
                _allocatedHits,
                _settings.Distance,
                _settings.CollisionMask
            );

            for (var i = 0; i < hitCount; ++i)
            {
                var hit = _allocatedHits[i];
                if (!hit.collider.TryGetComponent<IGameActor>(out var hitActor))
                {
                    continue;
                }

                _damageService.ApplyDamage(_ownerActor, hitActor, hit.point);
            }
        }

        private void ShowLaserView()
        {
            _laserViewFactory.Create(_originTransform).Show();
        }
    }
}