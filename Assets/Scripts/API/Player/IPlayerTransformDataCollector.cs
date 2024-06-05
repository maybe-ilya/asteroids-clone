using UnityEngine;

namespace MIG.API
{
    public interface IPlayerTransformDataCollector : IService
    {
        void UpdatePosition(Vector2 newPosition);
        void UpdateTurnAngle(float newTurnAngle);
        void UpdateSpeed(float newSpeed);
    }
}