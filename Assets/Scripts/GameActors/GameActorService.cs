using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using MIG.API;
using UnityEngine;

namespace MIG.GameActors
{
    [UsedImplicitly]
    public sealed class GameActorService : IGameActorService
    {
        private readonly Dictionary<Type, IGameActorFactory> _gameActorFactoriesMap = new();
        private readonly Dictionary<int, IGameActor> _gameActorsMap = new();
        private int _actorCounter;

        public void SetupFactories(IReadOnlyList<IGameActorFactory> gameActorFactories)
        {
            foreach (var factory in gameActorFactories)
            {
                _gameActorFactoriesMap[factory.ActorType] = factory;
            }
        }

        public TActor CreateActor<TActor>() where TActor : IGameActor
        {
            var actorType = typeof(TActor);
            var factory = GetActorFactory(actorType);
            var newId = GetNewActorId();
            var actor = factory.Create(newId);
            _gameActorsMap[newId] = actor;

            return (TActor)actor;
        }

        public TActor CreateActor<TActor>(Vector3 position) where TActor : IGameActor
        {
            var actorType = typeof(TActor);
            var factory = GetActorFactory(actorType);
            var newId = GetNewActorId();
            var actor = factory.Create(newId, position);
            _gameActorsMap[newId] = actor;

            return (TActor)actor;
        }

        public void DestroyActor(IGameActor gameActor)
            => DestroyActor(gameActor.Id);

        public void DestroyActor(int gameActorId)
        {
            if (!_gameActorsMap.Remove(gameActorId, out var actor)
                || actor is not IDestroyableActor destroyableActor)
            {
                return;
            }

            destroyableActor.Destroy();
        }

        public TActor GetActor<TActor>() where TActor : IGameActor
        {
            var data = _gameActorsMap.FirstOrDefault(pair => pair.Value is TActor);
            if (data.Value is null)
            {
                throw new KeyNotFoundException($"No actor of type = {typeof(TActor)}");
            }

            return (TActor)data.Value;
        }

        public IGameActor GetActor(int id)
        {
            if (!_gameActorsMap.TryGetValue(id, out var result))
            {
                throw new KeyNotFoundException($"No actor with id = {id}");
            }

            return result;
        }

        public TActor GetActor<TActor>(int id) where TActor : IGameActor
            => (TActor)GetActor(id);

        private int GetNewActorId()
            => ++_actorCounter;

        private IGameActorFactory GetActorFactory(Type actorType)
        {
            if (!_gameActorFactoriesMap.TryGetValue(actorType, out var result))
            {
                throw new KeyNotFoundException($"No factory for actor type {actorType.Name}");
            }

            return result;
        }
    }
}