namespace MIG.API
{
    public interface ILaserAttackDataUpdater : IService
    {
        void UpdateLaserCharges(int newCharges);
        void UpdateLaserRechargeTime(float rechargeTime);
    }
}