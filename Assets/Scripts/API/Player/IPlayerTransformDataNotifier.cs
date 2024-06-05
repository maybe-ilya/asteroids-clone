using System;
using UnityEngine;

namespace MIG.API
{
    public interface IPlayerTransformDataNotifier : IService
    {
        event Action<Vector2> OnPositionChanged;
        event Action<float> OnTurnAngleChanged;
        event Action<float> OnSpeedChanged;
    }
}