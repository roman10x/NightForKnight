using System;
using System.Collections.Generic;
using CodeBase.Logic;

namespace CodeBase.Infrastructure
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> m_states;
        private IExitableState m_activeState;

        public GameStateMachine(SceneLoader sceneLoader, LoadingCurtain loadingCurtain)
        {
            m_states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootStrapState)] = new BootStrapState(this, sceneLoader),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, loadingCurtain),
                [typeof(GameLoopState)] = new GameLoopState(this)
            };
        }
        
        public void Enter<TState>() where TState : class, IState
        {
            var state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            TState state = ChangeState<TState>();
            state.Enter(payload);
        }
        
        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            m_activeState?.Exit();
            TState state = GetState<TState>();
            m_activeState = state;
            return state;
        }
        
        private TState GetState<TState>() where TState : class, IExitableState
        {
            return m_states[typeof(TState)] as TState;
        }
    }

}
