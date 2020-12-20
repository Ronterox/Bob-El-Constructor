using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseMenu : PersistentSingleton<PauseMenu>
{
    public GameObject resumegame, quitgame, mainmenu;
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
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(resumegame);
    }

    /// <summary>
    /// Takes you to main menu
    /// </summary>
    public void MainMenu()
    {
        LevelLoadManager.instance.LoadScene("MAIN MENU");
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(mainmenu);
    }

    /// <summary>
    /// Quits The Game
    /// </summary>
    public void QuitGame()
    {
        LevelLoadManager.instance.QuitGame();
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(quitgame);
    }
}

