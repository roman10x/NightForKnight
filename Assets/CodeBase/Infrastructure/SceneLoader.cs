using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure
{

    public class SceneLoader
    {
        private readonly ICoroutineRunner m_coroutineRunner;
        public SceneLoader(ICoroutineRunner coroutineRunner)
        {
            m_coroutineRunner = coroutineRunner;
        }

        public void Load(string name, Action onLoaded = null) => m_coroutineRunner.StartCoroutine(LoadScene(name, onLoaded));

        private IEnumerator LoadScene(string nextSceneName, Action onLoaded = null)
        {
            if (SceneManager.GetActiveScene().name == nextSceneName)
            {
                onLoaded?.Invoke();
                yield break;
            }
            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextSceneName);

            //waitNextScene.completed += _ => onLoaded?.Invoke(); simple solution, left for further use
            while (!waitNextScene.isDone)
                yield return null;

            onLoaded?.Invoke();
        }
    }

}
