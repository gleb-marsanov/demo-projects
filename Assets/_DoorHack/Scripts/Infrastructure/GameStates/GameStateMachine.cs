using System;
using System.Collections.Generic;
using Infrastructure.Bootstrap;
using Services;
using Services.GameFactory;
using Services.GameTime;
using Services.Progress;
using Services.Ui;
using Services.Ui.Factories;
using Services.Ui.LoadingCurtain;
using StaticData;

namespace Infrastructure.GameStates
{
    public class GameStateMachine : IService, IUpdatable
    {
        private readonly Dictionary<Type, IGameState> _states;
        private IGameState _activeState;

        public GameStateMachine(SceneLoader sceneLoader, DiContainer diContainer, GameConfig gameConfig)
        {
            _states = new Dictionary<Type, IGameState>()
            {
                [typeof(BootstrapState)] = new BootstrapState
                (
                    stateMachine: this,
                    container: diContainer,
                    sceneLoader: sceneLoader,
                    gameConfig
                ),

                [typeof(LoadLevelState)] = new LoadLevelState
                (
                    stateMachine: this,
                    loadingCurtain: diContainer.Resolve<ILoadingCurtainService>(),
                    sceneLoader: sceneLoader,
                    gameConfig: gameConfig,
                    windowService: diContainer.Resolve<IWindowService>(),
                    uiFactory: diContainer.Resolve<IUiFactory>(),
                    timeService: diContainer.Resolve<IGameTimeService>(),
                    gameFactory: diContainer.Resolve<IGameFactory>(),
                    progressProvider: diContainer.Resolve<IProgressProvider>()
                ),

                [typeof(GameLoopState)] = new GameLoopState
                (
                    gameStateMachine: this,
                    timeService: diContainer.Resolve<IGameTimeService>(),
                    progressProvider: diContainer.Resolve<IProgressProvider>()
                ),
                [typeof(GameOverState)] = new GameOverState
                (
                    diContainer.Resolve<IWindowService>()
                ),
                [typeof(VictoryState)] = new VictoryState
                (
                    diContainer.Resolve<IWindowService>()
                )
            };
        }

        public void Update()
        {
            if (_activeState is IUpdatable updatableState)
                updatableState.Update();
        }

        public void Enter<TState>() where TState : class, IGameState
        {
            IGameState state = ChangeState<TState>();
            state.Enter();
        }

        private TState ChangeState<TState>() where TState : class, IGameState
        {
            _activeState?.Exit();

            var state = GetState<TState>();
            _activeState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class =>
            _states[typeof(TState)] as TState;
    }
}