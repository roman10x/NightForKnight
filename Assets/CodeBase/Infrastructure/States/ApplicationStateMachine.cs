using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class ApplicationStateMachine
    {
        private readonly Dictionary<Type, IExitableState> m_states;
        private IExitableState m_activeState;

        public ApplicationStateMachine(SceneLoader sceneLoader, LoadingCurtain loadingCurtain, AllServices services)
        {
            m_states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootStrapState)] = new BootStrapState(this, sceneLoader, services),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, loadingCurtain, services.Single<IGameFactory>()),
                [typeof(GameLoopState)] = new GameLoopState(this)
            };
        }
        
        public void Enter<TState>() where TState : class, IState
        {
            var state = ChangeState<TState>();
            state?.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            var state = ChangeState<TState>();
            state?.Enter(payload);
        }
        
        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            var strictState = m_activeState as IStrictState;
            if(strictState != null && !strictState.AvailableStates().Contains(typeof(TState)))
            {
                Debug.LogError("Can't enter to another state, because type of available states is strict");
                return null;
            }
            m_activeState?.Exit();
            var state = GetState<TState>();
            m_activeState = state;
            return state;
        }
        
        private TState GetState<TState>() where TState : class, IExitableState
        {
            return m_states[typeof(TState)] as TState;
        }
    }

}
