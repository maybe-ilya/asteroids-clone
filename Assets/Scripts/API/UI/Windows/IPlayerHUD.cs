using UnityEngine;

namespace MIG.API
{
    public interface IPlayerHUD : IWindow
    {
        void SetPosition(Vector2 position);
        void SetTurnAngle(float angle);
        void SetSpeed(float speed);
        void SetScore(int score);
        void SetLaserCharges(int charges);
        void SetLaserRechargeTime(float rollbackTime);
    }
}