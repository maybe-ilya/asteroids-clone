using System;
using MIG.API;

namespace MIG.GameMode
{
    public sealed class SurvivalMode : IGameMode
    {
        private readonly IDamageEventNotifier _damageEventNotifier;
        private readonly ISurvivalModeSystem[] _systems;

        public SurvivalMode(
            IDamageEventNotifier damageEventNotifier,
            ISurvivalModeSystem[] systems
        )
        {
            _damageEventNotifier = damageEventNotifier;
            _systems = systems;
        }

        public void Start()
        {
            _damageEventNotifier.OnActorDamaged += OnActorDamaged;
            foreach (var system in _systems)
            {
                system.Start();
            }
        }

        public void Stop()
        {
            _damageEventNotifier.OnActorDamaged -= OnActorDamaged;
            foreach (var system in _systems)
            {
                system.Stop();
            }
        }

        public event Action<GameModeResult> OnFinish;

        private void OnActorDamaged(DamageEventData damageEventData)
        {
            if (damageEventData.Target is IPlayerShipActor)
            {
                OnFinish?.Invoke(GameModeResult.Failure);
            }
        }
    }
}