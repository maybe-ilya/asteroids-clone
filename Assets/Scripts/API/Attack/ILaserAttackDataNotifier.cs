using System;

namespace MIG.API
{
    public interface ILaserAttackDataNotifier : IService
    {
        int LaserCharges { get; }
        float LaserRechargeTime { get; }

        event Action<int> LaserChargesChanged;
        event Action<float> LaserRechargeTimeChanged;
    }
}