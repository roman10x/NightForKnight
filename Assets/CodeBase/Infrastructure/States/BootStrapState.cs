using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using CodeBase.Services.Input;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class BootStrapState : IState, IStrictState
    {
        private const string InitialSceneName = "Initial";
        private readonly ApplicationStateMachine m_stateMachine;
        private readonly SceneLoader m_sceneLoader;
        private readonly AllServices m_services;

        private List<Type> _availableStates = new List<Type>() { typeof(LoadLevelState) };
        
        public List<Type> AvailableStates() => _availableStates;
        public BootStrapState(ApplicationStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
        {
            m_stateMachine = stateMachine;
            m_sceneLoader = sceneLoader;
            m_services = services;
            
            RegisterServices();
        }
        public void Enter()
        {
            m_sceneLoader.Load(InitialSceneName, onLoaded: EnterLoadLevel);
        }
        
        public void Exit()
        {
        }
        
        private void EnterLoadLevel()
        {
            m_stateMachine.Enter<LoadLevelState, string>("Main"); // "Main" scene left only for this stage of development
            //m_stateMachine.Enter<GameLoopState>(); Test code for strict state try
        }
        
        private void RegisterServices()
        {
            m_services.RegisterSingle(InputService());
            m_services.RegisterSingle<IAssets>(new AssetProvider());
            m_services.RegisterSingle<IGameFactory>(new GameFactory(m_services.Single<IAssets>()));
        }
        
        private static IInputService InputService()
        {
            if (Application.isEditor)
                return new StandaloneInputService();
            
            return new MobileInputService();
        }
    }
}
