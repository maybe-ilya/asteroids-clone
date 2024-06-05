using UnityEngine;

namespace MIG.API
{
    public interface IDamageService : IService
    {
        bool ApplyDamage(IGameActor invoker, IGameActor target, Vector3 point);
    }
}