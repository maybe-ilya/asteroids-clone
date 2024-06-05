using UnityEngine;

namespace MIG.API
{
    public interface IAttackModuleFactory : IFactory<IAttackModule, IGameActor, Transform>
    {
    }
}