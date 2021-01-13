using System.Collections;
using Plugins.Properties;
using Plugins.Tools;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Managers
{
    public struct LoadedEvent
    {
        public string sceneName;
        public LoadedEvent(string sceneName) => this.sceneName = sceneName;
    }

    [System.Serializable]
    public class OnLoadEvent : UnityEvent { }
    public class LevelLoadManager : PersistentSingleton<LevelLoadManager>
    {
        [Header("Scenes")]
        [SerializeField] [Scene] private string[] additiveScenes;

        /// <summary>
        /// Loads the selected scene by name
        /// </summary>
        /// <param name="scene"></param>
        public void LoadScene(string scene)
        {
            SceneManager.LoadScene(scene, LoadSceneMode.Single);
            GameManager.Instance.onLoadEvent.Invoke();
            MMEventManager.TriggerEvent(new LoadedEvent(scene));
        }

        public void LoadSceneAsync(string scene) => StartCoroutine(LoadSceneAsyncCoroutine(scene));

        private IEnumerator LoadSceneAsyncCoroutine(string scene = null)
        {
            AsyncOperation loadingOperation = SceneManager.LoadSceneAsync(
                string.IsNullOrEmpty(scene) ? SceneManager.GetActiveScene().buildIndex + 1 : SceneManager.GetSceneByName(scene).buildIndex);
            yield return new WaitUntil(() => loadingOperation.isDone);
            MMEventManager.TriggerEvent(new LoadedEvent(scene));
        }

        public void LoadNextSceneAsync()
        {
            StartCoroutine(LoadSceneAsyncCoroutine());
            Instance.LoadAdditiveAsyncScenes();
        }

        /// <summary>
        /// Loads the next scene on the list of scenes
        /// </summary>
        public void LoadNextScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
            Instance.LoadAdditiveAsyncScenes();
            GameManager.Instance.onLoadEvent.Invoke();
        }

        /// <summary>
        /// Loads all the additive scenes of the LevelLoadManager
        /// </summary>
        public void LoadAdditiveAsyncScenes()
        {
            foreach (string scene in additiveScenes) SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
        }

        public void UnloadAdditiveAsyncScenes()
        {
            foreach (string scene in additiveScenes)
            {
                Scene currentScene = SceneManager.GetSceneByName(scene);
                if(currentScene.isLoaded) SceneManager.UnloadSceneAsync(scene);
            }
        }

        /// <summary>
        /// Closes the Game or Stops the Inspector Game Window
        /// </summary>
        public void QuitGame()
        {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}
