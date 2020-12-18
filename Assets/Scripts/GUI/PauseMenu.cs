using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : PersistentSingleton<PauseMenu>
{
    [HideInInspector]
    public bool GameIsPaused = false;
    [SerializeField] 
    private GameObject pauseMenuUI;
   
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused) Resume();
            else Pause();
        }
    }

    /// <summary>
    /// Resumes the game
    /// </summary>
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

    }

    /// <summary>
    /// Pauses the ingame time
    /// </summary>
    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    /// <summary>
    /// Takes you to main menu
    /// </summary>
    public void MainMenu() { LevelLoadManager.instance.LoadScene("MAIN MENU"); }

    /// <summary>
    /// Quits The Game
    /// </summary>
    public void QuitGame() { LevelLoadManager.instance.QuitGame(); }
}
