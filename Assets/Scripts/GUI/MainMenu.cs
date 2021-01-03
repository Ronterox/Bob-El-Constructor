using System;
using Managers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GUI
{
    public class MainMenu : MonoBehaviour 
    {
        public GameObject startGame, quitGame, loadScene;

        private void Start() => LevelLoadManager.Instance.UnloadAdditiveAsyncScenes();

        /// <summary>
        /// Loads the game last saved Scene
        /// </summary>
        public void StartGame()
        {
            //Need To later depend on save file
            LevelLoadManager.Instance.LoadScene("Level 1");
            LevelLoadManager.Instance.LoadAdditiveAsyncScenes();

            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(startGame);
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
