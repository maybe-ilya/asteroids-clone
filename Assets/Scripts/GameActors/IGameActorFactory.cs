using System;
using MIG.API;
using UnityEngine;

namespace MIG.GameActors
{
    public interface IGameActorFactory :
        IFactory<IGameActor, int>,
        IFactory<IGameActor, int, Vector3>
    {
        Type ActorType { get; }
    }
}