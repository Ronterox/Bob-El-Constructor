using System.Collections;
using Managers;
using Managers.CameraManager;
using Plugins.Tools;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GUI
{
    public class MainMenu : MonoBehaviour 
    {
        public GameObject startGame, loadScene, quitGame;
        public TextMeshProUGUI buildVersionTMP;
        
        private void Start()
        {
            LevelLoadManager.Instance.UnloadAdditiveAsyncScenes();
            buildVersionTMP.text = "Build v"+Application.version;
        }

        /// <summary>
        /// Loads the game last saved Scene
        /// </summary>
        public void StartGame()
        {
            CheckForSaveState();
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(startGame);
        }

        /// <summary>
        /// Resumes the game if there is a SaveState else, just loads the first Scene
        /// </summary>
        private void CheckForSaveState()
        {
            if (SaveLoadManager.SaveExists($"saved_state_v{Application.version}", "SaveStates")) LevelLoadManager.Instance.ResumeGame();
            else
            {
                LevelLoadManager.Instance.LoadScene("Level 1");
                LevelLoadManager.Instance.LoadAdditiveAsyncScenes();
            }
        }

        /// <summary>
        /// Loads a specific scene
        /// </summary>
        /// <param name="scene"></param>
        public void LoadScene(string scene)
        {
            LevelLoadManager.Instance.LoadScene(scene);
            LevelLoadManager.Instance.LoadAdditiveAsyncScenes();
            
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(loadScene);
        }

        /// <summary>
        /// Closes or stops the game
        /// </summary>
        public void QuitApplication()
        {
            LevelLoadManager.Instance.QuitGame();
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(quitGame);
        }
    }
}
