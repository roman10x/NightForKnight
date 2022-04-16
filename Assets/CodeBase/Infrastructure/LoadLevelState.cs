using CodeBase.CameraLogic;
using CodeBase.Logic;
using UnityEngine;


namespace CodeBase.Infrastructure
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string InitialPointTagName = "InitialPoint";
        private const string PathToHeroPrefab = "Hero/hero";
        private const string PathToHudPrefab = "Hud/Hud";
        
        private readonly GameStateMachine m_stateMachine;
        private readonly SceneLoader m_sceneLoader;
        private readonly LoadingCurtain m_loadingCurtain;
        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain)
        {
            m_stateMachine = stateMachine;
            m_sceneLoader = sceneLoader;
            m_loadingCurtain = loadingCurtain;
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
            GameObject hero = Instantiate(PathToHeroPrefab, spawnPoint: initialPoint.transform.position);
            Instantiate(PathToHudPrefab);
            CameraFollow(hero);
            
            m_stateMachine.Enter<GameLoopState>();
        }

        private void CameraFollow(GameObject gameObject)
        {
           Camera.main.GetComponent<CameraFollow>().Follow(gameObject);
        }
        
        private static GameObject Instantiate(string path)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab);
        }
        
        private static GameObject Instantiate(string path, Vector3 spawnPoint)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, spawnPoint, Quaternion.identity);
        }
    }
}
