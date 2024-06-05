using MIG.API;
using TMPro;
using UnityEngine;

namespace MIG.UI.Windows
{
    public sealed class PlayerHUD : AbstractWindow, IPlayerHUD
    {
        [Header("Components")] [SerializeField] [CheckObject]
        private TMP_Text _scoreLabel;

        [SerializeField] [CheckObject] private TMP_Text _speedLabel;
        [SerializeField] [CheckObject] private TMP_Text _turnAngleLabel;
        [SerializeField] [CheckObject] private TMP_Text _positionLabel;
        [SerializeField] [CheckObject] private TMP_Text _chargesLabel;
        [SerializeField] [CheckObject] private TMP_Text _rechargeLabel;

        [Header("Formatting")] [SerializeField]
        private string _floatFormat;

        public void SetPosition(Vector2 position)
            => _positionLabel.text = position.ToString(_floatFormat);

        public void SetTurnAngle(float angle)
            => _turnAngleLabel.text = angle.ToString(_floatFormat);

        public void SetSpeed(float speed)
            => _speedLabel.text = speed.ToString(_floatFormat);

        public void SetScore(int score)
            => _scoreLabel.text = score.ToString();

        public void SetLaserCharges(int charges)
            => _chargesLabel.text = charges.ToString();

        public void SetLaserRechargeTime(float rollbackTime)
            => _rechargeLabel.text = rollbackTime.ToString(_floatFormat);
    }
}