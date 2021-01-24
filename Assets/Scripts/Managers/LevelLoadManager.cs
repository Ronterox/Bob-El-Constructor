﻿using System.Collections;
using Plugins.Properties;
using Plugins.Tools;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Managers
{
    public readonly struct LoadedEvent
    {
        public readonly string caller;
        public readonly string sceneName;
        public LoadedEvent(string sceneName, string caller)
        {
            this.sceneName = sceneName;
            this.caller = caller;
        } 
    }

    [System.Serializable]
    public class OnLoadEvent : UnityEvent { }
    public class LevelLoadManager : PersistentSingleton<LevelLoadManager>
    {
        [Header("Transitions")]
        [SerializeField] private Animator transitionAnimator;
        [Header("Scenes")]
        [SerializeField] [Scene] private string[] additiveScenes;

        private readonly int p_startTransition = Animator.StringToHash("Start");
        private readonly int p_endTransition = Animator.StringToHash("End");

        private Scene p_loadingScene;

        /// <summary>
        /// Checks if the scene by the name is loaded
        /// </summary>
        /// <param name="scene"></param>
        /// <returns></returns>
        public bool IsSceneLoaded() => p_loadingScene.isLoaded;

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
            float startTime = Time.time;
            
            transitionAnimator.SetTrigger(p_startTransition);

            Instance.LoadScene(savedData.lastLevel);
            Instance.LoadAdditiveAsyncScenes();

            p_loadingScene = SceneManager.GetSceneByName(savedData.lastLevel);

            yield return new WaitUntil(IsSceneLoaded);

            float timePassed = Time.time - startTime;
            if (timePassed < 1f) yield return new WaitForSeconds(1f - timePassed);

            transitionAnimator.SetTrigger(p_endTransition);
            
            GameManager.Instance.onLoadEvent.Invoke();
            
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
        public void LoadScene(string scene) => StartCoroutine(LoadSceneCoroutine(scene));

        private IEnumerator LoadSceneCoroutine(string scene, string caller = "")
        {
            float startTime = Time.time;
            
            transitionAnimator.SetTrigger(p_startTransition);
            
            SceneManager.LoadScene(scene, LoadSceneMode.Single);
            p_loadingScene = SceneManager.GetSceneByName(scene);

            yield return new WaitUntil(IsSceneLoaded);
            
            float timePassed = Time.time - startTime;
            if (timePassed < 1f) yield return new WaitForSeconds(1f - timePassed);

            transitionAnimator.SetTrigger(p_endTransition);
            
            GameManager.Instance.onLoadEvent.Invoke();
            MMEventManager.TriggerEvent(new LoadedEvent(scene, caller));
        }
        public void LoadSceneAsync(string scene) => StartCoroutine(LoadSceneAsyncCoroutine(scene));

        private IEnumerator LoadSceneAsyncCoroutine(string scene = null, string caller = "")
        {
            AsyncOperation loadingOperation = SceneManager.LoadSceneAsync(
                string.IsNullOrEmpty(scene) ? SceneManager.GetActiveScene().buildIndex + 1 : SceneManager.GetSceneByName(scene).buildIndex);
            yield return new WaitUntil(() => loadingOperation.isDone);
            GameManager.Instance.onLoadEvent.Invoke();
            MMEventManager.TriggerEvent(new LoadedEvent(scene, caller));
        }

        public void LoadNextSceneAsync(string caller)
        {
            StartCoroutine(LoadSceneAsyncCoroutine("", caller));
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
