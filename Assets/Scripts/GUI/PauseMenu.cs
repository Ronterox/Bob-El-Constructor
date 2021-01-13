using Managers;
using Plugins.Tools;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GUI
{
    public class PauseMenu : Singleton<PauseMenu>
    {
        public GameObject resumeGame, quitGame, mainMenu;
        [HideInInspector]
        public bool gameIsPaused;
        [SerializeField]
        private GameObject pauseMenuUI;

        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Escape)) return;
            if (gameIsPaused) Resume();
            else Pause();
        }

        /// <summary>
        /// Resumes the game
        /// </summary>
        public void Resume()
        {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            gameIsPaused = false;
        }

        /// <summary>
        /// Pauses the in game time
        /// </summary>
        public void Pause()
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            gameIsPaused = true;
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(resumeGame);
        }

        /// <summary>
        /// Takes you to main menu
        /// </summary>
        public void MainMenu()
        {
            Resume();
            LevelLoadManager.Instance.LoadScene("MAIN MENU");
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(mainMenu);
        }

        /// <summary>
        /// Quits The Game
        /// </summary>
        public void QuitGame()
        {
            LevelLoadManager.Instance.QuitGame();
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(quitGame);
        }
    }
}
