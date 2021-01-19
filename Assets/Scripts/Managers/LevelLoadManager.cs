using System.Collections;
using Plugins.Properties;
using Plugins.Tools;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Managers
{
    public readonly struct LoadedEvent
    {
        public readonly string sceneName;
        public LoadedEvent(string sceneName) => this.sceneName = sceneName;
    }

    [System.Serializable]
    public class OnLoadEvent : UnityEvent { }
    public class LevelLoadManager : PersistentSingleton<LevelLoadManager>
    {
        [Header("Scenes")]
        [SerializeField] [Scene] private string[] additiveScenes;

        /// <summary>
        /// Checks if the scene by the name is loaded
        /// </summary>
        /// <param name="scene"></param>
        /// <returns></returns>
        public bool IsSceneLoaded(string scene) => SceneManager.GetSceneByName(scene).isLoaded;

        /// <summary>
        /// Starts the Coroutine to resume the game
        /// </summary>
        public void ResumeGame() => StartCoroutine(ResumeCoroutine());
        
        /// <summary>
        /// Loads the last checkpoint state
        /// </summary>
        /// <returns></returns>
        private IEnumerator ResumeCoroutine()
        {
            var savedData = SaveLoadManager.Load<PlayerData>($"saved_state_v{Application.version}", "SavedStates");

            Instance.LoadScene(savedData.lastLevel);
            Instance.LoadAdditiveAsyncScenes();

            yield return new WaitUntil(() => Instance.IsSceneLoaded(savedData.lastLevel));
            
            Player.Player.Instance.transform.position = new Vector3(savedData.checkpoint.x, savedData.checkpoint.y, savedData.checkpoint.z);
            CameraManager.CameraManager.Instance.SetPriority(savedData.lastCameraID);

            GameManager.Instance.IncrementPickableGUI(savedData.gemsObtained);
        }

        /// <summary>
        /// Returns the current Scene name
        /// </summary>
        /// <returns></returns>
        public string GetSceneName() => SceneManager.GetActiveScene().name;

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
            GameManager.Instance.onLoadEvent.Invoke();
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
