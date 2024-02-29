using System;
using System.Collections.Generic;
using Infrastructure.Interfaces;
using Infrastructure.States.GameStates;
using Zenject;

namespace Infrastructure.States
{
    public class GameStateMachine : IGameStateMachine, IInitializable
    {
        private readonly Dictionary<Type, IStateBase> _states;
        private readonly IGameStateMachineProvider _stateMachineProvider;
        private IStateBase _activeState;

        public GameStateMachine(
            BootstrapState bootstrapState,
            LoadLevelState loadLevelState,
            GameLoopState gameLoopState,
            IGameStateMachineProvider stateMachineProvider
        )
        {
            _stateMachineProvider = stateMachineProvider;
            _states = new Dictionary<Type, IStateBase>
            {
                [typeof(BootstrapState)] = bootstrapState,
                [typeof(LoadLevelState)] = loadLevelState,
                [typeof(GameLoopState)] = gameLoopState,
            };
        }

        public void Initialize()
        {
            _stateMachineProvider.ActiveStateMachine = this;
            Enter<BootstrapState>();
        }

        public void Update()
        {
            if (_activeState is IUpdatable updatableState)
                updatableState.Update();
        }

        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            var state = ChangeState<TState>();
            state.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IStateBase
        {
            if (_activeState is IExitableState exitableState)
                exitableState.Exit();

            var state = GetState<TState>();
            _activeState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, IStateBase =>
            _states[typeof(TState)] as TState;
    }
}