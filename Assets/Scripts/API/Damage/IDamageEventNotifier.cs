using System;

namespace MIG.API
{
    public interface IDamageEventNotifier : IService
    {
        event Action<DamageEventData> OnActorDamaged;
    }
}