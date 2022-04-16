using System;
using CodeBase.Services.Input;
using Unity.IO.LowLevel.Unsafe;
using UnityEditor.SearchService;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class BootStrapState : IState
    {
        private const string InitialSceneName = "Initial";
        private readonly GameStateMachine m_stateMachine;
        private readonly SceneLoader m_sceneLoader;
        public BootStrapState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            m_stateMachine = stateMachine;
            m_sceneLoader = sceneLoader;
        }
        public void Enter()
        {
            RegisterServices();
            m_sceneLoader.Load(InitialSceneName, onLoaded: EnterLoadLevel);
        }
        private void EnterLoadLevel()
        {
            m_stateMachine.Enter<LoadLevelState, string>("Main"); // "Main" scene left only for this stage of development
        }
        
        private void RegisterServices()
        {
            Game.InputService = RegisterInputService();
        }
        public void Exit()
        {
        }
        
        private static IInputService RegisterInputService()
        {
            if (Application.isEditor)
                return new StandaloneInputService();
            else
                return new MobileInputService();
        }
    }
}
