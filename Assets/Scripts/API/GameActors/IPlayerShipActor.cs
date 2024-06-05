using UnityEngine;

namespace MIG.API
{
    public interface IPlayerShipActor : IGameActor
    {
        Transform Transform { get; }
    }
}