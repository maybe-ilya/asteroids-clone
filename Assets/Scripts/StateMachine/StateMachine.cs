using System;
using System.Collections.Generic;
using MIG.API;

namespace MIG.StateMachine
{
    public sealed class StateMachine<TBaseState> : IStateMachine<TBaseState> where TBaseState : IState
    {
        private readonly Dictionary<Type, TBaseState> _states = new();
        private TBaseState _currentState;

        public void AddState<TInputState>(TInputState newState) where TInputState : TBaseState
        {
            var type = typeof(TInputState);
            if (!_states.TryAdd(type, newState))
            {
                throw new ArgumentException($"Can't add new state {type.Name}");
            }
        }

        public void ChangeState<TInputState>() where TInputState : TBaseState, IEnterableState
        {
            TryToExitFromCurrentState();
            var newState = GetState<TInputState>();
            newState.Enter();
            _currentState = newState;
        }

        public void ChangeState<TInputState, TInData>(TInData data)
            where TInputState : TBaseState, IEnterableState<TInData>
        {
            TryToExitFromCurrentState();
            var newState = GetState<TInputState>();
            newState.Enter(data);
            _currentState = newState;
        }

        private void TryToExitFromCurrentState()
            => (_currentState as IExitableState)?.Exit();

        private TInputState GetState<TInputState>() where TInputState : TBaseState
        {
            var type = typeof(TInputState);
            if (!_states.TryGetValue(type, out var value))
            {
                throw new ArgumentException($"Can't get state of type {type.Name}");
            }

            return (TInputState)value;
        }
    }
}