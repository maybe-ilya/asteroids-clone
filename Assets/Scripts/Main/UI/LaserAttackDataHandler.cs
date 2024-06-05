using System;
using JetBrains.Annotations;
using MIG.API;

namespace MIG.Main
{
    [UsedImplicitly]
    public sealed class LaserAttackDataHandler :
        ILaserAttackDataNotifier,
        ILaserAttackDataUpdater
    {
        public int LaserCharges { get; private set; }
        public float LaserRechargeTime { get; private set; }

        public event Action<int> LaserChargesChanged;
        public event Action<float> LaserRechargeTimeChanged;

        public void UpdateLaserCharges(int newCharges)
        {
            LaserCharges = newCharges;
            LaserChargesChanged?.Invoke(LaserCharges);
        }

        public void UpdateLaserRechargeTime(float rechargeTime)
        {
            LaserRechargeTime = rechargeTime;
            LaserRechargeTimeChanged?.Invoke(LaserRechargeTime);
        }
    }
}