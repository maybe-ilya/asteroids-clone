using JetBrains.Annotations;
using MIG.API;
using UnityEngine;

namespace MIG.Asteroids
{
    [UsedImplicitly]
    public sealed class SmallAsteroidActorFactory : AsteroidActorFactory<ISmallAsteroidActor>
    {
        public SmallAsteroidActorFactory(AsteroidActorFactorySettings settings, IInnerOuterSpawner spawner,
            IDamageService damageService) : base(settings, spawner, damageService)
        {
        }

        public override IGameActor Create(int id)
            => (ISmallAsteroidActor)CreateInternal(id, GetPointInSpawner());

        public override IGameActor Create(int id, Vector3 input)
            => (ISmallAsteroidActor)CreateInternal(id, input);

        protected override Vector3 GetDirection(Vector3 origin)
            => (_spawner.GetRandomOuterPoint() - origin).normalized;
    }
}