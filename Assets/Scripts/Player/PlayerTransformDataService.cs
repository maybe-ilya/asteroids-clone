using System;
using JetBrains.Annotations;
using MIG.API;
using UnityEngine;

namespace MIG.Player
{
    [UsedImplicitly]
    public sealed class PlayerTransformDataService :
        IPlayerTransformDataCollector,
        IPlayerTransformDataNotifier
    {
        public event Action<Vector2> OnPositionChanged;
        public event Action<float> OnTurnAngleChanged;
        public event Action<float> OnSpeedChanged;

        public void UpdatePosition(Vector2 newPosition)
            => OnPositionChanged?.Invoke(newPosition);

        public void UpdateTurnAngle(float newTurnAngle)
            => OnTurnAngleChanged?.Invoke(newTurnAngle);

        public void UpdateSpeed(float newSpeed)
            => OnSpeedChanged?.Invoke(newSpeed);
    }
}