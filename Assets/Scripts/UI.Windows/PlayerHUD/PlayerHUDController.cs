using JetBrains.Annotations;
using MIG.API;
using UnityEngine;

namespace MIG.UI.Windows
{
    [UsedImplicitly]
    public sealed class PlayerHUDController : AbstractWindowController<IPlayerHUD>
    {
        private readonly IPlayerTransformDataNotifier _playerTransformDataNotifier;
        private readonly IScoreChangeNotifier _scoreChangeNotifier;
        private readonly ILaserAttackDataNotifier _laserAttackDataNotifier;

        public PlayerHUDController(
            IPlayerTransformDataNotifier playerTransformDataNotifier,
            IScoreChangeNotifier scoreChangeNotifier,
            ILaserAttackDataNotifier laserAttackDataNotifier
        )
        {
            _playerTransformDataNotifier = playerTransformDataNotifier;
            _scoreChangeNotifier = scoreChangeNotifier;
            _laserAttackDataNotifier = laserAttackDataNotifier;
        }

        protected override void OnWindowSet()
        {
            Subscribe();
            Window.SetLaserCharges(_laserAttackDataNotifier.LaserCharges);
            Window.SetLaserRechargeTime(_laserAttackDataNotifier.LaserRechargeTime);
        }

        protected override void AfterWindowClear()
        {
            Unsubscribe();
        }

        private void Subscribe()
        {
            _playerTransformDataNotifier.OnPositionChanged += OnPlayerPositionChanged;
            _playerTransformDataNotifier.OnTurnAngleChanged += OnPlayerTurnAngleChanged;
            _playerTransformDataNotifier.OnSpeedChanged += OnPlayerSpeedChanged;
            _scoreChangeNotifier.ScoreChanged += OnScoreChanged;
            _laserAttackDataNotifier.LaserChargesChanged += OnLaserChargesChanged;
            _laserAttackDataNotifier.LaserRechargeTimeChanged += OnLaserRechargeTimeChanged;
        }

        private void Unsubscribe()
        {
            _playerTransformDataNotifier.OnPositionChanged -= OnPlayerPositionChanged;
            _playerTransformDataNotifier.OnTurnAngleChanged -= OnPlayerTurnAngleChanged;
            _playerTransformDataNotifier.OnSpeedChanged -= OnPlayerSpeedChanged;
            _scoreChangeNotifier.ScoreChanged -= OnScoreChanged;
            _laserAttackDataNotifier.LaserChargesChanged -= OnLaserChargesChanged;
            _laserAttackDataNotifier.LaserRechargeTimeChanged -= OnLaserRechargeTimeChanged;
        }

        private void OnLaserChargesChanged(int newCharges)
            => Window.SetLaserCharges(newCharges);

        private void OnLaserRechargeTimeChanged(float rechargeTime)
            => Window.SetLaserRechargeTime(rechargeTime);

        private void OnPlayerSpeedChanged(float newSpeed)
            => Window.SetSpeed(newSpeed);

        private void OnPlayerTurnAngleChanged(float newTurnAngle)
            => Window.SetTurnAngle(newTurnAngle);

        private void OnPlayerPositionChanged(Vector2 newPosition)
            => Window.SetPosition(newPosition);

        private void OnScoreChanged(int newScore)
            => Window.SetScore(newScore);
    }
}