using UnityEngine;

namespace MIG.API
{
    public interface IUFOActor : IGameActor
    {
        void SetTarget(Transform newTarget);
    }
}