using UnityEngine;

namespace MIG.API
{
    public struct DamageEventData
    {
        public readonly int Id;
        public readonly IGameActor Invoker, Target;
        public readonly Vector3 Point;

        public DamageEventData(int id, IGameActor invoker, IGameActor target, Vector3 point)
        {
            Id = id;
            Invoker = invoker;
            Target = target;
            Point = point;
        }
    }
}