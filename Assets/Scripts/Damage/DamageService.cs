using System;
using JetBrains.Annotations;
using MIG.API;
using UnityEngine;

namespace MIG.Damage
{
    [UsedImplicitly]
    public sealed class DamageService : IDamageService, IDamageEventNotifier
    {
        private readonly IGameActorService _actorService;
        private int _damageEventCounter;

        public DamageService(IGameActorService actorService)
        {
            _actorService = actorService;
        }

        public event Action<DamageEventData> OnActorDamaged;

        public bool ApplyDamage(IGameActor invoker, IGameActor target, Vector3 point)
        {
            if (invoker.Id == target.Id)
            {
                return false;
            }

            OnActorDamaged?.Invoke(new DamageEventData(++_damageEventCounter, invoker, target, point));
            _actorService.DestroyActor(target);
            return true;
        }
    }
}