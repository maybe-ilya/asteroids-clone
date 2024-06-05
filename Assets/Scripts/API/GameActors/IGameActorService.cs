using UnityEngine;

namespace MIG.API
{
    public interface IGameActorService : IService
    {
        TActor CreateActor<TActor>() where TActor : IGameActor;
        TActor CreateActor<TActor>(Vector3 position) where TActor : IGameActor;
        void DestroyActor(IGameActor gameActor);
        void DestroyActor(int gameActorId);
        TActor GetActor<TActor>() where TActor : IGameActor;
        IGameActor GetActor(int id);
        TActor GetActor<TActor>(int id) where TActor : IGameActor;
    }
}