using CodeBase.CameraLogic;
using CodeBase.Infrastructure.Factory;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string InitialPointTagName = "InitialPoint";

        private readonly ApplicationStateMachine m_stateMachine;
        private readonly SceneLoader m_sceneLoader;
        private readonly LoadingCurtain m_loadingCurtain;
        private readonly IGameFactory m_gameFactory;
        public LoadLevelState(ApplicationStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain, IGameFactory gameFactory)
        {
            m_stateMachine = stateMachine;
            m_sceneLoader = sceneLoader;
            m_loadingCurtain = loadingCurtain;
            m_gameFactory = gameFactory;
        }
        public void Enter(string sceneName)
        {
           m_loadingCurtain.Show();
           m_sceneLoader.Load(sceneName, OnLoaded);
        }
        
        public void Exit()
        {
            m_loadingCurtain.Hide();
        }
        private void OnLoaded()
        {
            var initialPoint = GameObject.FindWithTag(InitialPointTagName);
            var hero = m_gameFactory.CreateHero(at: initialPoint);

            m_gameFactory.CreateHud();
            CameraFollow(hero);
            
            m_stateMachine.Enter<GameLoopState>();
        }

        private void CameraFollow(GameObject gameObject)
        {
           Camera.main.GetComponent<CameraFollow>().Follow(gameObject);
        }
    }
}
