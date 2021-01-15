using Managers;
using Plugins.Tools;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GUI
{
    public class MainMenu : MonoBehaviour
    {
        public GameObject startGame, loadScene, quitGame, continueButton;
        public TextMeshProUGUI buildVersionTMP;

        private void Start()
        {
            buildVersionTMP.text = "Build v" + Application.version;
            continueButton.SetActive(HasSavedFile());
            LevelLoadManager.Instance.UnloadAdditiveAsyncScenes();
            
            EventSystem.current.SetSelectedGameObject(continueButton.activeSelf? continueButton : startGame);
        }

        /// <summary>
        /// Loads the game last saved Scene
        /// </summary>
        public void StartNewGame()
        {
            LevelLoadManager.Instance.LoadScene("Level 1");
            LevelLoadManager.Instance.LoadAdditiveAsyncScenes();
        }

        private void LateUpdate()
        {
            if (!continueButton.activeSelf && HasSavedFile()) continueButton.SetActive(true);
        }

        /// <summary>
        /// Resumes an old save file
        /// </summary>
        public void ResumeGame() => LevelLoadManager.Instance.ResumeGame();

        /// <summary>
        /// Checks if there is a saved file
        /// </summary>
        private bool HasSavedFile() => SaveLoadManager.SaveExists($"saved_state_v{Application.version}", "SaveStates");

        /// <summary>
        /// Loads a specific scene
        /// </summary>
        /// <param name="scene"></param>
        public void LoadScene(string scene)
        {
            LevelLoadManager.Instance.LoadScene(scene);
            LevelLoadManager.Instance.LoadAdditiveAsyncScenes();
        }

        /// <summary>
        /// Closes or stops the game
        /// </summary>
        public void QuitApplication() => LevelLoadManager.Instance.QuitGame();
    }
}
